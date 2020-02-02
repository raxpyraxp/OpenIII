using System.Windows.Forms;

namespace OpenIII.Forms
{
    public partial class BaseWindow : Form
    {
        public BaseWindow()
        {
            InitializeComponent();
        }

        public void CloseWindow()
        {
            Close();
        }
    }
}
