using System;
using System.Threading;
using System.Windows.Forms;

namespace GMWU
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // This code here detects if the program is already running, prevents another instance from loading
            const string appName = "GMWU";
            bool createdNew;

            Mutex mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("You cannot have more than one instance of this program running", "Program is already running");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GMWU());
        }
    }
}
