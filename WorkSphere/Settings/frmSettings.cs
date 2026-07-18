using System;
using System.Configuration;
using System.Windows.Forms;
using WorkSphere.Global_Classes;

namespace WorkSphere.Settings
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }
        private void frmSettings_Load(object sender, EventArgs e)
        {
            _GetGeneralSettings();
            _GetConnectionSettings();
        }

        // -------------------------- General Settings ------------------------
        private void btnSaveGeneral_Click(object sender, EventArgs e)
        {
            try
            {

                Properties.Settings.Default.CompanyName = txtCompanyName.Text.Trim();
                Properties.Settings.Default.NumberOfGridViewItems = (int)numericUpDownItems.Value;
                Properties.Settings.Default.AUTOREFRESH=(double)numericUpDownAutoRefresh.Value;

                Properties.Settings.Default.Save();
                clsMessages.ShowSuccess("تم حفظ إعدادات العامة بنجاح، سيتم إعادة تشغيل التطبيق الآن لتطبيق التغييرات.");
                Application.Restart();
            }
            catch
            {
                clsMessages.ShowError("حدث خطأ أثناء حفظ الإعدادات العامة");
            }
        }

        private void _GetGeneralSettings()
        {
            txtCompanyName.Text = Properties.Settings.Default.CompanyName;
            numericUpDownItems.Value = Properties.Settings.Default.NumberOfGridViewItems;
            numericUpDownAutoRefresh.Value = (int)Properties.Settings.Default.AUTOREFRESH;
        }

        //---------------------- Connection Settings  ------------------

        private void _GetConnectionSettings()
        {
            if (Properties.Settings.Default.ConnectionType == "Local")
            {
                rbLocal.Checked = true;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                numericUpDownConnection.Enabled = false;
            }
            else
            {
                rbNetwork.Checked = true;
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
                numericUpDownConnection.Enabled = true;
            }

            txtServer.Text = Properties.Settings.Default.Server;
            txtDataBase.Text = Properties.Settings.Default.DataBase;
            txtUserName.Text = Properties.Settings.Default.UserName;
            txtPassword.Text = Properties.Settings.Default.Password;
            numericUpDownConnection.Value = Properties.Settings.Default.ConnectionDuration;
        }
        private void btnSaveConnection_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ حفظ الإعدادات في Properties.Settings
                Properties.Settings.Default.ConnectionType = rbLocal.Checked ? "Local" : "Network";
                Properties.Settings.Default.Server = txtServer.Text.Trim();
                Properties.Settings.Default.DataBase = txtDataBase.Text.Trim();
                Properties.Settings.Default.UserName = txtUserName.Text.Trim();
                Properties.Settings.Default.Password = txtPassword.Text.Trim();
                Properties.Settings.Default.ConnectionDuration = (int)numericUpDownConnection.Value;
                Properties.Settings.Default.Save();

                // 2️⃣ بناء نص الاتصال حسب نوع الاتصال
                string connectionString = _BuildConnectionString();

                // 3️⃣ تعديل App.config (ملف التنفيذ)
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.ConnectionStrings.ConnectionStrings["WorkSphereDB"] != null)
                {
                    config.ConnectionStrings.ConnectionStrings["WorkSphereDB"].ConnectionString = connectionString;
                }
                else
                {
                    config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("WorkSphereDB", connectionString, "System.Data.SqlClient"));
                }

                config.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("connectionStrings");

                // 4️⃣ إعلام المستخدم وإعادة تشغيل التطبيق
                clsMessages.ShowSuccess("تم حفظ إعدادات الاتصال بنجاح، سيتم إعادة تشغيل التطبيق الآن لتطبيق التغييرات.");
                Application.Restart();
            }
            catch (Exception ex)
            {
                clsMessages.ShowError("حدث خطأ أثناء حفظ إعدادات الاتصال:\n" + ex.Message);
            }
        }
        private void rbLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLocal.Checked)
            {
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                numericUpDownConnection.Enabled = false;
            }
        }
        private void rbNetwork_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNetwork.Checked)
            {
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
                numericUpDownConnection.Enabled = true;
            }
        }

        private string _BuildConnectionString()
        {
            if (rbLocal.Checked)
            {
                return $"Server={txtServer.Text.Trim()};Database={txtDataBase.Text.Trim()};Integrated Security=True;";
            }
            else
            {
                return $"Server={txtServer.Text.Trim()};Database={txtDataBase.Text.Trim()};User Id={txtUserName.Text.Trim()};Password={txtPassword.Text.Trim()};";
            }
        }
    }
}
