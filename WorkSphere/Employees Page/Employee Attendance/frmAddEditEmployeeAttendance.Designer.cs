namespace WorkSphere.Leave_Types
{
    partial class frmAddEditEmployeeAttendance
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpEmployee = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.ctrlEmployeeCardWithFilter1 = new WorkSphere.Employees_Page.ctrlEmployeeCardWithFilter();
            this.tpAttendance = new System.Windows.Forms.TabPage();
            this.dtpAttendanceDate = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckOutTime = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckInTime = new System.Windows.Forms.DateTimePicker();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.chkIsLate = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnprev = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tpEmployee.SuspendLayout();
            this.tpAttendance.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpEmployee);
            this.tabControl1.Controls.Add(this.tpAttendance);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(956, 837);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tpEmployee
            // 
            this.tpEmployee.BackColor = System.Drawing.Color.White;
            this.tpEmployee.Controls.Add(this.lblTitle);
            this.tpEmployee.Controls.Add(this.btnNext);
            this.tpEmployee.Controls.Add(this.ctrlEmployeeCardWithFilter1);
            this.tpEmployee.Location = new System.Drawing.Point(4, 5);
            this.tpEmployee.Name = "tpEmployee";
            this.tpEmployee.Padding = new System.Windows.Forms.Padding(3);
            this.tpEmployee.Size = new System.Drawing.Size(948, 828);
            this.tpEmployee.TabIndex = 0;
            this.tpEmployee.Text = "الموظف";
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Image = global::WorkSphere.Properties.Resources.left_arrow;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(19, 760);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(166, 50);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "التالي";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ctrlEmployeeCardWithFilter1
            // 
            this.ctrlEmployeeCardWithFilter1.BackColor = System.Drawing.Color.White;
            this.ctrlEmployeeCardWithFilter1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlEmployeeCardWithFilter1.Location = new System.Drawing.Point(7, 113);
            this.ctrlEmployeeCardWithFilter1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ctrlEmployeeCardWithFilter1.Name = "ctrlEmployeeCardWithFilter1";
            this.ctrlEmployeeCardWithFilter1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ctrlEmployeeCardWithFilter1.Size = new System.Drawing.Size(936, 632);
            this.ctrlEmployeeCardWithFilter1.TabIndex = 0;
            this.ctrlEmployeeCardWithFilter1.EmployeeDataLoaded += new System.EventHandler<WorkSphere.Global_Classes.EmployeeidAndStepidEventArgs>(this.ctrlEmployeeCardWithFilter1_EmployeeDataLoaded);
            // 
            // tpAttendance
            // 
            this.tpAttendance.BackColor = System.Drawing.Color.White;
            this.tpAttendance.Controls.Add(this.label4);
            this.tpAttendance.Controls.Add(this.dtpAttendanceDate);
            this.tpAttendance.Controls.Add(this.dtpCheckOutTime);
            this.tpAttendance.Controls.Add(this.dtpCheckInTime);
            this.tpAttendance.Controls.Add(this.txtNotes);
            this.tpAttendance.Controls.Add(this.chkIsLate);
            this.tpAttendance.Controls.Add(this.label5);
            this.tpAttendance.Controls.Add(this.label3);
            this.tpAttendance.Controls.Add(this.label2);
            this.tpAttendance.Controls.Add(this.label1);
            this.tpAttendance.Controls.Add(this.btnprev);
            this.tpAttendance.Controls.Add(this.btnCancel);
            this.tpAttendance.Controls.Add(this.btnSave);
            this.tpAttendance.Location = new System.Drawing.Point(4, 5);
            this.tpAttendance.Name = "tpAttendance";
            this.tpAttendance.Padding = new System.Windows.Forms.Padding(3);
            this.tpAttendance.Size = new System.Drawing.Size(948, 828);
            this.tpAttendance.TabIndex = 1;
            this.tpAttendance.Text = "الحضور و الانصراف";
            // 
            // dtpAttendanceDate
            // 
            this.dtpAttendanceDate.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpAttendanceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAttendanceDate.Location = new System.Drawing.Point(219, 166);
            this.dtpAttendanceDate.Name = "dtpAttendanceDate";
            this.dtpAttendanceDate.Size = new System.Drawing.Size(384, 50);
            this.dtpAttendanceDate.TabIndex = 22;
            // 
            // dtpCheckOutTime
            // 
            this.dtpCheckOutTime.CustomFormat = "HH:mm:ss";
            this.dtpCheckOutTime.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckOutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckOutTime.Location = new System.Drawing.Point(219, 354);
            this.dtpCheckOutTime.Name = "dtpCheckOutTime";
            this.dtpCheckOutTime.ShowUpDown = true;
            this.dtpCheckOutTime.Size = new System.Drawing.Size(384, 50);
            this.dtpCheckOutTime.TabIndex = 21;
            // 
            // dtpCheckInTime
            // 
            this.dtpCheckInTime.CustomFormat = "HH:mm:ss";
            this.dtpCheckInTime.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckInTime.Location = new System.Drawing.Point(219, 266);
            this.dtpCheckInTime.Name = "dtpCheckInTime";
            this.dtpCheckInTime.ShowUpDown = true;
            this.dtpCheckInTime.Size = new System.Drawing.Size(384, 50);
            this.dtpCheckInTime.TabIndex = 20;
            // 
            // txtNotes
            // 
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Location = new System.Drawing.Point(219, 456);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(384, 92);
            this.txtNotes.TabIndex = 19;
            // 
            // chkIsLate
            // 
            this.chkIsLate.AutoSize = true;
            this.chkIsLate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIsLate.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsLate.Location = new System.Drawing.Point(257, 582);
            this.chkIsLate.Name = "chkIsLate";
            this.chkIsLate.Size = new System.Drawing.Size(346, 52);
            this.chkIsLate.TabIndex = 18;
            this.chkIsLate.Text = "هل الموظف متأخر ؟";
            this.chkIsLate.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(609, 354);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(223, 45);
            this.label5.TabIndex = 17;
            this.label5.Text = "وقت الانصراف :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(609, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(209, 45);
            this.label3.TabIndex = 15;
            this.label3.Text = "تاريخ الحضور :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(609, 448);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 45);
            this.label2.TabIndex = 14;
            this.label2.Text = "الملاحظات :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(609, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 45);
            this.label1.TabIndex = 13;
            this.label1.Text = "وقت الحضور :";
            // 
            // btnprev
            // 
            this.btnprev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprev.Image = global::WorkSphere.Properties.Resources.right_arrow;
            this.btnprev.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnprev.Location = new System.Drawing.Point(749, 709);
            this.btnprev.Name = "btnprev";
            this.btnprev.Size = new System.Drawing.Size(162, 50);
            this.btnprev.TabIndex = 10;
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
            this.btnCancel.Location = new System.Drawing.Point(208, 713);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(147, 46);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "الغاء";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::WorkSphere.Properties.Resources.Save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(22, 713);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(147, 46);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTitle.Location = new System.Drawing.Point(282, 13);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTitle.Size = new System.Drawing.Size(420, 96);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "إضافة حضور";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label4.Location = new System.Drawing.Point(237, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(456, 96);
            this.label4.TabIndex = 23;
            this.label4.Text = "بيانات الحضور";
            // 
            // frmAddEditEmployeeAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(956, 837);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddEditEmployeeAttendance";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "اضافة / تعديل حضور ";
            this.Load += new System.EventHandler(this.frmAddEditEmployeeAttendance_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpEmployee.ResumeLayout(false);
            this.tpEmployee.PerformLayout();
            this.tpAttendance.ResumeLayout(false);
            this.tpAttendance.PerformLayout();
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpEmployee;
        private System.Windows.Forms.TabPage tpAttendance;
        private Employees_Page.ctrlEmployeeCardWithFilter ctrlEmployeeCardWithFilter1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnprev;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.CheckBox chkIsLate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpCheckOutTime;
        private System.Windows.Forms.DateTimePicker dtpCheckInTime;
        private System.Windows.Forms.DateTimePicker dtpAttendanceDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label4;
    }
}