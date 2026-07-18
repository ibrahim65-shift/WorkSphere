using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkSphere_DataAccess;
using WorkSphere_Entities;

namespace WorkSphere_Buisness
{
    public class AbsencesValidationError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }

    public class AbsencesValidationResult
    {
        public List<AbsencesValidationError> Errors { get; set; } = new List<AbsencesValidationError>();

        public bool IsValid => !Errors.Any();

        public void Add(string fieldName, string message)
            => Errors.Add(new AbsencesValidationError { FieldName = fieldName, Message = message });

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors.Select(e => $"{e.FieldName}: {e.Message}"));
        }

    }

    public class clsEmployeeAbsences
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsEmployeeAbsencesEntity _AbsenceInfo;

        public int AbsenceID
        {
            get => _AbsenceInfo.AbsenceID;
            private set => _AbsenceInfo.AbsenceID = value;
        }
        public int EmployeeID
        {
            get => _AbsenceInfo.EmployeeID;
            set => _AbsenceInfo.EmployeeID = value;
        }
        public DateTime AbsenceDate
        {
            get => _AbsenceInfo.AbsenceDate;
            set => _AbsenceInfo.AbsenceDate = value;
        }
        public bool IsExcused
        {
            get => _AbsenceInfo.IsExcused;
            set => _AbsenceInfo.IsExcused = value;
        }
        public string Reason //allows null
        {
            get => _AbsenceInfo.Reason;
            set => _AbsenceInfo.Reason = value;
        }
        public DateTime CreatedDate
        {
            get => _AbsenceInfo.CreatedDate;
            set => _AbsenceInfo.CreatedDate = value;
        }
        public int CreatedByUserID
        {
            get => _AbsenceInfo.CreatedByUserID;
            set => _AbsenceInfo.CreatedByUserID = value;
        }
        public DateTime? EditedDate
        {
            get => _AbsenceInfo.EditedDate;
            set => _AbsenceInfo.EditedDate = value;
        }
        public int? EditedByUserID
        {
            get => _AbsenceInfo.EditedByUserID;
            set => _AbsenceInfo.EditedByUserID = value;
        }

        public Task<clsEmployees> EmployeeInfo;

        public clsEmployeeAbsences()
        {
            _Mode = enMode.AddNew;
            _AbsenceInfo = new clsEmployeeAbsencesEntity();
        }
        private clsEmployeeAbsences(clsEmployeeAbsencesEntity entity)
        {
            if (entity != null)
            {
                _Mode = enMode.Update;
                _AbsenceInfo = entity;
                EmployeeInfo = clsEmployees.FindEmployeeByID(entity.EmployeeID);
            }
            else
            {
                _Mode = enMode.AddNew;
                _AbsenceInfo = new clsEmployeeAbsencesEntity();
            }
        }

        public static async Task<clsEmployeeAbsences> FindEmployeeAbsencesByIDAsync(int ID)
        {
            var entity = await clsEmployeeAbsencesData.GetEmployeeAbsencesInfoByIDAsync(ID);
            return (entity != null) ? new clsEmployeeAbsences(entity) : null;
        }
        public static async Task<List<clsEmployeeAbsences>> GetEmployeeAbsencesInfoByEmployeeIDAsync(int empID)
        {
            var list = await clsEmployeeAbsencesData.GetEmployeeAbsencesInfoByEmployeeIDAsync(empID);
            return list.Select(e => new clsEmployeeAbsences(e)).ToList();
        }
        public static async Task<bool> DeleteAsync(int ID) => await clsEmployeeAbsencesData.DeleteAsync(ID);
        public static async Task<DataTable> GetAllEmployeeAbsencesInfoAsync()
        {
            return await clsEmployeeAbsencesData.GetAllEmployeeAbsencesInfo();
        }
        public static async Task<(DataTable dt , int TotalPages)> GetEmployeeAbsencesInfoPageAsync(int PageNumber , int PageSize , string searchText = null)
        {
            return await clsEmployeeAbsencesData.GetEmployeeAbsencesInfoPageAsync(PageNumber , PageSize , searchText);
        }
        public async Task<AbsencesValidationResult> Validate()
        {
            var result = new AbsencesValidationResult();

            if (EmployeeID <= 0)
            {
                result.Add("EmployeeID", "معرف الموظف غير صالح");
                return result;
            }

            if (AbsenceDate > DateTime.Now)
            {
                result.Add("AbsenceDate", "لايمكن لتاريخ الغياب ان يكون في المستقبل");
            }

            if (!string.IsNullOrWhiteSpace(Reason))
            {
                if (Reason.Length > 255)
                {
                    result.Add("Reason", "سبب الغياب يجب ان لايتجاوز 255 حرف");
                }
            }

            if (IsExcused && string.IsNullOrWhiteSpace(Reason))
            {
                result.Add("Reason", "يجب كتابة سبب الغياب");
            }

            if (await clsEmployeeAbsencesData.HasAbsenceOnDateAsync(EmployeeID, AbsenceDate, _Mode == enMode.Update ? AbsenceID : (int?)null))
            {
                result.Add("AbsenceDate", "يوجد غياب مُسجّل لنفس التاريخ لهذا الموظف");
            }

            return result;
        }
        public async Task<AbsencesValidationResult> SaveAsync()
        {
            var validation = await Validate();

            if (!validation.IsValid)
            {
                return validation;
            }

            if (_Mode == enMode.AddNew)
            {
                _AbsenceInfo.CreatedDate = DateTime.Now;
                _AbsenceInfo.AbsenceID = await clsEmployeeAbsencesData.AddNewAsync(_AbsenceInfo);
                if (_AbsenceInfo.AbsenceID != -1)
                {
                    _Mode = enMode.Update;
                }
            }
            else
            {
                _AbsenceInfo.EditedDate = DateTime.Now;
                bool update = await clsEmployeeAbsencesData.UpdateAsync(_AbsenceInfo);
                if (!update)
                {
                    validation.Add("Update", "فشل تحديث بيانات الغياب");
                }
            }

            return validation;
        }
        public static async Task<List<int>> GetEmployeeIDsForAbsenceAsync(DateTime date)
        {
            return await clsEmployeeAbsencesData.GetEmployeeIDsForAbsenceAsync(date);
        }
        public static async Task<bool> AutoMarkAbsentEmployeesAsync(DateTime date, int currentUserID)
        {
            try
            {
                var employeeIDs = await clsEmployeeAbsencesData.GetEmployeeIDsForAbsenceAsync(date);

                if (!employeeIDs.Any())
                {
                    return false;
                }

                bool success = await clsEmployeeAbsencesData.BulkInsertAbsencesAsync(
                    employeeIDs, date, false, "غياب بدون عذر - تسجيل تلقائي", currentUserID);

                return success;
            }
            catch (Exception ex)
            {
                // log
                return false;
            }
        }
        public static async Task<int> GetNumberOfAbsenceEmployees() => await clsEmployeeAbsencesData.GetNumberOfAbsenceEmployeesAsync();
    }
}
