using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere_Buisness;

namespace WorkSphere.Roles
{
    public partial class frmAddEditRoles : Form
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _roleID;
        private clsRoles _role;
        public frmAddEditRoles()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _role = new clsRoles();
        }
        public frmAddEditRoles(int roleID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _roleID = roleID;
        }

        private async void frmAddEditRoles_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                try
                {
                    await _LoadRoleData();
                }
                catch
                {
                    clsMessages.ShowError("حدث خطأ أثناء تحميل بيانات الدور");
                }
            }
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            _ClearAllErrors();
            _MapUIToRole();

            var validation = await _role.SaveAsync();

            if (!validation.IsValid)
            {
                _ApplyValidationErrors(validation);
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                _Mode = enMode.Update;
                clsMessages.ShowSuccess($"تم حفظ بيانات الدور بنجاح والرقم التعريفي هو ({_role.RoleID.ToString()})");
                clsUtil.AddNewSystemRecord("اضافة", "اضافة دور", $"({_role.RoleID.ToString()}) تم اضافة دور يحمل الرقم التعريفي");
            }
            else
            {
                clsMessages.ShowSuccess($"تم تعديل بيانات الدور بنجاح والرقم التعريفي هو ({_role.RoleID.ToString()})");
                clsUtil.AddNewSystemRecord("تعديل", "تعديل دور", $"({_role.RoleID.ToString()}) تم تعديل دور حالي يحمل الرقم التعريفي");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ------------------ Methods -----------------

        private void _MapUIToRole()
        {
            _role.RoleName = txtRoleName.Text.Trim();
            _role.Description = string.IsNullOrEmpty(txtDescription.Text.Trim()) ? null : txtDescription.Text.Trim();
            _role.IsActive = chkIsActive.Checked;
        }
        private async Task _LoadRoleData()
        {
            _role = await clsRoles.FindRoleByroleIDAsync(_roleID);
            if (_role == null)
            {
                clsMessages.ShowError($"لايوجد دور يحمل الرقم التعريفي {_roleID.ToString()}");
                this.Close();
            }

            txtRoleName.Text = _role.RoleName;
            txtDescription.Text = _role.Description ?? null;
            chkIsActive.Checked = _role.IsActive;
        }
        private void _ClearAllErrors()
        {
            errorProvider1.Clear();
        }
        private void _ApplyValidationErrors(RolesValidationResult validation)
        {
            if (validation == null)
                return;

            _ClearAllErrors();

            foreach (var error in validation.Errors)
            {
                switch (error.FieldName)
                {
                    case "RoleName":
                        errorProvider1.SetError(txtRoleName, error.Message);
                        break;
                    case "IsActive":
                        errorProvider1.SetError(txtRoleName, error.Message);
                        break;
                    case "Update":
                        clsMessages.ShowError(error.Message);
                        break;
                }

            }
        }
    }
}
