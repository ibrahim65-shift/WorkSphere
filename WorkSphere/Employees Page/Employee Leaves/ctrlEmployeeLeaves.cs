using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WorkSphere.Global_Classes;
using WorkSphere.Home_Page.Loading;
using WorkSphere_Buisness;
using WorkSphere_Buisness.Global_Classes;

namespace WorkSphere.Leave_Types
{
    public partial class ctrlEmployeeLeaves : UserControl, IRefreshable
    {
        private frmLoading _frmLoading;
        private DataTable _dataEmployeeLeaves;
        private frmMain _frmMain;
        private int _currentPage = 1;
        private int _totalPages = 0;
        public ctrlEmployeeLeaves(frmMain frmMain)
        {
            InitializeComponent();
            clsPermissionHelper.ApplyToControlRoot(this);
            _frmLoading = new frmLoading();
            _frmMain = frmMain;
        }

        public async Task RefreshDataAsync()
        {
            await _LoadDataAsync();
        }
        private async void ctrlEmployeeLeaves_Load(object sender, EventArgs e)
        {
            await _LoadDataAsync();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnAdd.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية إضافة إجازة");
                return;
            }

            using (var frm = new frmAddEditEmployeeLeaves())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlEmployeeLeaves>();
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnEdit.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تعديل إجازة");
                return;
            }

            if (dgvEmployeeLeaves.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvEmployeeLeaves.CurrentRow, "LeaveID", out int leaveID) ||
                !_TryGetCellIntValue(dgvEmployeeLeaves.CurrentRow, "EmployeeID", out int empID))
            {
                clsMessages.ShowError("تعذر قراءة صفوف الجدول بشكل صحيح");
                return;
            }

            using (var frm = new frmAddEditEmployeeLeaves(leaveID, empID))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlEmployeeLeaves>();
                }
            }
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnDelete.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية حذف إجازة");
                return;
            }

            if (dgvEmployeeLeaves == null)
                return;

            if (!_TryGetCellIntValue(dgvEmployeeLeaves.CurrentRow, "LeaveID", out int ID))
            {
                clsMessages.ShowError("تعذر قراءة المعرف من الصف المحدد");
                return;
            }

            try
            {
                if (!await _CheckDatabaseConnectionAsync())
                    return;

                if (!clsMessages.ShowDeleteDialog())
                    return;

                bool deleted = await clsEmployeeAttendance.DeleteAsync(ID);
                if (deleted)
                {
                    clsUtil.AddNewSystemRecord("حذف", "حذف إجازة", $"({ID}) تم حذف إجازة تحمل الرقم التعريفي ");
                    clsMessages.ShowSuccess($"تم حذف السجل ({ID}) بنجاح");

                    clsPageManager.NotifyDataChanged<ctrlEmployeeLeaves>();
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
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات الإجازات");
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


                _dataEmployeeLeaves = await clsEmployeeLeaves.GetAllEmployeeLeavesAsync();

                if (_dataEmployeeLeaves == null || _dataEmployeeLeaves.Rows.Count == 0)
                {
                    clsMessages.ShowError("لا توجد بيانات للتصدير");
                    return;
                }

                _ExportExcel(_dataEmployeeLeaves);
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
            if (!clsAuthorizationCache.HasPermission(btnExportSomData.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات الإجازات");
                return;
            }
            var data = dgvEmployeeLeaves.DataSource as DataTable;
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
            if (e.KeyChar == (char)Key.Enter)
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
            if(_currentPage < _totalPages)
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
            var result = await clsEmployeeLeaves.GetEmployeeLeavesInfoPageAsync(_currentPage,
                Properties.Settings.Default.NumberOfGridViewItems, searchText);

            dgvEmployeeLeaves.DataSource = result.dt;
            lblNumberOfItems.Text = result.TotalPage.ToString();

            if (_totalPages != result.TotalPage)
            {
                _totalPages = result.TotalPage;
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
            if (dgvEmployeeLeaves.Columns.Count == 0)
                return;


            _SetColumnHeaderIfExists("LeaveID", "المعرف");
            _SetColumnHeaderIfExists("NationalID", "الهوية");
            _SetColumnHeaderIfExists("FullName", "الاسم الكامل");
            _SetColumnHeaderIfExists("Email", "البريد الالكتروني");
            _SetColumnHeaderIfExists("Phone", "الهاتف");
            _SetColumnHeaderIfExists("Address", "العنوان");
            _SetColumnHeaderIfExists("Description", "الوظيفة");
            _SetColumnHeaderIfExists("DepartmentName", "القسم");
            _SetColumnHeaderIfExists("Name", "نوع الإجازة");
            _SetColumnHeaderIfExists("StartDate", "بداية الإجازة");
            _SetColumnHeaderIfExists("EndDate", "نهاية الإجازة");
            _SetColumnHeaderIfExists("TotalDays", "المجموع");
            _SetColumnHeaderIfExists("Reason", "السبب");
            _SetColumnHeaderIfExists("Status", "الحالة");
            _SetColumnHeaderIfExists("ApprovedByUserID", "المعتمد");
            _SetColumnHeaderIfExists("CreatedDate", "تاريخ الانشاء");
            _SetColumnHeaderIfExists("CreatedByUserID", "المنشئ");
            _SetColumnHeaderIfExists("EditedDate", "تاريخ التعديل");
            _SetColumnHeaderIfExists("EditedByUserID", "المعدل");
            _SetColumnHeaderIfExists("EmployeeID", "معرف الموظف");

            dgvEmployeeLeaves.Columns["EmployeeID"].Visible = false;

            dgvEmployeeLeaves.Columns["StartDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvEmployeeLeaves.Columns["EndDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvEmployeeLeaves.Columns["CreatedDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvEmployeeLeaves.Columns["EditedDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        private void _SetColumnHeaderIfExists(string columnName, string headerText)
        {
            if (dgvEmployeeLeaves.Columns.Contains(columnName))
                dgvEmployeeLeaves.Columns[columnName].HeaderText = headerText;
        }
        private void _ShowEmptyDataState()
        {
            lblStateTitle.Text = Properties.Resources.EmptyDataStateTitle;
            lblStateDescription.Text = Properties.Resources.EmptyDataStateDescription;
            panelState.Visible = clsUtil.IsDataGridViewEmpty(dgvEmployeeLeaves);
        }
        private void _ShowServerErrorState()
        {
            lblStateTitle.Text = Properties.Resources.ServerErrorTitle;
            lblStateDescription.Text = Properties.Resources.ServerErrorDescription;
            panelState.Visible = true;
        }
        private void _ExportExcel(DataTable data)
        {
            if (data == null || data.Rows.Count == 0)
            {
                clsMessages.ShowError("لا توجد بيانات للتصدير.");
                return;
            }

            var optimized = _CreatePreOptimizedStructure();
            _FillOptimizedData(optimized, data);
            clsExcelHelper.Export(_frmMain, optimized, "Employee Leaves");
        }
        private DataTable _CreatePreOptimizedStructure()
        {
            var dt = new DataTable();
            dt.Columns.Add("المعرف", typeof(int));
            dt.Columns.Add("الهوية", typeof(string));
            dt.Columns.Add("الاسم الكامل", typeof(string));
            dt.Columns.Add("البريد الالكتروني", typeof(string));
            dt.Columns.Add("الهاتف", typeof(string));
            dt.Columns.Add("العنوان", typeof(string));
            dt.Columns.Add("الوظيفة", typeof(string));
            dt.Columns.Add("القسم", typeof(string));
            dt.Columns.Add("نوع الإجازة", typeof(string)); // نكتب كنص بعد التنسيق
            dt.Columns.Add("بداية الإجازة", typeof(string));
            dt.Columns.Add("نهاية الإجازة", typeof(string));
            dt.Columns.Add("المجموع", typeof(decimal));
            dt.Columns.Add("السبب", typeof(string));
            dt.Columns.Add("الحالة", typeof(string));
            dt.Columns.Add("المعتمد", typeof(int));
            dt.Columns.Add("تاريخ الانشاء", typeof(string));
            dt.Columns.Add("المنشئ", typeof(int));
            dt.Columns.Add("تاريخ التعديل", typeof(string));
            dt.Columns.Add("المعدل", typeof(int));
            dt.Columns.Add("معرف الموظف", typeof(int));
            return dt;
        }
        private void _FillOptimizedData(DataTable target, DataTable source)
        {
            foreach (DataRow row in source.Rows)
            {
                var nr = target.NewRow();

                nr["المعرف"] = row["LeaveID"];
                nr["الهوية"] = row["NationalID"];
                nr["الاسم الكامل"] = row["FullName"];
                nr["البريد الالكتروني"] = row["Email"] == DBNull.Value ? "" : row["Email"].ToString();
                nr["الهاتف"] = row["Phone"] == DBNull.Value ? "" : row["Phone"].ToString();
                nr["العنوان"] = row["Address"] == DBNull.Value ? "" : row["Address"].ToString();
                nr["الوظيفة"] = row["Description"];
                nr["القسم"] = row["DepartmentName"];
                nr["نوع الإجازة"] = row["Name"];
                nr["بداية الإجازة"] = clsUtil.FormatDate(row["StartDate"]);
                nr["نهاية الإجازة"] = clsUtil.FormatDate(row["EndDate"]);
                nr["المجموع"] = row["TotalDays"];
                nr["السبب"] = row["Reason"];
                nr["الحالة"] = row["Status"];
                nr["المعتمد"] = row["ApprovedByUserID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ApprovedByUserID"]);
                nr["تاريخ الانشاء"] = clsUtil.FormatDate(row["CreatedDate"]);
                nr["المنشئ"] = row["CreatedByUserID"];
                nr["تاريخ التعديل"] = row["EditedDate"] == DBNull.Value ? "" : clsUtil.FormatDate(row["EditedDate"]);
                nr["المعدل"] = row["EditedByUserID"];
                nr["معرف الموظف"] = row["EmployeeID"];

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
