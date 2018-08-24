using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Elijah_Window
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                SetExePath();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        static void SetExePath() 
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("Elijah Window");
            reg.SetValue("Path", Application.StartupPath);
        }
    }
}
