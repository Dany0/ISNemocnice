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
    /// Interaction logic for Tabulky.xaml
    /// </summary>
    public partial class Tabulky : UserControl
    {
        public Tabulky()
        {
            InitializeComponent();
        }

        public CollectionViewSource tbLekariViewSource;
        public CollectionViewSource tbLekarskeZpravyViewSource;
        public CollectionViewSource tbOrdinaceViewSource;
        public CollectionViewSource tbPacientiViewSource;
        public CollectionViewSource tbUctyViewSource;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                tbLekariViewSource = ((CollectionViewSource)(this.Resources["tbLekariViewSource"]));
                tbLekarskeZpravyViewSource = ((CollectionViewSource)(this.Resources["tbLekarskeZpravyViewSource"]));
                tbOrdinaceViewSource = ((CollectionViewSource)(this.Resources["tbOrdinaceViewSource"]));
                tbPacientiViewSource = ((CollectionViewSource)(this.Resources["tbPacientiViewSource"]));
                tbUctyViewSource = ((CollectionViewSource)(this.Resources["tbUctyViewSource"]));

                tbLekariViewSource.Source = IONemocnice.dbNemocniceContext.tbLekari.Execute().ToList();
                tbLekarskeZpravyViewSource.Source = IONemocnice.dbNemocniceContext.tbLekarskeZpravy.Execute().ToList();
                tbOrdinaceViewSource.Source = IONemocnice.dbNemocniceContext.tbOrdinace.Execute().ToList();
                tbPacientiViewSource.Source = IONemocnice.dbNemocniceContext.tbPacienti.Execute().ToList();
                tbUctyViewSource.Source = IONemocnice.dbNemocniceContext.tbUcty.Execute().ToList();
            }
        }
    }
}
