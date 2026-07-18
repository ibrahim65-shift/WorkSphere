using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkSphere_DataAccess;
using WorkSphere_Entities;

namespace WorkSphere_Buisness
{
    public class LeaveTypesValidationError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
    public class LeaveTypesValidationResult
    {
        public List<LeaveTypesValidationError> Errors { get; } = new List<LeaveTypesValidationError>();
        public bool IsValid => !Errors.Any();
        public void Add(string fieldName, string message)
        {
            Errors.Add(new LeaveTypesValidationError { FieldName = fieldName, Message = message });
        }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors.Select(e => $"{e.FieldName}: {e.Message}"));
        }
    }
    public class clsLeaveTypes
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsLeaveTypesEntity _LeaveTypeInfo;

        public int LeaveTypeID
        {
            get => _LeaveTypeInfo.LeaveTypeID;
            private set => _LeaveTypeInfo.LeaveTypeID = value;
        }
        public string Name
        {
            get => _LeaveTypeInfo.Name;
            set => _LeaveTypeInfo.Name = value;
        }
        public string Description // allows null
        {
            get => _LeaveTypeInfo.Description;
            set => _LeaveTypeInfo.Description = value;
        }
        public int? MaxDaysPerYear
        {
            get => _LeaveTypeInfo.MaxDaysPerYear;
            set => _LeaveTypeInfo.MaxDaysPerYear = value;
        }
        public bool IsPaid
        {
            get => _LeaveTypeInfo.IsPaid;
            set => _LeaveTypeInfo.IsPaid = value;
        }
        public DateTime CreatedDate
        {
            get => _LeaveTypeInfo.CreatedDate;
            set => _LeaveTypeInfo.CreatedDate = value;
        }
        public int CreatedByUserID
        {
            get => _LeaveTypeInfo.CreatedByUserID;
            set => _LeaveTypeInfo.CreatedByUserID = value;
        }

        public clsLeaveTypes()
        {
            _Mode = enMode.AddNew;
            _LeaveTypeInfo = new clsLeaveTypesEntity();
        }
        private clsLeaveTypes(clsLeaveTypesEntity entity)
        {
            if (entity != null)
            {
                _Mode = enMode.Update;
                _LeaveTypeInfo = entity;
            }
            else
            {
                _Mode = enMode.AddNew;
                _LeaveTypeInfo = new clsLeaveTypesEntity();
            }
        }

        public static async Task<clsLeaveTypes> FindLeaveTypeByIDAsync(int ID)
        {
            var entity = await clsLeaveTypesData.GetLeaveTypesInfoByIDAsync(ID);
            return entity != null ? new clsLeaveTypes(entity) : null;
        }
        public static async Task<clsLeaveTypes> FindLeaveTypeByNameAsync(string leaveTypeName)
        {
            var entity = await clsLeaveTypesData.GetLeaveTypesInfoByNameAsync(leaveTypeName);
            return entity != null ? new clsLeaveTypes(entity) : null;
        }

        public static async Task<bool> DeleteAsync(int ID) => await clsLeaveTypesData.DeleteAsync(ID);
        public static async Task<bool> IsLeavePaidAsync(int ID) => await clsLeaveTypesData.IsLeavePaidAsync(ID);
        public static async Task<List<clsLeaveTypes>> GetAllLeaveTypesAsync()
        {
            var list = await clsLeaveTypesData.GetAllAsync();
            return list.Select(e => new clsLeaveTypes(e)).ToList();
        }
        public static async Task<(List<clsLeaveTypes> list, int TotalPages)> GetLeaveTypesPageAsync(int PageNumber, int PageSize, string searchText = null)
        {
            var entities = await clsLeaveTypesData.GetLeaveTypesPageAsync(PageNumber,PageSize,searchText);
            return (entities.list.Select(e => new clsLeaveTypes(e)).ToList(), entities.TotalPages);
        }
        public async Task<LeaveTypesValidationResult> ValidateAsync()
        {
            var result = new LeaveTypesValidationResult();

            if (string.IsNullOrWhiteSpace(Name))
            {
                result.Add("Name", "الرجاء ادخال نوع الاجازة");
            }
            else if (Name.Length > 100)
            {
                result.Add("Name", "نوع الاجازة يجب ان لايتجاوز 100 حرف");
            }

            if (!string.IsNullOrWhiteSpace(Description))
            {
                if (Description.Length > 255)
                {
                    result.Add("Description", "الوصف يجب ان لايتجاوز 255 حرف");
                }
            }

            if (MaxDaysPerYear != null)
            {
                if (MaxDaysPerYear < 0 || MaxDaysPerYear > 365)
                {
                    result.Add("MaxDaysPerYear", "الرجاء ادخال رقم بين 0 و 365");
                }
            }

            if (_Mode == enMode.AddNew)
            {
                if (await clsLeaveTypesData.IsLeaveTypeNameExistsAsync(Name))
                {
                    result.Add("Name", "نوع هذه الاجازة موجود بالفعل");
                }
            }

            return result;
        }
        public async Task<LeaveTypesValidationResult> SaveAsync()
        {
            var validation = await ValidateAsync();

            if (!validation.IsValid)
            {
                return validation;
            }

            if (_Mode == enMode.AddNew)
            {
                _LeaveTypeInfo.CreatedDate = DateTime.Now;
                _LeaveTypeInfo.LeaveTypeID = await clsLeaveTypesData.AddNewAsync(_LeaveTypeInfo);
                if (_LeaveTypeInfo.LeaveTypeID != -1)
                {
                    _Mode = enMode.Update;
                }
            }
            else
            {
                bool update = await clsLeaveTypesData.UpdateAsync(_LeaveTypeInfo);
                if (!update)
                {
                    validation.Add("Update", "فشل تحديث بيانات نوع الاجازة");
                }
            }

            return validation;
        }
    }
}
