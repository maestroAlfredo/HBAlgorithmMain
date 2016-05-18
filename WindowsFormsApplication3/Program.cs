using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VoltageDropCalculatorApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //string[] args = System.Environment.Get
            if(args.Length==0)
            {
                Application.Run(new mainForm());           
            }
            else
            {
                string projxml = Path.ChangeExtension(args[0], ".xml"); //change hba extension to xml for nodefeeder hba
                Application.Run(new nodeFeederForm(projxml));                
            }
        }
        
    }
}
