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

namespace WorkSphere.System_Records_Page
{
    public partial class ctrlSystemRecords : UserControl,IRefreshable
    {
        private frmMain _frmMain;
        private frmLoading _frmLoading;
        private List<clsSystemRecords> _dataSystemRecords;
        private int _currentPage = 1;
        private int _totalPages = 0;
        public ctrlSystemRecords(frmMain main)
        {
            InitializeComponent();
            clsPermissionHelper.ApplyToControlRoot(this);
            _frmLoading = new frmLoading();
            _frmMain = main;
            _dataSystemRecords = new List<clsSystemRecords>();
        }

        public async Task RefreshDataAsync()
        {
            await _LoadData();
        }
        private async void ctrlSystemRecords_Load(object sender, EventArgs e)
        {
            await _LoadData();
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnDelete.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية حذف سجل حركة");
                return;
            }

            if (dgvListSystemRecords.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvListSystemRecords.CurrentRow, "ID", out int ID))
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

                bool deleted = clsSystemRecords.Delete(ID);
                if (deleted)
                {
                    clsUtil.AddNewSystemRecord("حذف", "حذف سجل", $"({ID}) تم حذف سجل يحمل الرقم التعريفي ");
                    clsMessages.ShowSuccess($"تم حذف السجل ({ID}) بنجاح");

                    clsPageManager.NotifyDataChanged<ctrlSystemRecords>();
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
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات سجل الحركة");
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


                _dataSystemRecords = clsSystemRecords.GetAllSystemRecords();

                if (_dataSystemRecords == null || _dataSystemRecords.Count == 0)
                {
                    clsMessages.ShowError("لا توجد بيانات للتصدير");
                    return;
                }

                _ExportExcel(_dataSystemRecords);
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
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات سجل الحركة");
                return;
            }
            var data = (List<clsSystemRecords>)dgvListSystemRecords.DataSource;
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
        private async Task _ApplyPaging(string searchtext)
        {
            var result = await clsSystemRecords.GetSystemRecordsPageAsync(_currentPage,
                Properties.Settings.Default.NumberOfGridViewItems, searchtext);

            dgvListSystemRecords.DataSource = result.list;
            lblNumberOfItem.Text = result.TotalPages.ToString();

            if(_totalPages!= result.TotalPages)
            {
                _totalPages = result.TotalPages;
                cbNumberOfPages.Items.Clear();
                for (int i = 1; i <= _totalPages; i++)
                    cbNumberOfPages.Items.Add(i);
            }

            int newIndex = _currentPage - 1;
            if (cbNumberOfPages.SelectedIndex != newIndex && newIndex>=0 && newIndex < cbNumberOfPages.Items.Count)
                cbNumberOfPages.SelectedIndex = newIndex;

            _SetColumns();
            _ShowEmptyDataState();
        }
        private void _SetColumns()
        {
            if (dgvListSystemRecords.Columns.Count == 0)
                return;

            _SetColumnHeaderIfExists("ID", "المعرف");
            _SetColumnHeaderIfExists("ActionType", "نوع الحركة");
            _SetColumnHeaderIfExists("DeviceName", "الجهاز");
            _SetColumnHeaderIfExists("MachinID", "معرف الجهاز");
            _SetColumnHeaderIfExists("Title", "العنوان");
            _SetColumnHeaderIfExists("Description", "الوصف");
            _SetColumnHeaderIfExists("CreatedDate", "تاريخ الإنشاء");
            _SetColumnHeaderIfExists("UserID", "المنشئ");

            dgvListSystemRecords.Columns["User"].Visible = false;
            dgvListSystemRecords.Columns["CreatedDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        private void _SetColumnHeaderIfExists(string columnName, string headerText)
        {
            if (dgvListSystemRecords.Columns.Contains(columnName))
                dgvListSystemRecords.Columns[columnName].HeaderText = headerText;
        }
        private void _ShowEmptyDataState()
        {
            lblStateTitle.Text = Properties.Resources.EmptyDataStateTitle;
            lblStateDescription.Text = Properties.Resources.EmptyDataStateDescription;
            panelState.Visible = clsUtil.IsDataGridViewEmpty(dgvListSystemRecords);
        }
        private void _ShowServerErrorState()
        {
            lblStateTitle.Text = Properties.Resources.ServerErrorTitle;
            lblStateDescription.Text = Properties.Resources.ServerErrorDescription;
            panelState.Visible = true;
        }
        private void _ExportExcel(List<clsSystemRecords> list)
        {
            if (list == null || list.Count == 0)
            {
                clsMessages.ShowError("لا توجد بيانات للتصدير.");
                return;
            }

            var optimized = _CreatePreOptimizedStructure();
            _FillOptimizedData(optimized, list);
            clsExcelHelper.Export(_frmMain, optimized, "System Records");
        }
        private DataTable _CreatePreOptimizedStructure()
        {
            var dt = new DataTable();
            dt.Columns.Add("المعرف", typeof(int));
            dt.Columns.Add("نوع الحركة", typeof(string));
            dt.Columns.Add("الجهاز", typeof(string));
            dt.Columns.Add("معرف الجهاز", typeof(string));
            dt.Columns.Add("العنوان", typeof(string));
            dt.Columns.Add("الوصف", typeof(string));
            dt.Columns.Add("تاريخ الإنشاء", typeof(string));
            dt.Columns.Add("المنشئ", typeof(int));

            return dt;
        }
        private void _FillOptimizedData(DataTable target, List<clsSystemRecords> list)
        {
            foreach (var rec in list)
            {
                var nr = target.NewRow();

                nr["المعرف"] = rec.ID;
                nr["نوع الحركة"] = rec.ActionType;
                nr["الجهاز"] = rec.DeviceName;
                nr["معرف الجهاز"] = rec.MachinID;
                nr["العنوان"] = rec.Title;
                nr["الوصف"] = rec.Description;
                nr["تاريخ الإنشاء"] = clsUtil.FormatDate(rec.CreatedDate);
                nr["المنشئ"] = rec.UserID;

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
