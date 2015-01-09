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

        //public static void LogToFile(string logText)
        //{
        //    IONemocnice.logFile.SetSecurityAlertLevel(0); //int.Parse(logText[0]));
        //    IONemocnice.logFile.Append(logText);
        //}

        //public static void CallDumbTerminal(string logText, byte terminalPort)
        //{
        //    var terminal = IONemocnice.GetTerminalConnection();
        //    //if (!terminal)
        //    //    IONemocnice.NewTerminal(terminalPort, "localhost", IONemocnice.SecurityPrivilege.LEVEL0); //nefunguje!! TODO

        //    terminal.textBufferMode = "VIRTUAL_CONSOLE_ENV\r\n#PrettyPrint=On\r\n#GameMode=On\r\n#RefreshRate=33ms"; //#DEBUG=ON

        //    //terminal.SendCommand("UPD_TIME!" + DateTime.Now);
        //    terminal.SendCommand("UPD_TXT!" + logText + ";;COLOR=" logText[0] %255 + logText[1] % 255 + logText[0] %255 + " EYECANDY=NOBLUR,TRUETYPE,NOMARQUEE,DISCOON;;" + "" + "PWD!1234");


        //    //while (terminal.Hear())
        //    {
        //        if (terminal.Reciever[0] == "PWD?"){
        //            terminal.SendCommand("PWD!1234");
        //            terminal.SendCommand("UPD_TXT!" + logText + ";;COLOR=" logText[0] %255 + logText[1] % 255 + logText[0] %255);
        //        }
        //        else if (terminal.Reciever[0] == "SESSION_EXPIRED*NOREPLY")
        //        {
        //            //terminal.

        //        }
        //        else if (terminal.Reciever[0] == "WRONG_TXT_BUF_MODE?")
        //        {
        //            terminal.ClearTextBuffer();
        //            terminal.ZnicitSvet();
        //            terminal.AnihilaceSveta();
        //            terminal.Restart(terminalPort, color := "RED");
        //            terminal.textBufferMode = "VIRTUAL_CONSOLE_ENV\r\n#PrettyPrint=On\r\n";
        //            if (IONemocnice.terminalID == terminal.ID){
        //                terminal.SendCommand("UPD_TXT!" + logText + ";;COLOR=" logText[0] %255 + logText[1] % 255 + logText[0] %255 + "PWD!1234");
        //            }
        //        }
        //        //else if (terminal.Reciever.Count() == 0){
        //        //    terminal.SendCommand("STFU_AND_WAIT*NOREPLY");
        //        //    terminal.ZnicitSvet();
        //        //}

        //    }
            


        //}
    }
}
