

using System;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere_Buisness;


namespace WorkSphere.Departments_Page
{
    public partial class frmAddEditDepartment : Form
    {
        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private clsDepartment _department;
        private int _depID;
        public frmAddEditDepartment()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _department = new clsDepartment();
        }

        public frmAddEditDepartment(int id)
        {
            InitializeComponent();
            _depID = id;
            _Mode = enMode.Update;
        }

        private void frmAddEditDepartment_Load(object sender, EventArgs e)
        {
            _ClearAllErrors();

            if (_Mode == enMode.Update)
            {
                _LoadDepartmentInfo();
            }
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            _ClearAllErrors();
            _MapUIToDepartment();

            var validation = await _department.Save();

            if (!validation.IsValid)
            {
                _ApplyValidationErrors(validation);
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                _Mode = enMode.Update;
                clsMessages.ShowSuccess($"تم حفظ بيانات القسم بنجاح والرقم التعريفي هو ({_department.DepartmentID.ToString()})");
                clsUtil.AddNewSystemRecord("اضافة", "اضافة قسم", $"({_department.DepartmentID.ToString()}) تم اضافة قسم يحمل الرقم التعريفي");
            }
            else
            {
                clsMessages.ShowSuccess($"تم تعديل بيانات القسم بنجاح والرقم التعريفي هو ({_department.DepartmentID.ToString()})");
                clsUtil.AddNewSystemRecord("تعديل", "تعديل قسم", $"({_department.DepartmentID.ToString()}) تم تعديل قسم حالي يحمل الرقم التعريفي");
            }

            this.DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Methods

        private async void _LoadDepartmentInfo()
        {
            _department = await clsDepartment.FindDepartmentByID(_depID);
            if (_department == null)
            {
                clsMessages.ShowError($"لايوجد قسم يحمل الرقم التعريفي ({_depID.ToString()})");
                return;

            }

            txtDepartmenName.Text = _department.DepartmentName;
            txtDescription.Text = _department.Description ?? "";
        }
        private void _ClearAllErrors()
        {
            errorProvider1.Clear();
        }
        private void _ApplyValidationErrors(DepartmentValidationResult validation)
        {
            _ClearAllErrors();
            foreach (var error in validation.Errors)
            {
                switch (error.FieldName)
                {
                    case "DepartmentName":
                        errorProvider1.SetError(txtDepartmenName, error.Message);
                        break;
                    case "Update":
                        clsMessages.ShowError(error.Message);
                        break;
                }

            }
        }
        private void _MapUIToDepartment()
        {
            _department.DepartmentName = txtDepartmenName.Text.Trim();
            _department.Description = txtDescription.Text.Trim();
            _department.CreatedDate = DateTime.Now;
        }

    }
}