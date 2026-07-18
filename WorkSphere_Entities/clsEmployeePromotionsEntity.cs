using System;

namespace WorkSphere_Entities
{
    public class clsEmployeePromotionsEntity
    {
        public int PromotionID { get; set; }
        public int EmployeeID { get; set; }
        public int OldStepID { get; set; }
        public int NewStepID { get; set; }
        public DateTime PromotionDate { get; set; }
        public string Notes { get; set; }//allows null
        public int CreatedByUserID { get; set; }
    }
}
