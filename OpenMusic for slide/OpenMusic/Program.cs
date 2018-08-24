using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenMusic.UI;

namespace OpenMusic
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

            //FormControl formControl = new FormControl();
            //formControl.StartPosition = FormStartPosition.Manual;
            //formControl.Left = 50;
            //formControl.Top = 50;

            Application.Run(new FormControl());
        }
    }
}
