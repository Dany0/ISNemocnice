using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ISNemocniceKlient
{
    public static class FormCommunicator
    {
        private static Dictionary<Control, Control> FormPairList = new Dictionary<Control,Control>();

        /// <summary>
        /// Zavolat po nacteni controlu.
        /// </summary>
        /// <param name="parent">Rodic okna.</param>
        /// <param name="child">Potomek okna.</param>
        public static void RegisterForm(Control parent, Control child)
        {
            try
            {
                FormPairList.Add(parent, child);
            }
            catch { }
        }

        public static Control GetChild(Control parent)
        {
            return FormPairList[parent];
        }

        public static Control GetParent(Control child)
        {
            return FormPairList.First(p => p.Value == child).Key;
        }
    }
}
