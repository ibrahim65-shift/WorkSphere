using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkSphere_DataAccess;
using WorkSphere_Entities;
using WrokSphere_Shared;

namespace WorkSphere_Buisness
{
    public class RolePermissionsValidationErrors
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
    public class RolePermissionsValidationResult
    {
        public List<RolePermissionsValidationErrors> Errors { get; set; } = new List<RolePermissionsValidationErrors>();

        public bool IsValid => !Errors.Any();

        public void Add(string fieldName, string message) =>
            Errors.Add(new RolePermissionsValidationErrors { FieldName = fieldName, Message = message });

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors.Select(s => $"{s.FieldName} : {s.Message}"));
        }

    }

    public class clsRolePermissions
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsRolePermissionsEntity _rolePermissionInfo;

        public int RolePermissionID
        {
            get => _rolePermissionInfo.RolePermissionID;
            private set => _rolePermissionInfo.RolePermissionID = value;
        }
        public int RoleID
        {
            get => _rolePermissionInfo.RoleID;
            set => _rolePermissionInfo.RoleID = value;
        }
        public int PermissionID
        {
            get => _rolePermissionInfo.PermissionID;
            set => _rolePermissionInfo.PermissionID = value;
        }
        public bool IsAllowed
        {
            get => _rolePermissionInfo.IsAllowed;
            set => _rolePermissionInfo.IsAllowed = value;
        }

        public clsRolePermissions()
        {
            _Mode = enMode.AddNew;
            _rolePermissionInfo = new clsRolePermissionsEntity();
        }
        private clsRolePermissions(clsRolePermissionsEntity entity)
        {
            if (entity != null)
            {
                _Mode = enMode.Update;
                _rolePermissionInfo = entity;
            }
            else
            {
                _Mode = enMode.AddNew;
                _rolePermissionInfo = new clsRolePermissionsEntity();
            }
        }

        public static async Task<clsRolePermissions> FindRolePermissionsByRolePermIDAsync(int rolePermID)
        {
            var entity = await clsRolePermissionsData.GetRolePermissionsByRolePermIDAsync(rolePermID);
            return entity == null ? null : new clsRolePermissions(entity);
        }
        public static async Task<List<clsRolePermissions>> GetListRolePermissionsBypermIDAsync(int permID)
        {
            var list = await clsRolePermissionsData.GetListRolePermissionsBypermIDAsync(permID);
            return list.Select(e => new clsRolePermissions(e)).ToList();
        }
        public static async Task<List<clsRolePermissions>> GetListRolePermissionsByRoleIDAsync(int roleID)
        {
            var list = await clsRolePermissionsData.GetListRolePermissionsByRoleIDAsync(roleID);
            return list.Select(e => new clsRolePermissions(e)).ToList();
        }
        public static async Task<bool> DeleteAsync(int rolePermID)
            => await clsRolePermissionsData.DeleteAsync(rolePermID);
        public static async Task<DataTable> GetAllRolePermissionInfoAsync()
            => await clsRolePermissionsData.GetAllRolePermissionInfoAsync();
        public static async Task<(DataTable dt , int TotalPages)> GetRolePermissionsPageAsync(int PageNumber, int PageSize, string searchText = null)
        {
            return await clsRolePermissionsData.GetRolePermissionsPageAsync(PageNumber,PageSize, searchText);
        }
        public async Task<RolePermissionsValidationResult> ValidateAsync()
        {
            var list = new RolePermissionsValidationResult();

            if (RoleID <= 0)
            {
                list.Add("RoleID", "! معرف الدور غير صالح");
                return list;
            }

            if (_Mode == enMode.AddNew)
            {
                if (await clsRolePermissionsData.IsRoleAlreadyHasThisPermissionAsync(RoleID, PermissionID))
                {
                    list.Add("RoleID", "! هذا الدور بالفعل لديه هذه الصلاحية");
                    return list;
                }
            }

            return list;
        }
        public async Task<RolePermissionsValidationResult> SaveRolePermissionsBulkAsync(int roleID, List<clsRolePermissionItem> permissions)
        {
            var validation = await ValidateAsync();

            if (!validation.IsValid)
            {
                return validation;
            }

            if (!await clsRolePermissionsData.SaveRolePermissionsBulkAsync(roleID, permissions))
            {
                validation.Add("Save", "فشل حفظ الصلاحيات للدور");
            }

            return validation;
        }
    }
}
