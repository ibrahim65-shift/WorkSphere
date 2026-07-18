using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkSphere_DataAccess;
using WorkSphere_Entities;

namespace WorkSphere_Buisness
{
    public class clsSalaryGrades
    {

        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsSalaryGradesEntity _SalaryGradesInfo { get; set; }
        public int GradeID
        {
            get => _SalaryGradesInfo.GradeID;
            private set => _SalaryGradesInfo.GradeID = value;
        }
        public short JobGrade
        {
            get => _SalaryGradesInfo.JobGrade;
            set => _SalaryGradesInfo.JobGrade = value;
        }
        public string Description
        {
            get => _SalaryGradesInfo.Description;
            set => _SalaryGradesInfo.Description = value;
        }
        public int DepartmentID
        {
            get => _SalaryGradesInfo.DepartmentID;
            set => _SalaryGradesInfo.DepartmentID = value;
        }

        public DateTime EffectiveFrom
        {
            get => _SalaryGradesInfo.EffectiveFrom;
            set => _SalaryGradesInfo.EffectiveFrom = value;
        }
        public DateTime? EffectiveTo
        {
            get => _SalaryGradesInfo.EffectiveTo;
            set => _SalaryGradesInfo.EffectiveTo = value;
        }
        public bool IsActive
        {
            get => _SalaryGradesInfo.IsActive;
            set => _SalaryGradesInfo.IsActive = value;
        }
        public DateTime CreatedDate
        {
            get => _SalaryGradesInfo.CreatedDate;
            set => _SalaryGradesInfo.CreatedDate = value;
        }
        public int? CreatedByUserID
        {
            get => _SalaryGradesInfo.CreatedByUserID;
            set => _SalaryGradesInfo.CreatedByUserID = value;
        }
        public DateTime? EditedDate
        {
            get => _SalaryGradesInfo.EditedDate;
            set => _SalaryGradesInfo.EditedDate = value;
        }
        public int? EditedByUserID
        {
            get => _SalaryGradesInfo.EditedByUserID;
            set => _SalaryGradesInfo.EditedByUserID = value;
        }
        public clsSalaryGrades()
        {
            _SalaryGradesInfo = new clsSalaryGradesEntity
            {
                CreatedDate = DateTime.Now,
                IsActive = true // ✅ مبدئياً نفترض أن الدرجة نشطة
            };
            _Mode = enMode.AddNew;
        }
        private clsSalaryGrades(clsSalaryGradesEntity entity)
        {
            if (entity != null)
            {
                _SalaryGradesInfo = entity;
                _Mode = enMode.Update;
            }
            else
            {
                _SalaryGradesInfo = new clsSalaryGradesEntity
                {
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };
                _Mode = enMode.AddNew;
            }
        }
        public static clsSalaryGrades FindSalaryGradesByID(int ID)
        {
            var entity = clsSalaryGradesData.GetSalaryGradesByID(ID);
            return (entity != null) ? new clsSalaryGrades(entity) : null;
        }
        public static async Task<List<clsSalaryGrades>> GetSalaryGradesByDepartmentID(int DepID)
        {
            var list = await clsSalaryGradesData.GetSalaryGradesByDepartmentID(DepID);
            return list.Select(l => new clsSalaryGrades(l)).ToList();
        }
        private bool _AddNewSalaryGrades()
        {
            _SalaryGradesInfo.CreatedDate = DateTime.Now;
            _SalaryGradesInfo.GradeID = clsSalaryGradesData.AddNew(_SalaryGradesInfo);
            return (_SalaryGradesInfo.GradeID != -1);
        }
        private bool _UpdateSalaryGrades()
        {
            _SalaryGradesInfo.EditedDate = DateTime.Now;
            return clsSalaryGradesData.Update(_SalaryGradesInfo);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewSalaryGrades())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateSalaryGrades();

                default:
                    return false;
            }
        }
        public static bool Delete(int ID)
        {
            return clsSalaryGradesData.Delete(ID);
        }
        public static DataTable GetAllSalaryGrades()
        {
            return clsSalaryGradesData.GetAllSalaryGrades();
        }
        public static async Task<(DataTable dt , int TotalPages)> GetSalaryGradesPageAsync(int PageNumber, int PageSize, string searchText = null)
        {
            return await clsSalaryGradesData.GetSalaryGradesPageAsync(PageNumber,PageSize,searchText);
        }
        public bool IsGobExists()
        {
            return clsSalaryGradesData.IsGobExists(this.DepartmentID, this.JobGrade, this.Description);
        }
    }
}