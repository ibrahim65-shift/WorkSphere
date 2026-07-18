using System;
using System.Collections.Generic;

namespace WorkSphere_Buisness.Global_Classes
{
    public static class clsAuthorizationCache
    {
        // حفظ صلاحيات المستخدم الحالي مؤقتًا
        private static HashSet<string> _permissionsCache;

        // تحميل الصلاحيات لمستخدم معين
        public static void LoadPermissions(IEnumerable<string> permissions)
        {
            if (permissions == null)
                _permissionsCache = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            else
                _permissionsCache = new HashSet<string>(permissions, StringComparer.OrdinalIgnoreCase);
        }

        // التحقق هل يمتلك المستخدم صلاحية محددة
        public static bool HasPermission(string permissionCode)
        {
            if (string.IsNullOrWhiteSpace(permissionCode))
                return false;

            return _permissionsCache != null && _permissionsCache.Contains(permissionCode);
        }

        // تفريغ الكاش عند تسجيل خروج المستخدم
        public static void Clear()
        {
            _permissionsCache?.Clear();
            _permissionsCache = null;
        }

        // لمعرفة هل تم تحميل صلاحيات أم لا
        public static bool IsLoaded => _permissionsCache != null;
    }
}
