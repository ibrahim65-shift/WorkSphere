using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere_Buisness;

namespace WorkSphere.Leave_Types
{
    public partial class frmAddEditEmployeeAbsences : Form
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsEmployeeAbsences _employeeAbsence;
        private int _employeeId;
        private int _absenceID;
        public frmAddEditEmployeeAbsences()
        {
            InitializeComponent();
            lblTitle.Text = "إضافة غياب";
            _Mode = enMode.AddNew;
            _employeeAbsence = new clsEmployeeAbsences();
        }
        public frmAddEditEmployeeAbsences(int absenceID, int empID)
        {
            InitializeComponent();
            lblTitle.Text = "تعديل غياب";
            _Mode = enMode.Update;
            _absenceID = absenceID;
            _employeeId = empID;
        }

        private async void frmAddEditEmployeeAbsences_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                dtpAbsence.MinDate = DateTime.Now;
            }

            if (_Mode == enMode.Update)
            {
                ctrlEmployeeCardWithFilter1.LoadEmployeeInfo(_employeeId);
                ctrlEmployeeCardWithFilter1.Filter = false;
                await _LoadEmployeeAbsenceData();

                if (!_CanEditAbsence())
                {
                    this.Close();
                }

            }
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            _MapToUIEmployeeAbsence();

            var validation = await _employeeAbsence.SaveAsync();

            if (!validation.IsValid)
            {
                _ApplyValidationErrors(validation);
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                _Mode = enMode.Update;
                clsMessages.ShowSuccess($"تم حفظ بيانات الغياب بنجاح والرقم التعريفي هو ({_employeeAbsence.AbsenceID.ToString()})");
                clsUtil.AddNewSystemRecord("غياب", "اضافة غياب", $"({_employeeAbsence.AbsenceID.ToString()}) تم اضافة غياب يحمل الرقم التعريفي");
                clsEventHub.RaiseAddNewAbsence();
            }
            else
            {
                clsMessages.ShowSuccess($"تم تعديل بيانات الغياب بنجاح والرقم التعريفي هو ({_employeeAbsence.AbsenceID.ToString()})");
                clsUtil.AddNewSystemRecord("غياب", "تعديل غياب", $"({_employeeAbsence.AbsenceID.ToString()}) تم تعديل غياب حالي يحمل الرقم التعريفي");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnprev_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tpEmployee;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlEmployeeCardWithFilter1.SelectedEmployee == null)
            {
                clsMessages.ShowError("لايمكن فتح صفحة الغياب , يجب تحميل معلومات الموظف اولا");
                return;
            }

            tabControl1.SelectedTab = tpAbsence;
        }
        private void ctrlEmployeeCardWithFilter1_EmployeeDataLoaded(object sender, EmployeeidAndStepidEventArgs e)
        {
            _employeeId = e.EmployeeID;
        }
        private async void btnShowMissingEmployees_Click(object sender, EventArgs e)
        {
            string fullName;
            string jobDescription;

            try
            {
                var missingEmployeeIDs = await clsEmployeeAbsences.GetEmployeeIDsForAbsenceAsync(DateTime.Now);

                if (missingEmployeeIDs.Any())
                {
                    listBox1.Items.Clear();

                    // طريقة مضمونة مع تقسيم السطور
                    listBox1.Items.Add("═╣ الموظفين الذين لم يحضرو ولم يسجل لهم غياب ╠═══════════════════════════════════════");
                    listBox1.Items.Add("");

                    int counter = 1;
                    foreach (var empID in missingEmployeeIDs)
                    {
                        var employee = await clsEmployees.FindEmployeeByID(empID);
                        if (employee != null)
                        {
                            fullName = $"{employee.FirstName} {employee.SecondName} {employee.ThirdName} {employee.LastName}";
                            jobDescription = employee.StepInfo?.GradeInfo?.Description ?? "غير محدد";

                            await employee.EnsureNavigationDepartmentInfoLoadedAsync();

                            listBox1.Items.Add($"{counter}. المعرف       : {employee.EmployeeID}");
                            listBox1.Items.Add($"   الاسم          : {fullName}");
                            listBox1.Items.Add($"   القسم         : {employee.DepartmentInfo.DepartmentName}");
                            listBox1.Items.Add($"   الوظيفة      : {jobDescription}");
                            listBox1.Items.Add(new string('-', 60));

                            counter++;
                        }
                    }

                    listBox1.Items.Add($"════════════════════════════════════════════════════════════════");
                    listBox1.Items.Add($"الإجمالي: {missingEmployeeIDs.Count} موظف");
                }
                else
                {
                    clsMessages.ShowInfo("لايوجد موظفين لم يسجلو حضور اليوم , الجميع سجل حضور");
                    return;
                }
            }
            catch (Exception ex)
            {
                clsMessages.ShowError(ex.Message);
            }

            btnAutoMarkAbsence.Visible = false;
            btnShowMissingEmployees.Visible = false;
            btnOpenEmployeePage.Visible = false;

            btnPrevOnMainPage.Visible = true;
            listBox1.Visible = true;
        }
        private async void btnAutoMarkAbsence_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show($"هل تريد تسجيل غياب تلقائي لجميع الموظفين غير المسجلين لليوم {DateTime.Today.ToString("dd/MM/yyyy")} ?",
                "تسجيل غياب تلقائي", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    btnAutoMarkAbsence.Enabled = false;
                    btnAutoMarkAbsence.Text = "جاري المعالجة . . . ";

                    bool success = await clsEmployeeAbsences.AutoMarkAbsentEmployeesAsync(DateTime.Now, clsCurrentUser.User.UserID);

                    if (success)
                    {
                        clsMessages.ShowSuccess("تم التسجيل التلقائي للغياب بنجاح");
                        clsEventHub.RaiseAddNewAbsence();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        clsMessages.ShowError("فشل التسجيل التلقائي للغياب");
                    }
                }

            }
            catch (Exception ex)
            {
                clsMessages.ShowError(ex.Message);
            }
            finally
            {
                btnAutoMarkAbsence.Enabled = true;
                btnAutoMarkAbsence.Text = "تسجيل غياب لجميع الموظفين الذين لم يحضرو اليوم";
            }
        }
        private void btnOpenEmployeePage_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tpEmployee;
        }
        private void btnPrevOnMainPage_Click(object sender, EventArgs e)
        {
            btnAutoMarkAbsence.Visible = true;
            btnShowMissingEmployees.Visible = true;
            btnOpenEmployeePage.Visible = true;

            btnPrevOnMainPage.Visible = false;
            listBox1.Visible = false;
        }
        private void btnPrevOnEmployeePage_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tpMain;
        }

        // ----------------------- Methods -------------------

        private void _ApplyValidationErrors(AbsencesValidationResult validation)
        {
            foreach (var error in validation.Errors)
            {
                switch (error.FieldName)
                {
                    case "EmployeeID":
                        clsMessages.ShowError(error.Message);
                        break;

                    case "AbsenceDate":
                        clsMessages.ShowError(error.Message);
                        break;

                    case "Reason":
                        clsMessages.ShowError(error.Message);
                        break;

                    case "Update":
                        clsMessages.ShowError(error.Message);
                        break;


                }

            }
        }
        private void _MapToUIEmployeeAbsence()
        {
            _employeeAbsence.EmployeeID = _employeeId;
            _employeeAbsence.AbsenceDate = dtpAbsence.Value;
            _employeeAbsence.IsExcused = rbExcuse.Checked;
            _employeeAbsence.Reason = string.IsNullOrEmpty(txtReason.Text.Trim()) ? null : txtReason.Text.Trim();

            if (_Mode == enMode.AddNew)
            {
                _employeeAbsence.CreatedDate = DateTime.Now;
                _employeeAbsence.CreatedByUserID = clsCurrentUser.User.UserID;
            }
            else
            {
                _employeeAbsence.EditedDate = DateTime.Now;
                _employeeAbsence.EditedByUserID = clsCurrentUser.User.UserID;
            }
        }
        private async Task _LoadEmployeeAbsenceData()
        {
            _employeeAbsence = await clsEmployeeAbsences.FindEmployeeAbsencesByIDAsync(_absenceID);
            if (_employeeAbsence == null)
            {
                clsMessages.ShowError($"لايوجد غياب يحمل الرقم التعريفي ({_absenceID})");
                this.Close();
                return;
            }

            if (_employeeAbsence.IsExcused)
            {
                rbExcuse.Checked = true;
            }
            else
            {
                rbWithoutExcuse.Checked = true;
            }

            dtpAbsence.Value = _employeeAbsence.AbsenceDate;
            txtReason.Text = _employeeAbsence.Reason ?? "";
        }
        private bool _CanEditAbsence()
        {
            if (_employeeAbsence.AbsenceDate < DateTime.Today.AddDays(-3))
            {
                clsMessages.ShowError("لايمكن تعديل الغياب لقد مضى عليه أكثر من 3 أيام");
                return false;
            }

            return true;
        }


    }
}
