using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.Interaction;

namespace TobiiMouse
{
    static class Program
    {
        private static Host _host = new Host();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            _host.InitializeWpfAgent();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TobiiMouseForm(_host));
            _host.Dispose();
        }
    }
}
