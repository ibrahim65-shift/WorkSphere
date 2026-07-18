using System;
using System.Windows.Forms;
using WorkSphere.AboutSystem;
using WorkSphere.Departments_Page;
using WorkSphere.Employees_Page;
using WorkSphere.Employees_Page.Employees_Promotions;
using WorkSphere.Global_Classes;
using WorkSphere.Helper;
using WorkSphere.Home_Pages;
using WorkSphere.Leave_Types;
using WorkSphere.Login;
using WorkSphere.PermissionsForRoles;
using WorkSphere.Roles;
using WorkSphere.Salary_Grades_Page;
using WorkSphere.Settings;
using WorkSphere.System_Records_Page;
using WorkSphere.Users_Page;
using WorkSphere_Buisness.Global_Classes;

namespace WorkSphere
{
    public partial class frmMain : Form
    {


        private clsPageHelper _PageHelper;
        private frmLogin _frmLogin;
        public frmMain(frmLogin login)
        {
            InitializeComponent();
            clsPermissionHelper.ApplyToForm(this);
            _frmLogin = login;
            _PageHelper = new clsPageHelper(this);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _SetWindowStatus();
        }
     
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _SaveWindowsStateSettings();
            clsCurrentUser.User = null;
            _frmLogin.Show();
        }
        private void _SetWindowStatus()
        {
            this.WindowState = Properties.Settings.Default.IsMaxScreen ?
                this.WindowState = FormWindowState.Maximized : this.WindowState = FormWindowState.Normal;
        }
        private void _SaveWindowsStateSettings()
        {
            Properties.Settings.Default.IsMaxScreen = this.WindowState == FormWindowState.Maximized ? true : false;
            Properties.Settings.Default.Save();
        }
        private void SalaryGradestoolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(SalaryGradestoolStripMenuItem2.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة سلم الرواتب");
                return;
            }
            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlSalaryGrades, frmMain>(this, f => new ctrlSalaryGrades(f)));
        }
        private void PromotionstoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(PromotionstoolStripMenuItem1.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة الترقيات");
                return;
            }
            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlEmployeePromotions, frmMain>(this, f => new ctrlEmployeePromotions(f)));
        }
        private void ReportstoolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }
        private void DepartmentstoolStripMenuItem10_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(DepartmentstoolStripMenuItem10.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة الأقسام");
                return;
            }
            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlDepartments, frmMain>(this, f => new ctrlDepartments(f)));
        }
        private void RecordstoolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(RecordstoolStripMenuItem6.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة سجل الحركة");
                return;
            }
            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlSystemRecords, frmMain>(this, f => new ctrlSystemRecords(f)));
        }
        private void SettingstoolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(SettingstoolStripMenuItem7.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية للتعديل على إعدادات النظام");
                return;
            }
            pictureBox1.Visible = false;
            using (var frm = new frmSettings())
            {
                frm.ShowDialog();
            }
        }
        private void LeaveTypestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(LeaveTypestoolStripMenuItem1.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة أنواع الإجازات");
                return;
            }
            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlLeaveTypes, frmMain>(this, f => new ctrlLeaveTypes(f)));
        }
        private void LeavestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(LeavestoolStripMenuItem1.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة الإجازات");
                return;
            }
            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlEmployeeLeaves, frmMain>(this, f => new ctrlEmployeeLeaves(f)));
        }
        private void AbsencestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(AbsencestoolStripMenuItem1.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة الغيابات");
                return;
            }
            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlEmployeeAbsences, frmMain>(this, f => new ctrlEmployeeAbsences(f)));
        }
        private void AttendancetoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(AttendancetoolStripMenuItem1.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة الحضور و الانصراف");
                return;
            }
            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlEmployeeAttendance, frmMain>(this, f => new ctrlEmployeeAttendance(f)));
        }
        private void HomePagetoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlHomePage, frmMain>(this, f => new ctrlHomePage(f)));
        }
        private void ManagementEmployeetoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(ManagementEmployeetoolStripMenuItem1.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة الموظفين");
                return;
            }
            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlEmployees, frmMain>(this, f => new ctrlEmployees(f)));
        }

        private void HometoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
        }

        private void PermissionsWithRolestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(PermissionsWithRolestoolStripMenuItem1.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة الصلاحيات للأدوار");
                return;
            }
            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlPermissionForRoles, frmMain>(this, f => new ctrlPermissionForRoles(f)));
        }

        private void UserManagmenttoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(UserManagmenttoolStripMenuItem1.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة المستخدمين");
                return;
            }
            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlUsers, frmMain>(this, f => new ctrlUsers(f)));
        }

        private void RolestoolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!clsAuthorizationCache.HasPermission(RolestoolStripMenuItem2.Tag.ToString()))
            {
                clsMessages.ShowWarning("ليس لديك الصلاحية لعرض صفحة الأدوار");
                return;
            }
            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlRoles, frmMain>(this, f => new ctrlRoles(f)));
        }

        private void AbouttoolStripMenuItem9_Click(object sender, EventArgs e)
        {

            pictureBox1.Visible = false;
            _PageHelper.SetPage(clsPageManager.GetPage<ctrlAboutSystem, frmMain>(this, f => new ctrlAboutSystem()));
        }

       
    }
}
