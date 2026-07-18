using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkSphere_DataAccess;
using WorkSphere_Entities;

namespace WorkSphere_Buisness
{
    public class PromotionsValidationError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }

    public class PromotionsValidationResult
    {
        public List<PromotionsValidationError> Errors { get; } = new List<PromotionsValidationError>();
        public bool IsValid => !Errors.Any();
        public void Add(string fieldName, string message)
        {
            Errors.Add(new PromotionsValidationError { FieldName = fieldName, Message = message });
        }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors.Select(e => $"{e.FieldName}: {e.Message}"));
        }
    }

    public class clsEmployeePromotions
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsEmployeePromotionsEntity _PromotionsInfo;
        private int _OriginalNewStepID;

        public int PromotionID
        {
            get => _PromotionsInfo.PromotionID;
            private set => _PromotionsInfo.PromotionID = value;
        }
        public int EmployeeID
        {
            get => _PromotionsInfo.EmployeeID;
            set => _PromotionsInfo.EmployeeID = value;
        }
        public int OldStepID
        {
            get => _PromotionsInfo.OldStepID;
            set => _PromotionsInfo.OldStepID = value;
        }
        public int NewStepID
        {
            get => _PromotionsInfo.NewStepID;
            set => _PromotionsInfo.NewStepID = value;
        }
        public DateTime PromotionDate
        {
            get => _PromotionsInfo.PromotionDate;
            set => _PromotionsInfo.PromotionDate = value;
        }
        public string Notes//allows null
        {
            get => _PromotionsInfo.Notes;
            set => _PromotionsInfo.Notes = value;
        }

        public int CreatedByUserID
        {
            get => _PromotionsInfo.CreatedByUserID;
            set => _PromotionsInfo.CreatedByUserID = value;
        }

        public Task<clsEmployees> EmployeeInfo;


        public clsEmployeePromotions()
        {
            _Mode = enMode.AddNew;
            _PromotionsInfo = new clsEmployeePromotionsEntity();
        }
        private clsEmployeePromotions(clsEmployeePromotionsEntity entity)
        {
            if (entity != null)
            {
                _Mode = enMode.Update;
                _PromotionsInfo = entity;
                _OriginalNewStepID = entity.NewStepID;
                EmployeeInfo = clsEmployees.FindEmployeeByID(entity.EmployeeID);
            }
            else
            {
                _Mode = enMode.AddNew;
                _PromotionsInfo = new clsEmployeePromotionsEntity();
                _OriginalNewStepID = 0;
            }
        }
        public static async Task<clsEmployeePromotions> FindEmployeePromotionByPromotionID(int ID)
        {
            var entity = await clsEmployeePromotionsData.GetEmployeePromotionsInfoByIDAsync(ID);
            return (entity != null) ? new clsEmployeePromotions(entity) : null;
        }
        public static async Task<List<clsEmployeePromotions>> GetEmployeePromotionsByEmployeeID(int ID)
        {
            var list = await clsEmployeePromotionsData.GetEmployeePromotionsInfoByEmployeeIDAsync(ID);
            return list.Select(e => new clsEmployeePromotions(e)).ToList();
        }
        public async Task<PromotionsValidationResult> Validate()
        {
            var result = new PromotionsValidationResult();

            if (EmployeeID <= 0)
            {
                result.Add("EmployeeID", "معرف الموظف غير صالح");
                return result;
            }

            if (NewStepID <= 0)
            {
                result.Add("NewStepID", "الرجاء اختيار خطوة ترقية صالحة");
                return result;
            }

            // هل نحتاج تحقق إضافي؟
            bool needsStepValidation = _Mode == enMode.AddNew ||
                                       (_Mode == enMode.Update && NewStepID != _OriginalNewStepID);

            if (needsStepValidation)
            {
                if (NewStepID <= OldStepID)
                    result.Add("NewStepID", "خطوة الترقية يجب أن تكون أكبر من الخطوة الحالية");

                // استعلام DB مرة وحدة فقط إذا ما كان فيه أخطاء حتى الآن
                if (result.IsValid &&
                    await clsEmployeePromotionsData.HasEmployeeReachedStepAsync(EmployeeID, NewStepID))
                {
                    result.Add("NewStepID", "الموظف وصل بالفعل إلى هذه الخطوة");
                }
            }

            return result;
        }
        public async Task<PromotionsValidationResult> Save()
        {
            var validation = await Validate();
            if (!validation.IsValid)
            {
                return validation;
            }

            if (_Mode == enMode.AddNew)
            {

                _PromotionsInfo.PromotionID = await clsEmployeePromotionsData.AddNewAsync(_PromotionsInfo);
                if (_PromotionsInfo.PromotionID != -1)
                {
                    await clsEmployees.SetNewStepID(this.EmployeeID, this.NewStepID);
                    _Mode = enMode.Update;
                }
            }
            else
            {
                bool update = await clsEmployeePromotionsData.UpdateAsync(_PromotionsInfo);
                if (!update)
                {
                    validation.Add("Update", "فشل تحديث بيانات الترقية");
                }
                else
                {
                    await clsEmployees.SetNewStepID(this.EmployeeID, this.NewStepID);
                }
            }

            return validation;
        }
        public static async Task<bool> Delete(int ID) => await clsEmployeePromotionsData.DeleteAsync(ID);
        public static async Task<bool> HasEmployeeReachedStepAsync(int EmployeeID, int NewStepID) => await clsEmployeePromotionsData.HasEmployeeReachedStepAsync(EmployeeID, NewStepID);
        public static async Task<List<clsEmployeePromotions>> GetAllEmployeePromotions()
        {
            var list = await clsEmployeePromotionsData.GetAllEmployeePromotions();
            return list.Select(e => new clsEmployeePromotions(e)).ToList();
        }
        public static async Task<(DataTable dt, int TotalPages)> GetEmployeePromotionsPageAsync(int PageNumber, int PageSize, string searchText = null)
        {
            return await clsEmployeePromotionsData.GetEmployeePromotionsPageAsync(PageNumber,PageSize,searchText);
        }
        public static async Task<DataTable> GetFullEmployeeInfoPromotions()
        {
            return await clsEmployeePromotionsData.GetFullEmployeeInfoPromotions();
        }
    }
}
