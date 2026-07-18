using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere_Buisness;

namespace WorkSphere.Leave_Types
{
    public partial class frmAddEditEmployeeAttendance : Form
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsEmployeeAttendance _employeeAttendance;
        private int _employeeID;
        private int _attendanceID;

        public frmAddEditEmployeeAttendance()
        {
            InitializeComponent();
            lblTitle.Text = "إضافة حضور";
            _Mode = enMode.AddNew;
            _employeeAttendance = new clsEmployeeAttendance();
        }
        public frmAddEditEmployeeAttendance(int attendanceID, int empID)
        {
            InitializeComponent();
            lblTitle.Text = "تعديل حضور";
            _employeeID = empID;
            _attendanceID = attendanceID;
            _Mode = enMode.Update;
        }


        private async void frmAddEditEmployeeAttendance_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                dtpAttendanceDate.MinDate = DateTime.Now;
                dtpCheckOutTime.Enabled = false;
            }
            else
            {
                dtpCheckInTime.Enabled = false;
                await _LoadEmployeeAttendanceData();
                ctrlEmployeeCardWithFilter1.LoadEmployeeInfo(_employeeID);
                ctrlEmployeeCardWithFilter1.Filter = false;

                if (!_CanEditAttendance())
                {
                    this.Close();
                }
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlEmployeeCardWithFilter1.SelectedEmployee == null)
            {
                clsMessages.ShowError("لايمكن فتح صفحة الحضور و الانصراف , يجب تحميل معلومات الموظف اولا");
                return;
            }

            tabControl1.SelectedTab = tpAttendance;
        }
        private void btnprev_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tpEmployee;
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            _MapToUIEmployeeAttendance();

            var validation = await _employeeAttendance.SaveAsync();

            if (!validation.IsValid)
            {
                _ApplyValidationErrors(validation);
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                _Mode = enMode.Update;
                clsMessages.ShowSuccess($"تم حفظ بيانات الحضور بنجاح والرقم التعريفي هو ({_employeeAttendance.AttendanceID.ToString()})");
                clsUtil.AddNewSystemRecord("حضور", "اضافة حضور", $"({_employeeAttendance.AttendanceID.ToString()}) تم اضافة حضور يحمل الرقم التعريفي");
            }
            else
            {
                clsMessages.ShowSuccess($"تم تعديل بيانات الحضور بنجاح والرقم التعريفي هو ({_employeeAttendance.AttendanceID.ToString()})");
                clsUtil.AddNewSystemRecord("حضور", "تعديل حضور", $"({_employeeAttendance.AttendanceID.ToString()}) تم تعديل حضور حالي يحمل الرقم التعريفي");

            }

            clsEventHub.RaiseAttendaneChanged();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ctrlEmployeeCardWithFilter1_EmployeeDataLoaded(object sender, EmployeeidAndStepidEventArgs e)
        {
            _employeeID = e.EmployeeID;
        }

        // ------------------- Methods --------------------

        private void _MapToUIEmployeeAttendance()
        {
            _employeeAttendance.EmployeeID = _employeeID;
            _employeeAttendance.Notes = !string.IsNullOrEmpty(txtNotes.Text.Trim()) ? txtNotes.Text.Trim() : null;
            _employeeAttendance.AttendanceDate = dtpAttendanceDate.Value;
            _employeeAttendance.IsLate = chkIsLate.Checked;

            if (_Mode == enMode.AddNew)
            {
                _employeeAttendance.CheckInTime = dtpCheckInTime.Value.TimeOfDay;
                _employeeAttendance.CheckOutTime = null;
                _employeeAttendance.CreatedDate = DateTime.Now;
                _employeeAttendance.CreatedByUserID = clsCurrentUser.User.UserID;
            }
            else
            {
                _employeeAttendance.CheckOutTime = dtpCheckOutTime.Value.TimeOfDay;
                _employeeAttendance.EditedDate = DateTime.Now;
                _employeeAttendance.EditedByUserID = clsCurrentUser.User.UserID;
            }

        }
        private void _ApplyValidationErrors(AttendanceValidationResult validation)
        {
            foreach (var error in validation.Errors)
            {
                switch (error.FieldName)
                {
                    case "EmployeeID":
                        clsMessages.ShowError(error.Message);
                        break;
                    case "AttendanceDate":
                        clsMessages.ShowError(error.Message);
                        break;
                    case "CheckOutTime":
                        clsMessages.ShowError(error.Message);
                        break;
                    case "CheckInTime":
                        clsMessages.ShowError(error.Message);
                        break;
                    case "Notes":
                        clsMessages.ShowError(error.Message);
                        break;
                    case "Update":
                        clsMessages.ShowError(error.Message);
                        break;


                }

            }
        }
        private async Task _LoadEmployeeAttendanceData()
        {
            _employeeAttendance = await clsEmployeeAttendance.FindEmployeeAttendanceByIDAsync(_attendanceID);
            if (_employeeAttendance == null)
            {
                clsMessages.ShowError($"لايوجد حضور يحمل الرقم التعريفي ({_attendanceID})");
                this.Close();
                return;
            }

            dtpAttendanceDate.Value = _employeeAttendance.AttendanceDate;
            txtNotes.Text = _employeeAttendance.Notes ?? null;
            chkIsLate.Checked = _employeeAttendance.IsLate;
            dtpCheckInTime.Value = DateTime.Today.Add(_employeeAttendance.CheckInTime as TimeSpan? ?? TimeSpan.FromHours(0));
            dtpCheckOutTime.Value = DateTime.Today.Add(_employeeAttendance.CheckOutTime as TimeSpan? ?? TimeSpan.FromHours(0));
        }
        private bool _CanEditAttendance()
        {

            if (_employeeAttendance.AttendanceDate < DateTime.Today)
            {
                clsMessages.ShowError("لا يمكن تعديل سجل حضور ليوم سابق، البيانات مغلقة.");
                return false;
            }

            if (_employeeAttendance.CheckOutTime != null)
            {
                clsMessages.ShowError("لا يمكن تعديل سجل الحضور بعد تسجيل وقت الانصراف.");
                return false;
            }

            return true;
        }
    }
}
