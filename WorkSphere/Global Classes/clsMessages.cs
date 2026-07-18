using System.Windows.Forms;

namespace WorkSphere.Global_Classes
{
    public static class clsMessages
    {
        // رسائل عامة
        public static void ShowSuccess(string message = "تمت العملية بنجاح.")
        {
            MessageBox.Show(message, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ShowError(string message = "حدث خطأ غير متوقع. يرجى المحاولة لاحقًا.")
        {
            MessageBox.Show(message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ShowWarning(string message = "تنبيه: يرجى التحقق من البيانات.")
        {
            MessageBox.Show(message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void ShowInfo(string message = "معلومة: يرجى الاطلاع.")
        {
            MessageBox.Show(message, "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // رسائل متخصصة
        public static void ShowServerError()
        {
            ShowError("تعذّر الاتصال بالخادم. يرجى المحاولة لاحقًا.");
        }
        public static void ShowRequiredFileds()
        {
            ShowWarning("جميع الحقول التي تحمل الرمز (*) هي إلزامية. يرجى تعبئتها والمحاولة مجددًا.");
        }
        public static void ShowNullUserObject(int ID)
        {
            ShowError($"لا يوجد مستخدم مسجل برقم التعريف ({ID}).");
        }
        public static void ShowDataSavedSuccessfully(int ID)
        {
            ShowSuccess($"تم حفظ البيانات بنجاح. رقم التعريف المخصّص هو: {ID}.");
        }
        public static void ShowDataEditedSuccessfully(int ID)
        {
            ShowSuccess($"تم تعديل البيانات بنجاح. لرقم التعريف : {ID}.");
        }
        public static void ShowDataSavedFaild()
        {
            ShowError("حدث خطأ أثناء عملية الحفظ. لم يتم تخزين البيانات.");
        }

        public static void ShowDataDeleteFaild()
        {
            ShowError("حدث خطأ أثناء عملية الحذف. لم يتم حذف البيانات.");
        }
        public static void ShowUserNameError()
        {
            ShowWarning("اسم المستخدم مسجّل مسبقًا. يرجى اختيار اسم آخر.");
        }
        public static void ShowEmailError()
        {
            ShowError("البريد الالكتروني خاطئ , يرجى التأكد منه والمحاولة مجددا");
        }
        public static void ShowDataGridViewEmpty()
        {
            ShowError("لايوجد بيانات , يرجى التأكد من الاتصال بالسيرفير والمحاولة مرة اخرى");
        }

        public static bool ShowDeleteDialog()
        {
            var result = MessageBox.Show("هل أنت متأكد من رغبتك في الحذف ؟", "حذف",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}