namespace WorkSphere_Entities
{
    public class clsSalaryGradeStepsEntity
    {
        public int StepID { get; set; }
        public int GradeID { get; set; }
        public byte StepNumber { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal? AnnualRaisePercent { get; set; }
        public byte MinYearsInStep { get; set; }
    }
}
