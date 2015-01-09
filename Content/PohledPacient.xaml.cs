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

        public ISNemocniceKlient.Pages.Ordinace Rodic { get; set; }
        private int pacientKZobrazeniID { get; set; }
        private string pacientKZobrazeniPoznamka { get; set; }
        private ModernWindow Potomek { get; set; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Rodic = (FormCommunicator.GetParent(this) as ISNemocniceKlient.Pages.Ordinace);
        }

        public void ZobrazPacienta(Pacient pacientKZobrazeni)
        {
            foreach (var ctrl in this.grid.Children)
            {
                (ctrl as Control).IsEnabled = true;
            }

            this.txtBoxJmeno.Text = pacientKZobrazeni.Jmeno;
            this.txtBoxPrijmeni.Text = pacientKZobrazeni.Prijmeni;
            this.txtBoxEmail.Text = pacientKZobrazeni.Email;
            this.txtBoxAdresa.Text = pacientKZobrazeni.Adresa;
            this.txtBoxRodneCislo.Text = pacientKZobrazeni.RodneCislo;
            this.txtBoxTelefon.Text = pacientKZobrazeni.Telefon;
            this.datePickerDatumNarozeni.SelectedDate = pacientKZobrazeni.DatumNarozeni;
            this.pacientKZobrazeniPoznamka = pacientKZobrazeni.Poznamka;
            this.pacientKZobrazeniID = pacientKZobrazeni.idPacient;
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
            bool pohlaviPacienta = true;
            switch(this.comboBoxPohlavi.SelectedIndex)
            {
                case 0:
                    pohlaviPacienta = true;
                    break;
                case 1:
                    pohlaviPacienta = false;
                    break;
            }
            Pacient pacient = NemocniceDataServiceReference.Pacient.CreatePacient(
                this.pacientKZobrazeniID,
                this.txtBoxJmeno.Text,
                this.txtBoxPrijmeni.Text,
                this.txtBoxRodneCislo.Text,
                pohlaviPacienta,
                this.datePickerDatumNarozeni.SelectedDate.Value,
                this.txtBoxAdresa.Text);
            pacient.Poznamka = this.pacientKZobrazeniPoznamka;
            pacient.Telefon = this.txtBoxTelefon.Text;
            pacient.Email = this.txtBoxEmail.Text;
            this.Rodic.UpdatePacienta(pacient);
        }

        public void NovyPacient()
        {
            this.txtBoxJmeno.Text = "Jméno pacienta";
            this.txtBoxPrijmeni.Text = "Příjmení pacienta";
            this.txtBoxEmail.Text = "Email (nepovinné)";
            this.txtBoxAdresa.Text = "Adresa";
            this.txtBoxRodneCislo.Text = "Rodné Číslo";
            this.txtBoxTelefon.Text = "Telefon (nepovinné)";
            this.datePickerDatumNarozeni.SelectedDate = DateTime.Now.AddDays(7);
            this.comboBoxPohlavi.SelectedIndex = 2;
        }

        private void btnPoznamka_Click(object sender, RoutedEventArgs e)
        {
            this.Potomek = new ModernWindow()
            {
                Content = new PohledPacientaPoznamkaDialog(this.pacientKZobrazeniPoznamka) { Margin = new Thickness(32) },
                Style = (Style)App.Current.Resources["EmptyWindow"],
                Width = 480,
                Height = 240
            };
            this.Potomek.Show();
            FormCommunicator.RegisterForm(this, (this.Potomek.Content as PohledPacientaPoznamkaDialog));
        }

        public void ZmenitPoznamku(string novyText)
        {
            this.pacientKZobrazeniPoznamka = novyText;
            this.Potomek.Close();
        }

        private void OnValidateForm(object sender, EventArgs e)
        {
            ControlValidator.Instance.ValidovatForm(new List<Control> { this.txtBoxAdresa, this.txtBoxEmail, this.txtBoxJmeno, this.txtBoxPrijmeni, this.txtBoxRodneCislo, this.txtBoxTelefon, this.datePickerDatumNarozeni, this.comboBoxPohlavi }, this.btnUlozit);
        }
    }
}
