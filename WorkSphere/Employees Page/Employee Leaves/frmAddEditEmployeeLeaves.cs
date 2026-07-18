using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere_Buisness;
using WorkSphere_Buisness.Global_Classes;

namespace WorkSphere.Leave_Types
{
    public partial class frmAddEditEmployeeLeaves : Form
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsEmployeeLeaves _employeeLeave;
        private int _leaveID;
        private int _EmployeeID;

        public frmAddEditEmployeeLeaves()
        {
            InitializeComponent();
            lblTitle.Text = "إضافة إجازة";
            _Mode = enMode.AddNew;
            _employeeLeave = new clsEmployeeLeaves();
        }
        public frmAddEditEmployeeLeaves(int leaveID, int empID)
        {
            InitializeComponent();
            lblTitle.Text = "تعديل إجازة";
            _Mode = enMode.Update;
            _leaveID = leaveID;
            _EmployeeID = empID;

        }

        private async void frmAddEditEmployeeLeaves_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                dtpStartLeave.MinDate = DateTime.Now;
                dtpEndLeave.MinDate = DateTime.Now.AddDays(1);
            }

            await _FillLeaveTypesCombobox();
            _FillStatusCombobox();

            if (_Mode == enMode.Update)
            {
                ctrlEmployeeCardWithFilter1.LoadEmployeeInfo(_EmployeeID);
                ctrlEmployeeCardWithFilter1.Filter = false;
                await _LoadEmployeeLeaveData();

                if (!_CanEditLeave())
                {
                    this.Close();
                }
            }
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            _ClearAllErrors();
            _MapToUIEmployeeLeave();

            var validation = await _employeeLeave.SaveAsync();

            if (!validation.IsValid)
            {
                _ApplyValidationErrors(validation);
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                _Mode = enMode.Update;
                clsMessages.ShowSuccess($"تم حفظ بيانات الإجازة بنجاح والرقم التعريفي هو ({_employeeLeave.LeaveID.ToString()})");
                clsUtil.AddNewSystemRecord("إجازة", "اضافة إجازة", $"({_employeeLeave.LeaveID.ToString()}) تم اضافة إجازة تحمل الرقم التعريفي");
            }
            else
            {
                clsMessages.ShowSuccess($"تم تعديل بيانات الإجازة بنجاح والرقم التعريفي هو ({_employeeLeave.LeaveID.ToString()})");
                clsUtil.AddNewSystemRecord("إجازة", "تعديل إجازة", $"({_employeeLeave.LeaveID.ToString()}) تم تعديل إجازة حالية تحمل الرقم التعريفي");
            }

            clsEventHub.RaiseLeaveChanged();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlEmployeeCardWithFilter1.SelectedEmployee == null)
            {
                clsMessages.ShowError("لايمكن فتح صفحة الإجازة , يجب تحميل معلومات الموظف اولا");
                return;
            }
            tabControl1.SelectedTab = tpLeave;
        }
        private void btnprev_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tpEmployee;
        }
        private void ctrlEmployeeCardWithFilter1_EmployeeDataLoaded(object sender, EmployeeidAndStepidEventArgs e)
        {
            _EmployeeID = e.EmployeeID;
        }

        // -------------------- Methods -----------------

        private async Task _FillLeaveTypesCombobox()
        {
            List<clsLeaveTypes> data = null;
            try
            {
                data = await clsLeaveTypes.GetAllLeaveTypesAsync();
            }
            catch (Exception ex)
            {
                clsMessages.ShowError(ex.Message);
            }

            if (data == null)
                return;

            cbLeaveType.DataSource = data;
            cbLeaveType.DisplayMember = "Name";
            cbLeaveType.ValueMember = "LeaveTypeID";
            if (cbLeaveType.Items.Count > 0)
                cbLeaveType.SelectedIndex = 0;
        }
        private void _FillStatusCombobox()
        {
            if (!clsAuthorizationCache.HasPermission(cbStatus.Tag?.ToString()))
                return;

            lblStatus.Visible = true;
            cbStatus.Visible = true;

            var Items = new[]
            {
               new {Text = "معلق" , Value  =(byte) clsEmployeeLeaves.enLeaveTypes.Pending},
               new {Text = "موافق" , Value =(byte) clsEmployeeLeaves.enLeaveTypes.Approved},
               new {Text = "مرفوض" , Value =(byte) clsEmployeeLeaves.enLeaveTypes.Rejected}
           }.ToList();

            cbStatus.DataSource = Items;
            cbStatus.DisplayMember = "Text";
            cbStatus.ValueMember = "Value";
            if (cbStatus.Items.Count > 0)
                cbStatus.SelectedIndex = 0;


        }
        private void _ClearAllErrors()
        {
            errorProvider1.Clear();
        }
        private void _ApplyValidationErrors(LeavesValidationResult validation)
        {
            _ClearAllErrors();

            foreach (var error in validation.Errors)
            {
                switch (error.FieldName)
                {
                    case "EmployeeID":
                        clsMessages.ShowError(error.Message);
                        break;
                    case "LeaveTypeID":
                        clsMessages.ShowError(error.Message);
                        break;
                    case "DateRange":
                        errorProvider1.SetError(dtpStartLeave, error.Message);
                        break;
                    case "StartDate":
                        errorProvider1.SetError(dtpStartLeave, error.Message);
                        break;
                    case "MaxDaysPerYear":
                        clsMessages.ShowError(error.Message);
                        break;
                    case "Overlap":
                        clsMessages.ShowError(error.Message);
                        break;
                    case "Update":
                        clsMessages.ShowError(error.Message);
                        break;

                }

            }
        }
        private void _MapToUIEmployeeLeave()
        {
            _employeeLeave.EmployeeID = _EmployeeID;

            if (cbLeaveType.SelectedValue != null && int.TryParse(cbLeaveType.SelectedValue.ToString(), out int leaveID))
            {
                _employeeLeave.LeaveTypeID = leaveID;
            }

            _employeeLeave.StartDate = dtpStartLeave.Value;
            _employeeLeave.EndDate = dtpEndLeave.Value;
            _employeeLeave.Reason = txtReason.Text.Trim() ?? null;

            if (_Mode == enMode.AddNew)
            {
                _employeeLeave.ApprovedByUserID = null;
                _employeeLeave.Status = (byte)clsEmployeeLeaves.enLeaveTypes.Pending;
                _employeeLeave.CreatedDate = DateTime.Now;
                _employeeLeave.CreatedByUserID = clsCurrentUser.User.UserID;
            }

            if (_Mode == enMode.Update)
            {
                byte newStatus;

                if (cbStatus.Visible && cbStatus.SelectedValue != null
                    && byte.TryParse(cbStatus.SelectedValue.ToString(), out byte statusID))
                {
                    newStatus = statusID;
                }
                else
                {
                    newStatus = (byte)clsEmployeeLeaves.enLeaveTypes.Pending;
                }

                _employeeLeave.Status = newStatus;

                if (newStatus == (byte)clsEmployeeLeaves.enLeaveTypes.Pending)
                {
                    _employeeLeave.ApprovedByUserID = null;
                }
                else
                {
                    _employeeLeave.ApprovedByUserID = clsCurrentUser.User.UserID;
                }

                _employeeLeave.EditedDate = DateTime.Now;
                _employeeLeave.EditedByUserID = clsCurrentUser.User.UserID;
            }
        }
        private async Task _LoadEmployeeLeaveData()
        {
            _employeeLeave = await clsEmployeeLeaves.FindEmployeeLeaveByLeaveIDAsync(_leaveID);
            if (_employeeLeave == null)
            {
                clsMessages.ShowError($"لاتوجد إجازة تحمل الرقم التعريفي ({_leaveID})");
                this.Close();
                return;
            }

            if (_employeeLeave.LeaveTypeID > 0)
            {
                if (cbLeaveType.DataSource != null)
                {
                    cbLeaveType.SelectedValue = _employeeLeave.LeaveTypeID;
                }
            }

            if (_employeeLeave.Status > 0)
            {
                if (cbStatus.DataSource != null)
                {
                    cbStatus.SelectedValue = _employeeLeave.Status;
                }
            }

            dtpStartLeave.Value = _employeeLeave.StartDate;
            dtpEndLeave.Value = _employeeLeave.EndDate;
            txtReason.Text = _employeeLeave.Reason;

        }
        private bool _CanEditLeave()
        {
            if (_leaveID <= 0)
                return false;

            if (_employeeLeave.Status == (byte)clsEmployeeLeaves.enLeaveTypes.Pending)
                return true;

            if (_employeeLeave.Status == (byte)clsEmployeeLeaves.enLeaveTypes.Rejected)
            {
                clsMessages.ShowError("لايمكن التعديل على الإجازة لأنها رفضت من المسؤول");
                return false;
            }

            if (_employeeLeave.Status == (byte)clsEmployeeLeaves.enLeaveTypes.Approved &&
               _employeeLeave.StartDate <= DateTime.Today)
            {
                clsMessages.ShowError("لايمكن التعديل على الإجازة لأنها قد بدأت بالفعل");
                return false;
            }

            if (_employeeLeave.EndDate < DateTime.Today)
            {
                clsMessages.ShowError("لايمكن التعديل على الإجازة لأنها قد انتهت بالفعل");
                return false;
            }

            return false;

        }
    }
}
