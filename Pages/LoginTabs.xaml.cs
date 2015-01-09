using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ISNemocniceKlient.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginTabs : UserControl
    {
        public LoginTabs()
        {
            InitializeComponent();
        }

        private void btnOdhlasitSe_Click(object sender, RoutedEventArgs e)
        {
            if (ModernDialog.ShowMessage("Opravdu se chcete odhlásit?", "Odhlášení", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                NemocniceKlient.Instance.OdhlasitUcet();
            }
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            this.btnOdhlasitSe.IsEnabled = (NemocniceKlient.Instance.PrihlasenyUcet != null);
        }
    }
}
