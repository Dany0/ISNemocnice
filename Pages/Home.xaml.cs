using ISNemocniceKlient.NemocniceDataServiceReference;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Services.Client;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        public CollectionViewSource tbLekariViewSource;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Lekar Honza = new Lekar()
                //{
                //    Jmeno = "Honza",
                //    Prijmeni = "Novak",
                //    DatumNarozeni = DateTime.Now,
                //    Email = "kolace@logi.ka"
                //};
                //Lekar.CreateLekar(
                //IONemocnice.dbNemocniceContext.tbLekari.Add(Honza);
                //IONemocnice.dbNemocniceContext.SaveChanges();

                //IONemocnice.dbNemocniceContext.AddTotbLekari(NemocniceDataServiceReference.Lekar.CreateLekar(0, "Honza", "Tester", DateTime.Now));
                //IONemocnice.dbNemocniceContext.SaveChanges();


                DataServiceQuery<Lekar> tbLekariQuery = IONemocnice.dbNemocniceContext.tbLekari;

                tbLekariViewSource = ((CollectionViewSource)(this.Resources["tbLekariViewSource"]));
                tbLekariViewSource.Source = tbLekariQuery.Execute().ToList();
                //tbLekariViewSource.View.MoveCurrentToFirst();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IONemocnice.dbNemocniceContext.AddTotbLekari(NemocniceDataServiceReference.Lekar.CreateLekar(0, "Button", "Tester", DateTime.Now));
            IONemocnice.dbNemocniceContext.SaveChanges();
            //tbLekariViewSource.View.Refresh();
            tbLekariViewSource.Source = IONemocnice.dbNemocniceContext.tbLekari.Execute().ToList();
            //this.tbLekariDataGrid.Items.Refresh();
        }
    }
}
