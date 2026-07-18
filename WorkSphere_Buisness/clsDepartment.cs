using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkSphere_DataAccess;
using WorkSphere_Entities;

namespace WorkSphere_Buisness
{
    public class DepartmentValidationError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }

    public class DepartmentValidationResult
    {
        public List<DepartmentValidationError> Errors { get; } = new List<DepartmentValidationError>();
        public bool IsValid => !Errors.Any();

        public void Add(string fieldName, string message)
        {
            Errors.Add(new DepartmentValidationError { FieldName = fieldName, Message = message });
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors.Select(e => $"{e.FieldName}: {e.Message}"));
        }
    }

    public class clsDepartment
    {
        private enum enMode { AddNew = 0, Update = 1 }

        private enMode _Mode;
        private clsDepartmentEntity _DepartmentInfo;
        private readonly string _originalDepartmentName;
        public int DepartmentID
        {
            get => _DepartmentInfo.DepartmentID;
            private set => _DepartmentInfo.DepartmentID = value;
        }
        public string DepartmentName
        {
            get => _DepartmentInfo.DepartmentName;
            set => _DepartmentInfo.DepartmentName = value;
        }
        public string Description // allows null
        {
            get => _DepartmentInfo.Description;
            set => _DepartmentInfo.Description = value;
        }
        public DateTime CreatedDate
        {
            get => _DepartmentInfo.CreatedDate;
            set => _DepartmentInfo.CreatedDate = value;
        }

        public clsDepartment()
        {
            _Mode = enMode.AddNew;
            _DepartmentInfo = new clsDepartmentEntity();
        }
        private clsDepartment(clsDepartmentEntity entity)
        {
            if (entity != null)
            {
                _Mode = enMode.Update;
                _DepartmentInfo = entity;
                _originalDepartmentName = entity.DepartmentName;
            }
            else
            {
                _Mode = enMode.AddNew;
                _DepartmentInfo = new clsDepartmentEntity();
            }
        }
        public static async Task<clsDepartment> FindDepartmentByID(int ID)
        {
            var entity = await clsDepartmentData.GetDepartmentInfoByIDAsync(ID);
            return (entity != null) ? new clsDepartment(entity) : null;
        }
        public static async Task<clsDepartment> FindDepartmentByName(string Name)
        {
            var entity = await clsDepartmentData.GetDepartmentInfoByDepartmentNameAsync(Name);
            return (entity != null) ? new clsDepartment(entity) : null;
        }
        public async Task<DepartmentValidationResult> Validate()
        {
            var result = new DepartmentValidationResult();

            if (string.IsNullOrWhiteSpace(DepartmentName))
            {
                result.Add("DepartmentName", "اسم القسم لايمكن ان يكون فارغ");
            }

            if (DepartmentName?.Length > 200)
            {
                result.Add("DepartmentName", "اسم القسم يجب ان لايزيد عن 200 حرف");
            }

            if (_Mode == enMode.AddNew ||
                (!string.Equals(_originalDepartmentName, DepartmentName, StringComparison.OrdinalIgnoreCase)))
            {
                if (await clsDepartmentData.IsDepartmentExists(DepartmentName))
                {
                    result.Add("DepartmentName", "هذا القسم موجود بالفعل الرجاء اضافة قسم اخر");
                }
            }

            return result;
        }
        public async Task<DepartmentValidationResult> Save()
        {
            var validation = await Validate();

            if (!validation.IsValid)
            {
                return validation;
            }

            if (_Mode == enMode.AddNew)
            {
                _DepartmentInfo.CreatedDate = DateTime.Now;
                _DepartmentInfo.DepartmentID = await clsDepartmentData.AddNewAsync(_DepartmentInfo);
                if (_DepartmentInfo.DepartmentID != -1)
                {
                    _Mode = enMode.Update;
                }
            }
            else
            {
                bool Update = await clsDepartmentData.UpdateAsync(_DepartmentInfo);
                if (!Update)
                {
                    validation.Add("Update", "فشل تحديث بيانات القسم");
                }
            }

            return validation;
        }
        public static async Task<bool> Delete(int ID) => await clsDepartmentData.DeleteAsync(ID);

        public static async Task<List<clsDepartment>> GetAllDepartments()
        {
            var list = await clsDepartmentData.GetAllDepartments();
            return list.Select(e => new clsDepartment(e)).ToList();
        }
        public static async Task <(List<clsDepartment> list ,int TotalPage)> GetDepartmentsPageAsync(int PageNumber , int PageSize , string searchText=null)
        {
            var entities = await clsDepartmentData.GetDepartmentsPageAsync(PageNumber, PageSize, searchText);
            return (entities.list.Select(e => new clsDepartment(e)).ToList(), entities.TotalPages);
        }
    }
}
