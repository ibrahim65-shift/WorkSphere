using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkSphere_DataAccess;
using WorkSphere_Entities;

namespace WorkSphere_Buisness
{
    public class AttendanceValidationError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
    public class AttendanceValidationResult
    {
        public List<AttendanceValidationError> Errors { get; } = new List<AttendanceValidationError>();

        public bool IsValid => !Errors.Any();

        public void Add(string fieldName, string messag)
            => Errors.Add(new AttendanceValidationError { FieldName = fieldName, Message = messag });

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors.Select(e => $"{e.FieldName}: {e.Message}"));
        }
    }
    public class clsEmployeeAttendance
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsEmployeeAttendanceEntity _AttendanceInfo;

        public int AttendanceID
        {
            get => _AttendanceInfo.AttendanceID;
            private set => _AttendanceInfo.AttendanceID = value;
        }
        public int EmployeeID
        {
            get => _AttendanceInfo.EmployeeID;
            set => _AttendanceInfo.EmployeeID = value;
        }
        public DateTime AttendanceDate
        {
            get => _AttendanceInfo.AttendanceDate;
            set => _AttendanceInfo.AttendanceDate = value;
        }
        public TimeSpan? CheckInTime
        {
            get => _AttendanceInfo.CheckInTime;
            set => _AttendanceInfo.CheckInTime = value;
        }
        public TimeSpan? CheckOutTime
        {
            get => _AttendanceInfo.CheckOutTime;
            set => _AttendanceInfo.CheckOutTime = value;
        }
        public bool IsLate
        {
            get => _AttendanceInfo.IsLate;
            set => _AttendanceInfo.IsLate = value;
        }
        public string Notes // allows null
        {
            get => _AttendanceInfo.Notes;
            set => _AttendanceInfo.Notes = value;
        }
        public DateTime CreatedDate
        {
            get => _AttendanceInfo.CreatedDate;
            set => _AttendanceInfo.CreatedDate = value;
        }
        public int CreatedByUserID
        {
            get => _AttendanceInfo.CreatedByUserID;
            set => _AttendanceInfo.CreatedByUserID = value;
        }
        public DateTime? EditedDate
        {
            get => _AttendanceInfo.EditedDate;
            set => _AttendanceInfo.EditedDate = value;
        }
        public int? EditedByUserID
        {
            get => _AttendanceInfo.EditedByUserID;
            set => _AttendanceInfo.EditedByUserID = value;
        }

        public Task<clsEmployees> EmployeeInfo;

        public clsEmployeeAttendance()
        {
            _Mode = enMode.AddNew;
            _AttendanceInfo = new clsEmployeeAttendanceEntity();
        }
        public clsEmployeeAttendance(clsEmployeeAttendanceEntity entity)
        {
            if (entity != null)
            {
                _Mode = enMode.Update;
                _AttendanceInfo = entity;
                EmployeeInfo = clsEmployees.FindEmployeeByID(entity.EmployeeID);
            }
            else
            {

                _Mode = enMode.AddNew;
                _AttendanceInfo = new clsEmployeeAttendanceEntity();
            }
        }

        public static async Task<clsEmployeeAttendance> FindEmployeeAttendanceByIDAsync(int ID)
        {
            var entity = await clsEmployeeAttendanceData.GetEmployeeAttendanceInfoByIDAsync(ID);
            return entity != null ? new clsEmployeeAttendance(entity) : null;
        }
        public static async Task<List<clsEmployeeAttendance>> GetEmployeeAttendancesByEmpIDAsync(int empID)
        {
            var list = await clsEmployeeAttendanceData.GetEmployeeAttendanceInfoByEmployeeIDAsync(empID);
            return list.Select(e => new clsEmployeeAttendance(e)).ToList();
        }
        public static async Task<bool> DeleteAsync(int ID) => await clsEmployeeAttendanceData.DeleteAsync(ID);
        public static async Task<DataTable> GetEmployeeAttendanceInfo()
            => await clsEmployeeAttendanceData.GetEmployeeAttendanceInfo();

        public async Task<AttendanceValidationResult> ValidateAsync()
        {
            var result = new AttendanceValidationResult();

            if (EmployeeID <= 0)
            {
                result.Add("EmployeeID", "معرف الموظف غير صحيح");
                return result;
            }

            if (AttendanceDate.Date > DateTime.Now)
            {
                result.Add("AttendanceDate", "لا يمكن اختيار تاريخ حضور مستقبلي");
            }

            if (await clsEmployeeAttendanceData.HasAttendanceForDateAsync(EmployeeID, AttendanceDate, _Mode == enMode.Update ? AttendanceID : (int?)null))
            {
                result.Add("AttendanceDate", "يوجد سجل حضور مسجل لهذا الموظف في نفس التاريخ");
            }

            if (CheckInTime.HasValue && CheckOutTime.HasValue)
            {
                if (CheckOutTime.Value <= CheckInTime.Value)
                    result.Add("CheckOutTime", "وقت الانصراف يجب أن يكون بعد وقت الحضور");
            }

            if (AttendanceDate.Date == DateTime.Today && CheckInTime.HasValue)
            {
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                TimeSpan margin = TimeSpan.FromMinutes(5); // هامش 5 دقائق

                if (CheckInTime.Value > currentTime + margin)
                {
                    result.Add("CheckInTime", "لا يمكن تسجيل وقت حضور في المستقبل");
                }
            }

            if (!string.IsNullOrWhiteSpace(Notes) && Notes.Length > 255)
            {
                result.Add("Notes", "الملاحظات يجب أن لا تتجاوز 255 حرفاً");
            }

            return result;
        }
        public async Task<AttendanceValidationResult> SaveAsync()
        {
            var validation = await ValidateAsync();

            if (!validation.IsValid)
            {
                return validation;
            }

            if (_Mode == enMode.AddNew)
            {
                CreatedDate = DateTime.Now;
                _AttendanceInfo.AttendanceID = await clsEmployeeAttendanceData.AddNewAsync(_AttendanceInfo);
                if (_AttendanceInfo.AttendanceID != -1)
                {
                    _Mode = enMode.Update;
                }
            }
            else
            {
                EditedDate = DateTime.Now;
                bool update = await clsEmployeeAttendanceData.UpdateAsync(_AttendanceInfo);
                if (!update)
                {
                    validation.Add("Update", "فشل تحديث بيانات الحضور");
                }
            }

            return validation;
        }
        public static async Task<(DataTable dt , int TotalPages)> GetEmployeeAttendancePageAsync(int PageNumber , int PageSize , string searchText=null)
        {
            return await clsEmployeeAttendanceData.GetEmployeeAttendancePageAsync(PageNumber,PageSize,searchText);
        }
        public static async Task<int> GetNumberOfAttendanceEmployeesAsync() => await clsEmployeeAttendanceData.GetNumberOfAttendanceEmployeesAsync();
        public static async Task<int> GetNumberOfLateEmployeesAsync() => await clsEmployeeAttendanceData.GetNumberOfLateEmployeesAsync();
    }
}
