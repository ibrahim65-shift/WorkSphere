using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere_Buisness;
using WrokSphere_Shared;

namespace WorkSphere.PermissionsForRoles
{
    public partial class frmAddEditPermissionForRoles : Form
    {
        private enum enMode { AddNew = 0, Update = 1 }

        private enMode _Mode;
        private int _rolePermissionID;
        private clsRolePermissions _rolePermissions;
        private List<clsPermissions> _allPermissions = new List<clsPermissions>();
        private List<clsRolePermissions> _rolePermissionsForSelectedRole = new List<clsRolePermissions>();

        public frmAddEditPermissionForRoles()
        {
            InitializeComponent();
            lblTitle.Text = "إضافة صلاحيات";
            _Mode = enMode.AddNew;
            _rolePermissions = new clsRolePermissions();
        }
        public frmAddEditPermissionForRoles(int rolePermID)
        {
            InitializeComponent();
            lblTitle.Text = "تعديل صلاحيات";
            _Mode = enMode.Update;
            _rolePermissionID = rolePermID;
        }

        private async void frmAddEditPermissionForRoles_Load(object sender, EventArgs e)
        {
            await _FillRolesComboBox();
            await _FillPermissionsDataGridView();

            if (cbRoles.SelectedValue != null && int.TryParse(cbRoles.SelectedValue.ToString(), out int roleID))
                await _DeterminePermissionsForRole(roleID);

            if (_Mode == enMode.Update)
                await _LoadRolePermissionsData();
        }
        private async void cbRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRoles.SelectedValue != null && int.TryParse(cbRoles.SelectedValue.ToString(), out int roleID))
                await _DeterminePermissionsForRole(roleID);
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _ClearAllErrors();
                var selectedPermissions = _MapUIToRolePermissions();

                if (selectedPermissions == null)
                    return;

                if (selectedPermissions.Count == 0)
                {
                    clsMessages.ShowError("يجب تحديد صلاحية واحدة على الأقل.");
                    return;
                }

                // حفظ الصلاحيات دفعة واحدة
                var validation = await _rolePermissions.SaveRolePermissionsBulkAsync(_rolePermissions.RoleID, selectedPermissions);

                if (!validation.IsValid)
                {
                    _ApplyValidationErrors(validation);
                    return;
                }

                if (_Mode == enMode.AddNew)
                {
                    _Mode = enMode.Update;
                    clsMessages.ShowSuccess($"تم حفظ الصلاحيات للدور {_rolePermissions.RoleID.ToString()}  بنجاح");
                    clsUtil.AddNewSystemRecord("اضافة", "اضافة صلاحية", $"({_rolePermissions.RolePermissionID.ToString()}) تم اضافة صلاحية للدور");
                }
                else
                {
                    clsMessages.ShowSuccess($"تم تعديل الصلاحيات للدور {_rolePermissions.RoleID.ToString()}  بنجاح");
                    clsUtil.AddNewSystemRecord("تعديل", "تعديل صلاحية", $"({_rolePermissions.RolePermissionID.ToString()}) تم تعديل صلاحية للدور");
                }


                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                clsMessages.ShowError("حدث خطأ أثناء الحفظ: " + ex.Message);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e) => _FilterData(txtSearch.Text.Trim());
        private void txtSearch_TextChanged(object sender, EventArgs e) => _FilterData(txtSearch.Text.Trim());
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                _FilterData(txtSearch.Text.Trim());
            }
        }
        // --------------------------- METHODS ------------------------------

        private async Task _FillRolesComboBox()
        {
            try
            {
                var data = await clsRoles.GetAllAsync();

                cbRoles.DataSource = data;
                cbRoles.DisplayMember = "Description";
                cbRoles.ValueMember = "RoleID";

                if (data.Count > 0)
                    cbRoles.SelectedIndex = 0;
            }
            catch
            {
                clsMessages.ShowError("حدث خطأ أثناء تحميل بيانات الأدوار");
            }
        }
        private async Task _FillPermissionsDataGridView()
        {
            try
            {
                _allPermissions = await clsPermissions.GetAllAsync();
                dataGridView1.Rows.Clear();

                foreach (var perm in _allPermissions)
                    dataGridView1.Rows.Add(perm.PermissionID, perm.PermissionName, false, false);
            }
            catch
            {
                clsMessages.ShowError("حدث خطأ أثناء تحميل بيانات الصلاحيات");
            }
        }
        private async Task _DeterminePermissionsForRole(int roleID)
        {
            try
            {
                _rolePermissionsForSelectedRole = await clsRolePermissions.GetListRolePermissionsByRoleIDAsync(roleID);
            }
            catch
            {
                clsMessages.ShowError("حدث خطأ أثناء تحميل الصلاحيات للدور");
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["IsChecked"].Value = false;
                row.Cells["IsAllowed"].Value = false;
            }

            foreach (var rolePerm in _rolePermissionsForSelectedRole)
            {
                var row = dataGridView1.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r => Convert.ToInt32(r.Cells["PermissionID"].Value) == rolePerm.PermissionID);

                if (row != null)
                {
                    row.Cells["IsChecked"].Value = true;
                    row.Cells["IsAllowed"].Value = rolePerm.IsAllowed;
                }
            }
        }
        private List<clsRolePermissionItem> _MapUIToRolePermissions()
        {
            if (cbRoles.SelectedValue != null && int.TryParse(cbRoles.SelectedValue.ToString(), out int roleID))
            {
                _rolePermissions.RoleID = roleID;
            }

            if (_rolePermissions.RoleID <= 0)
            {
                clsMessages.ShowError("الرجاء اختيار الدور أولاً");
                return null;
            }

            dataGridView1.EndEdit();

            var permissionsList = new List<clsRolePermissionItem>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["IsChecked"].Value);
                if (!isChecked)
                    continue;

                bool isAllowed = Convert.ToBoolean(row.Cells["IsAllowed"].Value);
                int permID = Convert.ToInt32(row.Cells["PermissionID"].Value);

                permissionsList.Add(new clsRolePermissionItem(permID, isAllowed));
            }


            return permissionsList;
        }
        private void _ClearAllErrors() => errorProvider1.Clear();
        private void _ApplyValidationErrors(RolePermissionsValidationResult validation)
        {
            if (validation?.Errors == null)
                return;

            foreach (var error in validation.Errors)
                clsMessages.ShowError(error.Message);
        }
        private async Task _LoadRolePermissionsData()
        {
            _rolePermissions = await clsRolePermissions.FindRolePermissionsByRolePermIDAsync(_rolePermissionID);
            if (_rolePermissions == null)
            {
                clsMessages.ShowError("تعذر تحميل بيانات الصلاحية المحددة");
                this.Close();
                return;
            }

            if (_rolePermissions.RoleID > 0 && cbRoles.DataSource != null)
                cbRoles.SelectedValue = _rolePermissions.RoleID;

            await _DeterminePermissionsForRole(_rolePermissions.RoleID);

            dataGridView1.Columns["IsChecked"].ReadOnly = true;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["IsChecked"].Value) == false)
                {
                    row.Cells["IsAllowed"].ReadOnly = true;
                }
            }
        }
        private void _FilterData(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Visible = true;
                }
                return;
            }

            searchText = searchText.ToLower().ToLowerInvariant();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Visible = row.Cells["PermissionName"].Value != null && row.Cells["PermissionName"].Value.ToString().ToLower().Contains(searchText);
            }
        }

    }
}