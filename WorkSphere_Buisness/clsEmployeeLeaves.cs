using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkSphere_DataAccess;
using WorkSphere_Entities;

namespace WorkSphere_Buisness
{
    public class LeavesValidationError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
    public class LeavesValidationResult
    {
        public List<LeavesValidationError> Errors { get; } = new List<LeavesValidationError>();

        public bool IsValid => !Errors.Any();

        public void Add(string fieldName, string message)
        {
            Errors.Add(new LeavesValidationError { FieldName = fieldName, Message = message });
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors.Select(e => $"{e.FieldName}: {e.Message}"));
        }
    }
    public class clsEmployeeLeaves
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsEmployeeLeavesEntity _LeaveInfo;

        public enum enLeaveTypes { Pending = 1, Approved = 2, Rejected = 3 };
        public int LeaveID
        {
            get => _LeaveInfo.LeaveID;
            private set => _LeaveInfo.LeaveID = value;
        }
        public int EmployeeID
        {
            get => _LeaveInfo.EmployeeID;
            set => _LeaveInfo.EmployeeID = value;
        }
        public int LeaveTypeID
        {
            get => _LeaveInfo.LeaveTypeID;
            set => _LeaveInfo.LeaveTypeID = value;
        }
        public DateTime StartDate
        {
            get => _LeaveInfo.StartDate;
            set => _LeaveInfo.StartDate = value;
        }
        public DateTime EndDate
        {
            get => _LeaveInfo.EndDate;
            set => _LeaveInfo.EndDate = value;
        }
        public string Reason  // allows null
        {
            get => _LeaveInfo.Reason;
            set => _LeaveInfo.Reason = value;
        }
        public byte Status
        {
            get => _LeaveInfo.Status;
            set => _LeaveInfo.Status = value;
        }
        public int? ApprovedByUserID
        {
            get => _LeaveInfo.ApprovedByUserID;
            set => _LeaveInfo.ApprovedByUserID = value;
        }
        public DateTime CreatedDate
        {
            get => _LeaveInfo.CreatedDate;
            set => _LeaveInfo.CreatedDate = value;
        }
        public int CreatedByUserID
        {
            get => _LeaveInfo.CreatedByUserID;
            set => _LeaveInfo.CreatedByUserID = value;
        }
        public DateTime? EditedDate
        {
            get => _LeaveInfo.EditedDate;
            set => _LeaveInfo.EditedDate = value;
        }
        public int? EditedByUserID
        {
            get => _LeaveInfo.EditedByUserID;
            set => _LeaveInfo.EditedByUserID = value;
        }

        public Task<clsEmployees> EmployeeInfo;
        public Task<clsLeaveTypes> LeaveTypesInfo;

        public clsEmployeeLeaves()
        {
            _Mode = enMode.AddNew;
            _LeaveInfo = new clsEmployeeLeavesEntity();
        }
        private clsEmployeeLeaves(clsEmployeeLeavesEntity entity)
        {
            if (entity != null)
            {
                _Mode = enMode.Update;
                _LeaveInfo = entity;
                EmployeeInfo = clsEmployees.FindEmployeeByID(entity.EmployeeID);
                LeaveTypesInfo = clsLeaveTypes.FindLeaveTypeByIDAsync(entity.LeaveTypeID);
            }
            else
            {
                _Mode = enMode.AddNew;
                _LeaveInfo = new clsEmployeeLeavesEntity();
            }
        }

        public static async Task<clsEmployeeLeaves> FindEmployeeLeaveByLeaveIDAsync(int leaveID)
        {
            var entity = await clsEmployeeLeavesData.GetEmployeeLeavesInfoByLeaveIDAsync(leaveID);
            return entity != null ? new clsEmployeeLeaves(entity) : null;
        }
        public static async Task<List<clsEmployeeLeaves>> GetEmployeeLeavesByEmployeeIDAsync(int empID)
        {
            var list = await clsEmployeeLeavesData.GetEmployeeLeavesInfoByEmployeeIDAsync(empID);
            return list.Select(e => new clsEmployeeLeaves(e)).ToList();
        }
        public static async Task<List<clsEmployeeLeaves>> GetEmployeeLeavesByLeaveTypesIDAsync(int leaveTypeID)
        {
            var list = await clsEmployeeLeavesData.GetEmployeeLeavesInfoByLeaveTypeIDAsync(leaveTypeID);
            return list.Select(e => new clsEmployeeLeaves(e)).ToList();
        }
        public static async Task<bool> DeleteAsync(int ID) => await clsEmployeeLeavesData.DeleteAsync(ID);
        public static async Task<DataTable> GetAllEmployeeLeavesAsync()
        {
            return await clsEmployeeLeavesData.GetAllAsync();
        }
        public async Task<LeavesValidationResult> ValidateAsync()
        {
            var result = new LeavesValidationResult();

            // EmployeeID, LeaveTypeID
            if (EmployeeID <= 0)
            {
                result.Add("EmployeeID", "معرف الموظف غير صالح");
                return result; // لا نستمر إذا Employee غير صالح
            }

            if (LeaveTypeID <= 0)
            {
                result.Add("LeaveTypeID", "الرجاء اختيار نوع الإجازة");
            }

            // Dates
            if (StartDate.Date > EndDate.Date)
            {
                result.Add("DateRange", "تاريخ البداية لا يمكن أن يكون بعد تاريخ النهاية");
            }

            // Optional: disallow start date in the past
            if (StartDate.Date < DateTime.Today)
            {
                result.Add("StartDate", "لا يمكن أن تبدأ الإجازة بتاريخ سابق لليوم");
            }

            // Load employee & leave-type info (to show friendly messages and use MaxDaysPerYear)
            var emp = await clsEmployees.FindEmployeeByID(EmployeeID);
            if (emp == null)
            {
                result.Add("EmployeeID", "الموظف غير موجود في النظام");
                return result;
            }

            var leaveType = await clsLeaveTypes.FindLeaveTypeByIDAsync(LeaveTypeID).ConfigureAwait(false);
            if (leaveType == null)
            {
                result.Add("LeaveTypeID", "نوع الإجازة غير موجود");
                return result;
            }

            // Days count
            int requestedDays = (EndDate.Date - StartDate.Date).Days + 1;
            if (requestedDays <= 0)
            {
                result.Add("DateRange", "المدة المحسوبة للإجازة غير صحيحة");
            }

            if (leaveType.MaxDaysPerYear.HasValue && requestedDays > leaveType.MaxDaysPerYear.Value)
            {
                result.Add("MaxDaysPerYear", $"عدد الأيام المطلوبة ({requestedDays}) يتجاوز الحد المسموح به لنوع الإجازة : {leaveType.Name} والذي هو : {leaveType.MaxDaysPerYear.Value}");
            }

            // Policy: check pending/approved existence
            if (_Mode == enMode.AddNew)
            {
                // إذا تريد منع وجود طلب معلق/معتمد سابقاً:
                bool hasPendingOrApproved = await clsEmployeeLeavesData.HasOverlappingLeaveAsync(EmployeeID, StartDate, EndDate, null);
                if (hasPendingOrApproved)
                    result.Add("EmployeeID", "يوجد إجازة معلقة أو معتمدة تتداخل مع هذه الفترة");
            }
            else // Update mode
            {
                // ننقح باستثناء السجل الحالي
                int? selfId = (LeaveID > 0) ? (int?)LeaveID : null;
                bool overlap = await clsEmployeeLeavesData.HasOverlappingLeaveAsync(EmployeeID, StartDate, EndDate, selfId);
                if (overlap)
                    result.Add("Overlap", "يوجد إجازة أخرى (معلقة أو معتمدة) تتداخل مع هذه الفترة");
            }

            return result;
        }
        public async Task<LeavesValidationResult> SaveAsync()
        {
            var validation = await ValidateAsync();

            if (!validation.IsValid)
            {
                return validation;
            }

            if (_Mode == enMode.AddNew)
            {
                CreatedDate = DateTime.Now;
                _LeaveInfo.LeaveID = await clsEmployeeLeavesData.AddNewAsync(_LeaveInfo);
                if (_LeaveInfo.LeaveID != -1)
                {
                    _Mode = enMode.Update;
                }
            }
            else
            {
                EditedDate = DateTime.Now;
                bool update = await clsEmployeeLeavesData.UpdateAsync(_LeaveInfo);
                if (!update)
                {
                    validation.Add("Update", "فشل تحديث بيانات الاجازة");
                }
            }

            return validation;
        }
        public static async Task<(DataTable dt , int TotalPage)> GetEmployeeLeavesInfoPageAsync(int PageNumber , int PageSize , string searchText=null)
        {
            return await clsEmployeeLeavesData.GetEmployeeLeavesInfoPageAsync(PageNumber, PageSize, searchText);
        }
        public static async Task<int> GetNumberOfCurrentLeavesAsync() => await clsEmployeeLeavesData.GetNumberOfCurrentLeavesAsync();
        public static async Task<int> GetNumberOfPendingLeavesAsync() => await clsEmployeeLeavesData.GetNumberOfPendingLeavesAsync();
    }
}
