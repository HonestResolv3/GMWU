using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GMWU
{
    static class Program
    {
        static Mutex mu;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // This code here detects if the program is already running, prevents another instance from loading
            const string appName = "GMWU";
            mu = new Mutex(true, appName, out bool createdNew);

            if (!createdNew)
            {
                TinyFD.tinyfd_messageBox($"{appName} is already running", "You cannot have more than one instance of this program running", "ok", "error", 1);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GMWU());
        }
    }
}
