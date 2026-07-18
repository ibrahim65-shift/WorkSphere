using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere_Buisness;

namespace WorkSphere.Employees_Page
{


    public partial class ctrlEmployeeCardWithFilter : UserControl
    {

        public event EventHandler<EmployeeidAndStepidEventArgs> EmployeeDataLoaded;
        private int _employeeID = -1;
        private int _StepID = -1;

        public int EmployeeID => ctrlEmployeeCard1.EmployeeID;
        public int StepID => ctrlEmployeeCard1.StepID;
        public clsEmployees SelectedEmployee
        {
            get { return ctrlEmployeeCard1.SelectedEmployee; }
        }
        public bool Filter
        {
            set => gbFilter.Enabled = value;
        }
        public ctrlEmployeeCardWithFilter()
        {
            InitializeComponent();

        }

        private async Task _FindNow()
        {
            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    {
                        await ctrlEmployeeCard1.LoadEmployeeInfo(int.Parse(numericTextBox1.Text.Trim()));
                        break;
                    }


                case 1:
                    {
                        await ctrlEmployeeCard1.LoadEmployeeInfo(txtSearch.Text.Trim());
                        break;
                    }

            }


            if (SelectedEmployee != null)
            {
                _employeeID = EmployeeID;
                _StepID = StepID;
                EmployeeDataLoaded?.Invoke(this, new EmployeeidAndStepidEventArgs(EmployeeID, StepID));
            }
        }
        public async void LoadEmployeeInfo(int employeeID)
        {
            cbFilter.SelectedIndex = 0;
            numericTextBox1.Text = employeeID.ToString();
            await _FindNow();
        }
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numericTextBox1.Text.Trim()) || !string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                await _FindNow();
            }

        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0)
            {
                numericTextBox1.Visible = true;
                txtSearch.Visible = false;
                numericTextBox1.Clear();
                numericTextBox1.Focus();
            }
            else
            {
                numericTextBox1.Visible = false;
                txtSearch.Visible = true;
                txtSearch.Clear();
                txtSearch.Focus();
            }
        }
        private void numericTextBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(numericTextBox1.Text.Trim()))
            {
                errorProvider1.SetError(numericTextBox1, "! الرجاء ادخال رقم المعرف للبحث");
            }
            else
            {
                errorProvider1.SetError(numericTextBox1, null);
            }
        }
        private void txtSearch_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                errorProvider1.SetError(txtSearch, "! الرجاء ادخال رقم الهوية للبحث");
            }
            else
            {
                errorProvider1.SetError(txtSearch, null);
            }
        }

        private void ctrlEmployeeCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            numericTextBox1.Focus();
        }
    }
}
