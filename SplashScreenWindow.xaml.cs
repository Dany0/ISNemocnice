using ISNemocniceKlient.NemocniceDataServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ISNemocniceKlient
{
    /// <summary>
    /// Interaction logic for SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : Window
    {
        public SplashScreenWindow()
        {
            InitializeComponent();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.InicializovatOkno();
        }

        private DispatcherTimer _casovac = new DispatcherTimer();
        private void InicializovatOkno()
        {
            this.OutputStav(-1);
            this.OutputStav(0);

            if (!this.ValidovatDatabazi())
            {
                this.OutputStav(-2);
                this.Close();
            }

            this.OutputStav(-1);
            
            this._casovac.Interval = TimeSpan.FromSeconds(0);
            this._casovac.Tick += new EventHandler(NacistHlavniOkno);
            this._casovac.Start();
        }

        private void NacistHlavniOkno(object sender, EventArgs e)
        {
            this.OutputStav(42);
            this._casovac.Stop();
            var mainWindow = new MainWindow();
            App.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();
        }

        private void OutputStav(int stav)
        {
            string text = string.Empty;
            switch (stav)
            {
                case -2:
                    text = "Vyskytla se chyba!";
                    break;
                case 0:
                    text = "Prověřuji připojení s databází...";
                    break;
                case 42:
                    text = "Vítejte ve ISNEMOCNICE!";
                    break;
                default:
                    text = "Loading...";
                    break;
            }
            ConsoleLogger.Log(text);
            this.LoadingString.Content = text;
        }

        private bool ValidovatDatabazi()
        {
            bool validita = false;

            try
            {
                var context = new dbNemocniceEntities(new Uri("http://localhost:16980/NemocniceDataService.svc/"));
                context.tbOrdinace.Count();
                validita = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Vyskytla se chyba! Obratte se na nekoho mezi zidli a klavesnici! \r\n {0}", e.Message), "Chyba!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            return validita;
        }
    }
}
