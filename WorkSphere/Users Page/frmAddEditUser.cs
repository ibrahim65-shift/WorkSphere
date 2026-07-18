using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere_Buisness;

namespace WorkSphere.Users_Page
{
    public partial class frmAddEditUser : Form
    {
        enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _userID;
        private clsUsers _User;

        public frmAddEditUser()
        {
            InitializeComponent();
            lblTitle.Text = "إضافة مستخدم";
            _Mode = enMode.AddNew;
            _User = new clsUsers();
        }

        public frmAddEditUser(int id)
        {
            InitializeComponent();
            lblTitle.Text = "تعديل مستخدم";
            _Mode = enMode.Update;
            _userID = id;
        }

        private async void frmAddEditUser_Load(object sender, EventArgs e)
        {
            await _FillComboBoxRoles();

            if (_Mode == enMode.Update)
            {
                _LoadUserData();
                txtPassword.Clear();
                txtPassword.Enabled = false;
                btnChangePassword.Visible = true;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                return;
            }

            _ClearAllErrors();
            _MapUIToUser();

            var validation = _User.Save();

            if (!validation.IsValid)
            {
                _ApplyValidationErrors(validation);
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                _Mode = enMode.Update;
                clsMessages.ShowSuccess($"تم حفظ بيانات المستخدم بنجاح والرقم التعريفي هو ({_User.UserID.ToString()})");
                clsUtil.AddNewSystemRecord("اضافة", "اضافة مستخدم", $"({_User.UserID.ToString()}) تم اضافة مستخدم يحمل الرقم التعريفي");
            }
            else
            {
                clsMessages.ShowSuccess($"تم تعديل بيانات المستخدم بنجاح والرقم التعريفي هو ({_User.UserID.ToString()})");
                clsUtil.AddNewSystemRecord("تعديل", "تعديل مستخدم", $"({_User.UserID.ToString()}) تم تعديل مستخدم حالي يحمل الرقم التعريفي");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                if (!clsUtil.IsValidEmail(txtEmail.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtEmail, "! الرجاء إدخال بريد الالكتروني صالح");
                }
                else
                {
                    errorProvider1.SetError(txtEmail, null);
                }
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }
        private void txtPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string password = txtPassword.Text.Trim();

            if (!string.IsNullOrEmpty(password) &&
                (password.Length < 8 || !password.Any(char.IsDigit) || !password.Any(char.IsLetter)))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "! كلمة السر يجب أن تكون 8 أحرف على الأقل وتحتوي على حرف و رقم");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            using (var frm = new frmChangePassword(_User.UserID))
            {
                frm.ShowDialog();
            }
        }

        // -------------------- Methods --------------------------

        private void _MapUIToUser()
        {

            _User.FullName = txtFullName.Text.Trim();
            _User.UserName = txtUserName.Text.Trim();
            _User.Phone = numericTextBoxPhone.Text.Trim();
            _User.Email = txtEmail.Text.Trim() ?? null;
            _User.Address = txtAddress.Text.Trim() ?? null;
            _User.IsActive = chkIsActive.Checked;
            if (cbRoles.SelectedValue != null && int.TryParse(cbRoles.SelectedValue.ToString(), out int roleID))
            {
                _User.RoleID = roleID;
            }

            if (_Mode == enMode.AddNew)
            {
                if (!string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    string salt = clsSecurity.GenerateSalt();
                    string hash = clsSecurity.HashPassword(txtPassword.Text.Trim(), salt);
                    _User.Password = clsSecurity.Pack(salt, hash);
                }

                _User.CreatedDate = DateTime.Now;
            }
            else
            {
                _User.EditDate = DateTime.Now;
            }

        }
        private void _LoadUserData()
        {
            _User = clsUsers.FindUserByID(_userID);
            if (_User == null)
            {
                clsMessages.ShowError($"المستخدم الذي يحمل الرقم التعريفي({_userID.ToString()}) غير موجود");
                this.Close();
                return;
            }

            txtFullName.Text = _User.FullName;
            txtUserName.Text = _User.UserName;
            txtEmail.Text = _User.Email ?? null;
            txtAddress.Text = _User.Address ?? null;
            numericTextBoxPhone.Text = _User.Phone;
            cbRoles.SelectedValue = _User.RoleID;
            chkIsActive.Checked = _User.IsActive;
        }
        private async Task _FillComboBoxRoles()
        {
            var data = new List<clsRoles>();
            try
            {
                data = await clsRoles.GetAllAsync();
            }
            catch
            {
                clsMessages.ShowError("حدث خطأ أثناء تحميل بيانات الصلاحيات");
            }

            if (data == null)
                return;

            cbRoles.DataSource = data;
            cbRoles.DisplayMember = "Description";
            cbRoles.ValueMember = "RoleID";

            if (cbRoles.Items.Count > 0)
                cbRoles.SelectedIndex = 0;
        }
        private void _ClearAllErrors()
        {
            errorProvider1.Clear();
        }
        private void _ApplyValidationErrors(UserValidationResult validation)
        {
            if (validation == null)
                return;

            _ClearAllErrors();

            foreach (var error in validation.Errors)
            {
                switch (error.FieldName)
                {
                    case "UserName":
                        errorProvider1.SetError(txtUserName, error.Message);
                        break;
                    case "Password":
                        errorProvider1.SetError(txtPassword, error.Message);
                        break;
                    case "FullName":
                        errorProvider1.SetError(txtFullName, error.Message);
                        break;
                    case "Phone":
                        errorProvider1.SetError(numericTextBoxPhone, error.Message);
                        break;
                    case "Update":
                        clsMessages.ShowError(error.Message);
                        break;

                }

            }
        }


    }
}