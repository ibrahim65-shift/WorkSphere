using System;
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
    public partial class ctrlEmployeeAttendance : UserControl, IRefreshable
    {
        private readonly frmLoading _frmLoading;
        private DataTable _dataEmployeeAttendance;
        private frmMain _frmMain;
        private int _currentPage = 1;
        private int _totalPages = 0;
        public ctrlEmployeeAttendance(frmMain frmMain)
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
        private async void ctrlEmployeeAttendance_Load(object sender, EventArgs e)
        {
            await _LoadDataAsync();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnAdd.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية إضافة حضور");
                return;
            }
            using (var frm = new frmAddEditEmployeeAttendance())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlEmployeeAttendance>();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnEdit.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تعديل حضور");
                return;
            }

            if (dgvEmployeeAttendance.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvEmployeeAttendance.CurrentRow, "AttendanceID", out int id) ||
                !_TryGetCellIntValue(dgvEmployeeAttendance.CurrentRow, "EmployeeID", out int empId))
            {
                clsMessages.ShowError("تعذر قراءة صفوف الجدول بشكل صحيح");
                return;
            }

            using (var frm = new frmAddEditEmployeeAttendance(id, empId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlEmployeeAttendance>();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnDelete.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية حذف حضور");
                return;
            }

            if (dgvEmployeeAttendance.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvEmployeeAttendance.CurrentRow, "AttendanceID", out int ID))
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

                bool deleted = await clsEmployeeAttendance.DeleteAsync(ID);
                if (deleted)
                {
                    clsUtil.AddNewSystemRecord("حذف", "حذف حضور و انصراف", $"({ID}) تم حذف حضور وانصراف يحمل الرقم التعريفي ");
                    clsMessages.ShowSuccess($"تم حذف السجل ({ID}) بنجاح");

                    clsPageManager.NotifyDataChanged<ctrlEmployeeAttendance>();
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
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات الحضور و الانصراف");
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


                _dataEmployeeAttendance = await clsEmployeeAttendance.GetEmployeeAttendanceInfo();

                if (_dataEmployeeAttendance == null || _dataEmployeeAttendance.Rows.Count == 0)
                {
                    clsMessages.ShowError("لا توجد بيانات للتصدير");
                    return;
                }

                _ExportExcel(_dataEmployeeAttendance);
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
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات الحضور و الانصراف");
                return;
            }

            var data = dgvEmployeeAttendance.DataSource as DataTable;
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
            var result = await clsEmployeeAttendance.GetEmployeeAttendancePageAsync(_currentPage,
                Properties.Settings.Default.NumberOfGridViewItems, searchText);

            dgvEmployeeAttendance.DataSource = result.dt;
            lblNumberOfItems.Text = result.TotalPages.ToString();

            if (_totalPages != result.TotalPages)
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
            if (dgvEmployeeAttendance.Columns.Count == 0)
                return;


            _SetColumnHeaderIfExists("AttendanceID", "المعرف");
            _SetColumnHeaderIfExists("FullName", "الاسم الكامل");
            _SetColumnHeaderIfExists("NationalID", "الهوية");
            _SetColumnHeaderIfExists("Description", "الوظيفة");
            _SetColumnHeaderIfExists("DepartmentName", "القسم");
            _SetColumnHeaderIfExists("AttendanceDate", "تاريخ الحضور");
            _SetColumnHeaderIfExists("CheckInTime", "الحضور");
            _SetColumnHeaderIfExists("CheckOutTime", "الانصراف");
            _SetColumnHeaderIfExists("HoursWorked", "ساعات العمل");
            _SetColumnHeaderIfExists("Late", "متأخر ؟");
            _SetColumnHeaderIfExists("Notes", "الملاحظات");
            _SetColumnHeaderIfExists("CreatedDate", "تاريخ الانشاء");
            _SetColumnHeaderIfExists("CreatedByUserID", "المنشئ");
            _SetColumnHeaderIfExists("EditedDate", "تاريخ التعديل");
            _SetColumnHeaderIfExists("EditedByUserID", "المعدل");
            _SetColumnHeaderIfExists("EmployeeID", "معرف الموظف");

            dgvEmployeeAttendance.Columns["EmployeeID"].Visible = false;

            dgvEmployeeAttendance.Columns["AttendanceDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvEmployeeAttendance.Columns["CreatedDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvEmployeeAttendance.Columns["EditedDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        private void _SetColumnHeaderIfExists(string columnName, string headerText)
        {
            if (dgvEmployeeAttendance.Columns.Contains(columnName))
                dgvEmployeeAttendance.Columns[columnName].HeaderText = headerText;
        }
        private void _ShowEmptyDataState()
        {
            lblStateTitle.Text = Properties.Resources.EmptyDataStateTitle;
            lblStateDescription.Text = Properties.Resources.EmptyDataStateDescription;
            panelState.Visible = clsUtil.IsDataGridViewEmpty(dgvEmployeeAttendance);
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
            clsExcelHelper.Export(_frmMain, optimized, "Employee Attendance");
        }
        private DataTable _CreatePreOptimizedStructure()
        {
            var dt = new DataTable();
            dt.Columns.Add("المعرف", typeof(int));
            dt.Columns.Add("الاسم الكامل", typeof(string));
            dt.Columns.Add("الهوية", typeof(string));
            dt.Columns.Add("الوظيفة", typeof(string));
            dt.Columns.Add("القسم", typeof(string));
            dt.Columns.Add("تاريخ الحضور", typeof(string)); // نكتب كنص بعد التنسيق
            dt.Columns.Add("الحضور", typeof(string));
            dt.Columns.Add("الانصراف", typeof(string));
            dt.Columns.Add("ساعات العمل", typeof(decimal));
            dt.Columns.Add("متأخر ؟", typeof(string));
            dt.Columns.Add("الملاحظات", typeof(string));
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

                nr["المعرف"] = row["AttendanceID"];
                nr["الاسم الكامل"] = row["FullName"];
                nr["الهوية"] = row["NationalID"];
                nr["الوظيفة"] = row["Description"];
                nr["القسم"] = row["DepartmentName"];
                nr["تاريخ الحضور"] = clsUtil.FormatDate(row["AttendanceDate"]);
                nr["الحضور"] = row["CheckInTime"] == DBNull.Value ? "" : row["CheckInTime"].ToString();
                nr["الانصراف"] = row["CheckOutTime"] == DBNull.Value ? "" : row["CheckOutTime"].ToString();
                nr["ساعات العمل"] = row["HoursWorked"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HoursWorked"]);
                nr["متأخر ؟"] = row["Late"]?.ToString();
                nr["الملاحظات"] = row["Notes"] == DBNull.Value ? "" : row["Notes"].ToString();
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