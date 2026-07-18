namespace WorkSphere.Leave_Types
{
    partial class frmAddEditEmployeeAbsences
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
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnprev = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.btnPrevOnMainPage = new System.Windows.Forms.Button();
            this.btnOpenEmployeePage = new System.Windows.Forms.Button();
            this.btnAutoMarkAbsence = new System.Windows.Forms.Button();
            this.btnShowMissingEmployees = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tpEmployee = new System.Windows.Forms.TabPage();
            this.btnPrevOnEmployeePage = new System.Windows.Forms.Button();
            this.ctrlEmployeeCardWithFilter1 = new WorkSphere.Employees_Page.ctrlEmployeeCardWithFilter();
            this.tpAbsence = new System.Windows.Forms.TabPage();
            this.dtpAbsence = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.rbWithoutExcuse = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rbExcuse = new System.Windows.Forms.RadioButton();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.tpEmployee.SuspendLayout();
            this.tpAbsence.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Image = global::WorkSphere.Properties.Resources.left_arrow;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(30, 781);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(147, 46);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "التالي";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = global::WorkSphere.Properties.Resources.cancel_32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(205, 701);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(147, 46);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "الغاء";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::WorkSphere.Properties.Resources.Save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(43, 701);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(147, 46);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnprev
            // 
            this.btnprev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprev.Image = global::WorkSphere.Properties.Resources.right_arrow;
            this.btnprev.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnprev.Location = new System.Drawing.Point(778, 701);
            this.btnprev.Name = "btnprev";
            this.btnprev.Size = new System.Drawing.Size(147, 46);
            this.btnprev.TabIndex = 12;
            this.btnprev.Text = "السابق";
            this.btnprev.UseVisualStyleBackColor = true;
            this.btnprev.Click += new System.EventHandler(this.btnprev_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tpMain);
            this.tabControl1.Controls.Add(this.tpEmployee);
            this.tabControl1.Controls.Add(this.tpAbsence);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1033, 858);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 13;
            // 
            // tpMain
            // 
            this.tpMain.BackColor = System.Drawing.Color.White;
            this.tpMain.Controls.Add(this.btnPrevOnMainPage);
            this.tpMain.Controls.Add(this.btnOpenEmployeePage);
            this.tpMain.Controls.Add(this.btnAutoMarkAbsence);
            this.tpMain.Controls.Add(this.btnShowMissingEmployees);
            this.tpMain.Controls.Add(this.listBox1);
            this.tpMain.Location = new System.Drawing.Point(4, 5);
            this.tpMain.Name = "tpMain";
            this.tpMain.Size = new System.Drawing.Size(1025, 826);
            this.tpMain.TabIndex = 2;
            this.tpMain.Text = "الصفحة الرئيسية";
            // 
            // btnPrevOnMainPage
            // 
            this.btnPrevOnMainPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevOnMainPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevOnMainPage.Image = global::WorkSphere.Properties.Resources.right_arrow;
            this.btnPrevOnMainPage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrevOnMainPage.Location = new System.Drawing.Point(863, 763);
            this.btnPrevOnMainPage.Name = "btnPrevOnMainPage";
            this.btnPrevOnMainPage.Size = new System.Drawing.Size(154, 46);
            this.btnPrevOnMainPage.TabIndex = 13;
            this.btnPrevOnMainPage.Text = "السابق";
            this.btnPrevOnMainPage.UseVisualStyleBackColor = true;
            this.btnPrevOnMainPage.Visible = false;
            this.btnPrevOnMainPage.Click += new System.EventHandler(this.btnPrevOnMainPage_Click);
            // 
            // btnOpenEmployeePage
            // 
            this.btnOpenEmployeePage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenEmployeePage.Location = new System.Drawing.Point(132, 255);
            this.btnOpenEmployeePage.Name = "btnOpenEmployeePage";
            this.btnOpenEmployeePage.Size = new System.Drawing.Size(332, 101);
            this.btnOpenEmployeePage.TabIndex = 0;
            this.btnOpenEmployeePage.Text = "تسجيل غياب لموظف لم يحضر اليوم";
            this.btnOpenEmployeePage.UseVisualStyleBackColor = true;
            this.btnOpenEmployeePage.Click += new System.EventHandler(this.btnOpenEmployeePage_Click);
            // 
            // btnAutoMarkAbsence
            // 
            this.btnAutoMarkAbsence.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoMarkAbsence.Location = new System.Drawing.Point(553, 255);
            this.btnAutoMarkAbsence.Name = "btnAutoMarkAbsence";
            this.btnAutoMarkAbsence.Size = new System.Drawing.Size(332, 101);
            this.btnAutoMarkAbsence.TabIndex = 1;
            this.btnAutoMarkAbsence.Text = "تسجيل غياب لجميع الموظفين الذين لم يحضرو اليوم";
            this.btnAutoMarkAbsence.UseVisualStyleBackColor = true;
            this.btnAutoMarkAbsence.Click += new System.EventHandler(this.btnAutoMarkAbsence_Click);
            // 
            // btnShowMissingEmployees
            // 
            this.btnShowMissingEmployees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowMissingEmployees.Location = new System.Drawing.Point(360, 389);
            this.btnShowMissingEmployees.Name = "btnShowMissingEmployees";
            this.btnShowMissingEmployees.Size = new System.Drawing.Size(332, 101);
            this.btnShowMissingEmployees.TabIndex = 2;
            this.btnShowMissingEmployees.Text = "عرض جميع الموظفين الذين لم يحضرو اليوم";
            this.btnShowMissingEmployees.UseVisualStyleBackColor = true;
            this.btnShowMissingEmployees.Click += new System.EventHandler(this.btnShowMissingEmployees_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 32;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1025, 708);
            this.listBox1.TabIndex = 14;
            this.listBox1.Visible = false;
            // 
            // tpEmployee
            // 
            this.tpEmployee.BackColor = System.Drawing.Color.White;
            this.tpEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpEmployee.Controls.Add(this.lblTitle);
            this.tpEmployee.Controls.Add(this.btnPrevOnEmployeePage);
            this.tpEmployee.Controls.Add(this.btnNext);
            this.tpEmployee.Controls.Add(this.ctrlEmployeeCardWithFilter1);
            this.tpEmployee.Location = new System.Drawing.Point(4, 5);
            this.tpEmployee.Name = "tpEmployee";
            this.tpEmployee.Padding = new System.Windows.Forms.Padding(3);
            this.tpEmployee.Size = new System.Drawing.Size(1025, 849);
            this.tpEmployee.TabIndex = 0;
            this.tpEmployee.Text = "البحث عن موظف";
            // 
            // btnPrevOnEmployeePage
            // 
            this.btnPrevOnEmployeePage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevOnEmployeePage.Image = global::WorkSphere.Properties.Resources.right_arrow;
            this.btnPrevOnEmployeePage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrevOnEmployeePage.Location = new System.Drawing.Point(847, 781);
            this.btnPrevOnEmployeePage.Name = "btnPrevOnEmployeePage";
            this.btnPrevOnEmployeePage.Size = new System.Drawing.Size(147, 46);
            this.btnPrevOnEmployeePage.TabIndex = 13;
            this.btnPrevOnEmployeePage.Text = "السابق";
            this.btnPrevOnEmployeePage.UseVisualStyleBackColor = true;
            this.btnPrevOnEmployeePage.Click += new System.EventHandler(this.btnPrevOnEmployeePage_Click);
            // 
            // ctrlEmployeeCardWithFilter1
            // 
            this.ctrlEmployeeCardWithFilter1.BackColor = System.Drawing.Color.White;
            this.ctrlEmployeeCardWithFilter1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlEmployeeCardWithFilter1.Location = new System.Drawing.Point(42, 124);
            this.ctrlEmployeeCardWithFilter1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ctrlEmployeeCardWithFilter1.Name = "ctrlEmployeeCardWithFilter1";
            this.ctrlEmployeeCardWithFilter1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ctrlEmployeeCardWithFilter1.Size = new System.Drawing.Size(936, 641);
            this.ctrlEmployeeCardWithFilter1.TabIndex = 4;
            this.ctrlEmployeeCardWithFilter1.EmployeeDataLoaded += new System.EventHandler<WorkSphere.Global_Classes.EmployeeidAndStepidEventArgs>(this.ctrlEmployeeCardWithFilter1_EmployeeDataLoaded);
            // 
            // tpAbsence
            // 
            this.tpAbsence.BackColor = System.Drawing.Color.White;
            this.tpAbsence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpAbsence.Controls.Add(this.label3);
            this.tpAbsence.Controls.Add(this.dtpAbsence);
            this.tpAbsence.Controls.Add(this.label4);
            this.tpAbsence.Controls.Add(this.rbWithoutExcuse);
            this.tpAbsence.Controls.Add(this.label2);
            this.tpAbsence.Controls.Add(this.rbExcuse);
            this.tpAbsence.Controls.Add(this.txtReason);
            this.tpAbsence.Controls.Add(this.label1);
            this.tpAbsence.Controls.Add(this.btnprev);
            this.tpAbsence.Controls.Add(this.btnCancel);
            this.tpAbsence.Controls.Add(this.btnSave);
            this.tpAbsence.Location = new System.Drawing.Point(4, 5);
            this.tpAbsence.Name = "tpAbsence";
            this.tpAbsence.Padding = new System.Windows.Forms.Padding(3);
            this.tpAbsence.Size = new System.Drawing.Size(1025, 849);
            this.tpAbsence.TabIndex = 1;
            this.tpAbsence.Text = "معلومات الغياب";
            // 
            // dtpAbsence
            // 
            this.dtpAbsence.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAbsence.Location = new System.Drawing.Point(221, 347);
            this.dtpAbsence.Name = "dtpAbsence";
            this.dtpAbsence.Size = new System.Drawing.Size(437, 39);
            this.dtpAbsence.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(664, 330);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 45);
            this.label4.TabIndex = 19;
            this.label4.Text = "تاريخ الغياب :";
            // 
            // rbWithoutExcuse
            // 
            this.rbWithoutExcuse.AutoSize = true;
            this.rbWithoutExcuse.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbWithoutExcuse.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbWithoutExcuse.Location = new System.Drawing.Point(346, 255);
            this.rbWithoutExcuse.Name = "rbWithoutExcuse";
            this.rbWithoutExcuse.Size = new System.Drawing.Size(148, 42);
            this.rbWithoutExcuse.TabIndex = 17;
            this.rbWithoutExcuse.Text = "بدون عذر";
            this.rbWithoutExcuse.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(664, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 45);
            this.label2.TabIndex = 16;
            this.label2.Text = "الغياب :";
            // 
            // rbExcuse
            // 
            this.rbExcuse.AutoSize = true;
            this.rbExcuse.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbExcuse.Checked = true;
            this.rbExcuse.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExcuse.Location = new System.Drawing.Point(565, 255);
            this.rbExcuse.Name = "rbExcuse";
            this.rbExcuse.Size = new System.Drawing.Size(93, 42);
            this.rbExcuse.TabIndex = 15;
            this.rbExcuse.TabStop = true;
            this.rbExcuse.Text = "بعذر";
            this.rbExcuse.UseVisualStyleBackColor = true;
            // 
            // txtReason
            // 
            this.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReason.Location = new System.Drawing.Point(221, 453);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(437, 135);
            this.txtReason.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(664, 427);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 45);
            this.label1.TabIndex = 13;
            this.label1.Text = "سبب الغياب :";
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTitle.Location = new System.Drawing.Point(307, 14);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTitle.Size = new System.Drawing.Size(390, 96);
            this.lblTitle.TabIndex = 14;
            this.lblTitle.Text = "اضافة غياب";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label3.Location = new System.Drawing.Point(305, 72);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(426, 96);
            this.label3.TabIndex = 21;
            this.label3.Text = "بيانات الغياب";
            // 
            // frmAddEditEmployeeAbsences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1033, 858);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddEditEmployeeAbsences";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "اضافة / تعديل غياب ";
            this.Load += new System.EventHandler(this.frmAddEditEmployeeAbsences_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.tpEmployee.ResumeLayout(false);
            this.tpEmployee.PerformLayout();
            this.tpAbsence.ResumeLayout(false);
            this.tpAbsence.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnprev;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpEmployee;
        private System.Windows.Forms.TabPage tpAbsence;
        private Employees_Page.ctrlEmployeeCardWithFilter ctrlEmployeeCardWithFilter1;
        private System.Windows.Forms.RadioButton rbExcuse;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbWithoutExcuse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpAbsence;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tpMain;
        private System.Windows.Forms.Button btnShowMissingEmployees;
        private System.Windows.Forms.Button btnAutoMarkAbsence;
        private System.Windows.Forms.Button btnOpenEmployeePage;
        private System.Windows.Forms.Button btnPrevOnMainPage;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnPrevOnEmployeePage;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label3;
    }
}