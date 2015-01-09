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
    /// Interaction logic for Statistika.xaml
    /// </summary>
    public partial class Statistika : UserControl
    {
        public Statistika()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.ZaplnitStatistiky();
        }

        private void ZaplnitStatistiky()
        {
            this.bbCodeBlockStatistikyNemocnice.BBCode =
                String.Format("[b]Zaznamenaných lékařů:[/b] [i]{0}[/i]\r\n[b]Zaznamenaných ordinací:[/b] [i]{1}[/i]\r\n[b]Zaznamenaných pacientů:[/b] [i]{2}[/i]\r\n[b]Uloženo lékařských zpráv:[/b] [i]{3}[/i]",
                    IONemocnice.dbNemocniceContext.tbLekari.Count(),
                    IONemocnice.dbNemocniceContext.tbOrdinace.Count(),
                    IONemocnice.dbNemocniceContext.tbPacienti.Count(),
                    IONemocnice.dbNemocniceContext.tbLekarskeZpravy.Count()
                );
            TimeSpan vek = DateTime.Now - NemocniceKlient.Instance.PrihlasenyUcet.DatumNarozeni;
            this.bbCodeBlockStatistikyPrihlasenyUcet.BBCode =
                String.Format("[b]Typ účtu:[/b] [color=red]{0}[/color]\r\n[b]Věk:[/b] {1} let {2} dní {3} hodin {4} minut",
                    NemocniceKlient.Instance.FunkcePrihlasenehoUctuString(),
                    Math.Floor(vek.TotalDays / 365.2425),
                    Math.Floor(vek.TotalDays),
                    Math.Floor(vek.TotalHours),
                    Math.Floor(vek.TotalMinutes - vek.TotalHours * 60)
                );
        }
    }
}
