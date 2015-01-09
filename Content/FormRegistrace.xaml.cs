using FirstFloor.ModernUI.Windows.Controls;
using ISNemocniceKlient.NemocniceDataServiceReference;
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
    public partial class FormRegistrace : UserControl
    {
        public FormRegistrace()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Ucet novyUcet = Ucet.CreateUcet(0,
                this.txtBoxNazevUctu.Text,
                "1234",
                this.txtBoxJmeno.Text,
                this.txtBoxPrijmeni.Text,
                (DateTime)this.datePickerDatumNarozeni.SelectedDate,
                (short)this.comboFunkce.SelectedIndex,
                (bool)this.chckBoxAktivovatPriVytvoreni.IsChecked);
            if (!((bool)this.radioPohlaviZ.IsChecked)) //kdyz N nezaskrtnuto
                novyUcet.Pohlavi = this.radioPohlaviM.IsChecked; //M nezaskrtnuto -> Z zaskrtnuto
            novyUcet.Adresa = this.txtBoxAdresa.Text;
            NemocniceKlient.Instance.RegistrovatUcet(novyUcet);
            MessageBoxResult vysledek = ModernDialog.ShowMessage("Účet byl vytvořen. Chcete se nyní odhlásit?", "Účet byl úspěšně vytvořen.", MessageBoxButton.YesNo);
            if (vysledek == MessageBoxResult.Yes)
            {
                NemocniceKlient.Instance.OdhlasitUcet();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (NemocniceKlient.Instance.PrihlasenyUcet != null && NemocniceKlient.Instance.PrihlasenyUcet.Funkce == 0)
            {
                this.FormValidovat_KeyDown(this, null);
                this.Form.IsEnabled = true;
                this.RegistrationLabelInfo.BBCode = "Upozornění: nový účet má defaultně heslo 1234, upozorněte uživatele aby si jej co nejdříve změnil.";
            }
            else
            {
                this.Form.IsEnabled = false;
                this.RegistrationLabelInfo.BBCode = "Registraci může provést pouze administrátor. Prosím obraťte se na svého správce sítě.";
            }
        }

        private void FormValidovat_KeyDown(object sender, KeyEventArgs e)
        {
            ControlValidator.Instance.ValidovatForm(new List<Control>()
                {
                    this.txtBoxNazevUctu,
                    this.txtBoxJmeno,
                    this.txtBoxPrijmeni,
                    this.datePickerDatumNarozeni,
                    this.comboFunkce,
                    this.txtBoxAdresa
                },
                this.btnDokoncitRegistraci);
        }
    }
}
