using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkSphere_DataAccess;
using WorkSphere_Entities;

namespace WorkSphere_Buisness
{
    public class RolesValidationErrors
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
    public class RolesValidationResult
    {
        public List<RolesValidationErrors> Errors { get; set; } = new List<RolesValidationErrors>();

        public bool IsValid => !Errors.Any();

        public void Add(string fieldName, string message) =>
            Errors.Add(new RolesValidationErrors { FieldName = fieldName, Message = message });

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors.Select(e => $"{e.FieldName} : {e.Message}"));
        }
    }
    public class clsRoles
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsRolesEntity _roleInfo;

        public int RoleID
        {
            get => _roleInfo.RoleID;
            private set => _roleInfo.RoleID = value;
        }
        public string RoleName
        {
            get => _roleInfo.RoleName;
            set => _roleInfo.RoleName = value;
        }
        public string Description  // allows null
        {
            get => _roleInfo.Description;
            set => _roleInfo.Description = value;
        }
        public bool IsActive
        {
            get => _roleInfo.IsActive;
            set => _roleInfo.IsActive = value;
        }

        public clsRoles()
        {
            _Mode = enMode.AddNew;
            _roleInfo = new clsRolesEntity();
        }
        private clsRoles(clsRolesEntity entity)
        {
            if (entity == null)
            {
                _Mode = enMode.AddNew;
                _roleInfo = new clsRolesEntity();
            }
            else
            {
                _Mode = enMode.Update;
                _roleInfo = entity;
            }
        }

        public static async Task<clsRoles> FindRoleByroleIDAsync(int roleID)
        {
            var entity = await clsRolesData.GetRoleByroleIDAsync(roleID);
            return entity == null ? null : new clsRoles(entity);
        }
        public static async Task<clsRoles> FindRoleByroleNameAsync(string roleName)
        {
            var entity = await clsRolesData.GetRoleByroleNameAsync(roleName);
            return entity == null ? null : new clsRoles(entity);
        }
        public static async Task<bool> DeleteAsync(int roleID) => await clsRolesData.DeleteAsync(roleID);
        public static async Task<List<clsRoles>> GetAllAsync()
        {
            var list = await clsRolesData.GetAllAsync();
            return list.Select(e => new clsRoles(e)).ToList();
        }
        public static async Task<(List<clsRoles> list , int TotalPages)> GetRolesPageAsync(int PageNumber, int PageSize, string searchText = null)
        {
            var entities = await clsRolesData.GetRolesPageAsync(PageNumber,PageSize,searchText);
            return (entities.list.Select(e => new clsRoles(e)).ToList(),entities.TotalPages);
        }
        public async Task<RolesValidationResult> ValidateAsync()
        {
            var list = new RolesValidationResult();


            if (string.IsNullOrWhiteSpace(RoleName))
            {
                list.Add("RoleName", "! لايمكن لحقل الدور أن يكون فارغ");
            }

            if (_Mode == enMode.AddNew)
            {
                if (await clsRolesData.IsRoleNameExists(RoleName))
                {
                    list.Add("RoleName", "! هذا الدور موجود بالفعل");
                }
            }
            else
            {
                if (!IsActive)
                {
                    list.Add("IsActive", "! لايمكن تحديث هذا الدور لأنه لم يعد نشط");
                }
            }

            return list;
        }
        public async Task<RolesValidationResult> SaveAsync()
        {
            var validation = await ValidateAsync();

            if (!validation.IsValid)
            {
                return validation;
            }

            if (_Mode == enMode.AddNew)
            {
                _roleInfo.RoleID = await clsRolesData.AddNewAsync(_roleInfo);
                if (_roleInfo.RoleID != -1)
                {
                    _Mode = enMode.Update;
                }
            }
            else
            {
                bool update = await clsRolesData.UpdateAsync(_roleInfo);
                if (!update)
                {
                    validation.Add("Update", "! فشل تحديث بيانات الدور");
                }
            }

            return validation;
        }
    }
}
