using FirstFloor.ModernUI.Presentation;
using ISNemocniceKlient.NemocniceDataServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ISNemocniceKlient
{
    public sealed class NemocniceKlient
    {
        private static readonly NemocniceKlient _instance = new NemocniceKlient();

        private NemocniceKlient() { }

        public static NemocniceKlient Instance
        {
            get
            {
                return _instance;
            }
        }
        
        private Ucet _prihlasenyUcet;

        public Ucet PrihlasenyUcet
        {
            get { return _prihlasenyUcet; }
            set { _prihlasenyUcet = value; }
        }

        public void RegistrovatUcet(Ucet novyUcet)
        {
            IONemocnice.dbNemocniceContext.AddTotbUcty(novyUcet);
            IONemocnice.dbNemocniceContext.SaveChanges();
            ConsoleLogger.Log(String.Format("Vytvoren ucet {0} pro {1} {2}. Nezapomente zmenit heslo.", novyUcet.NazevUctu, novyUcet.Jmeno, novyUcet.Prijmeni));
        }

        private int _pokusyOPrihlaseni = 1;
        
        public bool PrihlasitUcet(string login, string heslo)
        {
            var vyberUctu = IONemocnice.dbNemocniceContext.tbUcty.Where(u => (u.NazevUctu == login) && (u.Heslo == heslo) && (u.Aktivovany)).ToList();

            if (vyberUctu.Count() == 1)
            {
                this.PrihlasenyUcet = vyberUctu.ElementAt(0);
                ((MainWindow)Application.Current.MainWindow).LogoData = Geometry.Parse("F1 M 24.9015,43.0378L 25.0963,43.4298C 26.1685,49.5853 31.5377,54.2651 38,54.2651C 44.4623,54.2651 49.8315,49.5854 50.9037,43.4299L 51.0985,43.0379C 51.0985,40.7643 52.6921,39.2955 54.9656,39.2955C 56.9428,39.2955 58.1863,41.1792 58.5833,43.0379C 57.6384,52.7654 47.9756,61.75 38,61.75C 28.0244,61.75 18.3616,52.7654 17.4167,43.0378C 17.8137,41.1792 19.0572,39.2954 21.0344,39.2954C 23.3079,39.2954 24.9015,40.7643 24.9015,43.0378 Z M 26.7727,20.5833C 29.8731,20.5833 32.3864,23.0966 32.3864,26.197C 32.3864,29.2973 29.8731,31.8106 26.7727,31.8106C 23.6724,31.8106 21.1591,29.2973 21.1591,26.197C 21.1591,23.0966 23.6724,20.5833 26.7727,20.5833 Z M 49.2273,20.5833C 52.3276,20.5833 54.8409,23.0966 54.8409,26.197C 54.8409,29.2973 52.3276,31.8106 49.2273,31.8106C 46.127,31.8106 43.6136,29.2973 43.6136,26.197C 43.6136,23.0966 46.127,20.5833 49.2273,20.5833 Z");
                ConsoleLogger.Log(String.Format("Prihlasen ucet {0}. {1} pokusu o prihlaseni.", login, this._pokusyOPrihlaseni));
                this._pokusyOPrihlaseni = 1;

                ((MainWindow)(Application.Current.MainWindow)).TitleLinks.Add(new Link { DisplayName = "Statistiky", Source = new Uri(@"/Pages/Statistika.xaml", UriKind.Relative) });

                ((MainWindow)(Application.Current.MainWindow)).MenuLinkGroups.Add(
                    new LinkGroup
                    {
                        DisplayName = "Moje Ordinace",
                        Links = { new Link { DisplayName = "Ordinace", Source = new Uri(@"/Pages/Ordinace.xaml", UriKind.Relative) } }
                    }
                    );

                ((MainWindow)(Application.Current.MainWindow)).MenuLinkGroups.Add(
                    new LinkGroup
                    {
                        DisplayName = "Tabulky",
                        Links = { new Link { DisplayName = "Tabulky", Source = new Uri(@"/Pages/Tabulky.xaml", UriKind.Relative) } }
                    }
                    );
                return true;
            }
            else
            {
                vyberUctu = IONemocnice.dbNemocniceContext.tbUcty.Where(u => (u.NazevUctu == login) && (u.Aktivovany)).ToList();
                if (vyberUctu.Count() > 0)
                {
                    this._pokusyOPrihlaseni++;
                    ConsoleLogger.Log(String.Format("Nepodareny pokus o prihlaseni uctu {0}. Pravdepodobne spatne heslo. {1} pokusu o prihlaseni.", login, this._pokusyOPrihlaseni));
                }
                vyberUctu = IONemocnice.dbNemocniceContext.tbUcty.Where(u => (u.NazevUctu == login) && (!u.Aktivovany)).ToList();
                if (vyberUctu.Count() > 0)
                {
                    this._pokusyOPrihlaseni++;
                    ConsoleLogger.Log(String.Format("Nepodareny pokus o prihlaseni uctu {0}. Ucet zrejme neni aktivovany. {1} pokusu o prihlaseni.", login, this._pokusyOPrihlaseni));
                }
            }
            return false;
        }

        public void OdhlasitUcet()
        {
            ((MainWindow)Application.Current.MainWindow).LogoData = Geometry.Parse("F1 M 24.9015,54.3611C 24.9015,56.1567 23.3079,57.3167 21.0343,57.3167C 19.0572,57.3167 17.8137,55.829 17.4167,54.3611C 18.3616,46.6788 28.0244,39.5833 38,39.5833C 47.9756,39.5833 57.6384,46.6788 58.5833,54.3611C 58.1863,55.829 56.9428,57.3166 54.9656,57.3166C 52.6921,57.3166 51.0985,56.1566 51.0985,54.3611L 50.9037,54.0515C 49.8315,49.1903 44.4623,46.2861 38,46.2861C 31.5377,46.2861 26.1685,49.1903 25.0963,54.0515L 24.9015,54.3611 Z M 33.25,26.9167C 33.25,31.2889 30.7689,34.8333 27.7083,34.8333C 24.6478,34.8333 22.1667,31.2889 22.1667,26.9167L 28.5,19.0802C 31.1856,19.6289 33.25,22.9284 33.25,26.9167 Z M 42.75,26.9167C 42.75,22.9284 44.8144,19.6289 47.5,19.0802L 53.8333,26.9167C 53.8333,31.2889 51.3522,34.8333 48.2917,34.8333C 45.2311,34.8333 42.75,31.2889 42.75,26.9167 Z ");
            ConsoleLogger.Log(String.Format("Odhlasen ucet {0}.", this.PrihlasenyUcet.NazevUctu));
            ((MainWindow)(Application.Current.MainWindow)).TitleLinks.Remove(((MainWindow)(Application.Current.MainWindow)).TitleLinks.Where(l => l.DisplayName == "Statistiky").ElementAt(0));
            ((MainWindow)(Application.Current.MainWindow)).MenuLinkGroups.Remove(((MainWindow)(Application.Current.MainWindow)).MenuLinkGroups.Where(l => l.DisplayName == "Tabulky").ElementAt(0));
            ((MainWindow)(Application.Current.MainWindow)).MenuLinkGroups.Remove(((MainWindow)(Application.Current.MainWindow)).MenuLinkGroups.Where(l => l.DisplayName == "Moje Ordinace").ElementAt(0));
            this.PrihlasenyUcet = null;
        }

        public string FunkcePrihlasenehoUctuString()
        {
            switch (this.PrihlasenyUcet.Funkce)
            {
                case 0:
                    return "Admin";
                case 1:
                    return "Lékař";
                case 2:
                    return "Stážista";
                default:
                    return "Chyba!";
            }
        }
    }
}