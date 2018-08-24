using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using Microsoft.Win32;


namespace SetupElijahWindowLibrary
{
    [RunInstaller(true)]
    public partial class ElijahWindowInstaller : System.Configuration.Install.Installer
    {
        public ElijahWindowInstaller()
        {
            InitializeComponent();
        }
        public override void Install(IDictionary stateSaver) 
        {
            base.Install(stateSaver);
            string dir = this.Context.Parameters["targetdir"].ToString();
            dir = dir.Substring(0, dir.Length - 1);
            if (dir.Substring(dir.Length - 1, 1) != @"\")
            {
                dir = dir + "\\";
            }

            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("Elijah Window");
            reg.SetValue("Path", dir);
        }
    }
}
