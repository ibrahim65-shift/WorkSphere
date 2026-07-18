using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere.Home_Page.Loading;
using WorkSphere_Buisness;
using WorkSphere_Buisness.Global_Classes;

namespace WorkSphere.Salary_Grades_Page
{
    public partial class ctrlSalaryGrades : UserControl,IRefreshable
    {

        private frmMain _frmMain;
        private frmLoading _frmLoading;
        private DataTable _dataSalaryGrades;
        private int _currentPage = 1;
        private int _totalPages = 0;

        public ctrlSalaryGrades(frmMain main)
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
        private async void ctrlSalaryGrades_Load(object sender, EventArgs e)
        {
           await _LoadDataAsync();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnAdd.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية إضافة درجة وظيفية");
                return;
            }

            using (var frm = new frmAddEditSalaryGrade())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlSalaryGrades>();
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnEdit.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تعديل درجة وظيفية");
                return;
            }

            if (dgvListSalaryGrades.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvListSalaryGrades.CurrentRow, "GradeID", out int id))
            {
                clsMessages.ShowError("تعذر قراءة صفوف الجدول بشكل صحيح");
                return;
            }

            using (var frm = new frmAddEditSalaryGrade(id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clsPageManager.NotifyDataChanged<ctrlSalaryGrades>();
                }
            }
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnDelete.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية حذف درجة وظيفية");
                return;
            }

            if (dgvListSalaryGrades.CurrentRow == null)
            {
                clsMessages.ShowDataGridViewEmpty();
                return;
            }

            if (!_TryGetCellIntValue(dgvListSalaryGrades.CurrentRow, "GradeID", out int ID))
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

                bool deleted = clsSalaryGrades.Delete(ID);
                if (deleted)
                {
                    clsUtil.AddNewSystemRecord("حذف", "حذف وظيفة", $"({ID}) تم حذف وظيفة تحمل الرقم التعريفي ");
                    clsMessages.ShowSuccess($"تم حذف السجل ({ID}) بنجاح");

                    clsPageManager.NotifyDataChanged<ctrlSalaryGrades>();
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
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات الدرجات الوظيفية");
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


                _dataSalaryGrades = clsSalaryGrades.GetAllSalaryGrades();

                if (_dataSalaryGrades == null || _dataSalaryGrades.Rows.Count == 0)
                {
                    clsMessages.ShowError("لا توجد بيانات للتصدير");
                    return;
                }

                _ExportExcel(_dataSalaryGrades);
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
                clsMessages.ShowWarning("ليس لديك صلاحية تصدير بيانات الدرجات الوظيفية");
                return;
            }
            var data = dgvListSalaryGrades.DataSource as DataTable;
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
            var result = await clsSalaryGrades.GetSalaryGradesPageAsync(_currentPage,
                Properties.Settings.Default.NumberOfGridViewItems, searchText);

            dgvListSalaryGrades.DataSource = result.dt;
            lblNumberOfItem.Text = result.TotalPages.ToString();

            if(_totalPages!= result.TotalPages)
            {
                _totalPages= result.TotalPages;
                cbNumberOfPages.Items.Clear();
                for (int i = 1; i <= _totalPages; i++)
                    cbNumberOfPages.Items.Add(i);
            }

            int newIndex = _currentPage - 1;
            if(cbNumberOfPages.SelectedIndex!=newIndex&&newIndex>=0&&newIndex<cbNumberOfPages.Items.Count)
                cbNumberOfPages.SelectedIndex = newIndex;

            _SetColumns();
            _ShowEmptyDataState();
        }
        private void _SetColumns()
        {
            if (dgvListSalaryGrades.Columns.Count == 0)
                return;


            _SetColumnHeaderIfExists("GradeID", "المعرف");
            _SetColumnHeaderIfExists("JobGrade", "الدرجة الوظيفية");
            _SetColumnHeaderIfExists("Description", "المسمى الوظيفي");
            _SetColumnHeaderIfExists("DepartmentName", "القسم");
            _SetColumnHeaderIfExists("EffectiveFrom", "ساري من");
            _SetColumnHeaderIfExists("EffectiveTo", "ساري حتى");
            _SetColumnHeaderIfExists("IsActive", "نشطة ؟");
            _SetColumnHeaderIfExists("CreatedDate", "تاريخ الانشاء");
            _SetColumnHeaderIfExists("CreatedByUserID", "المنشئ");
            _SetColumnHeaderIfExists("EditedDate", "تاريخ التعديل");
            _SetColumnHeaderIfExists("EditedByUserID", "المعدل");


            dgvListSalaryGrades.Columns["EffectiveFrom"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvListSalaryGrades.Columns["EffectiveTo"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvListSalaryGrades.Columns["CreatedDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvListSalaryGrades.Columns["EditedDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        private void _SetColumnHeaderIfExists(string columnName, string headerText)
        {
            if (dgvListSalaryGrades.Columns.Contains(columnName))
                dgvListSalaryGrades.Columns[columnName].HeaderText = headerText;
        }
        private void _ShowEmptyDataState()
        {
            lblStateTitle.Text = Properties.Resources.EmptyDataStateTitle;
            lblStateDescription.Text = Properties.Resources.EmptyDataStateDescription;
            panelState.Visible = clsUtil.IsDataGridViewEmpty(dgvListSalaryGrades);
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
            clsExcelHelper.Export(_frmMain, optimized, "Salary Grades");
        }
        private DataTable _CreatePreOptimizedStructure()
        {
            var dt = new DataTable();

            dt.Columns.Add("المعرف", typeof(int));
            dt.Columns.Add("الدرجة الوظيفية", typeof(string));
            dt.Columns.Add("المسمى الوظيفي", typeof(string));
            dt.Columns.Add("القسم", typeof(string));
            dt.Columns.Add("ساري من", typeof(string));
            dt.Columns.Add("ساري حتى", typeof(string)); // نكتب كنص بعد التنسيق
            dt.Columns.Add("نشطة ؟", typeof(bool));
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

                nr["المعرف"] = row["GradeID"];
                nr["الدرجة الوظيفية"] = row["JobGrade"];
                nr["المسمى الوظيفي"] = row["Description"];
                nr["القسم"] = row["DepartmentName"];
                nr["ساري من"] = clsUtil.FormatDate(row["EffectiveFrom"]);
                nr["ساري حتى"] = row["EffectiveTo"] == DBNull.Value ? null : clsUtil.FormatDate(row["EffectiveTo"]);
                nr["نشطة ؟"] = row["IsActive"];
                nr["تاريخ الانشاء"] = clsUtil.FormatDate(row["CreatedDate"]);
                nr["المنشئ"] = row["CreatedByUserID"];
                nr["تاريخ التعديل"] = row["EditedDate"] == DBNull.Value ? null : clsUtil.FormatDate(row["EditedDate"]);
                nr["المعدل"] = row["EditedByUserID"];


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
