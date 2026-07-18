using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere.Home_Page.Loading;
using WorkSphere_Buisness;
using WorkSphere_Buisness.Global_Classes;

namespace WorkSphere.Roles
{
    public partial class ctrlRoles : UserControl, IRefreshable
    {

        private readonly frmMain _frmMain;
        private frmLoading _frmLoading;
        private List<clsRoles> _dataRoles;
        private int _currentPage = 1;
        private int _totalPages = 0;
        public ctrlRoles(frmMain main)
        {
            InitializeComponent();
            clsPermissionHelper.ApplyToControlRoot(this);
            _frmLoading = new frmLoading();
            _frmMain = main;
        }

        public async Task RefreshDataAsync()
        {
            await _LoadDataAsync();
        }
        private async void ctrlRoleManagement_Load(object sender, EventArgs e)
        {
            if (_dataRoles == null || _dataRoles.Count == 0)
                await _LoadDataAsync();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnAdd.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية إضافة دور");
                return;
            }
            using (var frm = new frmAddEditRoles())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlRoles>();
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnEdit.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تعديل دور");
                return;
            }
            if (dgvListPermissions.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvListPermissions.CurrentRow, "RoleID", out int id))
            {
                clsMessages.ShowError("تعذر قراءة صفوف الجدول بشكل صحيح");
                return;
            }

            using (var frm = new frmAddEditRoles(id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlRoles>();
                }
            }
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnDelete.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية حذف دور");
                return;
            }

            if (dgvListPermissions.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvListPermissions.CurrentRow, "RoleID", out int ID))
            {
                clsMessages.ShowError("تعذر قراءة المعرف من الصف المحدد");
                return;
            }

            try
            {
                // تحقق من الاتصال
                if (!await _CheckDatabaseConnectionAsync())
                    return;

                if (!clsMessages.ShowDeleteDialog())
                    return;

                bool deleted = await clsRoles.DeleteAsync(ID);
                if (deleted)
                {
                    clsUtil.AddNewSystemRecord("حذف", "حذف دور", $"({ID}) تم حذف دور تحمل الرقم التعريفي ");
                    clsMessages.ShowSuccess($"تم حذف السجل ({ID}) بنجاح");

                    clsPageManager.NotifyDataChanged<ctrlRoles>();
                }
                else
                {
                    clsMessages.ShowDataDeleteFaild();
                }
            }
            catch (Exception ex)
            {
                clsMessages.ShowError("حدث خطأ أثناء الحذف. راجع السجلات للمزيد من التفاصيل.");
            }
        }
        private async void btnExportAll_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnExportAll.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات الأدوار");
                return;
            }

            _frmLoading.Show();
            try
            {
                if (!await _CheckDatabaseConnectionAsync())
                {
                    return;
                }

                if (clsCurrentUser.User == null)
                    return;


                _dataRoles = await clsRoles.GetAllAsync();

                if (_dataRoles == null || _dataRoles.Count == 0)
                {
                    clsMessages.ShowError("لا توجد بيانات للتصدير");
                    return;
                }

                _ExportExcel(_dataRoles);
            }
            catch (Exception ex)
            {
                clsMessages.ShowError("حدث خطأ أثناء التصدير.");
            }
            finally
            {
                _frmLoading.Hide();
            }
        }
        private void btnExportSomData_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnExportSomeData.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات الأدوار");
                return;
            }

            var data = (List<clsRoles>)dgvListPermissions.DataSource;
            _ExportExcel(data);
        }
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            await _ApplyPaging(txtSearch.Text.Trim());
        }
        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            await _ApplyPaging(txtSearch.Text.Trim());
        }
        private async void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                _currentPage = 1;
                await _ApplyPaging(txtSearch.Text.Trim());
            }
        }
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            await _LoadDataAsync();
        }
        private async void cbNumberOfPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbNumberOfPages.SelectedIndex>=0)
            {
                _currentPage = cbNumberOfPages.SelectedIndex + 1;
                await _ApplyPaging(txtSearch.Text.Trim());
            }
        }
        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if(_currentPage>1)
            {
                _currentPage--;
                await _ApplyPaging(txtSearch.Text.Trim());
            }
        }
        private async void btnNext_Click(object sender, EventArgs e)
        {
            if(_currentPage<_totalPages)
            {
                _currentPage++;
                await _ApplyPaging(txtSearch.Text.Trim()); 
            }
        }

        // -------------- Methods -------------------
        private async Task _LoadDataAsync()
        {
            _frmLoading.Show();
            try
            {
                if (!await _CheckDatabaseConnectionAsync())
                {
                    _ShowServerErrorState();
                    return;
                }

                await _ApplyPaging(txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                clsMessages.ShowError("حدث خطأ أثناء تحميل البيانات. راجع السجلات.");
                _ShowServerErrorState();
            }
            finally
            {
                _frmLoading.Hide();
            }
        }
        private async Task _ApplyPaging(string searchText)
        {
            var result = await clsRoles.GetRolesPageAsync(_currentPage,Properties.Settings.Default.NumberOfGridViewItems,searchText);

            dgvListPermissions.DataSource = result.list;
            lblNumberOfItem.Text=result.TotalPages.ToString();

            if(_totalPages!= result.TotalPages)
            {
                _totalPages = result.TotalPages;
                cbNumberOfPages.Items.Clear();
                for (int i = 1; i <= _totalPages; i++)
                    cbNumberOfPages.Items.Add(i);
            }

            int newIndex = _currentPage - 1;
            if(cbNumberOfPages.SelectedIndex!=newIndex && newIndex>=0 && newIndex<cbNumberOfPages.Items.Count)
                cbNumberOfPages.SelectedIndex=newIndex;

            _SetColumns();
            _ShowEmptyDataState();
        }
        private void _SetColumns()
        {
            if (dgvListPermissions.Columns.Count == 0)
                return;


            _SetColumnHeaderIfExists("RoleID", "المعرف");
            _SetColumnHeaderIfExists("RoleName", "اسم الدور");
            _SetColumnHeaderIfExists("Description", "الوصف");
            _SetColumnHeaderIfExists("IsActive", "هل الدور نشط ؟");


        }
        private void _SetColumnHeaderIfExists(string columnName, string headerText)
        {
            if (dgvListPermissions.Columns.Contains(columnName))
                dgvListPermissions.Columns[columnName].HeaderText = headerText;
        }
        private void _ShowEmptyDataState()
        {
            lblStateTitle.Text = Properties.Resources.EmptyDataStateTitle;
            lblStateDescription.Text = Properties.Resources.EmptyDataStateDescription;
            panelState.Visible = clsUtil.IsDataGridViewEmpty(dgvListPermissions);
        }
        private void _ShowServerErrorState()
        {
            lblStateTitle.Text = Properties.Resources.ServerErrorTitle;
            lblStateDescription.Text = Properties.Resources.ServerErrorDescription;
            panelState.Visible = true;
        }
        private void _ExportExcel(List<clsRoles> data)
        {
            if (data == null || data.Count == 0)
            {
                clsMessages.ShowError("لا توجد بيانات للتصدير.");
                return;
            }

            var optimized = _CreatePreOptimizedStructure();
            _FillOptimizedData(optimized, data);
            clsExcelHelper.Export(_frmMain, optimized, "Roles");
        }
        private DataTable _CreatePreOptimizedStructure()
        {
            var dt = new DataTable();
            dt.Columns.Add("المعرف", typeof(int));
            dt.Columns.Add("اسم الدور", typeof(string));
            dt.Columns.Add("الوصف", typeof(string));
            dt.Columns.Add("هل الدور نشط ؟", typeof(bool));

            return dt;
        }
        private void _FillOptimizedData(DataTable target, List<clsRoles> list)
        {
            foreach (var d in list)
            {
                var nr = target.NewRow();

                nr["المعرف"] = d.RoleID;
                nr["اسم الدور"] = d.RoleName;
                nr["الوصف"] = d.Description;
                nr["هل الدور نشط ؟"] = d.IsActive;

                target.Rows.Add(nr);
            }
        }
        private static bool _TryGetCellIntValue(DataGridViewRow row, string columnName, out int value)
        {
            value = 0;
            try
            {
                if (row == null)
                    return false;

                if (!row.DataGridView.Columns.Contains(columnName))
                    return false;

                var cell = row.Cells[columnName];
                if (cell?.Value == null || cell.Value == DBNull.Value)
                    return false;

                value = Convert.ToInt32(cell.Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> _CheckDatabaseConnectionAsync()
        {
            return await clsUtil.CheckDatabaseConnection();
        }


    }
}
