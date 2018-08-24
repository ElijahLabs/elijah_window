using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Xml;
using System.IO;

namespace Elijah_Window
{
    public partial class FrmMain : Form
    {
        #region 窗体边框阴影效果变量申明

        const int CS_DropSHADOW = 0x20000;
        const int GCL_STYLE = (-26);
        //声明Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        #endregion
        #region 窗体停靠变量申明
        [DllImport("User32.dll")]
        public static extern bool PtInRect(ref Rectangle Rects, Point lpPoint);
        #endregion

        int staX, staY;
        bool IsDown = false;

        int selectedTabIndex = -1;
        TabElijahSoftware tab1, tab2;
        int buttonsCount;

        private bool m_bShowWnd;
        public FrmMain()
        {
            InitializeComponent();
            this.timerShowHide.Enabled = true;
            selectedTabIndex = 0;
            buttonsCount = 0;
            m_bShowWnd = true;

            IniUI();

            SettionTab1();
            SettionTab2();

            picTab1_MouseUp(null, null);

            Rectangle rect = Screen.GetWorkingArea(this);

            this.Location = new Point(rect.Width - this.Width - 100,80 );
        }


        private void IniUI()
        {
            Assembly assembly = GetType().Assembly;
            System.IO.Stream streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.TopLogo.png");
            picLogo.Image = new Bitmap(streamSmall);

            streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.Close1.png");
            picClose.Image = new Bitmap(streamSmall);


            streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.tab1logo.png");
            picTab1.Image = new Bitmap(streamSmall);

            streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.tab2logo.png");
            picTab2.Image = new Bitmap(streamSmall);

            streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.BottomBack.png");
            this.picButtons.Image = new Bitmap(streamSmall);

            streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.border.png");
            this.BackgroundImage = new Bitmap(streamSmall);

            #region 常用BUTTON设置


            

            button1.Click += new EventHandler(picButton_Click);
            button2.Click += new EventHandler(picButton_Click);
            button3.Click += new EventHandler(picButton_Click);
            button4.Click += new EventHandler(picButton_Click);
            button5.Click += new EventHandler(picButton_Click);


            string value = "";
            value = GetButton("1");
            if (value == "")
            {
                streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.ButtonDefault.png");
                button1.Image = new Bitmap(streamSmall);
            }
            else
            {
                buttonsCount = 1;
                SetButtonInfo(buttonsCount.ToString(), value);
            }
            value = GetButton("2");
            if (value == "")
            {
                streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.ButtonDefault.png");
                button2.Image = new Bitmap(streamSmall);
            }
            else
            {
                buttonsCount = 2;
                SetButtonInfo(buttonsCount.ToString(), value);
            }
            value = GetButton("3");
            if (value == "")
            {
                streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.ButtonDefault.png");
                button3.Image = new Bitmap(streamSmall);
            }
            else
            {
                buttonsCount = 3;
                SetButtonInfo(buttonsCount.ToString(), value);
            }
            value = GetButton("4");
            if (value == "")
            {
                streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.ButtonDefault.png");
                button4.Image = new Bitmap(streamSmall);
            }
            else
            {
                buttonsCount = 4;
                SetButtonInfo(buttonsCount.ToString(), value);
            }
            value = GetButton("5");
            if (value == "")
            {
                streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.ButtonDefault.png");
                button5.Image = new Bitmap(streamSmall);
            }
            else
            {
                buttonsCount = 5;
                SetButtonInfo(buttonsCount.ToString(), value);
            }
            #endregion

            tab1 = new TabElijahSoftware();
            tab1.Width = panMain.Width;
            tab1.Height = panMain.Height;
            tab1.Location = new Point(0, 0);
            tab1.ButtonClick += new TabElijahSoftware._ButtonClick(ButtonClick);

            tab2 = new TabElijahSoftware();
            tab2.Width = panMain.Width;
            tab2.Height = panMain.Height;
            tab2.Location = new Point(panMain.Width, 0);
            tab2.ButtonClick += new TabElijahSoftware._ButtonClick(ButtonClick);

            panMain.Controls.Add(tab1);
            panMain.Controls.Add(tab2);
        }

        

        void picButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button pic = sender as Button;
                if (pic.Tag != null)
                {
                    string value = pic.Tag.ToString();
                    if (value != "")
                    {
                        if (value.Length == 1)
                        {
                            PublicToolNames _name = (PublicToolNames)Convert.ToInt32(value);
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
                        else
                        {
                            System.Diagnostics.Process.Start(value);
                        }
                    }
                }
            }
            catch { }
        }




        void ButtonClick(ElijahSoft Button)
        {
            try
            {
                #region 如果点击的工具已经在快捷按钮中，将这个按钮设置为第一个
                bool isExist = false;
                int ButtonIndex = -1;
                for (int i = 1; i <= 5; i++)
                {
                    string info = GetButton(i.ToString());
                    if (Button.SoftInfo == info) 
                    {
                        isExist = true;
                        ButtonIndex = i;
                        break;
                    }
                }
                if (isExist == true) 
                {
                    string info = GetButton(ButtonIndex.ToString());
                    for (int i = ButtonIndex; i >1; i--)
                    {
                       SetButton(i.ToString(),GetButton(Convert.ToString(i-1)));
                       SetButtonInfo(i.ToString(), GetButton(Convert.ToString(i - 1)));
                    }
                    SetButton("1", info);
                    SetButtonInfo("1", info);
                    return;
                }
                #endregion

                //如果快捷按钮还没有放满，直接放到按钮中
                if (buttonsCount < 5)
                {
                    ++buttonsCount;
                    SetButton(buttonsCount.ToString(), Button.SoftInfo);
                    SetButtonInfo(buttonsCount.ToString(), Button.SoftInfo);
                }
                else
                {
                    List<string> values = new List<string>();
                    for (int i = 1; i <= 5; i++)
                    {
                        values.Add(GetButton(i.ToString()));
                    }
                    values.Insert(0, Button.SoftInfo);
                    values.RemoveAt(5);
                    for (int i = 1; i <= 5; i++)
                    {
                        SetButton(i.ToString(), values[i - 1]);
                        SetButtonInfo(i.ToString(), values[i - 1]);
                    }
                }
            }
            catch
            {

            }
        }
        private void SetButtonInfo(string buttonIndex, string value)
        {
            string controlName = "button" + buttonIndex;
            Button pic = panButtons.Controls[panButtons.Controls.IndexOfKey(controlName)] as Button;
            string[] info = value.Split(';');
            if (info[0] == "0")
            {
                pic.Image = Image.FromFile(info[1]);
                pic.Tag = info[2];
            }
            else
            {
                Assembly assembly = GetType().Assembly;
                System.IO.Stream streamSmall = null;
                PublicToolNames name = (PublicToolNames)Convert.ToInt32(info[1]);
                switch (name)
                {
                    case PublicToolNames.Calculator:
                        streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.calc.ico");
                        pic.Image = new Bitmap(streamSmall);
                        break;
                    case PublicToolNames.Cmd:
                        streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.cmd.ico");
                        pic.Image = new Bitmap(streamSmall);
                        break;
                    case PublicToolNames.Excel:
                        streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.excel.ico");
                        pic.Image = new Bitmap(streamSmall);
                        break;
                    case PublicToolNames.Notepad:
                        streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.notepad.ico");
                        pic.Image = new Bitmap(streamSmall);
                        break;
                    case PublicToolNames.Word:
                        streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.WINWORD.ico");
                        pic.Image = new Bitmap(streamSmall);
                        break;
                }
                pic.Tag = info[1];
            }
            pic.Refresh();
        }
        private void SettionTab1()
        {

            string toolsPath = Application.StartupPath + @"\Softwares\ElijahSofts\";
            if (Directory.Exists(toolsPath) == true)
            {
                DirectoryInfo dInfo = new DirectoryInfo(toolsPath);
                DirectoryInfo[] dInfos = dInfo.GetDirectories();
                int buttonCount = dInfos.Count();
                for (int i = 0; i < buttonCount; i++)
                {

                    FileInfo[] fInfos = dInfos[i].GetFiles("SoftConfig.xml");
                    if (fInfos.Count() > 0)
                    {
                        try
                        {
                            string[] values = GetSoftInfo(fInfos[0].FullName);
                            ElijahSoft soft = new ElijahSoft(fInfos[0].DirectoryName + "\\" + values[0], values[1], fInfos[0].DirectoryName + "\\" + values[2]);
                            if (buttonCount > 4) 
                            {
                                soft.Size = new Size(273, 75);
                            }
                            tab1.AddButton(soft);
                        }
                        catch
                        {
                            MessageBox.Show("加载某个软件时，出现错误" + fInfos[0].FullName);
                        }
                    }

                }
            }
        }
        private void SettionTab2()
        {
            string toolsPath = Application.StartupPath + @"\Softwares\PublicSofts\";
            try
            {
                int toolCount = 0;
               
                if (Directory.Exists(toolsPath) == true)
                {
                    DirectoryInfo dInfo = new DirectoryInfo(toolsPath);
                    DirectoryInfo[] dInfos = dInfo.GetDirectories();
                    toolCount = dInfos.Count();
                }
                if (toolCount > 0)
                {
                    tab2.AddButton(new ElijahSoft(PublicToolNames.Excel) { Size = new Size(273, 75) });
                    tab2.AddButton(new ElijahSoft(PublicToolNames.Word) { Size = new Size(273, 75) });
                    tab2.AddButton(new ElijahSoft(PublicToolNames.Notepad) { Size = new Size(273, 75) });
                    tab2.AddButton(new ElijahSoft(PublicToolNames.Calculator) { Size = new Size(273, 75) });
                    tab2.AddButton(new ElijahSoft(PublicToolNames.Cmd) { Size = new Size(273, 75) });
                }
                else
                {
                    tab2.AddButton(new ElijahSoft(PublicToolNames.Excel));
                    tab2.AddButton(new ElijahSoft(PublicToolNames.Word));
                    tab2.AddButton(new ElijahSoft(PublicToolNames.Notepad));
                    tab2.AddButton(new ElijahSoft(PublicToolNames.Calculator));
                    tab2.AddButton(new ElijahSoft(PublicToolNames.Cmd));
                }
            }
            catch 
            {
                tab2.AddButton(new ElijahSoft(PublicToolNames.Excel) { Size = new Size(273, 75) });
                tab2.AddButton(new ElijahSoft(PublicToolNames.Word) { Size = new Size(273, 75) });
                tab2.AddButton(new ElijahSoft(PublicToolNames.Notepad) { Size = new Size(273, 75) });
                tab2.AddButton(new ElijahSoft(PublicToolNames.Calculator) { Size = new Size(273, 75) });
                tab2.AddButton(new ElijahSoft(PublicToolNames.Cmd) { Size = new Size(273, 75) });
            }
            if (Directory.Exists(toolsPath) == true)
            {
                DirectoryInfo dInfo = new DirectoryInfo(toolsPath);
                DirectoryInfo[] dInfos = dInfo.GetDirectories();
                for (int i = 0; i < dInfos.Count(); i++)
                {

                    FileInfo[] fInfos = dInfos[i].GetFiles("SoftConfig.xml");
                    if (fInfos.Count() > 0)
                    {
                        try
                        {
                            string[] values = GetSoftInfo(fInfos[0].FullName);
                            ElijahSoft soft = new ElijahSoft(fInfos[0].DirectoryName + "\\" + values[0], values[1], fInfos[0].DirectoryName + "\\" + values[2]);
                            soft.Size = new Size(273, 75);
                            tab2.AddButton(soft);
                        }
                        catch
                        {
                            MessageBox.Show("加载某个软件时，出现错误" + fInfos[0].FullName);
                        }
                    }

                }
            }
        }
        private void FrmMain_Resize(object sender, EventArgs e)
        {
            this.Region = null;
            SetWindowRegion();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
        }
        public void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            //NVRClientLib.Class.frmResizeClass frmRsize = new NVRClientLib.Class.frmResizeClass();

            //FormPath = frmRsize.WinGetRoundedRectPath(rect, 30);
            //this.Region = new Region(FormPath);

            //frmRsize.WinAngleType(this, 26, 0.1);

            FormPath = WinGetRoundedRectPath(rect, 10);
            this.Region = new Region(FormPath);



        }
        private GraphicsPath WinGetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            // 左上角   
            path.AddArc(arcRect, 180, 90);
            // 右上角   
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            // 右下角   
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            // 左下角   
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void timerShowHide_Tick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                System.Drawing.Point cursorPoint = new Point(Cursor.Position.X, Cursor.Position.Y);//获取鼠标在屏幕的坐标点
                Rectangle Rects = new Rectangle(this.Left, this.Top, this.Left + this.Width, this.Top + this.Height);//存储当前窗体在屏幕的所在区域
                bool prInRect = PtInRect(ref Rects, cursorPoint);
                if (prInRect)
                {//当鼠标在当前窗体内
                    if (this.Top < 0)//窗体的Top属性小于0
                        this.Top = 0;
                    //else if (this.Left < 0)//窗体的Left属性小于0
                    //    this.Left = 0;
                    //else if (this.Right > Screen.PrimaryScreen.WorkingArea.Width)//窗体的Right属性大于屏幕宽度
                    //    this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
                }
                else
                {
                    if (this.Top < 5)               //当窗体的上边框与屏幕的顶端的距离小于5时
                        this.Top = 5 - this.Height; //将窗体隐藏到屏幕的顶端
                    //else if (this.Left < 5)         //当窗体的左边框与屏幕的左端的距离小于5时
                    //    this.Left = 5 - this.Width; //将窗体隐藏到屏幕的左端
                    //else if (this.Right > Screen.PrimaryScreen.WorkingArea.Width - 5)//当窗体的右边框与屏幕的右端的距离小于5时
                    //    this.Left = Screen.PrimaryScreen.WorkingArea.Width - 5;//将窗体隐藏到屏幕的右端
                }
            }

        }
        bool isCloseMove = false;
        private void picClose_MouseMove(object sender, MouseEventArgs e)
        {
            if (isCloseMove == false)
            {
                Assembly assembly = GetType().Assembly;
                System.IO.Stream streamSmall = streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.Close2.png");
                picClose.Image = new Bitmap(streamSmall);
                isCloseMove = true;
            }
        }

        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            if (isCloseMove == true)
            {
                Assembly assembly = GetType().Assembly;
                System.IO.Stream streamSmall = streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.Close1.png");
                picClose.Image = new Bitmap(streamSmall);
                isCloseMove = false;
            }
        }

        private void picClose_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Assembly assembly = GetType().Assembly;
                System.IO.Stream streamSmall = streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.Close3.png");
                picClose.Image = new Bitmap(streamSmall);
            }
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            if (m_bShowWnd == true)//隐藏主界面     　　
            { this.Visible = false; m_bShowWnd = false; }
            else//显示主界面     　　
            { this.Visible = true; m_bShowWnd = true; }
        }

        private void picLogo_MouseDown(object sender, MouseEventArgs e)
        {
            IsDown = true;
            staX = e.X;
            staY = e.Y;
        }

        private void picLogo_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDown == true)
            {
                int x = this.Location.X + (e.X - staX);
                int y = this.Location.Y + (e.Y - staY);
                if (x <= 0) { x = 0; }
                if (y <= 0) { y = 0; }
                this.Location = new Point(x, y);
            }
        }

        private void picLogo_MouseUp(object sender, MouseEventArgs e)
        {
            IsDown = false;

        }

        private string GetButton(string ButtonIndex)
        {
            string value = "";
            XmlDocument myDoc = new XmlDocument();
            myDoc.Load(Application.StartupPath + "\\Setting.xml");
            foreach (XmlNode node in myDoc["Setting"]["Buttons"].ChildNodes)
            {
                if (node.Name == "add")
                {
                    if (node.Attributes["key"].Value == ButtonIndex)
                    {
                        value = node.Attributes["value"].Value;
                        break;
                    }
                }
            }
            return value;
        }

        private void SetButton(string ButtonIndex, string value)
        {
            XmlDocument myDoc = new XmlDocument();
            myDoc.Load(Application.StartupPath + "\\Setting.xml");
            foreach (XmlNode node in myDoc["Setting"]["Buttons"].ChildNodes)
            {
                if (node.Name == "add")
                {
                    if (node.Attributes["key"].Value == ButtonIndex)
                    {
                        node.Attributes["value"].Value = value;
                        break;
                    }
                }
            }
            myDoc.Save(Application.StartupPath + "\\Setting.xml");
        }

        private string[] GetSoftInfo(string xmlPath)
        {
            string[] strs = new string[3];
            XmlDocument myDoc = new XmlDocument();
            myDoc.Load(xmlPath);
            foreach (XmlNode node in myDoc["AppSetting"].ChildNodes)
            {
                if (node.Name.ToUpper() == "LOGO")
                {
                    strs[0] = node.Attributes["value"].Value;
                }
                if (node.Name.ToUpper() == "LABEL")
                {
                    strs[1] = node.Attributes["value"].Value;
                }
                if (node.Name.ToUpper() == "START")
                {
                    strs[2] = node.Attributes["value"].Value;
                }
            }
            return strs;
        }


        private void MenuItemClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_trayIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) 
            {
                if (m_bShowWnd == true)//隐藏主界面     　　
                { this.Visible = false; m_bShowWnd = false; }
                else//显示主界面     　　
                { this.Visible = true; m_bShowWnd = true; }
            }
        }
        #region 处理TAB1的效果
       
        private void picTab1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e != null)
            {
                if (e.Button != System.Windows.Forms.MouseButtons.Left) { return; }
            }
            Assembly assembly = GetType().Assembly;
            System.IO.Stream streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.tab1logo2.png");
            picTab1.Image = new Bitmap(streamSmall);
        }
        private void picTab1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e != null) 
            {
                if (e.Button != System.Windows.Forms.MouseButtons.Left) { return; }
            }
            if (selectedTabIndex != 0)
            {
                tab2.Location = new Point(400, 0);
                tab2.Refresh();
                tab1.Location = new Point(0, 0);
                tab1.Refresh();
        
            }

            selectedTabIndex = 0;

            Assembly assembly = GetType().Assembly;
            System.IO.Stream streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.tab1logo1.png");
            picTab1.Image = new Bitmap(streamSmall);

            streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.tab2logo.png");
            picTab2.Image = new Bitmap(streamSmall);
        }
        #endregion
        #region 处理TAB2的效果
        
        private void picTab2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e != null)
            {
                if (e.Button != System.Windows.Forms.MouseButtons.Left) { return; }
            }
            Assembly assembly = GetType().Assembly;
            System.IO.Stream streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.tab2logo2.png");
            picTab2.Image = new Bitmap(streamSmall);
        }
        private void picTab2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e != null)
            {
                if (e.Button != System.Windows.Forms.MouseButtons.Left) { return; }
            }
            if (selectedTabIndex != 1)
            {
                tab1.Location = new Point(400, 0);
                tab1.Refresh();
                tab2.Location = new Point(0, 0);
                tab2.Refresh();
                

            }

            selectedTabIndex = 1;

            Assembly assembly = GetType().Assembly;
            System.IO.Stream streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.tab2logo1.png");
            picTab2.Image = new Bitmap(streamSmall);

            streamSmall = assembly.GetManifestResourceStream("Elijah_Window.Images.tab1logo.png");
            picTab1.Image = new Bitmap(streamSmall);
        }
        #endregion

    }
    


}
