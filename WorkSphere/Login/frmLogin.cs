using System;
using System.Windows.Forms;
using WorkSphere.Global_Classes;
using WorkSphere.Properties;
using WorkSphere_Buisness;
using WorkSphere_Buisness.Global_Classes;

namespace WorkSphere.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            clsUsers user = clsUsers.FindUserByUserName(txtUserName.Text.Trim());

            if (user != null && clsSecurity.Verify(txtPassword.Text.Trim(), user.Password))
            {
                if (chkRememberMe.Checked)
                {
                    clsUtil.RememberUserNameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsUtil.DeleteCredentialsFromRegistry();
                }

                if (!user.IsActive)
                {
                    clsMessages.ShowError("حسابك ليس نشط , تواصل مع المسؤول ");
                    return;
                }

                clsCurrentUser.User = user;
                pictureBox2.Image = Resources.Login_gif;

                var userPermissions = await clsUsers.GetUserPermissionsAsync(clsCurrentUser.User.UserID);
                clsAuthorizationCache.LoadPermissions(userPermissions);

                frmMain frm = new frmMain(this);
                frm.Show();
                this.Hide();
            }
            else
            {
                txtUserName.Focus();
                clsMessages.ShowError("اسم مستخدم/كلمة مرور غير صالحة");
                pictureBox2.Image = Resources.Password_gif;
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            string userName = "";
            string password = "";

            if (clsUtil.GetStoredCredential(ref userName, ref password))
            {
                txtUserName.Text = userName;
                txtPassword.Text = password;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
