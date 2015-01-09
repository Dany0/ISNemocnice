using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Controls;

namespace ISNemocniceKlient
{
    public sealed class ControlValidator
    {
        private static readonly ControlValidator _instance = new ControlValidator();

        private ControlValidator() { }

        public static ControlValidator Instance
        {
            get
            {
                return _instance;
            }
        }

        private Control _controlKValidaci { get; set; }

        public bool ValidovatControl(Control controlKValidaci)
        {
            this._controlKValidaci = controlKValidaci;

            bool controlValidni = true;
            string napoveda = String.Format("Řetězec `{0}` neodpovídá očekávanému vstupu", "");

            switch ((string) this._controlKValidaci.Tag)
            {
                case "txtBoxNePrazdnyString":
                    napoveda = "Musíte zadat nějaký text!";
                    controlValidni = this.TestRegex(".+", ((TextBox)this._controlKValidaci).Text);
                    break;
                case "passwdBoxNePrazdnyString":
                    napoveda = "Musíte zadat nějaké heslo!";
                    controlValidni = this.TestRegex(".+", ((PasswordBox)this._controlKValidaci).Password);
                    break;
                case "datePickerSpravneDatumNarozeni":
                    napoveda = "Datum narození nesmí být později než tento den či moment!";
                    controlValidni = ((DatePicker)this._controlKValidaci).SelectedDate <= DateTime.Now;
                    break;
                case "comboBoxVybrany":
                    napoveda = "Musíte vybrat jednu možnost!";
                    controlValidni = ((ComboBox)this._controlKValidaci).SelectedIndex != -1;
                    break;
                case "txtBoxEmail":
                    napoveda = "E-Mail musí být ve formátu např. jan.honza@novak.cz!";
                    controlValidni = this.TestRegex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ((TextBox)this._controlKValidaci).Text);
                    break;
                case "txtBoxTelefon":
                    napoveda = "Telefon může obsahovat pouze čísla, pomlčky a mezery!";
                    controlValidni = this.TestRegex(@"^(\d| |-)*$", ((TextBox)this._controlKValidaci).Text);
                    break;
                default:
                    break;
            }

            this.ZobrazChybu(controlValidni, napoveda);
            return controlValidni;
        }

        private bool TestRegex(string pattern, string text)
        {
            return (new Regex(pattern).IsMatch(text));
        }

        private void ZobrazChybu(bool controlValidni, string napoveda)
        {
            if (controlValidni)
            {
                this._controlKValidaci.ClearValue(System.Windows.Controls.Control.BorderBrushProperty);
                this._controlKValidaci.ClearValue(System.Windows.Controls.Control.ToolTipProperty);
            }
            else
            {
                this._controlKValidaci.BorderBrush = Brushes.Red;
                this._controlKValidaci.ToolTip = napoveda;
            }
        }

        public void ValidovatForm(IList<Control> formVstup, Button btnPotvrzeni)
        {
            bool formValidni = true;
            bool vysledekValidaceControlu = true; ;

            foreach (Control vstup in formVstup)
            {
                vysledekValidaceControlu = this.ValidovatControl(vstup);
                if (formValidni)
                {
                    formValidni = vysledekValidaceControlu ? true : false;
                }
            }

            btnPotvrzeni.IsEnabled = formValidni;
        }
    }
}
