using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenBible.UI
{
    public partial class FormInput : Form
    {
        public FormInput()
        {
            InitializeComponent();
        }

        public string strReturnValue;

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            strReturnValue = textBoxMessage.Text;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            strReturnValue = "";
            this.Close();
        }
    }
}
