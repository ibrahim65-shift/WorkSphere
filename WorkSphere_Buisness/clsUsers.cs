using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_DataAccess;
using WorkSphere_Entities;

namespace WorkSphere_Buisness
{
    public class UserValidationErrors
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
    public class UserValidationResult
    {
        public List<UserValidationErrors> Errors { get; set; } = new List<UserValidationErrors>();

        public bool IsValid => !Errors.Any();

        public void Add(string fieldName, string message) =>
            Errors.Add(new UserValidationErrors { FieldName = fieldName, Message = message });

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors.Select(s => $"{s.FieldName} : {s.Message}"));
        }

    }

    public class clsUsers
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;
        private clsUsersEntities UserInfo { get; set; }


        public int UserID
        {
            get => UserInfo.UserID;
            private set => UserInfo.UserID = value;
        }
        public string UserName
        {
            get => UserInfo.UserName;
            set => UserInfo.UserName = value;
        }
        public string Password
        {
            get => UserInfo.Password;
            set => UserInfo.Password = value;
        }
        public string FullName
        {
            get => UserInfo.FullName;
            set => UserInfo.FullName = value;
        }
        public string Email
        {
            get => UserInfo.Email;
            set => UserInfo.Email = value;
        }
        public string Phone
        {
            get => UserInfo.Phone;
            set => UserInfo.Phone = value;
        }
        public string Address
        {
            get => UserInfo.Address;
            set => UserInfo.Address = value;
        }
        public bool IsActive
        {
            get => UserInfo.IsActive;
            set => UserInfo.IsActive = value;
        }
        public int RoleID
        {
            get => UserInfo.RoleID;
            set => UserInfo.RoleID = value;
        }
        public string RoleName
        {
            get => UserInfo.RoleName;
        }
        public DateTime CreatedDate
        {
            get => UserInfo.CreatedDate;
            set => UserInfo.CreatedDate = value;
        }
        public DateTime EditDate
        {
            get => UserInfo.EditDate;
            set => UserInfo.EditDate = value;
        }



        public clsUsers()
        {
            UserInfo = new clsUsersEntities();
            _Mode = enMode.AddNew;
        }
        private clsUsers(clsUsersEntities userEntity)
        {
            if (userEntity != null)
            {
                UserInfo = userEntity;
                _Mode = enMode.Update;
            }
            else
            {
                UserInfo = new clsUsersEntities();
                _Mode = enMode.AddNew;
            }
        }


        public static clsUsers FindUserByID(int userID)
        {
            var entity = clsUsersData.GetUserByID(userID);
            return (entity != null) ? new clsUsers(entity) : null;
        }
        public static clsUsers FindUserByUserName(string userName)
        {
            var entity = clsUsersData.GetUserByUserName(userName);
            return (entity != null) ? new clsUsers(entity) : null;
        }
        public static clsUsers FindUserByUserNameAndPassword(string userName, string password)
        {
            var entity = clsUsersData.GetUserByUserName(userName);
            if (entity == null || string.IsNullOrWhiteSpace(entity.Password))
                return null;

            if (!clsSecurity.Verify(password, entity.Password))
                return null;

            return new clsUsers(entity);
        }
        public UserValidationResult Validate()
        {
            var list = new UserValidationResult();

            if (string.IsNullOrWhiteSpace(UserName))
            {
                list.Add("UserName", "! اسم المستخدم لايمكن أن يكون فارغ");
            }


            if (string.IsNullOrWhiteSpace(FullName))
            {
                list.Add("FullName", "! الاسم الكامل لايمكن أن يكون فارغ");
            }

            if (string.IsNullOrWhiteSpace(Phone))
            {
                list.Add("Phone", "! رقم الهاتف لايمكن أن يكون فارغ");
            }


            if (!string.IsNullOrWhiteSpace(UserName) && UserName.Length < 4)
            {
                list.Add("UserName", "! اسم المستخدم لايمكن أن أن يقل عن 4 خانات");
            }

            if (!string.IsNullOrWhiteSpace(Phone) && Phone.Length != 10)
            {
                list.Add("Phone", "! رقم الهاتف يجب أن يكون 10 خانات");
            }


            if (_Mode == enMode.AddNew)
            {
                if (string.IsNullOrWhiteSpace(Password))
                {
                    list.Add("Password", "! كلمة المرور لايمكن أن تكون فارغة");
                }

                if (clsUsersData.IsUserNameExists(UserName))
                {
                    list.Add("UserName", "! اسم المستخدم مستخدم من قبل مستخدم اخر");
                }

                if (clsUsersData.IsFullNameExists(FullName))
                {
                    list.Add("FullName", "! هذا الاسم موجود بالفعل , الرجاء اختيار اسم اخر");
                }
            }


            return list;
        }
        public UserValidationResult Save()
        {
            var validation = Validate();

            if (!validation.IsValid)
            {
                return validation;
            }

            if (_Mode == enMode.AddNew)
            {

                UserInfo.CreatedDate = DateTime.Now;
                UserInfo.EditDate = DateTime.Now;

                UserInfo.UserID = clsUsersData.AddNew(UserInfo);

                if (UserInfo.UserID != -1)
                {
                    _Mode = enMode.Update;
                }
            }
            else
            {
                UserInfo.EditDate = DateTime.Now;
                bool update = clsUsersData.Update(UserInfo);

                if (!update)
                {
                    validation.Add("Update", "! فشل تحديث بيانات المستخدم");
                }
            }

            return validation;
        }
        public static bool Delete(int userID) => clsUsersData.Delete(userID);
        public static List<clsUsers> GetAllUsers()
        {
            var entities = clsUsersData.GetAllUsers();
            return entities.Select(e => new clsUsers(e)).ToList();
        }
        public static async Task<(List<clsUsers> list , int TotalPages)> GetUsersPageAsync(int PageNumber ,int PageSize , string searchText=null)
        {
            var entites = await clsUsersData.GetUsersPageAsync(PageNumber, PageSize, searchText);
            return (entites.List.Select(e=> new clsUsers(e)).ToList() , entites.TotalPages);
        }
        public static bool UpdatePassword(string newPassword, int userID)
        {
            string salt = clsSecurity.GenerateSalt();
            string hash = clsSecurity.HashPassword(newPassword, salt);
            string packed = clsSecurity.Pack(salt, hash);

            return clsUsersData.UpdatePassword(packed, userID);
        }

        public static string GetPasswordByUserID(int userID) => clsUsersData.GetPasswordByUserID(userID);

        public static async Task<List<string>> GetUserPermissionsAsync(int userID)
            => await clsUsersData.GetUserPermissionsAsync(userID);
    }
}