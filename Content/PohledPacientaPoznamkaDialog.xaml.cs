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
    /// Interaction logic for PohledPacientaPoznamkaDialog.xaml
    /// </summary>
    public partial class PohledPacientaPoznamkaDialog : UserControl
    {
        public PohledPacientaPoznamkaDialog(string poznamkaText)
        {
            InitializeComponent();
            this.txtBoxPoznamka.Text = poznamkaText;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (FormCommunicator.GetParent(this) as PohledPacient).ZmenitPoznamku(this.txtBoxPoznamka.Text);
        }
    }
}
