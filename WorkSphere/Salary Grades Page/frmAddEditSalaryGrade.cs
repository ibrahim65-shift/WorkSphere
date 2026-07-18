using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere.Salary_Grades_Page.Grade_Steps;
using WorkSphere_Buisness;
using WorkSphere_Buisness.Global_Classes;

namespace WorkSphere.Salary_Grades_Page
{
    public partial class frmAddEditSalaryGrade : Form
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _ID;

        private clsSalaryGrades _Grade;
        private List<clsSalaryGradeSteps> _Data;

        public frmAddEditSalaryGrade()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _Grade = new clsSalaryGrades();
        }

        public frmAddEditSalaryGrade(int id)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _ID = id;

        }

        private async void frmAddEditSalaryGrade_Load(object sender, EventArgs e)
        {
            await _FillDepartmentsComboBox();

            if (_Mode == enMode.AddNew)
            {
                _ResetForm();
                tabControl1.TabPages[1].Enabled = false;
            }
            else if (_Mode == enMode.Update)
            {
                _LoadSalaryGradeData();
                _LoadGradeStepsData();
                tabControl1.TabPages[1].Enabled = true;
            }


        }
        private void ntxtGobGrade_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(ntxtGobGrade.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(ntxtGobGrade, "الرجاء ادخال الدرجة الوظيفة");
            }
            else
            {
                errorProvider1.SetError(ntxtGobGrade, null);

            }
        }
        private void txtDescription_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "الرجاء ادخال اسم الوظيفة");
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);

            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                clsMessages.ShowRequiredFileds();
                return;
            }

            _SaveSalaryGradeData();

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                _ResetForm();
            }
            else
            {
                _LoadSalaryGradeData();
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnAdd.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية إضافة راتب للدرجة");
                return;
            }

            if (_Grade.GradeID == 0)
            {
                clsMessages.ShowError("يرجى حفظ الدرجة اولا قبل اضافة الخطوات");
                return;
            }
            using (frmAddEditGradeSteps frm = new frmAddEditGradeSteps(_Grade.GradeID))
            {
                frm.ShowDialog();
            }
            _LoadGradeStepsData();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnEdit.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية تعديل راتب للدرجة");
                return;
            }

            if (dgvGradeSteps.CurrentRow == null)
                return;


            using (frmAddEditGradeSteps frm = new frmAddEditGradeSteps(_Grade.GradeID, (int)dgvGradeSteps.CurrentRow.Cells[0].Value))
            {
                frm.ShowDialog();
            }
            _LoadGradeStepsData();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnDelete.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك صلاحية حذف راتب للدرجة");
                return;
            }

            if (dgvGradeSteps.CurrentRow == null)
                return;

            if (!clsMessages.ShowDeleteDialog())
                return;


            int ID = (int)dgvGradeSteps.CurrentRow.Cells[0].Value;
            try
            {
                if (clsSalaryGradeSteps.Delete(ID))
                {
                    clsMessages.ShowSuccess("تم حذف خطوة الدرجة التي تحمل الرقم التعريف (" + ID.ToString() + ") بنجاح");
                    clsUtil.AddNewSystemRecord("حذف", "حذف درجة وظيفية", $"({_Grade.GradeID.ToString()}) تم حذف درجة وظيفية تحمل الرقم التعريفي");
                    _LoadGradeStepsData();
                }
                else
                {
                    clsMessages.ShowDataDeleteFaild();
                }
            }
            catch (Exception ex)
            {
                clsMessages.ShowError("خطأ أثناء الحذف : " + ex.Message);
            }
        }
        private void chkHasEndDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpEffectiveTo.Enabled = chkHasEndDate.Checked;
        }

        //methods
        private async Task _FillDepartmentsComboBox()
        {
            var departments = new List<clsDepartment>();
            try
            {
                departments = await clsDepartment.GetAllDepartments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (departments == null)
                return;


            cbDepartments.DataSource = departments;
            cbDepartments.DisplayMember = "DepartmentName";
            cbDepartments.ValueMember = "DepartmentID";
            if (cbDepartments.Items.Count > 0)
                cbDepartments.SelectedIndex = 0;
        }
        private void _ResetForm()
        {
            txtDescription.Clear();
            ntxtGobGrade.Clear();
            chkIsActive.Checked = true;
            dtpEffectiveFrom.Value = DateTime.Now;
            dtpEffectiveTo.Enabled = false;
            chkHasEndDate.Checked = false;
        }
        private void _SaveSalaryGradeData()
        {

            _Grade.JobGrade = short.Parse(ntxtGobGrade.Text.Trim());
            _Grade.EffectiveTo = chkHasEndDate.Checked ? dtpEffectiveTo.Value : (DateTime?)null;
            _Grade.Description = txtDescription.Text.Trim();
            _Grade.EffectiveFrom = dtpEffectiveFrom.Value;
            _Grade.IsActive = (chkIsActive.Checked) ? true : false;

            if (cbDepartments.SelectedValue != null && int.TryParse(cbDepartments.SelectedValue.ToString(), out int depID))
            {
                _Grade.DepartmentID = depID;
            }

            if (_Mode == enMode.AddNew)
            {
                if (_Grade.IsGobExists())
                {
                    clsMessages.ShowError("هذه الوظيفة موجودة بالفعل");
                    return;
                }
                _Grade.CreatedDate = DateTime.Now;
                _Grade.CreatedByUserID = clsCurrentUser.User.UserID;
            }
            else
            {
                _Grade.EditedDate = DateTime.Now;
                _Grade.EditedByUserID = clsCurrentUser.User.UserID;
            }



            try
            {
                if (_Grade.Save())
                {
                    if (_Mode == enMode.AddNew)
                    {
                        clsMessages.ShowDataSavedSuccessfully(_Grade.GradeID);
                        clsMessages.ShowSuccess("يمكنك الان الانتقال لاضافة الخطوات");
                        _Mode = enMode.Update;
                        _ID = _Grade.GradeID;
                        tabControl1.TabPages[1].Enabled = true;
                        clsUtil.AddNewSystemRecord("اضافة", "اضافة وظيفة", $"({_Grade.GradeID.ToString()}) تم اضافة وظيفة جديدة تحمل الرقم التعريفي");
                        _LoadGradeStepsData();
                    }
                    else
                    {
                        clsMessages.ShowDataEditedSuccessfully(_Grade.GradeID);
                        clsUtil.AddNewSystemRecord("تعديل", "تعديل وظيفة", $"({_Grade.GradeID.ToString()}) تم تعديل وظيفة تحمل الرقم التعريفي");
                    }
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    clsMessages.ShowDataSavedFaild();
                }
            }
            catch (Exception ex)
            {
                clsMessages.ShowError("خطأ أثناء الحفظ : " + ex.Message);
            }
        }
        private void _LoadSalaryGradeData()
        {
            _Grade = clsSalaryGrades.FindSalaryGradesByID(_ID);
            if (_Grade == null)
            {
                clsMessages.ShowError("لايوجد معلومات للدرجة للرقم التعريفي هذا : " + (_ID.ToString()));
                return;
            }

            ntxtGobGrade.Text = _Grade.JobGrade.ToString();
            dtpEffectiveFrom.Value = _Grade.EffectiveFrom;
            chkIsActive.Checked = _Grade.IsActive;
            txtDescription.Text = _Grade.Description;
            dtpEffectiveTo.Enabled = _Grade.EffectiveTo.HasValue;
            if (_Grade.EffectiveTo.HasValue)
                dtpEffectiveTo.Value = _Grade.EffectiveTo.Value;


            if (cbDepartments.DataSource != null && _Grade.DepartmentID > 0)
            {
                cbDepartments.SelectedValue = _Grade.DepartmentID;
            }

            cbDepartments.Enabled = false;
            ntxtGobGrade.Enabled = false;
        }
        private async void _LoadGradeStepsData()
        {
            try
            {
                if (_Grade.GradeID == 0)
                {
                    dgvGradeSteps.DataSource = null; ;
                    return;
                }

                _Data = await Task.Run(() => clsSalaryGradeSteps.GetByGradeID(_ID));
                dgvGradeSteps.DataSource = _Data;
                _SetColumns();
                ShowEmptyDataState();
            }
            catch (Exception ex)
            {
                clsMessages.ShowError("خطأ أثناء تحميل الخطوات : " + ex.Message);
            }
        }
        private void ShowEmptyDataState()
        {
            lblStateTitle.Text = Properties.Resources.EmptyDataStateTitle;
            lblStateDescription.Text = Properties.Resources.EmptyDataStateDescription;
            panelState.Visible = clsUtil.IsDataGridViewEmpty(dgvGradeSteps);
        }
        private void _SetColumns()
        {

            dgvGradeSteps.Columns[0].HeaderText = "معرف الخطوة";
            dgvGradeSteps.Columns[1].HeaderText = "معرف الدرجة";
            dgvGradeSteps.Columns[2].HeaderText = "رقم الخطوة";
            dgvGradeSteps.Columns[3].HeaderText = "الراتب الاساسي";
            dgvGradeSteps.Columns[4].HeaderText = "نسبة الزيادة السنوية";
            dgvGradeSteps.Columns[5].HeaderText = "الحد الادنى للسنوات";

        }

    }
}