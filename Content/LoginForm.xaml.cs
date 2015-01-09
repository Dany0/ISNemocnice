using FirstFloor.ModernUI.Windows.Controls;
using ISNemocniceKlient.Pages;
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

namespace ISNemocniceKlient.Content
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : UserControl
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.PrihlasitUcet();
        }

        private void PrihlasitUcet()
        {
            if (NemocniceKlient.Instance.PrihlasitUcet(this.txtBoxLogin.Text, this.pswdBoxHeslo.Password))
            {
                //MessageBox.Show(String.Format("Uzivatel {0} byl uspesne prihlasen.", this.txtBoxLogin.Text));
                this.pswdBoxHeslo.Password = string.Empty;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this.txtBoxLogin);
            if (NemocniceKlient.Instance.PrihlasenyUcet == null)
            {
                //((LoginTabs)this.Parent).btnOdhlasitSe.IsEnabled = false; //pozor nebezpeci
                //ModernDialog.ShowMessage(((Grid)(((ModernFrame)this.Parent).Parent)).Parent.ToString(), "", MessageBoxButton.OK);
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            ControlValidator.Instance.ValidovatForm(new List<Control>() { this.txtBoxLogin, this.pswdBoxHeslo }, this.btnPrihlasit);
            if (e.Key == Key.Enter)
            {
                this.PrihlasitUcet();
            }
        }
    }
}
