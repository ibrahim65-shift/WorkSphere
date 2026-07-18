namespace WorkSphere.Settings
{
    partial class frmSettings
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
            this.tpGeneralSettings = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownAutoRefresh = new System.Windows.Forms.NumericUpDown();
            this.btnSaveGeneral = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownItems = new System.Windows.Forms.NumericUpDown();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpConnectionSettings = new System.Windows.Forms.TabPage();
            this.btnSaveConnection = new System.Windows.Forms.Button();
            this.numericUpDownConnection = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDataBase = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbNetwork = new System.Windows.Forms.RadioButton();
            this.rbLocal = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tpGeneralSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownItems)).BeginInit();
            this.tpConnectionSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConnection)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpGeneralSettings);
            this.tabControl1.Controls.Add(this.tpConnectionSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(666, 659);
            this.tabControl1.TabIndex = 0;
            // 
            // tpGeneralSettings
            // 
            this.tpGeneralSettings.BackColor = System.Drawing.Color.White;
            this.tpGeneralSettings.Controls.Add(this.label8);
            this.tpGeneralSettings.Controls.Add(this.numericUpDownAutoRefresh);
            this.tpGeneralSettings.Controls.Add(this.btnSaveGeneral);
            this.tpGeneralSettings.Controls.Add(this.label2);
            this.tpGeneralSettings.Controls.Add(this.numericUpDownItems);
            this.tpGeneralSettings.Controls.Add(this.txtCompanyName);
            this.tpGeneralSettings.Controls.Add(this.label1);
            this.tpGeneralSettings.Location = new System.Drawing.Point(4, 41);
            this.tpGeneralSettings.Name = "tpGeneralSettings";
            this.tpGeneralSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneralSettings.Size = new System.Drawing.Size(658, 614);
            this.tpGeneralSettings.TabIndex = 0;
            this.tpGeneralSettings.Text = "إعدادات عامة";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(238, 347);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(402, 38);
            this.label8.TabIndex = 6;
            this.label8.Text = "الوقت التلقائي لتحديث الصفحات :";
            // 
            // numericUpDownAutoRefresh
            // 
            this.numericUpDownAutoRefresh.Location = new System.Drawing.Point(177, 403);
            this.numericUpDownAutoRefresh.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownAutoRefresh.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAutoRefresh.Name = "numericUpDownAutoRefresh";
            this.numericUpDownAutoRefresh.Size = new System.Drawing.Size(456, 39);
            this.numericUpDownAutoRefresh.TabIndex = 5;
            this.numericUpDownAutoRefresh.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnSaveGeneral
            // 
            this.btnSaveGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveGeneral.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveGeneral.Image = global::WorkSphere.Properties.Resources.Save;
            this.btnSaveGeneral.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveGeneral.Location = new System.Drawing.Point(216, 529);
            this.btnSaveGeneral.Name = "btnSaveGeneral";
            this.btnSaveGeneral.Size = new System.Drawing.Size(231, 57);
            this.btnSaveGeneral.TabIndex = 4;
            this.btnSaveGeneral.Text = "حفظ";
            this.btnSaveGeneral.UseVisualStyleBackColor = true;
            this.btnSaveGeneral.Click += new System.EventHandler(this.btnSaveGeneral_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(349, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(291, 38);
            this.label2.TabIndex = 3;
            this.label2.Text = "عدد العناصر المعروضة :";
            // 
            // numericUpDownItems
            // 
            this.numericUpDownItems.Location = new System.Drawing.Point(177, 261);
            this.numericUpDownItems.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownItems.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownItems.Name = "numericUpDownItems";
            this.numericUpDownItems.Size = new System.Drawing.Size(456, 39);
            this.numericUpDownItems.TabIndex = 2;
            this.numericUpDownItems.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompanyName.Location = new System.Drawing.Point(177, 124);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(456, 39);
            this.txtCompanyName.TabIndex = 1;
            this.txtCompanyName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(488, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "اسم الشركة:";
            // 
            // tpConnectionSettings
            // 
            this.tpConnectionSettings.BackColor = System.Drawing.Color.White;
            this.tpConnectionSettings.Controls.Add(this.btnSaveConnection);
            this.tpConnectionSettings.Controls.Add(this.numericUpDownConnection);
            this.tpConnectionSettings.Controls.Add(this.label7);
            this.tpConnectionSettings.Controls.Add(this.txtPassword);
            this.tpConnectionSettings.Controls.Add(this.label6);
            this.tpConnectionSettings.Controls.Add(this.txtUserName);
            this.tpConnectionSettings.Controls.Add(this.label5);
            this.tpConnectionSettings.Controls.Add(this.txtDataBase);
            this.tpConnectionSettings.Controls.Add(this.label4);
            this.tpConnectionSettings.Controls.Add(this.txtServer);
            this.tpConnectionSettings.Controls.Add(this.label3);
            this.tpConnectionSettings.Controls.Add(this.rbNetwork);
            this.tpConnectionSettings.Controls.Add(this.rbLocal);
            this.tpConnectionSettings.Location = new System.Drawing.Point(4, 41);
            this.tpConnectionSettings.Name = "tpConnectionSettings";
            this.tpConnectionSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpConnectionSettings.Size = new System.Drawing.Size(658, 614);
            this.tpConnectionSettings.TabIndex = 1;
            this.tpConnectionSettings.Text = "إعدادات الاتصال";
            // 
            // btnSaveConnection
            // 
            this.btnSaveConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveConnection.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveConnection.Image = global::WorkSphere.Properties.Resources.Save;
            this.btnSaveConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveConnection.Location = new System.Drawing.Point(280, 534);
            this.btnSaveConnection.Name = "btnSaveConnection";
            this.btnSaveConnection.Size = new System.Drawing.Size(203, 49);
            this.btnSaveConnection.TabIndex = 12;
            this.btnSaveConnection.Text = "حفظ";
            this.btnSaveConnection.UseVisualStyleBackColor = true;
            this.btnSaveConnection.Click += new System.EventHandler(this.btnSaveConnection_Click);
            // 
            // numericUpDownConnection
            // 
            this.numericUpDownConnection.Location = new System.Drawing.Point(180, 477);
            this.numericUpDownConnection.Name = "numericUpDownConnection";
            this.numericUpDownConnection.Size = new System.Drawing.Size(456, 39);
            this.numericUpDownConnection.TabIndex = 11;
            this.numericUpDownConnection.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(399, 432);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(244, 38);
            this.label7.TabIndex = 10;
            this.label7.Text = "فترة الاتصال : (ثانية)";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(180, 386);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(456, 39);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(448, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(195, 38);
            this.label6.TabIndex = 8;
            this.label6.Text = "اسم المستخدم :";
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Location = new System.Drawing.Point(180, 295);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.PasswordChar = '*';
            this.txtUserName.Size = new System.Drawing.Size(456, 39);
            this.txtUserName.TabIndex = 7;
            this.txtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(456, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 38);
            this.label5.TabIndex = 6;
            this.label5.Text = "قاعدة البيانات :";
            // 
            // txtDataBase
            // 
            this.txtDataBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDataBase.Location = new System.Drawing.Point(180, 204);
            this.txtDataBase.Name = "txtDataBase";
            this.txtDataBase.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDataBase.Size = new System.Drawing.Size(456, 39);
            this.txtDataBase.TabIndex = 5;
            this.txtDataBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(502, 341);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 38);
            this.label4.TabIndex = 4;
            this.label4.Text = "كلمة السر :";
            // 
            // txtServer
            // 
            this.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServer.Location = new System.Drawing.Point(180, 113);
            this.txtServer.Name = "txtServer";
            this.txtServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtServer.Size = new System.Drawing.Size(456, 39);
            this.txtServer.TabIndex = 3;
            this.txtServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(536, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 38);
            this.label3.TabIndex = 2;
            this.label3.Text = "السيرفر:";
            // 
            // rbNetwork
            // 
            this.rbNetwork.AutoSize = true;
            this.rbNetwork.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNetwork.Location = new System.Drawing.Point(230, 18);
            this.rbNetwork.Name = "rbNetwork";
            this.rbNetwork.Size = new System.Drawing.Size(111, 42);
            this.rbNetwork.TabIndex = 1;
            this.rbNetwork.Text = "شبكي";
            this.rbNetwork.UseVisualStyleBackColor = true;
            this.rbNetwork.CheckedChanged += new System.EventHandler(this.rbNetwork_CheckedChanged);
            // 
            // rbLocal
            // 
            this.rbLocal.AutoSize = true;
            this.rbLocal.Checked = true;
            this.rbLocal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLocal.Location = new System.Drawing.Point(408, 18);
            this.rbLocal.Name = "rbLocal";
            this.rbLocal.Size = new System.Drawing.Size(106, 42);
            this.rbLocal.TabIndex = 0;
            this.rbLocal.TabStop = true;
            this.rbLocal.Text = "محلي";
            this.rbLocal.UseVisualStyleBackColor = true;
            this.rbLocal.CheckedChanged += new System.EventHandler(this.rbLocal_CheckedChanged);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(666, 659);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmSettings";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "الإعدادات";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpGeneralSettings.ResumeLayout(false);
            this.tpGeneralSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownItems)).EndInit();
            this.tpConnectionSettings.ResumeLayout(false);
            this.tpConnectionSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConnection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpGeneralSettings;
        private System.Windows.Forms.TabPage tpConnectionSettings;
        private System.Windows.Forms.NumericUpDown numericUpDownItems;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveGeneral;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbNetwork;
        private System.Windows.Forms.RadioButton rbLocal;
        private System.Windows.Forms.Button btnSaveConnection;
        private System.Windows.Forms.NumericUpDown numericUpDownConnection;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDataBase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownAutoRefresh;
    }
}