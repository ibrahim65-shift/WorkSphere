namespace WorkSphere.Leave_Types
{
    partial class frmAddEditEmployeeLeaves
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
            this.tpEmployee = new System.Windows.Forms.TabPage();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.ctrlEmployeeCardWithFilter1 = new WorkSphere.Employees_Page.ctrlEmployeeCardWithFilter();
            this.tpLeave = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.dtpEndLeave = new System.Windows.Forms.DateTimePicker();
            this.dtpStartLeave = new System.Windows.Forms.DateTimePicker();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.cbLeaveType = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnprev = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1.SuspendLayout();
            this.tpEmployee.SuspendLayout();
            this.tpLeave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tpEmployee);
            this.tabControl1.Controls.Add(this.tpLeave);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(986, 875);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 4;
            // 
            // tpEmployee
            // 
            this.tpEmployee.BackColor = System.Drawing.Color.White;
            this.tpEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpEmployee.Controls.Add(this.lblTitle);
            this.tpEmployee.Controls.Add(this.btnNext);
            this.tpEmployee.Controls.Add(this.ctrlEmployeeCardWithFilter1);
            this.tpEmployee.Location = new System.Drawing.Point(4, 5);
            this.tpEmployee.Name = "tpEmployee";
            this.tpEmployee.Padding = new System.Windows.Forms.Padding(3);
            this.tpEmployee.Size = new System.Drawing.Size(978, 866);
            this.tpEmployee.TabIndex = 0;
            this.tpEmployee.Text = "tabPage1";
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTitle.Location = new System.Drawing.Point(301, 23);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTitle.Size = new System.Drawing.Size(378, 96);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "إضافة إجازة";
            // 
            // btnNext
            // 
            this.btnNext.Image = global::WorkSphere.Properties.Resources.left_arrow;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(32, 775);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(155, 54);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "التالي ";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ctrlEmployeeCardWithFilter1
            // 
            this.ctrlEmployeeCardWithFilter1.BackColor = System.Drawing.Color.White;
            this.ctrlEmployeeCardWithFilter1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlEmployeeCardWithFilter1.Location = new System.Drawing.Point(36, 123);
            this.ctrlEmployeeCardWithFilter1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ctrlEmployeeCardWithFilter1.Name = "ctrlEmployeeCardWithFilter1";
            this.ctrlEmployeeCardWithFilter1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ctrlEmployeeCardWithFilter1.Size = new System.Drawing.Size(936, 632);
            this.ctrlEmployeeCardWithFilter1.TabIndex = 0;
            this.ctrlEmployeeCardWithFilter1.EmployeeDataLoaded += new System.EventHandler<WorkSphere.Global_Classes.EmployeeidAndStepidEventArgs>(this.ctrlEmployeeCardWithFilter1_EmployeeDataLoaded);
            // 
            // tpLeave
            // 
            this.tpLeave.BackColor = System.Drawing.Color.White;
            this.tpLeave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpLeave.Controls.Add(this.label5);
            this.tpLeave.Controls.Add(this.txtReason);
            this.tpLeave.Controls.Add(this.dtpEndLeave);
            this.tpLeave.Controls.Add(this.dtpStartLeave);
            this.tpLeave.Controls.Add(this.cbStatus);
            this.tpLeave.Controls.Add(this.cbLeaveType);
            this.tpLeave.Controls.Add(this.lblStatus);
            this.tpLeave.Controls.Add(this.label4);
            this.tpLeave.Controls.Add(this.label3);
            this.tpLeave.Controls.Add(this.label2);
            this.tpLeave.Controls.Add(this.label1);
            this.tpLeave.Controls.Add(this.btnprev);
            this.tpLeave.Controls.Add(this.btnCancel);
            this.tpLeave.Controls.Add(this.btnSave);
            this.tpLeave.Location = new System.Drawing.Point(4, 5);
            this.tpLeave.Name = "tpLeave";
            this.tpLeave.Padding = new System.Windows.Forms.Padding(3);
            this.tpLeave.Size = new System.Drawing.Size(978, 866);
            this.tpLeave.TabIndex = 1;
            this.tpLeave.Text = "tabPage2";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label5.Location = new System.Drawing.Point(273, 47);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(414, 96);
            this.label5.TabIndex = 20;
            this.label5.Text = "بيانات الإجازة";
            // 
            // txtReason
            // 
            this.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReason.Location = new System.Drawing.Point(284, 453);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(303, 111);
            this.txtReason.TabIndex = 19;
            // 
            // dtpEndLeave
            // 
            this.dtpEndLeave.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndLeave.Location = new System.Drawing.Point(284, 369);
            this.dtpEndLeave.Name = "dtpEndLeave";
            this.dtpEndLeave.Size = new System.Drawing.Size(303, 39);
            this.dtpEndLeave.TabIndex = 18;
            // 
            // dtpStartLeave
            // 
            this.dtpStartLeave.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartLeave.Location = new System.Drawing.Point(284, 289);
            this.dtpStartLeave.Name = "dtpStartLeave";
            this.dtpStartLeave.Size = new System.Drawing.Size(303, 39);
            this.dtpStartLeave.TabIndex = 17;
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(284, 618);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(303, 40);
            this.cbStatus.TabIndex = 16;
            this.cbStatus.Tag = "Leaves.Edit";
            this.cbStatus.Visible = false;
            // 
            // cbLeaveType
            // 
            this.cbLeaveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLeaveType.FormattingEnabled = true;
            this.cbLeaveType.Location = new System.Drawing.Point(284, 210);
            this.cbLeaveType.Name = "cbLeaveType";
            this.cbLeaveType.Size = new System.Drawing.Size(303, 40);
            this.cbLeaveType.TabIndex = 15;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(593, 618);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(155, 38);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "حالة الإجازة :";
            this.lblStatus.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(593, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 38);
            this.label4.TabIndex = 13;
            this.label4.Text = "بداية الإجازة :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(593, 451);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 38);
            this.label3.TabIndex = 12;
            this.label3.Text = "سبب الإجازة :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(593, 370);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 38);
            this.label2.TabIndex = 11;
            this.label2.Text = "نهاية الإجازة :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(593, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 38);
            this.label1.TabIndex = 10;
            this.label1.Text = "نوع الإجازة :";
            // 
            // btnprev
            // 
            this.btnprev.Image = global::WorkSphere.Properties.Resources.right_arrow;
            this.btnprev.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnprev.Location = new System.Drawing.Point(758, 737);
            this.btnprev.Name = "btnprev";
            this.btnprev.Size = new System.Drawing.Size(162, 50);
            this.btnprev.TabIndex = 7;
            this.btnprev.Text = "السابق";
            this.btnprev.UseVisualStyleBackColor = true;
            this.btnprev.Click += new System.EventHandler(this.btnprev_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = global::WorkSphere.Properties.Resources.cancel_32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(217, 741);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(147, 46);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "الغاء";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::WorkSphere.Properties.Resources.Save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(31, 741);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(147, 46);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.RightToLeft = true;
            // 
            // frmAddEditEmployeeLeaves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(986, 875);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddEditEmployeeLeaves";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "اضافة / تعديل إجازة ";
            this.Load += new System.EventHandler(this.frmAddEditEmployeeLeaves_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpEmployee.ResumeLayout(false);
            this.tpEmployee.PerformLayout();
            this.tpLeave.ResumeLayout(false);
            this.tpLeave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpEmployee;
        private Employees_Page.ctrlEmployeeCardWithFilter ctrlEmployeeCardWithFilter1;
        private System.Windows.Forms.TabPage tpLeave;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnprev;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbLeaveType;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.DateTimePicker dtpStartLeave;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.DateTimePicker dtpEndLeave;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label5;
    }
}