using System;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere.Login;
using WorkSphere.Settings;

namespace WorkSphere
{
    partial class frmStart : Form
    {
        public frmStart()
        {
            InitializeComponent();

        }

        private void llCloseProgram_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

        private void llSettingConnection_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSettings frm = new frmSettings();
            frm.Show();
        }

        private async void timerStart_Tick(object sender, EventArgs e)
        {
            lblState.Text = "جاري الاتصال . . .";
            bool IsConnected = await clsUtil.CheckDatabaseConnection();

            if (IsConnected)
            {
                timerStart.Enabled = false;

                frmLogin frm = new frmLogin();
                frm.Show();

                this.Hide();

            }
            else
            {
                panelSettings.Visible = true;
                lblState.Text = "فشل الاتصال . . . سنعاود المحاولة مرة أخرى";
            }
        }
    }
}
