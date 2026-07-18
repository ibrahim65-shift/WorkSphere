using System.Linq;
using System.Windows.Forms;

namespace WorkSphere.Helper
{
    public class clsPageHelper
    {
        private readonly frmMain frmMain;
        public clsPageHelper(frmMain main)
        {
            this.frmMain = main;
        }

        public void SetPage(UserControl pageUserControl)
        {
            var oldPage = frmMain.panelContainer.Controls.OfType<UserControl>().FirstOrDefault();

            if (oldPage != null && oldPage != pageUserControl)
            {
                frmMain.panelContainer.Controls.Remove(oldPage);
            }

            if (oldPage != pageUserControl)
            {
                pageUserControl.Dock = DockStyle.Fill;
                frmMain.panelContainer.Controls.Add(pageUserControl);
            }
        }
    }
}
