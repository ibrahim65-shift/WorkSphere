using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere.Home_Page.Loading;
using WorkSphere_Buisness;
using WorkSphere_Buisness.Global_Classes;

namespace WorkSphere.Employees_Page.Employees_Promotions
{
    public partial class ctrlEmployeePromotions : UserControl, IRefreshable
    {

        private static frmMain _frmMain;
        private frmLoading _frmLoading;
        private DataTable _dataEmloyeePromotions;
        private int _currentPage = 1;
        private int _totalPages = 0;

        public ctrlEmployeePromotions(frmMain main)
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
        private async void ctrlEmployeePromotions_Load(object sender, EventArgs e)
        {
            await _LoadDataAsync();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnAdd.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية إضافة ترقية");
                return;
            }

            using (var frm = new frmAddEditPromotions())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlEmployeePromotions>();
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnEdit.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تعديل ترقية");
                return;
            }

            if (dgvListPromotions.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvListPromotions.CurrentRow, "PromotionID", out int promotionID) ||
               !_TryGetCellIntValue(dgvListPromotions.CurrentRow, "EmployeeID", out int empID) ||
               !_TryGetCellIntValue(dgvListPromotions.CurrentRow, "StepID", out int stepID))
            {
                clsMessages.ShowError("تعذر قراءة صفوف الجدول بشكل صحيح");
                return;
            }

            using (var frm = new frmAddEditPromotions(promotionID, empID, stepID))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlEmployeePromotions>();
                }
            }
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnDelete.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية حذف ترقية");
                return;
            }

            if (dgvListPromotions.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvListPromotions.CurrentRow, "PromotionID", out int ID))
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

                bool deleted = await clsEmployeePromotions.Delete(ID);
                if (deleted)
                {
                    clsUtil.AddNewSystemRecord("حذف", "حذف ترقية", $"({ID}) تم حذف ترقية تحمل الرقم التعريفي ");
                    clsMessages.ShowSuccess($"تم حذف السجل ({ID}) بنجاح");

                    clsPageManager.NotifyDataChanged<ctrlEmployeePromotions>();
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
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات الترقيات");
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

                _dataEmloyeePromotions = await clsEmployeePromotions.GetFullEmployeeInfoPromotions();

                if (_dataEmloyeePromotions == null || _dataEmloyeePromotions.Rows.Count == 0)
                {
                    clsMessages.ShowError("لا توجد بيانات للتصدير");
                    return;
                }

                _ExportExcel(_dataEmloyeePromotions);
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
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات الترقيات");
                return;
            }

            var data = dgvListPromotions.DataSource as DataTable;
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
            if(cbNumberOfPages.SelectedIndex >= 0)
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
            if(_currentPage< _totalPages)
            {
                _currentPage++;
                await _ApplyPaging(txtSearch.Text.Trim());
            }
        }


        // Methods

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
            var result = await clsEmployeePromotions.GetEmployeePromotionsPageAsync(_currentPage,
                Properties.Settings.Default.NumberOfGridViewItems, searchText);

            dgvListPromotions.DataSource = result.dt;
            lblNumberOfItem.Text = result.TotalPages.ToString();

            if(_totalPages!=result.TotalPages)
            {
                _totalPages= result.TotalPages;
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
            if (dgvListPromotions.Columns.Count == 0)
                return;


            _SetColumnHeaderIfExists("PromotionID", "المعرف");
            _SetColumnHeaderIfExists("EmployeeID", "معرف الموظف");
            _SetColumnHeaderIfExists("NationalID", "الهوية");
            _SetColumnHeaderIfExists("FullName", "الاسم الكامل");
            _SetColumnHeaderIfExists("HireDate", "تاريخ التعيين");
            _SetColumnHeaderIfExists("DepartmentName", "القسم");
            _SetColumnHeaderIfExists("OldJobTitle", "الوظيفة السابقة");
            _SetColumnHeaderIfExists("OldStep", "الخطوة السابقة");
            _SetColumnHeaderIfExists("OldBaseSalary", "الراتب السابق");
            _SetColumnHeaderIfExists("NewJobTitle", "الوظيفة الجديدة");
            _SetColumnHeaderIfExists("NewStep", "الخطوة الجديدة");
            _SetColumnHeaderIfExists("NewBaseSalary", "الراتب الجديد");
            _SetColumnHeaderIfExists("PromotionDate", "تاريخ الترقية");
            _SetColumnHeaderIfExists("Notes", "الملاحظات");
            _SetColumnHeaderIfExists("CreatedByUserID", "المنشئ");
            _SetColumnHeaderIfExists("StepID", "معرف الخطوة");

            dgvListPromotions.Columns["EmployeeID"].Visible = false;
            dgvListPromotions.Columns["StepID"].Visible = false;

            dgvListPromotions.Columns["HireDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvListPromotions.Columns["PromotionDate"].DefaultCellStyle.Format = "dd/MM/yyyy";

        }
        private void _SetColumnHeaderIfExists(string columnName, string headerText)
        {
            if (dgvListPromotions.Columns.Contains(columnName))
                dgvListPromotions.Columns[columnName].HeaderText = headerText;
        }
        private void _ShowEmptyDataState()
        {
            lblStateTitle.Text = Properties.Resources.EmptyDataStateTitle;
            lblStateDescription.Text = Properties.Resources.EmptyDataStateDescription;
            panelState.Visible = clsUtil.IsDataGridViewEmpty(dgvListPromotions);
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
            clsExcelHelper.Export(_frmMain, optimized, "Employee Promotions");
        }
        private DataTable _CreatePreOptimizedStructure()
        {
            var dt = new DataTable();
            dt.Columns.Add("المعرف", typeof(int));
            dt.Columns.Add("معرف الموظف", typeof(int));
            dt.Columns.Add("الهوية", typeof(string));
            dt.Columns.Add("الاسم الكامل", typeof(string));
            dt.Columns.Add("تاريخ التعيين", typeof(string));
            dt.Columns.Add("القسم", typeof(string)); // نكتب كنص بعد التنسيق
            dt.Columns.Add("الوظيفة السابقة", typeof(string));
            dt.Columns.Add("الخطوة السابقة", typeof(int));
            dt.Columns.Add("الراتب السابق", typeof(decimal));
            dt.Columns.Add("الوظيفة الجديدة", typeof(string));
            dt.Columns.Add("الخطوة الجديدة", typeof(int));
            dt.Columns.Add("الراتب الجديد", typeof(decimal));
            dt.Columns.Add("تاريخ الترقية", typeof(string));
            dt.Columns.Add("الملاحظات", typeof(string));
            dt.Columns.Add("المنشئ", typeof(int));
            dt.Columns.Add("معرف الخطوة", typeof(int));
            return dt;
        }
        private void _FillOptimizedData(DataTable target, DataTable source)
        {
            foreach (DataRow row in source.Rows)
            {
                var nr = target.NewRow();

                nr["المعرف"] = row["PromotionID"];
                nr["معرف الموظف"] = row["EmployeeID"];
                nr["الهوية"] = row["NationalID"];
                nr["الاسم الكامل"] = row["FullName"];
                nr["تاريخ التعيين"] = clsUtil.FormatDate(row["HireDate"]);
                nr["القسم"] = row["DepartmentName"];
                nr["الوظيفة السابقة"] = row["OldJobTitle"];
                nr["الخطوة السابقة"] = row["OldStep"];
                nr["الراتب السابق"] = row["OldBaseSalary"];
                nr["الوظيفة الجديدة"] = row["NewJobTitle"];
                nr["الخطوة الجديدة"] = row["NewStep"];
                nr["الراتب الجديد"] = row["NewBaseSalary"];
                nr["تاريخ الترقية"] = clsUtil.FormatDate(row["PromotionDate"]);
                nr["الملاحظات"] = row["Notes"] == DBNull.Value ? null : row["Notes"].ToString();
                nr["المنشئ"] = row["CreatedByUserID"];
                nr["معرف الخطوة"] = row["StepID"];

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
