using System.Windows.Forms;
using WorkSphere_Buisness.Global_Classes;

namespace WorkSphere.Global_Classes
{
    public class clsPermissionHelper
    {

        /// <summary>
        /// تطبيق الصلاحيات على الفورم بالكامل (Controls + MenuStrip)
        /// </summary>
        public static void ApplyToForm(Form form)
        {
            if (form == null)
                return;

            ApplyToControlCollection(form.Controls);

            // إذا كان للفورم MenuStrip
            if (form.MainMenuStrip != null)
            {
                ApplyToToolStripItemCollection(form.MainMenuStrip.Items);
            }
        }

        /// <summary>
        /// تطبيق الصلاحيات على UserControl رئيسي
        /// </summary>
        public static void ApplyToControlRoot(Control controlRoot)
        {
            if (controlRoot == null)
                return;

            ApplyToControl(controlRoot);

            if (controlRoot.HasChildren)
                ApplyToControlCollection(controlRoot.Controls);

            // لو يحتوي MenuStrip
            foreach (Control child in controlRoot.Controls)
            {
                if (child is MenuStrip menu)
                {
                    ApplyToToolStripItemCollection(menu.Items);
                }
            }
        }

        // تطبيق الصلاحيات على مجموعة Controls
        private static void ApplyToControlCollection(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                ApplyToControl(c);

                if (c.HasChildren)
                    ApplyToControlCollection(c.Controls);
            }
        }

        // تطبيق الصلاحيات على Control مفرد
        private static void ApplyToControl(Control control)
        {
            if (control?.Tag == null) return;

            // نتوقع أن الـ Tag يحتوي على كود الصلاحية: مثل "Employees.Add"
            string permCode = control.Tag.ToString().Trim();

            if (string.IsNullOrWhiteSpace(permCode))
                return;

            bool hasPerm = clsAuthorizationCache.HasPermission(permCode);

            // يمكنك تغيير هذا السلوك (إخفاء بدلاً من تعطيل)
            // control.Enabled = hasPerm;


            // مثال إذا أردت الإخفاء بدلاً من التعطيل:
            control.Visible = hasPerm;
        }

        // تطبيق الصلاحيات على ToolStripItems (مثل القوائم)
        private static void ApplyToToolStripItemCollection(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                ApplyToToolStripItem(item);

                if (item is ToolStripMenuItem menu)
                    ApplyToToolStripItemCollection(menu.DropDownItems);
            }
        }

        private static void ApplyToToolStripItem(ToolStripItem item)
        {
            if (item?.Tag == null) return;

            string permCode = item.Tag.ToString().Trim();

            if (string.IsNullOrWhiteSpace(permCode))
                return;

            bool hasPerm = clsAuthorizationCache.HasPermission(permCode);

            //item.Enabled = hasPerm;
            // أو للإخفاء:
            item.Visible = hasPerm;
        }

        /// <summary>
        /// لإعادة تطبيق الصلاحيات بعد تسجيل الدخول أو تغيير الدور
        /// </summary>
        public static void Refresh(Form form)
        {
            ApplyToForm(form);
        }

    }
}
