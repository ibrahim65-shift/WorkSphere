using System;

namespace WorkSphere_Entities
{
    public class clsSystemRecordEntity
    {
        public int ID { get; set; }
        public string ActionType { get; set; }
        public string DeviceName { get; set; }
        public string MachinID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserID { get; set; }

    }
}
