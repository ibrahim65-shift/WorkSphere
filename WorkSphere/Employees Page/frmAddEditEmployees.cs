using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere.Properties;
using WorkSphere_Buisness;



namespace WorkSphere.Employees_Page
{
    public partial class frmAddEditEmployees : Form
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _employeeID;

        private clsEmployees _employee;
        public frmAddEditEmployees()
        {
            InitializeComponent();
            lblTitle.Text = "إضافة موظف";
            _Mode = enMode.AddNew;
            _employee = new clsEmployees();
        }
        public frmAddEditEmployees(int id)
        {
            InitializeComponent();
            lblTitle.Text = "تعديل موظف";
            _Mode = enMode.Update;
            _employeeID = id;
        }

        private async void frmAddEditEmployees_Load(object sender, EventArgs e)
        {
            _ClearAllErrors();
            await _FillDepartmentComboBox();

            dtpBirthDate.MaxDate = DateTime.Now.AddYears(-20);
            dtpBirthDate.MinDate = DateTime.Now.AddYears(-100);
            dtpBirthDate.Value = dtpBirthDate.MaxDate;
            dtpHireDate.MaxDate = DateTime.Now;

            if (_Mode == enMode.Update)
            {
                await _LoadEmployeeData();
            }

        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            _ClearAllErrors();
            _MapUIToEmployee();


            if (_Mode == enMode.Update && _employee.IsStepChanged())
            {
                if (MessageBox.Show("هل أنت متأكد من تغير الوظيفة للموظف ؟ ", "ترقية", MessageBoxButtons.YesNo
                    , MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }

            var validation = await _employee.Save();

            if (!validation.IsValid)
            {
                _ApplyValidationErrors(validation);
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                _Mode = enMode.Update;
                clsMessages.ShowSuccess($"تم حفظ بيانات الموظف بنجاح والرقم التعريفي هو ({_employee.EmployeeID.ToString()})");
                clsUtil.AddNewSystemRecord("اضافة", "اضافة موظف", $"({_employee.EmployeeID.ToString()}) تم اضافة موظف يحمل الرقم التعريفي");
                clsEventHub.RaiseAddNewEmployee();
            }
            else
            {
                clsMessages.ShowSuccess($"تم تعديل بيانات الموظف بنجاح والرقم التعريفي هو ({_employee.EmployeeID.ToString()})");
                clsUtil.AddNewSystemRecord("تعديل", "تعديل موظف", $"({_employee.EmployeeID.ToString()}) تم تعديل موظف حالي يحمل الرقم التعريفي");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                errorProvider1.SetError(txtEmail, null);
                return;
            }

            if (!clsUtil.IsValidEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "!يرجى ادخال بريد الالكتروني صحيح");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }
        private async void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDepartment.SelectedValue != null && int.TryParse(cbDepartment.SelectedValue.ToString(), out int DepID))
            {
                await _FillGobsCombobox(DepID);
            }

        }
        private async void cbJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbJob.SelectedValue != null && int.TryParse(cbJob.SelectedValue.ToString(), out int gradeID))
            {
                await _FillStepsComboBox(gradeID);
            }
        }

        private void rbMan_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMan.Checked)
                lblImageGender.Image = Resources.man_32;
        }

        private void rbWoman_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWoman.Checked)
                lblImageGender.Image = Resources.woman_32;
        }

        //Methods

        private async Task _FillGobsCombobox(int DepartmentID)
        {
            List<clsSalaryGrades> Jobs = null;

            try
            {
                Jobs = await Task.Run(() => clsSalaryGrades.GetSalaryGradesByDepartmentID(DepartmentID));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (Jobs == null)
                return;

            var jobData = Jobs.Select(s => new { s.GradeID, Display = $"{s.Description}" }).ToList();

            cbJob.DataSource = jobData;
            cbJob.DisplayMember = "Display";
            cbJob.ValueMember = "GradeID";
            if (cbJob.Items.Count > 0)
            {
                cbJob.SelectedIndex = 0;
                if (cbJob.SelectedValue != null && int.TryParse(cbJob.SelectedValue.ToString(), out int gradeID))
                {
                    await _FillStepsComboBox(gradeID);
                }
            }
        }
        private async Task _FillDepartmentComboBox()
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


            cbDepartment.DataSource = departments;
            cbDepartment.DisplayMember = "DepartmentName";
            cbDepartment.ValueMember = "DepartmentID";
            if (cbDepartment.Items.Count > 0)
            {
                cbDepartment.SelectedIndex = 0;
                if (cbDepartment.SelectedValue != null && int.TryParse(cbDepartment.SelectedValue.ToString(), out int DepID))
                {
                    await _FillGobsCombobox(DepID);
                }
            }

        }
        private async Task _FillStepsComboBox(int gradeID)
        {
            List<clsSalaryGradeSteps> steps = null;
            try
            {
                steps = await Task.Run(() => clsSalaryGradeSteps.GetByGradeID(gradeID));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (steps == null)
                return;


            var stepData = steps.Select(s => new
            {
                s.StepID,
                Display = $"الدرجة {s.StepNumber} - الراتب({s.BaseSalary})"
            }).ToList();


            cbStep.DataSource = stepData;
            cbStep.DisplayMember = "Display";
            cbStep.ValueMember = "StepID";
            if (cbStep.Items.Count > 0)
                cbStep.SelectedIndex = 0;
        }
        private async Task _LoadEmployeeData()
        {
            _employee = await clsEmployees.FindEmployeeByID(_employeeID);
            if (_employee == null)
            {
                clsMessages.ShowError($"الموظف الذي يحمل الرقم التعريفي({this._employee.ToString()}) غير موجود");
                this.Close();
                return;
            }

            txtFirstName.Text = _employee.FirstName;
            txtSecondName.Text = _employee.SecondName;
            txtThirdName.Text = _employee.ThirdName ?? "";
            txtLastName.Text = _employee.LastName;
            ntxtPhone.Text = _employee.Phone ?? "";
            txtEmail.Text = _employee.Email ?? "";
            txtAddress.Text = _employee.Address ?? "";
            ntxtNationalID.Text = _employee.NationalID;
            dtpBirthDate.Value = _employee.BirthDate;
            dtpHireDate.Value = _employee.HireDate;
            chkIsActive.Checked = _employee.IsActive;
            if (_employee.Gender)
            {
                rbMan.Checked = true;
                lblImageGender.Image = Resources.man_32;
            }
            else
            {
                rbWoman.Checked = true;
                lblImageGender.Image = Resources.woman_32;
            }

            if (_employee.DepartmentID > 0)
            {
                cbDepartment.SelectedValue = _employee.DepartmentID;
                await _FillGobsCombobox(_employee.DepartmentID);

                if (_employee.StepInfo.GradeID > 0 && cbJob.DataSource != null)
                {
                    cbJob.SelectedValue = _employee.StepInfo.GradeID;
                    await _FillStepsComboBox(_employee.StepInfo.GradeID);

                    if (_employee.StepID > 0 && cbStep.DataSource != null)
                    {
                        cbStep.SelectedValue = _employee.StepID;

                    }
                }
            }

        }
        private void _ClearAllErrors()
        {
            errorProvider1.Clear();
        }
        private void _ApplyValidationErrors(EmployeeValidationResult validation)
        {
            _ClearAllErrors();

            foreach (var error in validation.Errors)
            {
                switch (error.FieldName)
                {
                    case "NationalID":
                        errorProvider1.SetError(ntxtNationalID, error.Message);
                        break;
                    case "FirstName":
                        errorProvider1.SetError(txtFirstName, error.Message);
                        break;
                    case "SecondName":
                        errorProvider1.SetError(txtSecondName, error.Message);
                        break;
                    case "LastName":
                        errorProvider1.SetError(txtLastName, error.Message);
                        break;
                    case "BirthDate":
                        errorProvider1.SetError(dtpBirthDate, error.Message);
                        break;
                    case "HireDate":
                        errorProvider1.SetError(dtpHireDate, error.Message);
                        break;
                    case "StepID":
                        errorProvider1.SetError(cbStep, error.Message);
                        break;
                    case "DepartmentID":
                        errorProvider1.SetError(cbDepartment, error.Message);
                        break;
                    case "Update":
                        clsMessages.ShowError(error.Message);
                        break;
                }

            }


        }
        private void _MapUIToEmployee()
        {
            _employee.NationalID = ntxtNationalID.Text.Trim();
            _employee.FirstName = txtFirstName.Text.Trim();
            _employee.SecondName = txtSecondName.Text.Trim();
            _employee.ThirdName = string.IsNullOrWhiteSpace(txtThirdName.Text) ? null : txtThirdName.Text.Trim();
            _employee.LastName = txtLastName.Text.Trim();
            _employee.Phone = string.IsNullOrWhiteSpace(ntxtPhone.Text) ? null : ntxtPhone.Text.Trim();
            _employee.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
            _employee.Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim();
            _employee.IsActive = chkIsActive.Checked;
            _employee.BirthDate = dtpBirthDate.Value;
            _employee.HireDate = dtpHireDate.Value;
            _employee.Gender = rbMan.Checked;

            if (cbDepartment.SelectedValue != null && int.TryParse(cbDepartment.SelectedValue.ToString(), out int deptID))
            {
                _employee.DepartmentID = deptID;
            }
            else
            {
                _employee.DepartmentID = 0;
            }

            if (cbStep.SelectedValue != null && int.TryParse(cbStep.SelectedValue.ToString(), out int stepID))
            {
                _employee.StepID = stepID;
            }
            else
            {
                _employee.StepID = 0;
            }

            if (_Mode == enMode.AddNew)
            {
                _employee.CreatedDate = DateTime.Now;
                _employee.CreatedByUserID = clsCurrentUser.User.UserID;
            }
            else
            {
                _employee.EditDate = DateTime.Now;
                _employee.EditedByUserID = clsCurrentUser.User.UserID;
            }
        }


    }
}