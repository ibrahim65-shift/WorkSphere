namespace WorkSphere.Salary_Grades_Page.Grade_Steps
{
    partial class frmAddEditGradeSteps
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.NtxtBaseSalary = new WorkSphere.Controls.NumericTextBox();
            this.NtxtMinYearsInStep = new WorkSphere.Controls.NumericTextBox();
            this.NtxtAnnualRaise = new WorkSphere.Controls.NumericTextBox();
            this.NtxtStepNumber = new WorkSphere.Controls.NumericTextBox();
            this.lblGradeInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "الحد الادنى للسنوات : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(235, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "نسبة الزيادة السنوية : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "الراتب الاساسي : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 32);
            this.label4.TabIndex = 4;
            this.label4.Text = "رقم الخطوة :";
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::WorkSphere.Properties.Resources.cancel_32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(137, 380);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(156, 50);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "الغاء";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::WorkSphere.Properties.Resources.Save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(359, 380);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(156, 50);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 268);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 32);
            this.label5.TabIndex = 13;
            this.label5.Text = "الدرجة الوظيفية :";
            // 
            // NtxtBaseSalary
            // 
            this.NtxtBaseSalary.AllowDecimal = true;
            this.NtxtBaseSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NtxtBaseSalary.Location = new System.Drawing.Point(241, 83);
            this.NtxtBaseSalary.MaxValue = null;
            this.NtxtBaseSalary.MinValue = null;
            this.NtxtBaseSalary.Name = "NtxtBaseSalary";
            this.NtxtBaseSalary.Size = new System.Drawing.Size(344, 39);
            this.NtxtBaseSalary.TabIndex = 8;
            this.NtxtBaseSalary.Validating += new System.ComponentModel.CancelEventHandler(this.NtxtBaseSalary_Validating);
            // 
            // NtxtMinYearsInStep
            // 
            this.NtxtMinYearsInStep.AllowDecimal = false;
            this.NtxtMinYearsInStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NtxtMinYearsInStep.Location = new System.Drawing.Point(241, 200);
            this.NtxtMinYearsInStep.MaxValue = null;
            this.NtxtMinYearsInStep.MinValue = null;
            this.NtxtMinYearsInStep.Name = "NtxtMinYearsInStep";
            this.NtxtMinYearsInStep.Size = new System.Drawing.Size(344, 39);
            this.NtxtMinYearsInStep.TabIndex = 7;
            this.NtxtMinYearsInStep.Validating += new System.ComponentModel.CancelEventHandler(this.NtxtMinYearsInStep_Validating);
            // 
            // NtxtAnnualRaise
            // 
            this.NtxtAnnualRaise.AllowDecimal = true;
            this.NtxtAnnualRaise.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NtxtAnnualRaise.Location = new System.Drawing.Point(241, 142);
            this.NtxtAnnualRaise.MaxValue = null;
            this.NtxtAnnualRaise.MinValue = null;
            this.NtxtAnnualRaise.Name = "NtxtAnnualRaise";
            this.NtxtAnnualRaise.Size = new System.Drawing.Size(344, 39);
            this.NtxtAnnualRaise.TabIndex = 6;
            this.NtxtAnnualRaise.Validating += new System.ComponentModel.CancelEventHandler(this.NtxtAnnualRaise_Validating);
            // 
            // NtxtStepNumber
            // 
            this.NtxtStepNumber.AllowDecimal = false;
            this.NtxtStepNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NtxtStepNumber.Enabled = false;
            this.NtxtStepNumber.Location = new System.Drawing.Point(241, 27);
            this.NtxtStepNumber.MaxValue = null;
            this.NtxtStepNumber.MinValue = null;
            this.NtxtStepNumber.Name = "NtxtStepNumber";
            this.NtxtStepNumber.Size = new System.Drawing.Size(344, 39);
            this.NtxtStepNumber.TabIndex = 5;
            this.NtxtStepNumber.Validating += new System.ComponentModel.CancelEventHandler(this.NtxtStepNumber_Validating);
            // 
            // lblGradeInfo
            // 
            this.lblGradeInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGradeInfo.Location = new System.Drawing.Point(232, 268);
            this.lblGradeInfo.Name = "lblGradeInfo";
            this.lblGradeInfo.Size = new System.Drawing.Size(353, 43);
            this.lblGradeInfo.TabIndex = 14;
            this.lblGradeInfo.Text = "label6";
            this.lblGradeInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAddEditGradeSteps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 456);
            this.Controls.Add(this.lblGradeInfo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.NtxtBaseSalary);
            this.Controls.Add(this.NtxtMinYearsInStep);
            this.Controls.Add(this.NtxtAnnualRaise);
            this.Controls.Add(this.NtxtStepNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddEditGradeSteps";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddEditGradeSteps";
            this.Load += new System.EventHandler(this.frmAddEditGradeSteps_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Controls.NumericTextBox NtxtStepNumber;
        private Controls.NumericTextBox NtxtAnnualRaise;
        private Controls.NumericTextBox NtxtMinYearsInStep;
        private Controls.NumericTextBox NtxtBaseSalary;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblGradeInfo;
    }
}