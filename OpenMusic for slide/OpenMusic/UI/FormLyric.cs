using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenMusic.UI
{
    public partial class FormLyric : Form
    {
        private string _lyric = "";
        public FormLyric()
        {
            InitializeComponent();
        }

        private void FormLyric_Load(object sender, EventArgs e)
        {
            textBoxLyric.Text = _lyric;
        }

        //歌词
        public string Lyric
        {
            get
            {
                return _lyric;
            }
            set
            {
                _lyric = value;
            }

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            _lyric = textBoxLyric.Text;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            _lyric = "";
            this.Close();
        }
    }
}
