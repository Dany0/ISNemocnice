using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISNemocniceKlient
{
    public static class ConsoleLogger
    {
        public static void Log(string logText)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(String.Format("[{0:dd:mm:yyyy HH:mm:ss}] : {1}", DateTime.Now, logText));
        }
    }
}
