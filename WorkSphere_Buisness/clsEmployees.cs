using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_DataAccess;
using WorkSphere_Entities;

namespace WorkSphere_Buisness
{
    public class EmployeeValidationError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
    public class EmployeeValidationResult
    {

        public List<EmployeeValidationError> Errors { get; } = new List<EmployeeValidationError>();
        public bool IsValid => !Errors.Any();

        public void Add(string fieldName, string message)
        {
            Errors.Add(new EmployeeValidationError { FieldName = fieldName, Message = message });
        }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors.Select(e => $"{e.FieldName}: {e.Message}"));
        }
    }
    public class clsEmployees
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsEmployeeEntity _EmployeeInfo { get; set; }
        private readonly string _originalNationalID;
        private readonly int _originalStepID;
        public int EmployeeID
        {
            get => _EmployeeInfo.EmployeeID;
            private set => _EmployeeInfo.EmployeeID = value;
        }
        public string NationalID
        {
            get => _EmployeeInfo.NationalID;
            set => _EmployeeInfo.NationalID = value;
        }
        public string FirstName
        {
            get => _EmployeeInfo.FirstName;
            set => _EmployeeInfo.FirstName = value;
        }
        public string SecondName
        {
            get => _EmployeeInfo.SecondName;
            set => _EmployeeInfo.SecondName = value;
        }
        public string ThirdName  // allows null
        {
            get => _EmployeeInfo.ThirdName;
            set => _EmployeeInfo.ThirdName = value;
        }
        public string LastName
        {
            get => _EmployeeInfo.LastName;
            set => _EmployeeInfo.LastName = value;
        }
        public bool Gender
        {
            get => _EmployeeInfo.Gender;
            set => _EmployeeInfo.Gender = value;
        }
        public DateTime BirthDate
        {
            get => _EmployeeInfo.BirthDate;
            set => _EmployeeInfo.BirthDate = value;
        }
        public string Email // allows null
        {
            get => _EmployeeInfo.Email;
            set => _EmployeeInfo.Email = value;
        }
        public string Phone// allows null
        {
            get => _EmployeeInfo.Phone;
            set => _EmployeeInfo.Phone = value;
        }
        public string Address // allows null
        {
            get => _EmployeeInfo.Address;
            set => _EmployeeInfo.Address = value;
        }
        public DateTime HireDate
        {
            get => _EmployeeInfo.HireDate;
            set => _EmployeeInfo.HireDate = value;
        }
        public bool IsActive
        {
            get => _EmployeeInfo.IsActive;
            set => _EmployeeInfo.IsActive = value;
        }
        public int StepID
        {
            get => _EmployeeInfo.StepID;
            set => _EmployeeInfo.StepID = value;
        }
        public int DepartmentID
        {
            get => _EmployeeInfo.DepartmentID;
            set => _EmployeeInfo.DepartmentID = value;
        }
        public DateTime CreatedDate
        {
            get => _EmployeeInfo.CreatedDate;
            set => _EmployeeInfo.CreatedDate = value;
        }
        public int CreatedByUserID
        {
            get => _EmployeeInfo.CreatedByUserID;
            set => _EmployeeInfo.CreatedByUserID = value;
        }
        public DateTime? EditDate
        {
            get => _EmployeeInfo.EditDate;
            set => _EmployeeInfo.EditDate = value;
        }
        public int? EditedByUserID
        {
            get => _EmployeeInfo.EditedByUserID;
            set => _EmployeeInfo.EditedByUserID = value;
        }

        public clsSalaryGradeSteps StepInfo;
        private clsDepartment _departmentInfo;
        public clsDepartment DepartmentInfo
        {
            get => _departmentInfo;
        }
        public async Task EnsureNavigationDepartmentInfoLoadedAsync()
        {
            if (_departmentInfo == null && DepartmentID > 0)
                _departmentInfo = await clsDepartment.FindDepartmentByID(DepartmentID);
        }

        public clsEmployees()
        {
            _Mode = enMode.AddNew;
            _EmployeeInfo = new clsEmployeeEntity();
            _originalNationalID = null;
            _originalStepID = 0;
        }
        private clsEmployees(clsEmployeeEntity entity)
        {
            if (entity != null)
            {
                _Mode = enMode.Update;
                _EmployeeInfo = entity;
                StepInfo = clsSalaryGradeSteps.FindByID(entity.StepID);
                _originalNationalID = entity.NationalID;
                _originalStepID = entity.StepID;

            }
            else
            {
                _Mode = enMode.AddNew;
                _EmployeeInfo = new clsEmployeeEntity();
                _originalNationalID = null;
                _originalStepID = 0;
            }
        }
        public static async Task<clsEmployees> FindEmployeeByID(int ID)
        {
            var entity = await clsEmployeesData.GetEmployeeInfoByIDAsync(ID);
            return (entity != null) ? new clsEmployees(entity) : null;
        }
        public static async Task<clsEmployees> FindEmployeeByNationalID(string nationalId)
        {
            var entity = await clsEmployeesData.GetEmployeeInfoByNationalIDAsync(nationalId);
            return (entity != null) ? new clsEmployees(entity) : null;
        }
        public async Task<EmployeeValidationResult> Validate()
        {
            var result = new EmployeeValidationResult();

            if (string.IsNullOrWhiteSpace(NationalID))
            {
                result.Add("NationalID", "الرجاء ادخال رقم الهوية الوطني");
            }
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                result.Add("FirstName", "الرجاء ادخال الاسم الاول");
            }
            if (string.IsNullOrWhiteSpace(SecondName))
            {
                result.Add("SecondName", "الرجاء ادخال الاسم الثاني");
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                result.Add("LastName", "الرجاء ادخال الاسم الاخير");
            }
            if (DepartmentID <= 0)
            {
                result.Add("DepartmentID", "الرجاء اختيار القسم");
            }
            if (StepID <= 0)
            {
                result.Add("StepID", "الرجاء اختيار خطوة الراتب");
            }
            if (BirthDate > DateTime.Now)
            {
                result.Add("BirthDate", "تاريخ الميلاد غير صالح");
            }
            if (HireDate < BirthDate)
            {
                result.Add("HireDate", "تاريخ التعين لايمكن ان يكون قبل تاريخ الميلاد");
            }
            if (_Mode == enMode.AddNew)
            {
                if (await clsEmployeesData.IsNationalIDExistsAsync(NationalID))
                {
                    result.Add("NationalID", "رقم الهوية موجود بالفعل");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(_originalNationalID) &&
                !string.Equals(_originalNationalID, NationalID, StringComparison.OrdinalIgnoreCase))
                {
                    if (await clsEmployeesData.IsNationalIDExistsAsync(NationalID))
                    {
                        result.Add("NationalID", "رقم الهوية الذي تحاول تعيينه موجود بالفعل");
                    }

                }
            }

            return result;
        }
        public async Task<EmployeeValidationResult> Save()
        {
            var validation = await Validate();
            if (!validation.IsValid)
            {
                return validation;
            }

            if (_Mode == enMode.AddNew)
            {
                if (_EmployeeInfo.CreatedDate == DateTime.MinValue)
                {
                    _EmployeeInfo.CreatedDate = DateTime.Now;
                }

                _EmployeeInfo.EmployeeID = await clsEmployeesData.AddNewAsync(_EmployeeInfo);
                if (EmployeeID != -1)
                {
                    _Mode = enMode.Update;
                }
            }
            else
            {

                // ----------- هنا التغيير الرئيسي -----------


                if (IsStepChanged())
                {
                    // 1) إنشاء كائن الترقية
                    var promo = new clsEmployeePromotions();
                    promo.EmployeeID = this.EmployeeID;
                    promo.OldStepID = _originalStepID;  // القيمة الأصلية المحفوظة عند التحميل
                    promo.NewStepID = this.StepID;      // القيمة الجديدة
                    promo.CreatedByUserID = clsCurrentUser.User.UserID;
                    promo.PromotionDate = DateTime.Now;
                    promo.Notes = "ترقية تلقائية نتيجة تغيير الدرجة في شاشة الموظف";

                    // 2) نحاول حفظ الترقية
                    var promoValidation = await promo.Save();
                    if (!promoValidation.IsValid)
                    {
                        foreach (var err in promoValidation.Errors)
                        {
                            validation.Add("Promotion_" + err.FieldName, err.Message);
                        }
                        return validation; // لا نكمل لو فشلت الترقية
                    }

                    // 3) نحدّث الموظف بعد نجاح الترقية
                    _EmployeeInfo.EditDate = DateTime.Now;
                    bool updated = await clsEmployeesData.UpdateAsync(_EmployeeInfo);
                    if (!updated)
                    {
                        // الترقية انحفظت لكن التحديث فشل
                        validation.Add("Update", "فشل تحديث بيانات الموظف بعد إنشاء الترقية");
                        return validation;
                    }

                    _Mode = enMode.Update;
                    return validation;
                }
                else
                {
                    // الحالة العادية: لم يتغير StepID
                    _EmployeeInfo.EditDate = DateTime.Now;
                    bool updated = await clsEmployeesData.UpdateAsync(_EmployeeInfo);
                    if (!updated)
                    {
                        validation.Add("Update", "فشل تحديث بيانات الموظف");
                    }
                }

            }

            return validation;
        }
        public static async Task<bool> Delete(int ID) => await clsEmployeesData.DeleteAsync(ID);
        public static async Task<bool> IsNationalIDExists(string nationalId) => await clsEmployeesData.IsNationalIDExistsAsync(nationalId);
        public static async Task<List<clsEmployees>> GetAllEmployees()
        {
            var list = await clsEmployeesData.GetAllEmployees();
            return list.Select(e => new clsEmployees(e)).ToList();
        }

        public static async Task<DataTable> GetEmployeeFullInfo()
        {
            return await clsEmployeesData.GetEmployeeFullInfo();
        }

        public bool IsStepChanged()
        {
            return _EmployeeInfo.StepID != _originalStepID;
        }

        public static async Task<bool> SetNewStepID(int empID, int newStepID)
            => await clsEmployeesData.SetNewStepID(empID, newStepID);

        public static async Task<int> GetNumberOfEmployeesAsync() => await clsEmployeesData.GetNumberOfEmployeesAsync();


        public static async Task<(DataTable Data, int TotalPages)> GetEmployeesPageAsync(int PageNumber, int PageSize, string searchText = null)
             => await clsEmployeesData.GetEmployeesPageAsync(PageNumber, PageSize, searchText);
    }
}
