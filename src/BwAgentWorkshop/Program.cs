using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Botworx.AgentLib.ClientLib.Workshop.Gui;

namespace Botworx.AgentLib.Workshop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AppForm());
        }
    }
}
