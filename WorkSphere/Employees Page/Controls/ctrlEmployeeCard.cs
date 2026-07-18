using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere.Properties;
using WorkSphere_Buisness;

namespace WorkSphere.Employees_Page
{
    public partial class ctrlEmployeeCard : UserControl
    {
        private int _employeeID;
        private int _StepID;
        private clsEmployees _employee;

        public int EmployeeID => _employeeID;
        public int StepID => _employee?.StepID ?? -1;
        public clsEmployees SelectedEmployee => _employee;

        public ctrlEmployeeCard()
        {
            InitializeComponent();
        }

        public async Task LoadEmployeeInfo(int employeeID)
        {
            _employee = await clsEmployees.FindEmployeeByID(employeeID);
            if (_employee == null)
            {
                clsMessages.ShowError($"({employeeID.ToString()}) لايوجد موظف يحمل الرقم التعريفي");
                _ResetDefaultData();
                return;
            }
            _employeeID = employeeID;
            await _FillEmployeeDataAsync();
        }
        public async Task LoadEmployeeInfo(string NationalID)
        {
            _employee = await clsEmployees.FindEmployeeByNationalID(NationalID);
            if (_employee == null)
            {
                clsMessages.ShowError($"({NationalID}) لايوجد موظف يحمل الهوية");
                _ResetDefaultData();
                return;
            }
            _employeeID = _employee.EmployeeID;
            await _FillEmployeeDataAsync();
        }
        private void _ResetDefaultData()
        {
            lblEmployeeID.Text = "????";
            lblFullName.Text = "????";
            lblNationalID.Text = "????";
            lblGender.Text = "????";
            lblIsActive.Text = "????";
            lblPhone.Text = "????";
            lblEmail.Text = "????";
            lblAddress.Text = "????";
            lblBirthDate.Text = "????";
            lblHireDate.Text = "????";
            lblDepartment.Text = "????";
            lblJob.Text = "????";
        }
        private async Task _FillEmployeeDataAsync()
        {
            string fullName = $"{_employee.FirstName} {_employee.SecondName} {_employee.ThirdName} {_employee.LastName}";

            lblEmployeeID.Text = _employee.EmployeeID.ToString();
            lblFullName.Text = fullName;
            lblNationalID.Text = _employee.NationalID;
            lblImageGender.Image = _employee.Gender ? Resources.man_32 : Resources.woman_32;
            lblGender.Text = _employee.Gender ? "ذكر" : "أنثى";
            lblIsActive.Text = _employee.IsActive ? "نعم" : "لا";
            lblPhone.Text = _employee.Phone;
            lblEmail.Text = _employee.Email;
            lblAddress.Text = _employee.Address;
            lblBirthDate.Text = _employee.BirthDate.ToString("dd/MM/yyyy");
            lblHireDate.Text = _employee.HireDate.ToString("dd/MM/yyyy");

            await _employee.EnsureNavigationDepartmentInfoLoadedAsync();
            lblDepartment.Text = _employee.DepartmentInfo.DepartmentName;

            var job = _employee.StepInfo.GradeInfo;
            lblJob.Text = job.Description;


        }
    }
}
