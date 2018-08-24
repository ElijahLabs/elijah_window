using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.OleDb;

namespace Elijah_Window
{
    public partial class TabElijahSoftware : UserControl
    {
        public delegate void _ButtonClick(ElijahSoft Button);
        public event _ButtonClick ButtonClick;
        public TabElijahSoftware()
        {
            InitializeComponent();
            this.ButtonClick += new _ButtonClick(TabElijahSoftware_ButtonClick);
        }

        void TabElijahSoftware_ButtonClick(ElijahSoft Button)
        {
            
        }
        private void IniUI() 
        {
            
            
        }


        private void panMain_Paint(object sender, PaintEventArgs e)
        {
        }
        public void MyRefresh() 
        {
            IniUI();
        }
        public void AddButton(ElijahSoft soft) 
        {
            soft.ButtonClick += new ElijahSoft._ButtonClick(soft_ButtonClick);
           
            this.panSofts.Controls.Add(soft);
        }

        void soft_ButtonClick(ElijahSoft Button)
        {
            this.ButtonClick(Button);
        }
    }
}
