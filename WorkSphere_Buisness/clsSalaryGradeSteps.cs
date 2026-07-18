using System.Collections.Generic;
using System.Linq;
using WorkSphere_DataAccess;
using WorkSphere_Entities;

namespace WorkSphere_Buisness
{
    public class clsSalaryGradeSteps
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsSalaryGradeStepsEntity _SalaryStepsInfo { get; set; }

        public int StepID
        {
            get => _SalaryStepsInfo.StepID;
            private set => _SalaryStepsInfo.StepID = value;
        }
        public int GradeID
        {
            get => _SalaryStepsInfo.GradeID;
            set => _SalaryStepsInfo.GradeID = value;
        }
        public byte StepNumber
        {
            get => _SalaryStepsInfo.StepNumber;
            set => _SalaryStepsInfo.StepNumber = value;
        }
        public decimal BaseSalary
        {
            get => _SalaryStepsInfo.BaseSalary;
            set => _SalaryStepsInfo.BaseSalary = value;
        }
        public decimal? AnnualRaisePercent
        {
            get => _SalaryStepsInfo.AnnualRaisePercent;
            set => _SalaryStepsInfo.AnnualRaisePercent = value;
        }
        public byte MinYearsInStep
        {
            get => _SalaryStepsInfo.MinYearsInStep;
            set => _SalaryStepsInfo.MinYearsInStep = value;
        }

        public clsSalaryGrades GradeInfo;
        public clsSalaryGradeSteps()
        {
            _SalaryStepsInfo = new clsSalaryGradeStepsEntity();
            _Mode = enMode.AddNew;
        }

        private clsSalaryGradeSteps(clsSalaryGradeStepsEntity entity)
        {
            if (entity != null)
            {
                _SalaryStepsInfo = entity;
                _Mode = enMode.Update;
                GradeInfo = clsSalaryGrades.FindSalaryGradesByID(entity.GradeID);
            }
            else
            {
                _SalaryStepsInfo = new clsSalaryGradeStepsEntity();
                _Mode = enMode.AddNew;
            }
        }

        public static clsSalaryGradeSteps FindByID(int ID)
        {
            var entity = clsSalaryGradeStepsData.GetSalaryGradeStepsByID(ID);
            return (entity != null) ? new clsSalaryGradeSteps(entity) : null;
        }

        // === NEW: return list of steps for a grade ===
        public static List<clsSalaryGradeSteps> GetByGradeID(int gradeID)
        {
            var entities = clsSalaryGradeStepsData.GetSalaryGradeStepsByGradeID(gradeID);
            return entities.Select(e => new clsSalaryGradeSteps(e)).ToList();
        }

        private bool _AddNew()
        {

            _SalaryStepsInfo.StepID = clsSalaryGradeStepsData.AddNew(_SalaryStepsInfo);
            return (_SalaryStepsInfo.StepID != -1);
        }

        private bool _Update()
        {
            // if step number changed, ensure uniqueness
            return clsSalaryGradeStepsData.Update(_SalaryStepsInfo);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _Update();
            }
            return false;
        }

        public static bool Delete(int ID) => clsSalaryGradeStepsData.Delete(ID);

        public static List<clsSalaryGradeSteps> GetAll()
        {
            return clsSalaryGradeStepsData.GetAllSalaryGradeSteps().Select(e => new clsSalaryGradeSteps(e)).ToList();
        }

        public bool IsDuplicateCombination()
        {
            return clsSalaryGradeStepsData.IsStepNumberExistsByGradeIDAndStepNumber(this.GradeID, this.StepNumber);
        }

        public static int GetStepNumber(int GradeID)
        {
            return clsSalaryGradeStepsData.GetStepNumber(GradeID);
        }
    }
}