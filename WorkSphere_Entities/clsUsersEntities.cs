using System;

namespace WorkSphere_Entities
{
    public class clsUsersEntities
    {
        public int UserID { get; set; }           // المفتاح الأساسي
        public string UserName { get; set; }      // اسم المستخدم
        public string Password { get; set; }      // كلمة المرور (مشفرة)
        public string FullName { get; set; }      // الاسم الكامل
        public string Email { get; set; }         // يمكن أن يكون Null
        public string Phone { get; set; }         // رقم الهاتف
        public string Address { get; set; }       // يمكن أن يكون Null
        public bool IsActive { get; set; }        // حالة الحساب
        public int RoleID { get; set; }           // مفتاح خارجي من جدول RoleDefinitions
        public string RoleName { get; set; }
        public DateTime CreatedDate { get; set; } // تاريخ الإنشاء
        public DateTime EditDate { get; set; }   // تاريخ التعديل 

    }
}