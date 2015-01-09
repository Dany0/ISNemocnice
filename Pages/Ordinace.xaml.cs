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
            this.LocalDataPacienti = IONemocnice.dbNemocniceContext.tbPacienti.Execute().ToList();
            this.LocalDataLekarskeZaznamy = IONemocnice.dbNemocniceContext.tbLekarskeZpravy.Execute().ToList();
            this.UpdatePacienti();
            this.UpdateLekarskeZaznamy();
        }

        private void UpdatePacienti()
        {
            foreach (Pacient pac in this.LocalDataPacienti)
            {
                this.listBoxPacienti.Items.Add(String.Format("{0} {1} | {2} | {3}", pac.Jmeno, pac.Prijmeni, pac.Adresa, pac.RodneCislo));
            }
        }

        private void UpdateLekarskeZaznamy()
        {
            foreach (LekarskaZprava lz in this.LocalDataLekarskeZaznamy)
            {
                this.listBoxZaznamy.Items.Add(String.Format("{0} {1}", lz.Titulek, lz.Uzavrena.ToString()));
            }
        }

        private void listBoxPacienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.VybranyPacient = IONemocnice.dbNemocniceContext.tbPacienti.ToList().ElementAt(((ListBox)sender).SelectedIndex);
            ((PohledPacient)this.FramePacient.Content).ZobrazPacienta(this.VybranyPacient);
        }
    }
}
