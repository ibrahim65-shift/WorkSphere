using System.Windows.Forms;

namespace WorkSphere.Home_Page.Loading
{
    public partial class frmLoading : Form
    {
        private static frmLoading _LoadingForm;
        private static frmMain _Main;
        public frmLoading()
        {
            InitializeComponent();
            this.Owner = _Main;
        }

        public static frmLoading Instance(frmMain main)
        {
            _Main = main;
            return _LoadingForm ?? (_LoadingForm = new frmLoading());
        }
    }

}
