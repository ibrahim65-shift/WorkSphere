using System;
using System.ComponentModel;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere_Buisness;

namespace WorkSphere.Salary_Grades_Page.Grade_Steps
{
    public partial class frmAddEditGradeSteps : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _stepID;
        private clsSalaryGradeSteps _GradeStep;
        private int _GradeID;
        public frmAddEditGradeSteps(int gradeID)
        {
            InitializeComponent();
            _GradeStep = new clsSalaryGradeSteps();
            _GradeID = gradeID;
            _Mode = enMode.AddNew;
        }
        public frmAddEditGradeSteps(int gradeID, int setpID)
        {
            InitializeComponent();
            _GradeID = gradeID;
            _stepID = setpID;
            _Mode = enMode.Update;
        }

        private void frmAddEditGradeSteps_Load(object sender, EventArgs e)
        {
            _GetJobName();
            _GetStepNumber();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void NtxtStepNumber_Validating(object sender, CancelEventArgs e)
        {
            if (!byte.TryParse(NtxtStepNumber.Text.Trim(), out byte stepNumber) || stepNumber <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(NtxtStepNumber, "رقم الخطوة يجب ان يكون عددا موجبا");
            }
            else
            {
                errorProvider1.SetError(NtxtStepNumber, null);

            }
        }

        private void NtxtBaseSalary_Validating(object sender, CancelEventArgs e)
        {
            if (!decimal.TryParse(NtxtBaseSalary.Text.Trim(), out decimal baseSalary) || baseSalary <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(NtxtBaseSalary, "الراتب الاساسي يجب ان يكون رقم موجبا");
            }
            else
            {
                errorProvider1.SetError(NtxtBaseSalary, null);

            }
        }

        private void NtxtAnnualRaise_Validating(object sender, CancelEventArgs e)
        {
            if (!decimal.TryParse(NtxtAnnualRaise.Text.Trim(), out decimal annualRaise) || annualRaise < 0 || annualRaise > 100)
            {
                e.Cancel = true;
                errorProvider1.SetError(NtxtAnnualRaise, "نسبة الزيادة يجب ان تكون بين 0 و 100");
            }
            else
            {
                errorProvider1.SetError(NtxtAnnualRaise, null);

            }
        }

        private void NtxtMinYearsInStep_Validating(object sender, CancelEventArgs e)
        {
            if (!byte.TryParse(NtxtMinYearsInStep.Text.Trim(), out byte yearsStep) || yearsStep < 1)
            {
                e.Cancel = true;
                errorProvider1.SetError(NtxtMinYearsInStep, "الحد الادنى للسنوات يجب ان يكون 1 او اكثر");
            }
            else
            {
                errorProvider1.SetError(NtxtMinYearsInStep, null);

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                clsMessages.ShowRequiredFileds();
                return;
            }

            _SaveGradeStepData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                NtxtStepNumber.Clear();
                NtxtBaseSalary.Clear();
                NtxtAnnualRaise.Clear();
                NtxtMinYearsInStep.Clear();
            }
            else
            {
                _LoadData();
            }
        }

        // Methods

        private void _SaveGradeStepData()
        {
            _GradeStep.GradeID = _GradeID;
            _GradeStep.StepNumber = byte.Parse(NtxtStepNumber.Text.Trim());
            _GradeStep.BaseSalary = decimal.Parse(NtxtBaseSalary.Text.Trim());
            _GradeStep.AnnualRaisePercent = decimal.Parse(NtxtAnnualRaise.Text.Trim());
            _GradeStep.MinYearsInStep = byte.Parse(NtxtMinYearsInStep.Text.Trim());


            if (_Mode == enMode.AddNew && _GradeStep.IsDuplicateCombination())
            {
                clsMessages.ShowError("الخطوة الذي تحاول اضافتها موجودة بالفعل");
                return;
            }

            try
            {
                if (_GradeStep.Save())
                {
                    if (_Mode == enMode.AddNew)
                    {
                        clsUtil.AddNewSystemRecord("اضافة", "اضافة درجة وظيفية", $"({_GradeStep.StepID.ToString()}) تم اضافة درجة وظيفية جديدة تحمل الرقم التعريفي");
                        clsMessages.ShowDataSavedSuccessfully(_GradeStep.StepID);
                        _Mode = enMode.Update;
                    }
                    else
                    {
                        clsUtil.AddNewSystemRecord("تعديل", "تعديل درجة وظيفية", $"({_GradeStep.StepID.ToString()}) تم تعديل درجة وظيفية تحمل الرقم التعريفي");
                        clsMessages.ShowDataEditedSuccessfully(_GradeStep.StepID);
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
        private void _GetJobName()
        {
            var grade = clsSalaryGrades.FindSalaryGradesByID(_GradeID);
            if (grade != null)
            {
                lblGradeInfo.Text = $"الدرجة {grade.JobGrade} - {grade.Description}";
            }
            else
            {
                lblGradeInfo.Text = "الدرجة الوظيفية غير موجودة";
            }
        }
        private void _LoadData()
        {
            _GradeStep = clsSalaryGradeSteps.FindByID(_stepID);
            if (_GradeStep == null)
            {
                clsMessages.ShowNullUserObject(_stepID);
                return;
            }

            NtxtStepNumber.Text = _GradeStep.StepNumber.ToString();
            NtxtBaseSalary.Text = _GradeStep.BaseSalary.ToString();
            NtxtAnnualRaise.Text = _GradeStep.AnnualRaisePercent.ToString();
            NtxtMinYearsInStep.Text = _GradeStep.MinYearsInStep.ToString();
            // cbGrades.SelectedValue = _GradeStep.GradeID;

            NtxtStepNumber.Enabled = false;
            //  cbGrades.Enabled = false;
        }

        private void _GetStepNumber()
        {
            int StepNumber = clsSalaryGradeSteps.GetStepNumber(_GradeID);

            if (StepNumber != 0)
            {
                StepNumber++;
                NtxtStepNumber.Text = StepNumber.ToString();
            }
            else
            {
                NtxtStepNumber.Text = "1";
            }
        }

    }
}
