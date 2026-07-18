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

namespace WorkSphere.Leave_Types
{
    public partial class ctrlLeaveTypes : UserControl, IRefreshable
    {
        private static frmMain _frmMain;
        private frmLoading _frmLoading;
        private List<clsLeaveTypes> _dataLeaveTypes;
        private int _currentPage = 1;
        private int _totalPages = 0;
        public ctrlLeaveTypes(frmMain main)
        {
            InitializeComponent();
            clsPermissionHelper.ApplyToControlRoot(this);
            _frmLoading = new frmLoading();
            _dataLeaveTypes = new List<clsLeaveTypes>();
            _frmMain = main;
        }
        public async Task RefreshDataAsync()
        {
            await _LoadDataAsync();
        }
        private async void ctrlLeaveTypes_Load(object sender, EventArgs e)
        {

            await _LoadDataAsync();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnAdd.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية إضافة نوع إجازة");
                return;
            }

            using (var frm = new frmAddEditLeaveTypes())
            {

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlLeaveTypes>();
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnEdit.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تعديل نوع إجازة");
                return;
            }

            if (dgvLeaveTypes.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvLeaveTypes.CurrentRow, "LeaveTypeID", out int leaveTypeID))
            {
                clsMessages.ShowError("تعذر قراءة صفوف الجدول بشكل صحيح");
                return;
            }

            using (var frm = new frmAddEditLeaveTypes(leaveTypeID))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlLeaveTypes>();
                }
            }
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnDelete.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية حذف نوع إجازة");
                return;
            }

            if (dgvLeaveTypes.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvLeaveTypes.CurrentRow, "LeaveTypeID", out int ID))
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

                bool deleted = await clsLeaveTypes.DeleteAsync(ID);
                if (deleted)
                {
                    clsUtil.AddNewSystemRecord("حذف", "حذف نوع إجازة", $"({ID}) تم حذف نوع إجازة تحمل الرقم التعريفي ");
                    clsMessages.ShowSuccess($"تم حذف السجل ({ID}) بنجاح");

                    clsPageManager.NotifyDataChanged<ctrlLeaveTypes>();
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
                clsMessages.ShowWarning("ليس لديك صلاحية بيانات أنواع الإجازات");
                return;
            }

            _frmLoading.Show();
            try
            {
                if (!await _CheckDatabaseConnectionAsync())
                {
                    clsMessages.ShowServerError();
                    return;
                }

                if (clsCurrentUser.User == null)
                    return;


                _dataLeaveTypes = await clsLeaveTypes.GetAllLeaveTypesAsync();

                if (_dataLeaveTypes == null || _dataLeaveTypes.Count == 0)
                {
                    clsMessages.ShowError("لا توجد بيانات للتصدير");
                    return;
                }

                _ExportExcel(_dataLeaveTypes);
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
            if (!clsAuthorizationCache.HasPermission(btnExportSomData.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية بيانات أنواع الإجازات");
                return;
            }
            var data = (List<clsLeaveTypes>)dgvLeaveTypes.DataSource;
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
            if(_currentPage > 1)
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


        // --------------------- Methods ---------------------

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
            var result = await clsLeaveTypes.GetLeaveTypesPageAsync(_currentPage,Properties.Settings.Default.NumberOfGridViewItems,
                searchText);

            dgvLeaveTypes.DataSource = result.list;
            lblNumberOfItems.Text = result.TotalPages.ToString();

            if(_totalPages!=result.TotalPages)
            {
                _totalPages = result.TotalPages;
                cbNumberOfPages.Items.Clear();
                for (int i = 1; i <= _totalPages; i++)
                    cbNumberOfPages.Items.Add(i);
            }

            int newIndex = _currentPage - 1;
            if (cbNumberOfPages.SelectedIndex != newIndex && newIndex >= 0 && newIndex < cbNumberOfPages.Items.Count)
                cbNumberOfPages.SelectedIndex = newIndex;

            _SetColumns();
            _ShowEmptyDataState();
        }
        private void _SetColumns()
        {
            if (dgvLeaveTypes.Columns.Count == 0)
                return;

            _SetColumnHeaderIfExists("LeaveTypeID", "المعرف");
            _SetColumnHeaderIfExists("Name", "نوع الإجازة");
            _SetColumnHeaderIfExists("Description", "الوصف");
            _SetColumnHeaderIfExists("MaxDaysPerYear", "الحد الأقصى للأيام في السنة");
            _SetColumnHeaderIfExists("IsPaid", "مدفوعة ؟");
            _SetColumnHeaderIfExists("CreatedDate", "تاريخ الإنشاء");
            _SetColumnHeaderIfExists("CreatedByUserID", "المنشئ");

            dgvLeaveTypes.Columns["CreatedDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        private void _SetColumnHeaderIfExists(string columnName, string headerText)
        {
            if (dgvLeaveTypes.Columns.Contains(columnName))
                dgvLeaveTypes.Columns[columnName].HeaderText = headerText;
        }
        private void _ShowEmptyDataState()
        {
            lblStateTitle.Text = Properties.Resources.EmptyDataStateTitle;
            lblStateDescription.Text = Properties.Resources.EmptyDataStateDescription;
            panelState.Visible = clsUtil.IsDataGridViewEmpty(dgvLeaveTypes);
        }
        private void _ShowServerErrorState()
        {
            lblStateTitle.Text = Properties.Resources.ServerErrorTitle;
            lblStateDescription.Text = Properties.Resources.ServerErrorDescription;
            panelState.Visible = true;
        }
        private void _ExportExcel(List<clsLeaveTypes> list)
        {
            if (list == null || list.Count == 0)
            {
                clsMessages.ShowError("لا توجد بيانات للتصدير.");
                return;
            }

            var optimized = _CreatePreOptimizedStructure();
            _FillOptimizedData(optimized, list);
            clsExcelHelper.Export(_frmMain, optimized, "Leave Types");
        }
        private DataTable _CreatePreOptimizedStructure()
        {
            var dt = new DataTable();
            dt.Columns.Add("LeaveTypeID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("MaxDaysPerYear", typeof(int));
            dt.Columns.Add("IsPaid", typeof(bool));
            dt.Columns.Add("CreatedDate", typeof(string));
            dt.Columns.Add("CreatedByUserID", typeof(int));

            return dt;
        }
        private void _FillOptimizedData(DataTable target, List<clsLeaveTypes> list)
        {
            foreach (var leaveType in list)
            {
                var nr = target.NewRow();

                nr["LeaveTypeID"] = leaveType.LeaveTypeID;
                nr["Name"] = leaveType.Name;
                nr["Description"] = leaveType.Description;
                nr["MaxDaysPerYear"] = leaveType.MaxDaysPerYear ?? 0;
                nr["IsPaid"] = leaveType.IsPaid;
                nr["CreatedDate"] = clsUtil.FormatDate(leaveType.CreatedDate);
                nr["CreatedByUserID"] = leaveType.CreatedByUserID;

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
