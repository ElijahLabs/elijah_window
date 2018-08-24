using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Elijah_Window
{
    public enum PublicToolNames 
    {
        Excel, Word, Notepad, Cmd, Calculator
    }
    public partial class ElijahSoft : UserControl
    {
        public delegate void _ButtonClick(ElijahSoft Button);
        public event _ButtonClick ButtonClick;
        private void Ini() 
        {
            this.Click += new EventHandler(Control_Click);
            this.picLogo.Click += new EventHandler(Control_Click);
            this.labInfo.Click += new EventHandler(Control_Click);
            this.ButtonClick += new _ButtonClick(ElijahSoft_ButtonClick);


            this.MouseMove += new MouseEventHandler(ElijahSoft_MouseMove);
            this.MouseLeave += new EventHandler(ElijahSoft_MouseLeave);
            this.MouseDown += new MouseEventHandler(ElijahSoft_MouseDown);
            this.MouseUp += new MouseEventHandler(ElijahSoft_MouseUp);


            this.picLogo.MouseMove += new MouseEventHandler(ElijahSoft_MouseMove);
            this.picLogo.MouseLeave += new EventHandler(ElijahSoft_MouseLeave);
            this.picLogo.MouseDown += new MouseEventHandler(ElijahSoft_MouseDown);
            this.picLogo.MouseUp += new MouseEventHandler(ElijahSoft_MouseUp);

            this.labInfo.MouseMove += new MouseEventHandler(ElijahSoft_MouseMove);
            this.labInfo.MouseLeave += new EventHandler(ElijahSoft_MouseLeave);
            this.labInfo.MouseDown += new MouseEventHandler(ElijahSoft_MouseDown);
            this.labInfo.MouseUp += new MouseEventHandler(ElijahSoft_MouseUp);

        }
        bool isMove = false;
        void ElijahSoft_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMove == false)
            {
                isMove = true;
                Assembly assembly = GetType().Assembly;
                System.IO.Stream streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.MainBackItem_hover.png");
                this.BackgroundImage = new Bitmap(streamSmall);
            }
            else
            {
                Assembly assembly = GetType().Assembly;
                System.IO.Stream streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.MainBackItem.png");
                this.BackgroundImage = new Bitmap(streamSmall);
                isMove = false;
            }
        }

        void ElijahSoft_MouseDown(object sender, MouseEventArgs e)
        {
            Assembly assembly = GetType().Assembly;
            System.IO.Stream streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.MainBackItem_click.png");
            this.BackgroundImage = new Bitmap(streamSmall);
        }

        void ElijahSoft_MouseLeave(object sender, EventArgs e)
        {
            if (isMove == true)
            {
                Assembly assembly = GetType().Assembly;
                System.IO.Stream streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.MainBackItem.png");
                this.BackgroundImage = new Bitmap(streamSmall);
                isMove = false;
            }
        }

        void ElijahSoft_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove == false)
            {
                isMove = true;
                Assembly assembly = GetType().Assembly;
                System.IO.Stream streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.MainBackItem_hover.png");
                this.BackgroundImage = new Bitmap(streamSmall);
            }
        }

        void ElijahSoft_ButtonClick(ElijahSoft Button)
        {
            
        }
        private string _logofile;
        private string _exefile;
        public ElijahSoft(string logofile,string info,string exefile)
        {
            InitializeComponent();
            Ini();
            _SoftType = 0;

            picLogo.Image = Image.FromFile(logofile);
            labInfo.Text = info;
            _exefile = exefile;
            _logofile = logofile;
        }
        private PublicToolNames _name;
        public ElijahSoft(PublicToolNames name) 
        {
            InitializeComponent();
            _name = name;
            Ini();
            _SoftType = 1;
            Assembly assembly = GetType().Assembly;
            System.IO.Stream streamSmall = null;
            switch (name) 
            {
                case PublicToolNames.Calculator: 
                    streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.calc.ico");
                    picLogo.Image = new Bitmap(streamSmall);
                    labInfo.Text = "使用屏幕“计算器”执行基本的算术任务";
                    break;
                case PublicToolNames.Cmd: 
                    streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.cmd.ico");
                    picLogo.Image = new Bitmap(streamSmall);
                    labInfo.Text = "执行基于文本的(命令行)功能";
                    break;
                case PublicToolNames.Excel:
                    streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.excel.ico");
                    picLogo.Image = new Bitmap(streamSmall);
                    labInfo.Text = "使用 Microsoft Office Excel 执行计算、分析信息以及可视化电子表格中的数据";
                    break;
                case PublicToolNames.Notepad:
                    streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.notepad.ico");
                    picLogo.Image = new Bitmap(streamSmall);
                    labInfo.Text = "使用基本文本格式创建和编辑文本文件";
                    break;
                case PublicToolNames.Word: 
                    streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.WINWORD.ico");
                    picLogo.Image = new Bitmap(streamSmall);
                    labInfo.Text = "使用 Microsoft Office Word 创建和编辑具有专业外观的文档，如信函、论文、报告和小册子";
                    break;
            }
        }
        int _SoftType;
        /// <summary>
        /// 0=EXE文件，1=是软件内定的
       /// </summary>
        public int SoftType 
        {
            get { return _SoftType; }
        }

        private void Control_Click(object sender, EventArgs e)
        {
            this.ButtonClick(this);
            try
            {
                if (SoftType == 0)
                {
                    System.Diagnostics.Process.Start(_exefile);
                }
                else
                {
                    #region 打开指定命令
                    switch (_name)
                    {
                        case PublicToolNames.Calculator:
                            System.Diagnostics.Process.Start("calc.exe");
                            break;
                        case PublicToolNames.Cmd:
                            System.Diagnostics.Process.Start("cmd.exe");
                            break;
                        case PublicToolNames.Excel:
                            try
                            {
                                System.Type excelType = System.Type.GetTypeFromProgID("Excel.Application");
                                Object excel = System.Activator.CreateInstance(excelType);
                                excelType.InvokeMember("Visible", BindingFlags.SetProperty, null, excel, new Object[] { true });
                                Object workbooks = excelType.InvokeMember("workbooks", BindingFlags.GetProperty, null, excel, null);
                                Object workbook = workbooks.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, workbooks, null);
                            }
                            catch 
                            {
                                MessageBox.Show("在电脑中未找到Office软件");
                            }
                            break;
                        case PublicToolNames.Notepad:
                            System.Diagnostics.Process.Start("notepad.exe");
                            break;
                        case PublicToolNames.Word:
                            try
                            {
                                System.Type wordType = System.Type.GetTypeFromProgID("Word.Application");
                                Object word = System.Activator.CreateInstance(wordType);
                                wordType.InvokeMember("Visible", BindingFlags.SetProperty, null, word, new Object[] { true });
                                Object documents = wordType.InvokeMember("Documents", BindingFlags.GetProperty, null, word, null);
                                Object document = documents.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, documents, null);
                            }
                            catch 
                            {
                                MessageBox.Show("在电脑中未找到Office软件");
                            }
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string SoftInfo 
        {
            get 
            {
                if (SoftType == 0)
                {
                    return "0;"+ _logofile + ";" + _exefile;
                }
                else 
                {
                    return "1;" + Convert.ToInt32(_name).ToString();
                }
            }
        }
    }
}
