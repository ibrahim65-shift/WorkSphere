namespace WorkSphere.Salary_Grades_Page
{
    partial class frmAddEditSalaryGrade
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpGradeInfo = new System.Windows.Forms.TabPage();
            this.cbDepartments = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkHasEndDate = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.dtpEffectiveTo = new System.Windows.Forms.DateTimePicker();
            this.dtpEffectiveFrom = new System.Windows.Forms.DateTimePicker();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.ntxtGobGrade = new WorkSphere.Controls.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tpGradeSteps = new System.Windows.Forms.TabPage();
            this.panelState = new System.Windows.Forms.Panel();
            this.lblStateDescription = new System.Windows.Forms.Label();
            this.lblStateTitle = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvGradeSteps = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1.SuspendLayout();
            this.tpGradeInfo.SuspendLayout();
            this.tpGradeSteps.SuspendLayout();
            this.panelState.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGradeSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpGradeInfo);
            this.tabControl1.Controls.Add(this.tpGradeSteps);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1080, 685);
            this.tabControl1.TabIndex = 0;
            // 
            // tpGradeInfo
            // 
            this.tpGradeInfo.Controls.Add(this.cbDepartments);
            this.tpGradeInfo.Controls.Add(this.label5);
            this.tpGradeInfo.Controls.Add(this.chkHasEndDate);
            this.tpGradeInfo.Controls.Add(this.btnCancel);
            this.tpGradeInfo.Controls.Add(this.btnSave);
            this.tpGradeInfo.Controls.Add(this.chkIsActive);
            this.tpGradeInfo.Controls.Add(this.dtpEffectiveTo);
            this.tpGradeInfo.Controls.Add(this.dtpEffectiveFrom);
            this.tpGradeInfo.Controls.Add(this.txtDescription);
            this.tpGradeInfo.Controls.Add(this.ntxtGobGrade);
            this.tpGradeInfo.Controls.Add(this.label4);
            this.tpGradeInfo.Controls.Add(this.label3);
            this.tpGradeInfo.Controls.Add(this.label2);
            this.tpGradeInfo.Controls.Add(this.label1);
            this.tpGradeInfo.Location = new System.Drawing.Point(4, 41);
            this.tpGradeInfo.Name = "tpGradeInfo";
            this.tpGradeInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpGradeInfo.Size = new System.Drawing.Size(1072, 640);
            this.tpGradeInfo.TabIndex = 0;
            this.tpGradeInfo.Text = "معلومات الدرجة";
            this.tpGradeInfo.UseVisualStyleBackColor = true;
            // 
            // cbDepartments
            // 
            this.cbDepartments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepartments.FormattingEnabled = true;
            this.cbDepartments.Location = new System.Drawing.Point(339, 39);
            this.cbDepartments.Name = "cbDepartments";
            this.cbDepartments.Size = new System.Drawing.Size(329, 40);
            this.cbDepartments.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(674, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 32);
            this.label5.TabIndex = 12;
            this.label5.Text = "القسم :";
            // 
            // chkHasEndDate
            // 
            this.chkHasEndDate.AutoSize = true;
            this.chkHasEndDate.Location = new System.Drawing.Point(355, 415);
            this.chkHasEndDate.Name = "chkHasEndDate";
            this.chkHasEndDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkHasEndDate.Size = new System.Drawing.Size(313, 36);
            this.chkHasEndDate.TabIndex = 11;
            this.chkHasEndDate.Text = "هل لدى الدرجة تاريخ نهاية ؟";
            this.chkHasEndDate.UseVisualStyleBackColor = true;
            this.chkHasEndDate.CheckedChanged += new System.EventHandler(this.chkHasEndDate_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = global::WorkSphere.Properties.Resources.cancel_32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(534, 529);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(156, 50);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "الغاء";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::WorkSphere.Properties.Resources.Save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(339, 529);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(156, 50);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Location = new System.Drawing.Point(441, 466);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkIsActive.Size = new System.Drawing.Size(227, 36);
            this.chkIsActive.TabIndex = 8;
            this.chkIsActive.Text = "هل الدرجة نشطة ؟";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // dtpEffectiveTo
            // 
            this.dtpEffectiveTo.Enabled = false;
            this.dtpEffectiveTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEffectiveTo.Location = new System.Drawing.Point(339, 355);
            this.dtpEffectiveTo.Name = "dtpEffectiveTo";
            this.dtpEffectiveTo.Size = new System.Drawing.Size(329, 39);
            this.dtpEffectiveTo.TabIndex = 7;
            // 
            // dtpEffectiveFrom
            // 
            this.dtpEffectiveFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEffectiveFrom.Location = new System.Drawing.Point(339, 285);
            this.dtpEffectiveFrom.Name = "dtpEffectiveFrom";
            this.dtpEffectiveFrom.Size = new System.Drawing.Size(329, 39);
            this.dtpEffectiveFrom.TabIndex = 6;
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Location = new System.Drawing.Point(339, 182);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(329, 74);
            this.txtDescription.TabIndex = 5;
            this.txtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.txtDescription_Validating);
            // 
            // ntxtGobGrade
            // 
            this.ntxtGobGrade.AllowDecimal = false;
            this.ntxtGobGrade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ntxtGobGrade.Location = new System.Drawing.Point(339, 110);
            this.ntxtGobGrade.MaxValue = null;
            this.ntxtGobGrade.MinValue = null;
            this.ntxtGobGrade.Name = "ntxtGobGrade";
            this.ntxtGobGrade.Size = new System.Drawing.Size(329, 39);
            this.ntxtGobGrade.TabIndex = 4;
            this.ntxtGobGrade.Validating += new System.ComponentModel.CancelEventHandler(this.ntxtGobGrade_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(674, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 32);
            this.label4.TabIndex = 3;
            this.label4.Text = "المسمى الوظيفي :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(674, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "ساري من :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(674, 355);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "ساري حتى : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(674, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "الدرجة الوظيفة :";
            // 
            // tpGradeSteps
            // 
            this.tpGradeSteps.Controls.Add(this.panelState);
            this.tpGradeSteps.Controls.Add(this.flowLayoutPanel1);
            this.tpGradeSteps.Controls.Add(this.dgvGradeSteps);
            this.tpGradeSteps.Location = new System.Drawing.Point(4, 41);
            this.tpGradeSteps.Name = "tpGradeSteps";
            this.tpGradeSteps.Padding = new System.Windows.Forms.Padding(3);
            this.tpGradeSteps.Size = new System.Drawing.Size(1072, 640);
            this.tpGradeSteps.TabIndex = 1;
            this.tpGradeSteps.Tag = "GradeSteps.View";
            this.tpGradeSteps.Text = "خطوات الدرجة";
            this.tpGradeSteps.UseVisualStyleBackColor = true;
            // 
            // panelState
            // 
            this.panelState.BackColor = System.Drawing.Color.White;
            this.panelState.Controls.Add(this.lblStateDescription);
            this.panelState.Controls.Add(this.lblStateTitle);
            this.panelState.Location = new System.Drawing.Point(307, 328);
            this.panelState.Name = "panelState";
            this.panelState.Size = new System.Drawing.Size(431, 139);
            this.panelState.TabIndex = 5;
            this.panelState.Visible = false;
            // 
            // lblStateDescription
            // 
            this.lblStateDescription.BackColor = System.Drawing.Color.White;
            this.lblStateDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStateDescription.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStateDescription.ForeColor = System.Drawing.Color.LightGray;
            this.lblStateDescription.Location = new System.Drawing.Point(0, 45);
            this.lblStateDescription.Name = "lblStateDescription";
            this.lblStateDescription.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStateDescription.Size = new System.Drawing.Size(431, 45);
            this.lblStateDescription.TabIndex = 4;
            this.lblStateDescription.Text = "لايوجد بيانات";
            this.lblStateDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStateTitle
            // 
            this.lblStateTitle.BackColor = System.Drawing.Color.White;
            this.lblStateTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStateTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStateTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStateTitle.Location = new System.Drawing.Point(0, 0);
            this.lblStateTitle.Name = "lblStateTitle";
            this.lblStateTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStateTitle.Size = new System.Drawing.Size(431, 45);
            this.lblStateTitle.TabIndex = 3;
            this.lblStateTitle.Text = "لايوجد بيانات";
            this.lblStateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnEdit);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1066, 57);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::WorkSphere.Properties.Resources.Add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.Location = new System.Drawing.Point(948, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(115, 50);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Tag = "GradeSteps.Add";
            this.btnAdd.Text = "اضافة    ";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::WorkSphere.Properties.Resources.Edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.Location = new System.Drawing.Point(827, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(115, 50);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Tag = "GradeSteps.Edit";
            this.btnEdit.Text = "تعديل    ";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::WorkSphere.Properties.Resources.delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.Location = new System.Drawing.Point(706, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(115, 50);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Tag = "GradeSteps.Delete";
            this.btnDelete.Text = "حذف   ";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvGradeSteps
            // 
            this.dgvGradeSteps.AllowUserToAddRows = false;
            this.dgvGradeSteps.AllowUserToDeleteRows = false;
            this.dgvGradeSteps.AllowUserToOrderColumns = true;
            this.dgvGradeSteps.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGradeSteps.BackgroundColor = System.Drawing.Color.White;
            this.dgvGradeSteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGradeSteps.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvGradeSteps.Location = new System.Drawing.Point(3, 67);
            this.dgvGradeSteps.Name = "dgvGradeSteps";
            this.dgvGradeSteps.ReadOnly = true;
            this.dgvGradeSteps.RowHeadersWidth = 62;
            this.dgvGradeSteps.RowTemplate.Height = 28;
            this.dgvGradeSteps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGradeSteps.Size = new System.Drawing.Size(1066, 570);
            this.dgvGradeSteps.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(0, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 26);
            this.txtSearch.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.RightToLeft = true;
            // 
            // frmAddEditSalaryGrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1080, 685);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddEditSalaryGrade";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "اضافة \\ تعديل درجة الراتب";
            this.Load += new System.EventHandler(this.frmAddEditSalaryGrade_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpGradeInfo.ResumeLayout(false);
            this.tpGradeInfo.PerformLayout();
            this.tpGradeSteps.ResumeLayout(false);
            this.panelState.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGradeSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpGradeInfo;
        private System.Windows.Forms.TabPage tpGradeSteps;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.DateTimePicker dtpEffectiveTo;
        private System.Windows.Forms.DateTimePicker dtpEffectiveFrom;
        private System.Windows.Forms.TextBox txtDescription;
        private Controls.NumericTextBox ntxtGobGrade;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvGradeSteps;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panelState;
        private System.Windows.Forms.Label lblStateDescription;
        private System.Windows.Forms.Label lblStateTitle;
        private System.Windows.Forms.CheckBox chkHasEndDate;
        private System.Windows.Forms.ComboBox cbDepartments;
        private System.Windows.Forms.Label label5;
    }
}