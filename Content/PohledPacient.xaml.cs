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
    /// Interaction logic for PohledPacient.xaml
    /// </summary>
    public partial class PohledPacient : UserControl
    {
        public PohledPacient()
        {
            InitializeComponent();
        }

        public void ZobrazPacienta(Pacient pacientKZobrazeni)
        {
            this.txtBoxJmeno.Text = pacientKZobrazeni.Jmeno;
            this.txtBoxPrijmeni.Text = pacientKZobrazeni.Prijmeni;
            this.txtBoxEmail.Text = pacientKZobrazeni.Email;
            this.txtBoxAdresa.Text = pacientKZobrazeni.Adresa;
            this.txtBoxRodneCislo.Text = pacientKZobrazeni.RodneCislo;
            this.txtBoxTelefon.Text = pacientKZobrazeni.Telefon;
            this.datePickerDatumNarozeni.SelectedDate = pacientKZobrazeni.DatumNarozeni;
            switch (pacientKZobrazeni.Pohlavi)
            {
                case true:
                    this.comboBoxPohlavi.SelectedIndex = 0;
                    break;
                case false:
                    this.comboBoxPohlavi.SelectedIndex = 1;
                    break;
                default:
                    this.comboBoxPohlavi.SelectedIndex = 2;
                    break;

            }
        }

        private void btnUlozit_Click(object sender, RoutedEventArgs e)
        {
            //todo: Ukladani
        }
    }
}
