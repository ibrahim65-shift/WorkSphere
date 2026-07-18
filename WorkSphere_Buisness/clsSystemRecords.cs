using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkSphere_DataAccess;
using WorkSphere_Entities;

namespace WorkSphere_Buisness
{
    public class clsSystemRecords
    {
        public enum enMode { AddNew = 0, Update = 1 }

        private enMode _Mode = enMode.AddNew;

        private clsSystemRecordEntity SystemRecordInfo { get; set; }


        // === Wrappers for Presentation Layer ===
        public int ID
        {
            get => SystemRecordInfo.ID;
            private set => SystemRecordInfo.ID = value;
        }

        public string ActionType
        {
            get => SystemRecordInfo.ActionType;
            set => SystemRecordInfo.ActionType = value;
        }
        public string DeviceName
        {
            get => SystemRecordInfo.DeviceName;
            set => SystemRecordInfo.DeviceName = value;
        }
        public string MachinID
        {
            get => SystemRecordInfo.MachinID;
            set => SystemRecordInfo.MachinID = value;
        }
        public string Title
        {
            get => SystemRecordInfo.Title;
            set => SystemRecordInfo.Title = value;
        }
        public string Description
        {
            get => SystemRecordInfo.Description;
            set => SystemRecordInfo.Description = value;
        }
        public DateTime CreatedDate
        {
            get => SystemRecordInfo.CreatedDate;
            set => SystemRecordInfo.CreatedDate = value;
        }
        public int UserID
        {
            get => SystemRecordInfo.UserID;
            set => SystemRecordInfo.UserID = value;
        }
        public clsUsers User { get; private set; }

        public clsSystemRecords()
        {
            SystemRecordInfo = new clsSystemRecordEntity();
            User = null;
            _Mode = enMode.AddNew;
        }

        private clsSystemRecords(clsSystemRecordEntity entity)
        {
            if (entity != null)
            {
                SystemRecordInfo = entity;
                User = clsUsers.FindUserByID(SystemRecordInfo.UserID);
                _Mode = enMode.Update;
            }
            else
            {
                SystemRecordInfo = new clsSystemRecordEntity();
                User = null;
                _Mode = enMode.AddNew;
            }
        }

        public static clsSystemRecords FindByID(int ID)
        {
            var entity = clsSystemRecordsData.GetSystemRecordInfoByID(ID);
            return (entity != null) ? new clsSystemRecords(entity) : null;
        }

        public static clsSystemRecords FindByUserID(int UserID)
        {
            var entity = clsSystemRecordsData.GetSystemRecordInfoByUserID(UserID);
            return (entity != null) ? new clsSystemRecords(entity) : null;
        }

        private bool _AddNew()
        {
            if (User != null)
                SystemRecordInfo.UserID = User.UserID;

            SystemRecordInfo.ID = clsSystemRecordsData.AddNew(SystemRecordInfo);
            return (SystemRecordInfo.ID != -1);
        }

        private bool _Update()
        {
            if (User != null)
                SystemRecordInfo.UserID = User.UserID;

            return clsSystemRecordsData.Update(SystemRecordInfo);
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

        public static bool Delete(int ID)
        {
            return clsSystemRecordsData.Delete(ID);
        }

        public static List<clsSystemRecords> GetAllSystemRecords()
        {
            var entities = clsSystemRecordsData.GetAllSystemRecords();
            return entities.Select(e => new clsSystemRecords(e)).ToList();
        }
        public static async Task<(List<clsSystemRecords> list , int TotalPages)> GetSystemRecordsPageAsync(int PageNumber, int PageSize, string searchText = null)
        {
            var entities = await clsSystemRecordsData.GetSystemRecordsPageAsync(PageNumber,PageSize, searchText);
            return (entities.list.Select(e => new clsSystemRecords(e)).ToList() , entities.TotalPages);
        }
    }
}