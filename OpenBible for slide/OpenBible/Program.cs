using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenBible.UI;

namespace OpenBible
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
            //Application.Run(new OpenBible.UI.FormMain());
            Application.Run(new FormControl());
            //Application.Run(new Form1());

        }
    }
}
