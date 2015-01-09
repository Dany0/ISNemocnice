using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using ISNemocniceKlient.Content;
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

namespace ISNemocniceKlient.Pages
{
    /// <summary>
    /// Interaction logic for Ordinace.xaml
    /// </summary>
    public partial class Ordinace : UserControl
    {
        public Ordinace()
        {
            InitializeComponent();
        }

        public IList<Pacient> LocalDataPacienti { get; set; }
        public IList<LekarskaZprava> LocalDataLekarskeZaznamy { get; set; }

        public Pacient VybranyPacient { get; set; }
        public LekarskaZprava VybranaZprava { get; set; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FormCommunicator.RegisterForm(this, (this.FramePacient.Content as PohledPacient));
            this.LocalDataPacienti = IONemocnice.dbNemocniceContext.tbPacienti.Execute().ToList();
            this.LocalDataLekarskeZaznamy = IONemocnice.dbNemocniceContext.tbLekarskeZpravy.Execute().ToList();
            this.UpdatePacientiListBox();
            this.UpdateLekarskeZaznamyListBox();
        }

        private void UpdatePacientiListBox()
        {
            this.listBoxPacienti.Items.Clear();
            foreach (Pacient pac in this.LocalDataPacienti)
            {
                this.listBoxPacienti.Items.Add(String.Format("{0} {1} | {2} | {3}", pac.Jmeno, pac.Prijmeni, pac.Adresa, pac.RodneCislo));
            }
            this.listBoxPacienti.Items.Add(new ListBoxItem() { Content = "--- Nový pacient ---", HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center });
        }

        private void UpdateLekarskeZaznamyListBox()
        {
            this.listBoxZaznamy.Items.Clear();
            if (this.VybranyPacient != null)
            {
                foreach (LekarskaZprava lz in this.LocalDataLekarskeZaznamy.Where(lz => lz.idPacient == this.VybranyPacient.idPacient))
                {
                    string uzavrenaString = lz.Uzavrena ? "Uzavřeno" : "Neuzaveřeno";
                    this.listBoxZaznamy.Items.Add(String.Format("{0} | {1} : {2}", uzavrenaString, lz.Titulek, lz.DatumDalsiKontroly.ToString()));
                }
            }
        }

        private void listBoxPacienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (sender as ListBox);
            if (listBox.SelectedIndex < listBox.Items.Count - 1 && listBox.SelectedIndex >= 0) // posledni prvek je specialni - "Novy Pacient"
            {
                this.VybranyPacient = IONemocnice.dbNemocniceContext.tbPacienti.ToList().ElementAt(listBox.SelectedIndex);
                (FormCommunicator.GetChild(this) as PohledPacient).ZobrazPacienta(this.VybranyPacient);
                (listBox.Items.GetItemAt(listBox.Items.Count - 1) as ListBoxItem).Content = "--- Nový pacient ---";
            }
            else if (listBox.SelectedIndex == listBox.Items.Count - 1 && listBox.Items.Count > 0) //listBox.Items.Count > 0 bugfix
            {
                this.VybranyPacient = null;
                (listBox.SelectedItem as ListBoxItem).Content = "--- Zadávate nového pacienta ---";
                (FormCommunicator.GetChild(this) as PohledPacient).NovyPacient();
            }
            this.UpdateLekarskeZaznamyListBox();
        }

        public void UpdatePacienta(Pacient pacientVstup)
        {
            if (this.VybranyPacient == null)
            {

            }
            else
            {
                Pacient pacientNaZmeneni = IONemocnice.dbNemocniceContext.tbPacienti.Where(p => p.idPacient == pacientVstup.idPacient).Single();
                pacientNaZmeneni.Jmeno = pacientVstup.Jmeno;
                pacientNaZmeneni.Prijmeni = pacientVstup.Prijmeni;
                pacientNaZmeneni.Pohlavi = pacientVstup.Pohlavi;
                pacientNaZmeneni.RodneCislo = pacientVstup.RodneCislo;
                pacientNaZmeneni.Telefon = pacientVstup.Telefon;
                pacientNaZmeneni.Email = pacientVstup.Email;
                pacientNaZmeneni.DatumNarozeni = pacientVstup.DatumNarozeni;
                pacientNaZmeneni.Adresa = pacientVstup.Adresa;
                pacientNaZmeneni.Poznamka = pacientVstup.Poznamka;
                IONemocnice.dbNemocniceContext.UpdateObject(pacientNaZmeneni);
                IONemocnice.dbNemocniceContext.SaveChanges();
                this.UpdatePacientiListBox();
            }
        }
    }
}
