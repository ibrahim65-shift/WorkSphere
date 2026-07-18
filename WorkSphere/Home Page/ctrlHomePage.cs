using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere.Helper;
using WorkSphere.Leave_Types;
using WorkSphere.Settings;
using WorkSphere_Buisness;
using WorkSphere_Buisness.Global_Classes;

namespace WorkSphere.Home_Pages
{
    public partial class ctrlHomePage : UserControl
    {
        private clsPageHelper _PageHelper;
        private frmMain _frmMain;

        public ctrlHomePage(frmMain main)
        {
            InitializeComponent();
            _frmMain = main;
            _PageHelper = new clsPageHelper(_frmMain);
            SubscribeOnEvents();
        }
        private async void ctrlHomePage_Load(object sender, EventArgs e)
        {
            lblCompanyName.Text = Properties.Settings.Default.CompanyName;
            lblUserName.Text = clsCurrentUser.User.FullName;
            await LoadDataAsync();
        }
        private void btnAttendance_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnAttendance.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة الحضور و الانصراف");
                return;
            }
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlEmployeeAttendance, frmMain>(_frmMain, f => new ctrlEmployeeAttendance(f)));
        }
        private void btnAbsence_Click(object sender, EventArgs e)
        {

            if (!clsAuthorizationCache.HasPermission(btnAbsence.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة الغيابات");
                return;
            }
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlEmployeeAbsences, frmMain>(_frmMain, f => new ctrlEmployeeAbsences(f)));
        }
        private void btnLeaves_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnLeaves.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة الإجازات");
                return;
            }
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlLeaveTypes, frmMain>(_frmMain, f => new ctrlLeaveTypes(f)));
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(btnSettings.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية للتعديل على إعدادات النظام");
                return;
            }
            using (var frm = new frmSettings())
            {
                frm.ShowDialog();
            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            clsAuthorizationCache.Clear();
            _frmMain.Close();
        }

        // ------------------- Events ------------------
        private void SubscribeOnEvents()
        {
            clsEventHub.AttendanceChanged += OnAttendanceChanged;
            clsEventHub.AddNewEmployee += OnAddNewEmployee;
            clsEventHub.AddNewAbsence += OnAddNewAbsence;
            clsEventHub.LeaveChanged += OnLeaveChanged;

            this.Disposed += (s, e) =>
            {
                clsEventHub.AttendanceChanged -= OnAttendanceChanged;
                clsEventHub.AddNewEmployee -= OnAddNewEmployee;
                clsEventHub.AddNewAbsence -= OnAddNewAbsence;
                clsEventHub.LeaveChanged -= OnLeaveChanged;
            };
        }
        private async void OnAttendanceChanged(object sender, BoolEventArgs args)
        {
            if (!args.result)
                return;
            try
            {
                lblAttendanceEmployees.Text = (await clsEmployeeAttendance.GetNumberOfAttendanceEmployeesAsync()).ToString();
                lblLateEmployees.Text = (await clsEmployeeAttendance.GetNumberOfLateEmployeesAsync()).ToString();
            }
            catch
            {
                clsMessages.ShowError("حدث خطأ أثناء تحديث عدد الحضور و التأخير");
            }
        }
        private async void OnAddNewEmployee(object sender, BoolEventArgs args)
        {
            if (!args.result)
                return;

            try
            {
                lblEmployees.Text = (await clsEmployees.GetNumberOfEmployeesAsync()).ToString();
            }
            catch
            {
                clsMessages.ShowError("حدث خطأ أثناء تحديث عدد الموظفين");
            }
        }
        private async void OnAddNewAbsence(object sender, BoolEventArgs args)
        {
            if (!args.result)
                return;

            try
            {
                lblAbsenceEmployees.Text = (await clsEmployeeAbsences.GetNumberOfAbsenceEmployees()).ToString();
            }
            catch
            {
                clsMessages.ShowError("حدث خطأ أثناء تحديث عدد الغياب");
            }
        }
        private async void OnLeaveChanged(object sender, BoolEventArgs args)
        {
            if (!args.result)
                return;

            try
            {
                lblCurrentLeaves.Text = (await clsEmployeeLeaves.GetNumberOfCurrentLeavesAsync()).ToString();
                lblPendingLeaves.Text = (await clsEmployeeLeaves.GetNumberOfPendingLeavesAsync()).ToString();
            }
            catch
            {
                clsMessages.ShowError("حدث خطأ أثناء تحديث عدد الإجازات الحالية و المعلقة");
            }
        }

        // --------------- Methods ---------------------
        private async Task LoadDataAsync()
        {
            try
            {
                var employees = clsEmployees.GetNumberOfEmployeesAsync();
                var attendanceEmployees = clsEmployeeAttendance.GetNumberOfAttendanceEmployeesAsync();
                var lateEmployees = clsEmployeeAttendance.GetNumberOfLateEmployeesAsync();
                var absenceEmployees = clsEmployeeAbsences.GetNumberOfAbsenceEmployees();
                var currentLeaves = clsEmployeeLeaves.GetNumberOfCurrentLeavesAsync();
                var pendingLeaves = clsEmployeeLeaves.GetNumberOfPendingLeavesAsync();

                await Task.WhenAll(employees, attendanceEmployees, lateEmployees, absenceEmployees, currentLeaves, pendingLeaves);

                lblEmployees.Text = employees.Result.ToString();
                lblAttendanceEmployees.Text = attendanceEmployees.Result.ToString();
                lblLateEmployees.Text = lateEmployees.Result.ToString();
                lblAbsenceEmployees.Text = absenceEmployees.Result.ToString();
                lblCurrentLeaves.Text = currentLeaves.Result.ToString();
                lblPendingLeaves.Text = pendingLeaves.Result.ToString();
            }
            catch
            {
                clsMessages.ShowError("حدث خطأ أثناء تحميل البيانات");
            }
        }


    }
}
