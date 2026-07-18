namespace WorkSphere.Employees_Page.Employees_Promotions
{
    partial class frmAddEditPromotions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddEditPromotions));
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.dtpPromotionDate = new System.Windows.Forms.DateTimePicker();
            this.cbSteps = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpEmployee = new System.Windows.Forms.TabPage();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.ctrlEmployeeCardWithFilter1 = new WorkSphere.Employees_Page.ctrlEmployeeCardWithFilter();
            this.tpPromotion = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.btnprev = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tpEmployee.SuspendLayout();
            this.tpPromotion.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNotes
            // 
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Location = new System.Drawing.Point(158, 416);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(478, 110);
            this.txtNotes.TabIndex = 5;
            // 
            // dtpPromotionDate
            // 
            this.dtpPromotionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPromotionDate.Location = new System.Drawing.Point(323, 324);
            this.dtpPromotionDate.Name = "dtpPromotionDate";
            this.dtpPromotionDate.Size = new System.Drawing.Size(313, 39);
            this.dtpPromotionDate.TabIndex = 4;
            // 
            // cbSteps
            // 
            this.cbSteps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSteps.FormattingEnabled = true;
            this.cbSteps.Location = new System.Drawing.Point(323, 247);
            this.cbSteps.Name = "cbSteps";
            this.cbSteps.Size = new System.Drawing.Size(313, 40);
            this.cbSteps.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(642, 324);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "تاريخ الترقية :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(642, 400);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "الملاحظات :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(642, 247);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "الخطوة الجديدة : ";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.RightToLeft = true;
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::WorkSphere.Properties.Resources.Save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(21, 642);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 46);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(200, 642);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(147, 46);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "الغاء";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tpEmployee);
            this.tabControl1.Controls.Add(this.tpPromotion);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(950, 875);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 4;
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
            this.tpEmployee.Size = new System.Drawing.Size(942, 866);
            this.tpEmployee.TabIndex = 0;
            this.tpEmployee.Text = "الموظف";
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTitle.Location = new System.Drawing.Point(283, 27);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTitle.Size = new System.Drawing.Size(384, 96);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "إضافة ترقية";
            // 
            // btnNext
            // 
            this.btnNext.Image = global::WorkSphere.Properties.Resources.left_arrow;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(10, 788);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(172, 54);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "التالي";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ctrlEmployeeCardWithFilter1
            // 
            this.ctrlEmployeeCardWithFilter1.BackColor = System.Drawing.Color.White;
            this.ctrlEmployeeCardWithFilter1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlEmployeeCardWithFilter1.Location = new System.Drawing.Point(10, 145);
            this.ctrlEmployeeCardWithFilter1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ctrlEmployeeCardWithFilter1.Name = "ctrlEmployeeCardWithFilter1";
            this.ctrlEmployeeCardWithFilter1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ctrlEmployeeCardWithFilter1.Size = new System.Drawing.Size(936, 632);
            this.ctrlEmployeeCardWithFilter1.TabIndex = 0;
            this.ctrlEmployeeCardWithFilter1.EmployeeDataLoaded += new System.EventHandler<WorkSphere.Global_Classes.EmployeeidAndStepidEventArgs>(this.ctrlEmployeeCardWithFilter1_EmployeeDataLoaded);
            // 
            // tpPromotion
            // 
            this.tpPromotion.BackColor = System.Drawing.Color.White;
            this.tpPromotion.Controls.Add(this.label5);
            this.tpPromotion.Controls.Add(this.btnprev);
            this.tpPromotion.Controls.Add(this.cbSteps);
            this.tpPromotion.Controls.Add(this.label1);
            this.tpPromotion.Controls.Add(this.dtpPromotionDate);
            this.tpPromotion.Controls.Add(this.txtNotes);
            this.tpPromotion.Controls.Add(this.label3);
            this.tpPromotion.Controls.Add(this.btnCancel);
            this.tpPromotion.Controls.Add(this.label2);
            this.tpPromotion.Controls.Add(this.btnSave);
            this.tpPromotion.Location = new System.Drawing.Point(4, 5);
            this.tpPromotion.Name = "tpPromotion";
            this.tpPromotion.Padding = new System.Windows.Forms.Padding(3);
            this.tpPromotion.Size = new System.Drawing.Size(942, 866);
            this.tpPromotion.TabIndex = 1;
            this.tpPromotion.Text = "الترقية";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label5.Location = new System.Drawing.Point(288, 57);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(420, 96);
            this.label5.TabIndex = 8;
            this.label5.Text = "بيانات الترقية";
            // 
            // btnprev
            // 
            this.btnprev.Image = global::WorkSphere.Properties.Resources.right_arrow;
            this.btnprev.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnprev.Location = new System.Drawing.Point(757, 634);
            this.btnprev.Name = "btnprev";
            this.btnprev.Size = new System.Drawing.Size(162, 54);
            this.btnprev.TabIndex = 6;
            this.btnprev.Text = "السابق";
            this.btnprev.UseVisualStyleBackColor = true;
            this.btnprev.Click += new System.EventHandler(this.btnprev_Click);
            // 
            // frmAddEditPromotions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 875);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddEditPromotions";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddEditPromotions";
            this.Load += new System.EventHandler(this.frmAddEditPromotions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tpEmployee.ResumeLayout(false);
            this.tpEmployee.PerformLayout();
            this.tpPromotion.ResumeLayout(false);
            this.tpPromotion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.DateTimePicker dtpPromotionDate;
        private System.Windows.Forms.ComboBox cbSteps;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpEmployee;
        private System.Windows.Forms.TabPage tpPromotion;
        private ctrlEmployeeCardWithFilter ctrlEmployeeCardWithFilter1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnprev;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label5;
    }
}