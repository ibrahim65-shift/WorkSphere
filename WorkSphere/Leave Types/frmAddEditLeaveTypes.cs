using System;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere_Buisness;

namespace WorkSphere.Leave_Types
{
    public partial class frmAddEditLeaveTypes : Form
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsLeaveTypes _LeaveType;
        private int _LeaveTypeID;
        public frmAddEditLeaveTypes()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _LeaveType = new clsLeaveTypes();
        }
        public frmAddEditLeaveTypes(int ID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _LeaveTypeID = ID;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            _ClearAllErrors();
            _MapToUILeaveType();

            var validation = await _LeaveType.SaveAsync();

            if (!validation.IsValid)
            {
                _ApplyValidationErrors(validation);
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                _Mode = enMode.Update;
                clsMessages.ShowSuccess($"تم حفظ بيانات نوع الإجازة بنجاح والرقم التعريفي هو ({_LeaveType.LeaveTypeID.ToString()})");
                clsUtil.AddNewSystemRecord("اضافة", "اضافة نوع إجازة", $"({_LeaveType.LeaveTypeID.ToString()}) تم اضافة نوع إجازة تحمل الرقم التعريفي");

            }
            else
            {
                clsMessages.ShowSuccess($"تم تعديل بيانات نوع الإجازة بنجاح والرقم التعريفي هو ({_LeaveType.LeaveTypeID.ToString()})");
                clsUtil.AddNewSystemRecord("تعديل", "تعديل نوع الإجازة", $"({_LeaveType.LeaveTypeID.ToString()}) تم تعديل نوع إجازة حالية تحمل الرقم التعريفي");

            }
            this.DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmAddEditLeaveTypes_Load(object sender, EventArgs e)
        {
            _ClearAllErrors();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }


        // ----------------- Methods ---------------------

        private void _MapToUILeaveType()
        {
            _LeaveType.Name = txtLeaveType.Text.Trim();
            _LeaveType.Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim();
            _LeaveType.MaxDaysPerYear = int.TryParse(numericTextBox1.Text.Trim(), out int days) ? (int?)days : null;
            _LeaveType.IsPaid = chkIsPaid.Checked;
            _LeaveType.CreatedDate = DateTime.Now;
            _LeaveType.CreatedByUserID = clsCurrentUser.User.UserID;
        }
        private async void _LoadData()
        {
            _LeaveType = await clsLeaveTypes.FindLeaveTypeByIDAsync(_LeaveTypeID);
            if (_LeaveType == null)
            {
                clsMessages.ShowError($"لايوجد نوع إجازة يحمل الرقم التعريفي التالي ({_LeaveTypeID.ToString()})");
                this.Close();
                return;
            }

            txtLeaveType.Text = _LeaveType.Name;
            txtDescription.Text = _LeaveType.Description ?? null;
            numericTextBox1.Text = _LeaveType.MaxDaysPerYear.ToString() ?? "";
            chkIsPaid.Checked = _LeaveType.IsPaid;
        }
        private void _ClearAllErrors()
        {
            errorProvider1.Clear();
        }
        private void _ApplyValidationErrors(LeaveTypesValidationResult validation)
        {
            _ClearAllErrors();

            foreach (var error in validation.Errors)
            {
                switch (error.FieldName)
                {
                    case "Name":
                        errorProvider1.SetError(txtLeaveType, error.Message);
                        break;

                    case "Description":
                        errorProvider1.SetError(txtDescription, error.Message);
                        break;

                    case "MaxDaysPerYear":
                        errorProvider1.SetError(numericTextBox1, error.Message);
                        break;

                    case "Update":
                        clsMessages.ShowError(error.Message);
                        break;
                }

            }
        }

    }
}
