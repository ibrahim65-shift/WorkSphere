using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere_Buisness;

namespace WorkSphere.Employees_Page.Employees_Promotions
{
    public partial class frmAddEditPromotions : Form
    {
        private enum enMode { AddNew = 0, Update = 1 }

        private enMode _Mode;
        private clsEmployeePromotions _Promotion;
        private clsSalaryGradeSteps _SalaryGradeSteps;
        private int _PromotionID;
        private int _EmpID;
        private int _NewStepID;
        private int _OldStepID;
        private int _CheckStepID;

        public frmAddEditPromotions()
        {
            InitializeComponent();
            lblTitle.Text = "إضافة ترقية";
            _Mode = enMode.AddNew;
            _Promotion = new clsEmployeePromotions();
        }
        public frmAddEditPromotions(int prmID, int empID, int stepID)
        {
            InitializeComponent();
            lblTitle.Text = "تعديل ترقية";
            _PromotionID = prmID;
            _EmpID = empID;
            _CheckStepID = stepID;
            _Mode = enMode.Update;

        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            _ClearAllErrors();
            _MapUIToPromotion();

            var validation = await _Promotion.Save();

            if (!validation.IsValid)
            {
                _ApplyValidationErrors(validation);
                return;
            }
            if (_Mode == enMode.AddNew)
            {
                _Mode = enMode.Update;
                clsMessages.ShowSuccess($"تم حفظ بيانات الترقية بنجاح والرقم التعريفي هو ({_Promotion.PromotionID.ToString()})");
                clsUtil.AddNewSystemRecord("ترقية", "اضافة ترقية", $"({_Promotion.PromotionID.ToString()}) تم اضافة ترقية تحمل الرقم التعريفي");
            }
            else
            {
                clsMessages.ShowSuccess($"تم تعديل بيانات الترقية بنجاح والرقم التعريفي هو ({_Promotion.PromotionID.ToString()})");
                clsUtil.AddNewSystemRecord("ترقية", "تعديل ترقية", $"({_Promotion.PromotionID.ToString()}) تم تعديل ترقية حالية تحمل الرقم التعريفي");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void frmAddEditPromotions_Load(object sender, EventArgs e)
        {

            if (_Mode == enMode.Update)
            {
                ctrlEmployeeCardWithFilter1.LoadEmployeeInfo(_EmpID);
                ctrlEmployeeCardWithFilter1.Filter = false;
                await _LoadPromotionData();
                await _FillStepsComboBox();
                tpPromotion.Enabled = true;
            }
        }

        // Methods

        private async Task _FillStepsComboBox()
        {
            if (_Mode == enMode.AddNew)
            {
                _SalaryGradeSteps = clsSalaryGradeSteps.FindByID(_OldStepID);
            }
            else if (_Mode == enMode.Update)
            {
                _SalaryGradeSteps = clsSalaryGradeSteps.FindByID(_NewStepID);
            }


            List<clsSalaryGradeSteps> steps = null;

            try
            {
                if (_SalaryGradeSteps != null)
                {
                    steps = await Task.Run(() => clsSalaryGradeSteps.GetByGradeID(_SalaryGradeSteps.GradeID));
                }
            }
            catch (Exception ex)
            {
                clsMessages.ShowError(ex.Message);
            }

            if (steps == null)
                return;

            var stepsData = steps.Select(s => new
            {
                s.StepID,
                Display = $"الدرجة {s.StepNumber} - الراتب({s.BaseSalary})"
            }).ToList();

            cbSteps.DataSource = stepsData;
            cbSteps.DisplayMember = "Display";
            cbSteps.ValueMember = "StepID";
            if (cbSteps.Items.Count > 0)
            {
                if (_Mode == enMode.AddNew)
                {
                    cbSteps.SelectedValue = _OldStepID;
                }
                else if (_Mode == enMode.Update)
                {
                    cbSteps.SelectedValue = _NewStepID;
                }
            }
        }
        private void _ClearAllErrors()
        {
            errorProvider1.Clear();
        }
        private async Task _LoadPromotionData()
        {
            _Promotion = await clsEmployeePromotions.FindEmployeePromotionByPromotionID(_PromotionID);
            if (_Promotion == null)
            {
                clsMessages.ShowError($"لاتوجد ترقية تحمل الرقم التعريفي ({_PromotionID})");
                this.Close();
                return;
            }
            _EmpID = _Promotion.EmployeeID;
            _NewStepID = _Promotion.NewStepID;
            _OldStepID = _Promotion.OldStepID;
            txtNotes.Text = _Promotion.Notes ?? "";
            dtpPromotionDate.Value = _Promotion.PromotionDate;

        }
        private void _ApplyValidationErrors(PromotionsValidationResult validation)
        {
            _ClearAllErrors();

            foreach (var error in validation.Errors)
            {
                switch (error.FieldName)
                {
                    case "NewStepID":
                        errorProvider1.SetError(cbSteps, error.Message);
                        break;
                    case "EmployeeID":
                        clsMessages.ShowError(error.Message);
                        break;
                    case "Update":
                        clsMessages.ShowError(error.Message);
                        break;

                }

            }
        }
        private void _MapUIToPromotion()
        {
            _Promotion.EmployeeID = _EmpID;
            _Promotion.OldStepID = _OldStepID;
            if (cbSteps.SelectedValue != null && int.TryParse(cbSteps.SelectedValue.ToString(), out int newStep))
            {
                _Promotion.NewStepID = newStep;
            }
            _Promotion.Notes = txtNotes.Text ?? null;
            _Promotion.PromotionDate = dtpPromotionDate.Value;
            _Promotion.CreatedByUserID = clsCurrentUser.User.UserID;
        }

        private async void ctrlEmployeeCardWithFilter1_EmployeeDataLoaded(object sender, EmployeeidAndStepidEventArgs e)
        {
            _EmpID = e.EmployeeID;
            _OldStepID = e.StepID;
            await _FillStepsComboBox();

            if (_Mode == enMode.Update)
            {
                if (_CheckStepID != ctrlEmployeeCardWithFilter1.SelectedEmployee.StepID)
                {
                    clsMessages.ShowError("لايمكن التعديل على هذه الترقية لأنه لم يعد يعمل في هذه الوظيفة");
                    this.Close();
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlEmployeeCardWithFilter1.SelectedEmployee == null)
            {
                clsMessages.ShowError("لايمكن فتح صفحة الترقية , يجب تحميل معلومات الموظف اولا");
                return;
            }

            tabControl1.SelectedTab = tpPromotion;
        }

        private void btnprev_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tpEmployee;
        }
    }
}
