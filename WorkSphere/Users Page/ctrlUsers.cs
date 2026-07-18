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

namespace WorkSphere.Users_Page
{
    public partial class ctrlUsers : UserControl,IRefreshable
    {
        private frmMain _frmMain;
        private frmLoading _frmLoading;
        private List<clsUsers> _dataUsers;
        private int _currentPage = 1;
        private int _totalPages = 0;
        public ctrlUsers(frmMain main)
        {
            InitializeComponent();
            clsPermissionHelper.ApplyToControlRoot(this);
            _frmLoading = new frmLoading();
            _frmMain = main;
            _dataUsers = new List<clsUsers>();
        }
        public async Task RefreshDataAsync()
        {
            await _LoadData();
        }
        private async void ctrlUsers_Load(object sender, EventArgs e)
        {
           await _LoadData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnAdd.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية إضافة مستخدم");
                return;
            }
            using (var frm = new frmAddEditUser())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlUsers>();
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnEdit.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تعديل مستخدم");
                return;
            }

            if (dgvListUsers.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvListUsers.CurrentRow, "UserID", out int userID))
            {
                clsMessages.ShowError("تعذر قراءة صفوف الجدول بشكل صحيح");
                return;
            }

            using (var frm = new frmAddEditUser(userID))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlUsers>();
                }
            }
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnDelete.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية حذف مستخدم");
                return;
            }

            if (dgvListUsers.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvListUsers.CurrentRow, "UserID", out int ID))
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

                bool deleted = clsUsers.Delete(ID);
                if (deleted)
                {
                    clsUtil.AddNewSystemRecord("حذف", "حذف مستخدم", $"({ID}) تم حذف مستخدم يحمل الرقم التعريفي ");
                    clsMessages.ShowSuccess($"تم حذف السجل ({ID}) بنجاح");

                    clsPageManager.NotifyDataChanged<ctrlUsers>();
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
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات المستخدمين");
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


                _dataUsers = clsUsers.GetAllUsers();

                if (_dataUsers == null || _dataUsers.Count == 0)
                {
                    clsMessages.ShowError("لا توجد بيانات للتصدير");
                    return;
                }

                _ExportExcel(_dataUsers);
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
        private void btnExportSomeData_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnExportSomeData.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات المستخدمين");
                return;
            }
            var data = (List<clsUsers>)dgvListUsers.DataSource;
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
            await _LoadData();
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
            if(_currentPage > 1)
            {
                _currentPage--;
                await _ApplyPaging(txtSearch.Text.Trim());
            }
        }
        private async void btnNext_Click(object sender, EventArgs e)
        {
            if(_currentPage < _totalPages)
            {
                _currentPage++;
                await _ApplyPaging(txtSearch.Text.Trim());
            }
        }


        // --------------------- Methods ---------------------

        private async Task _LoadData()
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
            catch (Exception)
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
            var result = await clsUsers.GetUsersPageAsync(_currentPage,
                  Properties.Settings.Default.NumberOfGridViewItems , searchText);

            dgvListUsers.DataSource = result.list;
            lblNumberOfItem.Text = result.TotalPages.ToString();

            if(_totalPages!= result.TotalPages)
            {
                _totalPages = result.TotalPages;
                cbNumberOfPages.Items.Clear();
                for(int i=1;i<=_totalPages;i++)
                    cbNumberOfPages.Items.Add(i);
            }

            int newIndex = _currentPage - 1;
            if(cbNumberOfPages.SelectedIndex != newIndex && newIndex>=0 && newIndex <=cbNumberOfPages.Items.Count-1)
                cbNumberOfPages.SelectedIndex = newIndex;

            _SetColumns();
            _ShowEmptyDataState();
        }
        private void _SetColumns()
        {
            if (dgvListUsers.Columns.Count == 0)
                return;

            _SetColumnHeaderIfExists("UserID", "المعرف");
            _SetColumnHeaderIfExists("UserName", "اسم المستخدم");
            _SetColumnHeaderIfExists("Password", "الرمز");
            _SetColumnHeaderIfExists("FullName", "الاسم الكامل");
            _SetColumnHeaderIfExists("Email", "البريد الالكتروني");
            _SetColumnHeaderIfExists("Phone", "الهاتف");
            _SetColumnHeaderIfExists("Address", "العنوان");
            _SetColumnHeaderIfExists("IsActive", "نشط ؟");
            _SetColumnHeaderIfExists("RoleName", "الصلاحية");
            _SetColumnHeaderIfExists("RoleID", "معرف الصلاحيات");
            _SetColumnHeaderIfExists("CreatedDate", "تاريخ الإنشاء");
            _SetColumnHeaderIfExists("EditDate", "تاريخ التعديل");

            dgvListUsers.Columns["Password"].Visible = false;
            dgvListUsers.Columns["RoleID"].Visible = false;
            dgvListUsers.Columns["CreatedDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvListUsers.Columns["EditDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        private void _SetColumnHeaderIfExists(string columnName, string headerText)
        {
            if (dgvListUsers.Columns.Contains(columnName))
                dgvListUsers.Columns[columnName].HeaderText = headerText;
        }
        private void _ShowEmptyDataState()
        {
            lblStateTitle.Text = Properties.Resources.EmptyDataStateTitle;
            lblStateDescription.Text = Properties.Resources.EmptyDataStateDescription;
            panelState.Visible = clsUtil.IsDataGridViewEmpty(dgvListUsers);
        }
        private void _ShowServerErrorState()
        {
            lblStateTitle.Text = Properties.Resources.ServerErrorTitle;
            lblStateDescription.Text = Properties.Resources.ServerErrorDescription;
            panelState.Visible = true;
        }
        private void _ExportExcel(List<clsUsers> list)
        {
            if (list == null || list.Count == 0)
            {
                clsMessages.ShowError("لا توجد بيانات للتصدير.");
                return;
            }

            var optimized = _CreatePreOptimizedStructure();
            _FillOptimizedData(optimized, list);
            clsExcelHelper.Export(_frmMain, optimized, "Users");
        }
        private DataTable _CreatePreOptimizedStructure()
        {
            var dt = new DataTable();
            dt.Columns.Add("المعرف", typeof(int));
            dt.Columns.Add("اسم المستخدم", typeof(string));
            dt.Columns.Add("الاسم الكامل", typeof(string));
            dt.Columns.Add("البريد الالكتروني", typeof(string));
            dt.Columns.Add("الهاتف", typeof(string));
            dt.Columns.Add("العنوان", typeof(string));
            dt.Columns.Add("نشط ؟", typeof(bool));
            dt.Columns.Add("تاريخ الإنشاء", typeof(string));
            dt.Columns.Add("تاريخ التعديل", typeof(string));

            return dt;
        }
        private void _FillOptimizedData(DataTable target, List<clsUsers> list)
        {
            foreach (var user in list)
            {
                var nr = target.NewRow();

                nr["المعرف"] = user.UserID;
                nr["اسم المستخدم"] = user.UserName;
                nr["الاسم الكامل"] = user.FullName;
                nr["البريد الالكتروني"] = user.Email;
                nr["الهاتف"] = user.Phone;
                nr["العنوان"] = user.Address;
                nr["نشط ؟"] = user.IsActive;
                nr["تاريخ الإنشاء"] = clsUtil.FormatDate(user.CreatedDate);
                nr["تاريخ التعديل"] = clsUtil.FormatDate(user.EditDate);

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
