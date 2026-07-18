using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkSphere_DataAccess;
using WorkSphere_Entities;

namespace WorkSphere_Buisness
{
    public class PermissionsValidationError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
    public class PermissionsValidationResult
    {
        List<PermissionsValidationError> Errors { get; set; } = new List<PermissionsValidationError>();

        public bool IsValid => !Errors.Any();
        public void Add(string fieldName, string message) =>
              Errors.Add(new PermissionsValidationError { FieldName = fieldName, Message = message });

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors.Select(e => $"{e.FieldName} : {e.Message}"));
        }
    }
    public class clsPermissions
    {
        private enum enMode { AddNew = 0, Update = 1 }

        private enMode _Mode;
        private clsPermissionsEntity _PermissionInfo;

        public int PermissionID
        {
            get => _PermissionInfo.PermissionID;
            private set => _PermissionInfo.PermissionID = value;
        }
        public string PermissionCode
        {
            get => _PermissionInfo.PermissionCode;
            set => _PermissionInfo.PermissionCode = value;
        }
        public string PermissionName
        {
            get => _PermissionInfo.PermissionName;
            set => _PermissionInfo.PermissionName = value;
        }

        public clsPermissions()
        {
            _Mode = enMode.AddNew;
            _PermissionInfo = new clsPermissionsEntity();
        }

        private clsPermissions(clsPermissionsEntity entity)
        {
            if (entity == null)
            {
                _Mode = enMode.AddNew;
                _PermissionInfo = new clsPermissionsEntity();
            }
            else
            {
                _Mode = enMode.Update;
                _PermissionInfo = entity;
            }
        }

        public static async Task<clsPermissions> FindPermissionByPermissionIDAsync(int permID)
        {
            var entity = await clsPermissionsData.GetPermissionByPermissionIDAsync(permID);
            return (entity == null) ? null : new clsPermissions(entity);
        }
        public static async Task<clsPermissions> FindPermissionByPermissionCodeAsync(string permCode)
        {
            var entity = await clsPermissionsData.GetPermissionByPermissionCodeAsync(permCode);
            return (entity == null) ? null : new clsPermissions(entity);
        }
        public static async Task<bool> DeleteAsync(int permID) => await clsPermissionsData.DeleteAsync(permID);
        public static async Task<List<clsPermissions>> GetAllAsync()
        {
            var list = await clsPermissionsData.GetAllAsync();
            return list.Select(e => new clsPermissions(e)).ToList();
        }
        public async Task<PermissionsValidationResult> ValidateAsync()
        {
            var list = new PermissionsValidationResult();

            if (string.IsNullOrWhiteSpace(PermissionCode) || string.IsNullOrWhiteSpace(PermissionName))
            {
                list.Add("PermissionCode", "! لايمكن لحقل الصلاحية أن يكون فارغ");
                list.Add("PermissionName", "! لايمكن لحقل اسم الصلاحية أن يكون فارغ");
                return list;
            }

            if (_Mode == enMode.AddNew)
            {
                if (await clsPermissionsData.IsPermissionCodeExists(PermissionCode))
                {
                    list.Add("PermissionCode", "! هذه الصلاحية موجودة بالفعل");
                    return list;
                }
            }

            return list;
        }
        public async Task<PermissionsValidationResult> SaveAsync()
        {
            var validation = await ValidateAsync();

            if (!validation.IsValid)
            {
                return validation;
            }

            if (_Mode == enMode.AddNew)
            {
                _PermissionInfo.PermissionID = await clsPermissionsData.AddNewAsync(_PermissionInfo);
                if (_PermissionInfo.PermissionID != -1)
                {
                    _Mode = enMode.Update;
                }
            }
            else
            {
                bool update = await clsPermissionsData.UpdateAsync(_PermissionInfo);
                if (!update)
                {
                    validation.Add("Update", "! فشل تحديث بيانات الصلاحية");
                }
            }

            return validation;
        }
    }
}
