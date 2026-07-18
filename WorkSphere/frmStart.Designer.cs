namespace WorkSphere
{
    partial class frmStart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblState = new System.Windows.Forms.Label();
            this.llSettingConnection = new System.Windows.Forms.LinkLabel();
            this.llCloseProgram = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.timerStart = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(11, 369);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(664, 44);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // lblState
            // 
            this.lblState.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.Location = new System.Drawing.Point(4, 328);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(637, 38);
            this.lblState.TabIndex = 1;
            this.lblState.Text = "جاري الاتصال . . .";
            // 
            // llSettingConnection
            // 
            this.llSettingConnection.AutoSize = true;
            this.llSettingConnection.Location = new System.Drawing.Point(423, 5);
            this.llSettingConnection.Name = "llSettingConnection";
            this.llSettingConnection.Size = new System.Drawing.Size(238, 32);
            this.llSettingConnection.TabIndex = 2;
            this.llSettingConnection.TabStop = true;
            this.llSettingConnection.Text = "تعديل إعدادات الاتصال";
            this.llSettingConnection.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSettingConnection_LinkClicked);
            // 
            // llCloseProgram
            // 
            this.llCloseProgram.AutoSize = true;
            this.llCloseProgram.Location = new System.Drawing.Point(272, 5);
            this.llCloseProgram.Name = "llCloseProgram";
            this.llCloseProgram.Size = new System.Drawing.Size(145, 32);
            this.llCloseProgram.TabIndex = 3;
            this.llCloseProgram.TabStop = true;
            this.llCloseProgram.Text = "إغلاق البرنامج";
            this.llCloseProgram.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCloseProgram_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WorkSphere.Properties.Resources.WorkSphere_Icon;
            this.pictureBox1.Location = new System.Drawing.Point(86, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(529, 305);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // panelSettings
            // 
            this.panelSettings.Controls.Add(this.llSettingConnection);
            this.panelSettings.Controls.Add(this.llCloseProgram);
            this.panelSettings.Location = new System.Drawing.Point(11, 419);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(664, 46);
            this.panelSettings.TabIndex = 5;
            this.panelSettings.Visible = false;
            // 
            // timerStart
            // 
            this.timerStart.Enabled = true;
            this.timerStart.Interval = 5000;
            this.timerStart.Tick += new System.EventHandler(this.timerStart_Tick);
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(689, 478);
            this.Controls.Add(this.panelSettings);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.progressBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStart";
            this.Padding = new System.Windows.Forms.Padding(20, 22, 20, 22);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "aboutBoxStart";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.LinkLabel llSettingConnection;
        private System.Windows.Forms.LinkLabel llCloseProgram;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Timer timerStart;
    }
}
