using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere_Buisness;

namespace WorkSphere.Users_Page
{
    public partial class frmChangePassword : Form
    {
        private int _userID;

        public frmChangePassword(int userID)
        {
            InitializeComponent();
            _userID = userID;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            if (_userID <= 0)
            {
                clsMessages.ShowError("! معرف المستخدم غير صالح");
                this.Close();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                return;
            }


            if (clsUsers.UpdatePassword(txtNewPassword.Text.Trim(), _userID))
            {
                clsUtil.AddNewSystemRecord("تعديل", "تعديل كلمة السر", $"{clsCurrentUser.User.UserID} من قبل المستخدم {_userID.ToString()} تم تعديل كلمة السر للمستخدم");
                clsMessages.ShowSuccess("تم تعديل كلمة السر بنجاح");
                this.Close();
            }
            else
            {
                clsMessages.ShowError("! فشل تعديل كلمة السر");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtOldPassword_Validating(object sender, CancelEventArgs e)
        {
            string storedPassword = clsUsers.GetPasswordByUserID(_userID);

            if (string.IsNullOrEmpty(txtOldPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtOldPassword, "! الرجاء إدخال كلمة السر القديمة");
                return;
            }

            if (!clsSecurity.Verify(txtOldPassword.Text.Trim(), storedPassword))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtOldPassword, "! كلمة السر غير صحيحة");
            }
            else
            {
                errorProvider1.SetError(txtOldPassword, null);
            }
        }
        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim();

            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "! الرجاء إدخال كلمة السر الجديدة");
                return;
            }

            if (newPassword.Length < 8 || !newPassword.Any(char.IsDigit) || !newPassword.Any(char.IsLetter))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "! كلمة السر يجب أن تكون 8 أحرف على الأقل وتحتوي على حرف و رقم");
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }
        }
        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "! الرجاء تأكيد كلمة السر");
                return;
            }

            if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "! كلمة السر غير متطابقة");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }
    }
}
