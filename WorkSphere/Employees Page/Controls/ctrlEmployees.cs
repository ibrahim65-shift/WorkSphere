using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere.Home_Page.Loading;
using WorkSphere_Buisness;
using WorkSphere_Buisness.Global_Classes;

namespace WorkSphere.Employees_Page
{
    public partial class ctrlEmployees : UserControl, IRefreshable
    {

        private readonly frmMain _frmMain;
        private frmLoading _frmLoading;
        private DataTable _dataEmployees;
        private int _currentPage = 1;
        private int _totalPages = 0;
        public ctrlEmployees(frmMain main)
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
        private async void ctrlEmployees_Load(object sender, EventArgs e)
        {
            if (_dataEmployees == null || _dataEmployees.Rows.Count == 0)
                await _LoadDataAsync();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnAdd.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية إضافة موظف");
                return;
            }
            using (var frm = new frmAddEditEmployees())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlEmployees>();
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnEdit.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تعديل موظف");
                return;
            }
            if (dgvListEmployees.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvListEmployees.CurrentRow, "EmployeeID", out int id))
            {
                clsMessages.ShowError("تعذر قراءة صفوف الجدول بشكل صحيح");
                return;
            }

            using (var frm = new frmAddEditEmployees(id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlEmployees>();
                }
            }
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnDelete.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تعديل موظف");
                return;
            }
            if (dgvListEmployees.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvListEmployees.CurrentRow, "EmployeeID", out int ID))
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

                bool deleted = await clsEmployees.Delete(ID);
                if (deleted)
                {
                    clsUtil.AddNewSystemRecord("حذف", "حذف موظف", $"({ID}) تم حذف موظف يحمل الرقم التعريفي ");
                    clsMessages.ShowSuccess($"تم حذف السجل ({ID}) بنجاح");

                    clsPageManager.NotifyDataChanged<ctrlEmployees>();
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
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات الموظفين");
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


                _dataEmployees = await clsEmployees.GetEmployeeFullInfo();

                if (_dataEmployees == null || _dataEmployees.Rows.Count == 0)
                {
                    clsMessages.ShowError("لا توجد بيانات للتصدير");
                    return;
                }

                _ExportExcel(_dataEmployees);
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
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات الموظفين");
                return;
            }
            var data = dgvListEmployees.DataSource as DataTable;
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
            if (cbNumberOfPages.SelectedIndex >= 0)
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
            var result = await clsEmployees.GetEmployeesPageAsync(_currentPage,
                             Properties.Settings.Default.NumberOfGridViewItems, searchText);

            dgvListEmployees.DataSource = result.Data;
            lblNumberOfItem.Text = result.TotalPages.ToString();


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
            if (dgvListEmployees.Columns.Count == 0)
                return;


            _SetColumnHeaderIfExists("EmployeeID", "المعرف");
            _SetColumnHeaderIfExists("NationalID", "الهوية");
            _SetColumnHeaderIfExists("FullName", "الاسم الكامل");
            _SetColumnHeaderIfExists("Gender", "الجنس");
            _SetColumnHeaderIfExists("BirthDate", "تاريخ الميلاد");
            _SetColumnHeaderIfExists("Email", "البريد الالكتروني");
            _SetColumnHeaderIfExists("Phone", "الهاتف");
            _SetColumnHeaderIfExists("Address", "العنوان");
            _SetColumnHeaderIfExists("HireDate", "تاريخ التعيين");
            _SetColumnHeaderIfExists("IsActive", "نشط ؟");
            _SetColumnHeaderIfExists("Description", "الوظيفة");
            _SetColumnHeaderIfExists("DepartmentName", "القسم");
            _SetColumnHeaderIfExists("CreatedDate", "تاريخ الانشاء");
            _SetColumnHeaderIfExists("CreatedByUserID", "المنشئ");
            _SetColumnHeaderIfExists("EditDate", "تاريخ التعديل");
            _SetColumnHeaderIfExists("EditedByUserID", "المعدل");

            dgvListEmployees.Columns["BirthDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvListEmployees.Columns["HireDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvListEmployees.Columns["CreatedDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvListEmployees.Columns["EditDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        private void _SetColumnHeaderIfExists(string columnName, string headerText)
        {
            if (dgvListEmployees.Columns.Contains(columnName))
                dgvListEmployees.Columns[columnName].HeaderText = headerText;
        }
        private void _ShowEmptyDataState()
        {
            lblStateTitle.Text = Properties.Resources.EmptyDataStateTitle;
            lblStateDescription.Text = Properties.Resources.EmptyDataStateDescription;
            panelState.Visible = clsUtil.IsDataGridViewEmpty(dgvListEmployees);
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
            dt.Columns.Add("الهوية", typeof(string));
            dt.Columns.Add("الاسم الكامل", typeof(string));
            dt.Columns.Add("الجنس", typeof(string));
            dt.Columns.Add("تاريخ الميلاد", typeof(string));
            dt.Columns.Add("البريد الالكتروني", typeof(string)); // نكتب كنص بعد التنسيق
            dt.Columns.Add("الهاتف", typeof(string));
            dt.Columns.Add("العنوان", typeof(string));
            dt.Columns.Add("تاريخ التعيين", typeof(string));
            dt.Columns.Add("نشط ؟", typeof(string));
            dt.Columns.Add("الوظيفة", typeof(string));
            dt.Columns.Add("القسم", typeof(string));
            dt.Columns.Add("تاريخ الانشاء", typeof(string));
            dt.Columns.Add("المنشئ", typeof(int));
            dt.Columns.Add("تاريخ التعديل", typeof(string));
            dt.Columns.Add("المعدل", typeof(int));
            return dt;
        }
        private void _FillOptimizedData(DataTable target, DataTable source)
        {
            foreach (DataRow row in source.Rows)
            {
                var nr = target.NewRow();

                nr["المعرف"] = row["EmployeeID"];
                nr["الهوية"] = row["NationalID"];
                nr["الاسم الكامل"] = row["FullName"];
                nr["الجنس"] = row["Gender"];
                nr["تاريخ الميلاد"] = clsUtil.FormatDate(row["BirthDate"]);
                nr["البريد الالكتروني"] = row["Email"] == DBNull.Value ? null : row["Email"].ToString();
                nr["الهاتف"] = row["Phone"] == DBNull.Value ? null : row["Phone"].ToString();
                nr["العنوان"] = row["Address"] == DBNull.Value ? null : row["Address"].ToString();
                nr["تاريخ التعيين"] = clsUtil.FormatDate(row["HireDate"]);
                nr["نشط ؟"] = row["IsActive"];
                nr["الوظيفة"] = row["Description"];
                nr["القسم"] = row["DepartmentName"];
                nr["تاريخ الانشاء"] = clsUtil.FormatDate(row["CreatedDate"]);
                nr["المنشئ"] = row["CreatedByUserID"];
                nr["تاريخ التعديل"] = row["EditDate"] == DBNull.Value ? null : clsUtil.FormatDate(row["EditDate"]);
                nr["المعدل"] = row["EditedByUserID"] == DBNull.Value ? 0 : row["EditedByUserID"];

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
