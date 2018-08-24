using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenBible.Properties;
using OpenBible.BLL;
using System.Diagnostics;
using System.IO;

namespace OpenBible.UI
{
    public partial class FormControl : Form
    {

        //标题
        public string StrTitle;
        //节号
        public string StrVerseNumber;
        //经文
        public string StrLection;

        //开始节号
        public int IntVerseNumberBegin;

        //结束节号
        public int IntVerseNumberEnd;

        ///所有节号字符串
        public string StrArrayVerseNumber;

        //使用拼音查找经文
        public string StrSearchByPinYin;

        //标题字体名
        public string StrTitleFontName;
        //经文字体名
        public string StrLectionFontName;
        //节号字体名
        public string StrVerseNumberFontName;

        //标题颜色
        public Color ColorTitle;
        //节号颜色
        public Color ColorVerseNumber;
        //经文颜色
        public Color ColorLection;
        //背景颜色
        public Color ColorBack;
        //标题字号
        public int IntTitleFontSize;
        //节号字号
        public int IntVerseNumberFontSize;
        //经文字号
        public int IntLectionFontSize;
        //标题样式
        public FontStyle FontStyleTitle;
        //节号样式
        public FontStyle FontStyleVerseNumber;
        //经文样式
        public FontStyle FontStyleLection;


        //标题画刷
        public Brush BrushTitle;
        //标题字体
        public Font FontTitle;

        //节号画刷
        public Brush BrushVerseNumber;

        //经文画刷
        public Brush BrushLection;
        //经文字体
        public Font FontLection;

        //字体间距倍数
        public float FloatFontSpaceScale;

        //经文行间距倍数
        public float FloatLineSpaceScale;

        public int IntLectionLeftSpace;

        //上一次选择的经文列表
        public string StrLastLectionList;



        //投影窗口
        FormScreen _formScreen = null;


        //字符串
        private static string _strCreateNewList = "[新建列表]";

        //配置文件写入类
        ConfigHelper _configHelper;

        //安装到当前机器中的字体
        private System.Drawing.Text.InstalledFontCollection _objFont = new System.Drawing.Text.InstalledFontCollection();



        public FormControl()
        {
            InitializeComponent();
        }

        private void FormControl_Load(object sender, EventArgs e)
        {

            //读取配置文件的初始值
            StrTitleFontName = Settings.Default.TitleFontName;
            StrLectionFontName = Settings.Default.LectionFontName;
            ColorTitle = Settings.Default.TitleColor;
            ColorVerseNumber = Settings.Default.VerseNumberColor;
            ColorLection = Settings.Default.LectionColor;
            ColorBack = Settings.Default.BackColor;
            IntTitleFontSize = Settings.Default.TitleFontSize;
            IntLectionFontSize = Settings.Default.LectionFontSize;

            IntLectionLeftSpace = Settings.Default.LectionLeftSpace;

            StrLastLectionList = Settings.Default.LastLectionList;

            //FontStyleTitle = Settings.Default.TitleFontStyle;
            switch (Settings.Default.TitleFontStyle)
            {
                case "正常":
                    FontStyleTitle = FontStyle.Regular;
                    break;
                case "加粗":
                    FontStyleTitle = FontStyle.Bold;
                    break;
                case "倾斜":
                    FontStyleTitle = FontStyle.Italic;
                    break;
                default:
                    FontStyleTitle = FontStyle.Regular;
                    break;
            }

            //FontStyleLection = Settings.Default.LectionFontStyle;
            switch (Settings.Default.LectionFontStyle)
            {
                case "正常":
                    FontStyleLection = FontStyle.Regular;
                    break;
                case "加粗":
                    FontStyleLection = FontStyle.Bold;
                    break;
                case "倾斜":
                    FontStyleLection = FontStyle.Italic;
                    break;
                default:
                    FontStyleLection = FontStyle.Regular;
                    break;
            }

            FloatFontSpaceScale = Settings.Default.FontSpaceScale;
            FloatLineSpaceScale = Settings.Default.LineSpaceScale;

            ReloadConfigParameters();

            _configHelper = new ConfigHelper();


            //显示投影窗口
            ShowScreen();
            //移动投影窗口到右上角
            _formScreen.Left = Screen.GetWorkingArea(this).Right - _formScreen.Width;
            _formScreen.Top = Screen.GetWorkingArea(this).Top;


            //初始化控件原始数据

            #region 初始化控制台listview选择经文控件数据
            //填充经文书卷数据
            listViewVolume.Items.Clear();

            string[] arrayResult = new string[66];
            arrayResult = BibleHelper.GetFullNameList();

            for (int i = 0; i < 66; i++)
            {
                listViewVolume.Items.Add(arrayResult[i]);

                if (i < 39)
                {
                    listViewVolume.Items[i].BackColor = Color.LightPink;
                }
                else
                {
                    listViewVolume.Items[i].BackColor = Color.LightGreen;
                }
            }

            Array.Clear(arrayResult, 0, arrayResult.Length);

            #endregion


            #region 初始化控件数据
            //初始化经文列表下拉列表控件
            InitLectionList();

            //初始化标题字体，经文字体控件
            foreach (System.Drawing.FontFamily i in _objFont.Families)
            {
                comboBoxLectionFontName.Items.Add(i.Name.ToString());
                comboBoxTitleFontName.Items.Add(i.Name.ToString());
            }


            //初始化标题、经文字号
            comboBoxTitleFontSize.Items.Add("12");
            comboBoxTitleFontSize.Items.Add("20");
            comboBoxTitleFontSize.Items.Add("30");
            comboBoxTitleFontSize.Items.Add("35");
            comboBoxTitleFontSize.Items.Add("40");
            comboBoxTitleFontSize.Items.Add("45");
            comboBoxTitleFontSize.Items.Add("50");
            comboBoxTitleFontSize.Items.Add("60");
            comboBoxTitleFontSize.Items.Add("70");
            comboBoxTitleFontSize.Items.Add("80");
            comboBoxTitleFontSize.Items.Add("90");
            comboBoxTitleFontSize.Items.Add("100");
            comboBoxTitleFontSize.Items.Add("110");
            comboBoxTitleFontSize.Items.Add("120");
            comboBoxTitleFontSize.Items.Add("130");
            comboBoxTitleFontSize.Items.Add("140");
            comboBoxTitleFontSize.Items.Add("150");
            comboBoxTitleFontSize.Items.Add("200");





            comboBoxLectionFontSize.Items.Add("12");
            comboBoxLectionFontSize.Items.Add("20");
            comboBoxLectionFontSize.Items.Add("30");
            comboBoxLectionFontSize.Items.Add("35");
            comboBoxLectionFontSize.Items.Add("40");
            comboBoxLectionFontSize.Items.Add("45");
            comboBoxLectionFontSize.Items.Add("50");
            comboBoxLectionFontSize.Items.Add("60");
            comboBoxLectionFontSize.Items.Add("70");
            comboBoxLectionFontSize.Items.Add("80");
            comboBoxLectionFontSize.Items.Add("90");
            comboBoxLectionFontSize.Items.Add("110");
            comboBoxLectionFontSize.Items.Add("120");
            comboBoxLectionFontSize.Items.Add("130");
            comboBoxLectionFontSize.Items.Add("140");
            comboBoxLectionFontSize.Items.Add("150");
            comboBoxLectionFontSize.Items.Add("200");



            //初始化标题、经文的字体样式
            //comboBoxLectionFontStyle.Items.Add(FontStyle.Bold.ToString());
            //comboBoxLectionFontStyle.Items.Add(FontStyle.Italic.ToString());
            //comboBoxLectionFontStyle.Items.Add(FontStyle.Regular.ToString());
            //comboBoxLectionFontStyle.Items.Add(FontStyle.Strikeout.ToString());
            //comboBoxLectionFontStyle.Items.Add(FontStyle.Underline.ToString());

            comboBoxLectionFontStyle.Items.Add("正常");
            comboBoxLectionFontStyle.Items.Add("加粗");
            comboBoxLectionFontStyle.Items.Add("倾斜");

            //comboBoxTitleFontStyle.Items.Add(FontStyle.Bold.ToString());
            //comboBoxTitleFontStyle.Items.Add(FontStyle.Italic.ToString());
            //comboBoxTitleFontStyle.Items.Add(FontStyle.Regular.ToString());
            //comboBoxTitleFontStyle.Items.Add(FontStyle.Strikeout.ToString());
            //comboBoxTitleFontStyle.Items.Add(FontStyle.Underline.ToString());

            comboBoxTitleFontStyle.Items.Add("正常");
            comboBoxTitleFontStyle.Items.Add("加粗");
            comboBoxTitleFontStyle.Items.Add("倾斜");


            //初始化行间距
            comboBoxLineSpacing.Items.Add("-1");
            comboBoxLineSpacing.Items.Add("-0.9");
            comboBoxLineSpacing.Items.Add("-0.8");
            comboBoxLineSpacing.Items.Add("-0.7");
            comboBoxLineSpacing.Items.Add("-0.6");
            comboBoxLineSpacing.Items.Add("-0.5");
            comboBoxLineSpacing.Items.Add("-0.4");
            comboBoxLineSpacing.Items.Add("-0.3");
            comboBoxLineSpacing.Items.Add("-0.2");
            comboBoxLineSpacing.Items.Add("-0.1");
            comboBoxLineSpacing.Items.Add("0");
            comboBoxLineSpacing.Items.Add("0.1");
            comboBoxLineSpacing.Items.Add("0.2");
            comboBoxLineSpacing.Items.Add("0.3");
            comboBoxLineSpacing.Items.Add("0.4");
            comboBoxLineSpacing.Items.Add("0.5");
            comboBoxLineSpacing.Items.Add("0.6");
            comboBoxLineSpacing.Items.Add("0.7");
            comboBoxLineSpacing.Items.Add("0.8");
            comboBoxLineSpacing.Items.Add("0.9");
            comboBoxLineSpacing.Items.Add("1.0");
            comboBoxLineSpacing.Items.Add("1.2");
            comboBoxLineSpacing.Items.Add("1.5");
            comboBoxLineSpacing.Items.Add("2.0");
            comboBoxLineSpacing.Items.Add("2.5");
            comboBoxLineSpacing.Items.Add("2.3");
            comboBoxLineSpacing.Items.Add("5");


            //初始化字间距
            comboBoxFontSpacing.Items.Add("-1");
            comboBoxFontSpacing.Items.Add("-0.9");
            comboBoxFontSpacing.Items.Add("-0.8");
            comboBoxFontSpacing.Items.Add("-0.7");
            comboBoxFontSpacing.Items.Add("-0.6");
            comboBoxFontSpacing.Items.Add("-0.5");
            comboBoxFontSpacing.Items.Add("-0.4");
            comboBoxFontSpacing.Items.Add("-0.3");
            comboBoxFontSpacing.Items.Add("-0.2");
            comboBoxFontSpacing.Items.Add("-0.1");
            comboBoxFontSpacing.Items.Add("0");
            comboBoxFontSpacing.Items.Add("0.1");
            comboBoxFontSpacing.Items.Add("0.2");
            comboBoxFontSpacing.Items.Add("0.3");
            comboBoxFontSpacing.Items.Add("0.4");
            comboBoxFontSpacing.Items.Add("0.5");
            comboBoxFontSpacing.Items.Add("0.6");
            comboBoxFontSpacing.Items.Add("0.7");
            comboBoxFontSpacing.Items.Add("0.8");
            comboBoxFontSpacing.Items.Add("0.9");
            comboBoxFontSpacing.Items.Add("1.0");
            comboBoxFontSpacing.Items.Add("1.2");
            comboBoxFontSpacing.Items.Add("1.5");
            comboBoxFontSpacing.Items.Add("2.0");
            comboBoxFontSpacing.Items.Add("2.5");
            comboBoxFontSpacing.Items.Add("2.3");
            comboBoxFontSpacing.Items.Add("5");

            comboBoxLectionLeftSpace.Items.Add("-100");
            comboBoxLectionLeftSpace.Items.Add("-80");
            comboBoxLectionLeftSpace.Items.Add("-50");
            comboBoxLectionLeftSpace.Items.Add("-20");
            comboBoxLectionLeftSpace.Items.Add("-10");
            comboBoxLectionLeftSpace.Items.Add("-5");
            comboBoxLectionLeftSpace.Items.Add("0");
            comboBoxLectionLeftSpace.Items.Add("5");
            comboBoxLectionLeftSpace.Items.Add("10");
            comboBoxLectionLeftSpace.Items.Add("20");
            comboBoxLectionLeftSpace.Items.Add("30");
            comboBoxLectionLeftSpace.Items.Add("50");
            comboBoxLectionLeftSpace.Items.Add("70");
            comboBoxLectionLeftSpace.Items.Add("100");
            comboBoxLectionLeftSpace.Items.Add("150");
            comboBoxLectionLeftSpace.Items.Add("200");
            comboBoxLectionLeftSpace.Items.Add("300");
            comboBoxLectionLeftSpace.Items.Add("400");

            #endregion



            #region 尺寸与颜色，其他默认值

            //用设计器设置这里居然有bug，只好用代码。。。
            //设置panel的最小尺寸
            this.splitContainer1.Panel2MinSize = 300;
            this.splitContainer1.Panel1MinSize = 400;


            //画布背景色
            //this.panelLectionContent.BackColor = ColorBack;

            //经文背景色
            //this.richTextBoxLectionContent.BackColor = ColorBack;

            //背景颜色
            this.labelBackColor.BackColor = ColorBack;

            //标题颜色
            this.labelTitleColor.BackColor = ColorTitle;

            //节号颜色
            this.labelVerseNumberColor.BackColor = ColorVerseNumber;

            //经文颜色
            this.labelLectionColor.BackColor = ColorLection;

            //标题、经文字体名称
            comboBoxLectionFontName.Text = StrLectionFontName;
            comboBoxTitleFontName.Text = StrTitleFontName;

            //标题、经文字号控件
            comboBoxLectionFontSize.Text = IntLectionFontSize.ToString();
            comboBoxTitleFontSize.Text = IntTitleFontSize.ToString();

            //标题、经文字样式
            comboBoxTitleFontStyle.Text = Settings.Default.TitleFontStyle;// FontStyleTitle.ToString();
            comboBoxLectionFontStyle.Text = Settings.Default.LectionFontStyle;// FontStyleLection.ToString();

            //行间距
            comboBoxLineSpacing.Text = FloatLineSpaceScale.ToString();

            comboBoxFontSpacing.Text = FloatFontSpaceScale.ToString();

            comboBoxLectionLeftSpace.Text = IntLectionLeftSpace.ToString();

            #endregion

        }

        //初始化与配置文件有关的参数
        private void ReloadConfigParameters()
        {
            #region 配置文件相关参数

            if (BrushTitle != null)
            {
                BrushTitle.Dispose();
            }
            BrushTitle = new SolidBrush(ColorTitle);

            if (FontTitle != null)
            {
                FontTitle.Dispose();
            }
            FontTitle = new Font(StrTitleFontName, IntTitleFontSize, FontStyleTitle);

            if (BrushLection != null)
            {
                BrushLection.Dispose();
            }
            BrushLection = new SolidBrush(ColorLection);

            if (BrushVerseNumber != null)
            {
                BrushVerseNumber.Dispose();
            }
            BrushVerseNumber = new SolidBrush(ColorVerseNumber);

            if (FontLection != null)
            {
                FontLection.Dispose();
            }
            FontLection = new Font(StrLectionFontName, IntLectionFontSize, FontStyleLection);

            #endregion

        }



        //初始化经文列表下拉列表控件
        private void InitLectionList()
        {
            comboBoxLectionList.Items.Clear();

            comboBoxLectionList.Items.Add("");
            comboBoxLectionList.Items.Add(_strCreateNewList);

            string strList = ListHelper.GetFileList();
            if (strList != "")
            {
                string[] strLections = strList.Split('|');
                for (int i = 0; i < strLections.Length; i++)
                {
                    if (strLections[i] == "")
                    {

                    }
                    else
                    {
                        comboBoxLectionList.Items.Add(strLections[i].ToLower().Replace(".txt", ""));
                    }
                }
                Array.Clear(strLections, 0, strLections.Length);
            }


            //定位到刚创建的经文列表
            for (int i = 0; i < this.comboBoxLectionList.Items.Count; i++)
            {
                if (StrLastLectionList == this.comboBoxLectionList.Items[i].ToString())
                {
                    this.comboBoxLectionList.SelectedIndex = i;
                    break;
                }
            }

        }


        //显示投影窗口
        private void ShowScreen()
        {
            if (_formScreen == null || !_formScreen.Visible)
            {
                _formScreen = new FormScreen(this);
            }

            _formScreen.Show();


        }


        private void buttonKeyboardControl_Click(object sender, EventArgs e)
        {
            _formScreen.Focus();
        }

        //查找卷
        private void listViewVolume_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iChapterCount = BibleHelper.GetChapterCountByVolumeSN(listViewVolume.SelectedIndices[0] + 1);

                listViewChapter.Items.Clear();
                listViewVerseNumberBegin.Items.Clear();
                listViewVerseNumberEnd.Items.Clear();


                for (int i = 0; i < iChapterCount; i++)
                {
                    listViewChapter.Items.Add((i + 1).ToString() + " 章");
                }

            }
            catch (Exception)
            {
            }
        }

        //查找章
        private void listViewChapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iVerseCount = BibleHelper.GetVerseCountByVolumeSNAndChapterSN(listViewVolume.SelectedIndices[0] + 1, listViewChapter.SelectedIndices[0] + 1);

                listViewVerseNumberBegin.Items.Clear();
                listViewVerseNumberEnd.Items.Clear();


                for (int i = 0; i < iVerseCount; i++)
                {
                    listViewVerseNumberBegin.Items.Add((i + 1).ToString() + " 节");
                }

            }
            catch (Exception)
            {
            }
        }

        //查找开始节
        private void listViewVerseNumberBegin_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iVerseCount = listViewVerseNumberBegin.Items.Count;

                listViewVerseNumberEnd.Items.Clear();


                if (listViewVerseNumberBegin.SelectedItems.Count <= 0)
                {
                    return;
                }

                for (int i = listViewVerseNumberBegin.SelectedIndices[0] + 1; i < iVerseCount; i++)
                {
                    listViewVerseNumberEnd.Items.Add("--> " + (i + 1).ToString() + " 节");

                    listViewVerseNumberEnd.Items[listViewVerseNumberEnd.Items.Count - 1].Tag = i;
                }

                ////如果当前选择的节数是本章最后一节，则直接显示这节经文
                //if (listViewVerseNumberBegin.SelectedIndices[0] + 1 == iVerseCount)
                //{
                StrTitle = "";
                StrLection = "";

                StrTitle = listViewVolume.SelectedItems[0].Text + " " + (listViewChapter.SelectedIndices[0] + 1).ToString() + ":" + (listViewVerseNumberBegin.SelectedIndices[0] + 1).ToString();
                StrLection = BibleHelper.GetLectionByVolumeSNandChapterSN(listViewVolume.SelectedIndices[0] + 1, listViewChapter.SelectedIndices[0] + 1, listViewVerseNumberBegin.SelectedIndices[0] + 1);


                //开始节号与结束节号
                CalculateVerseNumberCount(listViewVerseNumberBegin.SelectedIndices[0] + 1, listViewVerseNumberBegin.SelectedIndices[0] + 1);


                //显示经文
                ShowLectionContent();

                //保存此处经文到右侧列表中
                this.listViewLectionList.Items.Add(new ListViewItem(new string[] { (this.listViewLectionList.Items.Count + 1).ToString(), StrTitle }));

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //查找结束节
        private void listViewVerseNumberEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (listViewVerseNumberEnd.SelectedIndices.Count > 0)
                {
                    StrTitle = "";
                    StrLection = "";

                    StrTitle = listViewVolume.SelectedItems[0].Text + " " + (listViewChapter.SelectedIndices[0] + 1).ToString() + ":" + (listViewVerseNumberBegin.SelectedIndices[0] + 1).ToString() + "-" + ((int)listViewVerseNumberEnd.SelectedItems[0].Tag + 1).ToString();

                    StrLection = BibleHelper.GetLectionByVolumeSNandChapterSN(listViewVolume.SelectedIndices[0] + 1, listViewChapter.SelectedIndices[0] + 1, listViewVerseNumberBegin.SelectedIndices[0] + 1, (int)listViewVerseNumberEnd.SelectedItems[0].Tag + 1);

                    ////开始节号与结束节号
                    CalculateVerseNumberCount(listViewVerseNumberBegin.SelectedIndices[0] + 1, (int)listViewVerseNumberEnd.SelectedItems[0].Tag + 1);

                    //显示经文
                    ShowLectionContent();

                    //保存此处经文到右侧列表中
                    this.listViewLectionList.Items.Add(new ListViewItem(new string[] { (this.listViewLectionList.Items.Count + 1).ToString(), StrTitle }));

                    this.Cursor = Cursors.Default;

                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        //显示内容
        private void ShowLectionContent()
        {

            ShowScreen();
            _formScreen.DrawText();
        }

        public void CalculateVerseNumberCount(int intVerseNumberBegin, int intVerseNumberEnd)
        {
            //开始节号与结束节号
            IntVerseNumberBegin = intVerseNumberBegin;
            IntVerseNumberEnd = intVerseNumberEnd;

            //生成节号字符串
            StrArrayVerseNumber = "";
            for (int i = IntVerseNumberBegin; i <= IntVerseNumberEnd; i++)
            {
                StrArrayVerseNumber += i.ToString() + " ";
            }
        }

        private void buttonFullScreen_Click(object sender, EventArgs e)
        {
            ShowScreen();

            if (_formScreen.FormBorderStyle == FormBorderStyle.None)
            {
                _formScreen.FormBorderStyle = FormBorderStyle.Sizable;
                _formScreen.WindowState = FormWindowState.Normal;
            }
            else
            {
                _formScreen.FormBorderStyle = FormBorderStyle.None;
                _formScreen.WindowState = FormWindowState.Maximized;
            }
        }

        private void comboBoxLectionList_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                //显示经文列表，读取文件中的内容并显示到listView中
                string strSelectedFileName = comboBoxLectionList.Items[comboBoxLectionList.SelectedIndex].ToString();
                if (strSelectedFileName == "")
                {
                    //空白，清空
                    listViewLectionList.Items.Clear();
                }
                else if (strSelectedFileName == _strCreateNewList)
                {
                    //空白，清空
                    listViewLectionList.Items.Clear();

                    ////新建经文列表
                    ////弹出窗口让用户输入经文列表名

                    string strListName = CreateNewList();

                    if (strListName != "")
                    {
                        ShowNewList(strListName);
                    }
                    else
                    {
                        strSelectedFileName = "";
                    }

                }
                else
                {
                    //空白，清空
                    listViewLectionList.Items.Clear();

                    //显示经文列表
                    string strFileContent = ListHelper.ReadFileContent(strSelectedFileName + ".txt");
                    if (strFileContent != "")
                    {
                        string[] strContent = strFileContent.Split('|');

                        for (int i = 0; i < strContent.Length; i++)
                        {
                            ListViewItem item = new ListViewItem((i + 1).ToString());
                            item.SubItems.Add(strContent[i]);

                            listViewLectionList.Items.Add(item);
                        }
                    }
                }

                StrLastLectionList = strSelectedFileName;
                _configHelper.Write("LastLectionList", strSelectedFileName);

            }
            catch (Exception)
            {
            }

        }


        private string CreateNewList()
        {
            //新建经文列表
            //弹出窗口让用户输入播经文列表名
            FormInput formInput = new FormInput();
            formInput.Text = "请输入经文列表名称";
            formInput.textBoxMessage.Text = DateTime.Now.ToString("yyyy-MM-dd") + " ";
            formInput.ShowDialog();

            if (formInput.strReturnValue != "")
            {
                bool bFileCreated = false;
                string strNewFileName = formInput.strReturnValue + ".txt";

                //输入后，保存成.txt文件
                //如果有同名的文件，提示是否覆盖
                if (ListHelper.FileExists(strNewFileName))
                {
                    if (MessageBox.Show("已存在同名文件，是否覆盖？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bFileCreated = ListHelper.CreateFile(strNewFileName);
                    }
                }
                else
                {
                    bFileCreated = ListHelper.CreateFile(strNewFileName);
                }

            }

            string strReturn = formInput.strReturnValue;

            formInput.Dispose();

            return strReturn;


        }


        /// <summary>
        /// 新建经文列表
        /// </summary>
        /// <returns></returns>
        private void ShowNewList(string strInput)
        {
            if (strInput != "")
            {
                StrLastLectionList = strInput;

                //重新加载经文列表
                InitLectionList();

            }
        }

        private void listViewLectionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //显示经文内容
                if (listViewLectionList.SelectedIndices.Count > 0)
                {
                    StrTitle = "";
                    StrLection = "";

                    string strSelectedLection = listViewLectionList.Items[listViewLectionList.SelectedIndices[0]].SubItems[1].Text;


                    ShowLectionByVolumeNameEtc(strSelectedLection, false);

                    //如果使用完整卷名无法显示经文时，尝试使用缩略卷名显示经文
                    if (this.StrLection == "")
                    {
                        ShowLectionByVolumeNameEtc(strSelectedLection, true);
                    }


                    ShowLectionContent();
                }
            }
            catch (Exception)
            {
            }
        }





        //根据缩略名和章节等显示经文
        private void ShowLectionByVolumeNameEtc(string strVolumeNameEtc, bool bIsShortVolumeName)
        {
            //拆解卷名，章号，起始节号，结束节号
            string strVolumeName = "";
            int iChapterSN = -1;
            int iVerseSNBegin = -1;
            int iVerseSNEnd = -1;

            if (strVolumeNameEtc.Contains(' '))
            {
                strVolumeName = strVolumeNameEtc.Substring(0, strVolumeNameEtc.IndexOf(' '));

                strVolumeNameEtc = strVolumeNameEtc.Substring(strVolumeNameEtc.IndexOf(' ') + 1);

                if (strVolumeNameEtc[0] == ':')
                {
                    return;
                }

                iChapterSN = int.Parse(strVolumeNameEtc.Substring(0, strVolumeNameEtc.IndexOf(':')));

                strVolumeNameEtc = strVolumeNameEtc.Substring(strVolumeNameEtc.IndexOf(':') + 1);


                if (strVolumeNameEtc.Contains('-'))
                {
                    //路 1:1-2

                    iVerseSNBegin = int.Parse(strVolumeNameEtc.Substring(0, strVolumeNameEtc.IndexOf('-')));
                    strVolumeNameEtc = strVolumeNameEtc.Substring(strVolumeNameEtc.IndexOf('-') + 1);
                    iVerseSNEnd = int.Parse(strVolumeNameEtc);

                    StrTitle = strVolumeName + " " + iChapterSN + ":" + iVerseSNBegin + "-" + iVerseSNEnd;
                }
                else
                {
                    //路 1:3
                    iVerseSNBegin = int.Parse(strVolumeNameEtc);
                    iVerseSNEnd = iVerseSNBegin;

                    StrTitle = strVolumeName + " " + iChapterSN + ":" + iVerseSNBegin;
                }

                if (iVerseSNEnd >= iVerseSNBegin)
                {
                    if (bIsShortVolumeName)
                    {
                        StrLection = BibleHelper.GetLectionByShortNameandChapterSN(strVolumeName, iChapterSN, iVerseSNBegin, iVerseSNEnd);
                    }
                    else
                    {
                        StrLection = BibleHelper.GetLectionByFullNameandChapterSN(strVolumeName, iChapterSN, iVerseSNBegin, iVerseSNEnd);
                    }
                }

                //开始节号与结束节号
                CalculateVerseNumberCount(iVerseSNBegin, iVerseSNEnd);

            }

        }

        private void buttonDeleteList_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除此列表？","提示",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                try
                {
                    //删除经文列表
                    bool bResult = ListHelper.DeleteFile(this.comboBoxLectionList.Items[this.comboBoxLectionList.SelectedIndex].ToString() + ".txt");

                    if (bResult)
                    {
                        //MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        listViewLectionList.Items.Clear();

                        InitLectionList();
                    }
                    else
                    {
                        MessageBox.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception)
                {
                }

            }

        }

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            if (this.listViewLectionList.SelectedIndices.Count > 0)
            {
                if (listViewLectionList.SelectedItems[0].Index > 0)
                {
                    //保存上一条记录

                    string strOldItemText = listViewLectionList.Items[listViewLectionList.SelectedItems[0].Index - 1].SubItems[1].Text;

                    //将当前选中的赋值给上一条
                    listViewLectionList.Items[listViewLectionList.SelectedItems[0].Index - 1].SubItems[1].Text =
                        listViewLectionList.Items[listViewLectionList.SelectedItems[0].Index].SubItems[1].Text;

                    //将上一条记录赋值给当前选中的
                    listViewLectionList.Items[listViewLectionList.SelectedItems[0].Index].SubItems[1].Text = strOldItemText;

                    listViewLectionList.Items[listViewLectionList.SelectedItems[0].Index - 1].Selected = true;

                }

            }
        }

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            if (this.listViewLectionList.SelectedIndices.Count > 0)
            {
                if (listViewLectionList.SelectedItems[0].Index + 1 < this.listViewLectionList.Items.Count)
                {
                    //保存上一条记录

                    string strOldItemText = listViewLectionList.Items[listViewLectionList.SelectedItems[0].Index + 1].SubItems[1].Text;

                    //将当前选中的赋值给上一条
                    listViewLectionList.Items[listViewLectionList.SelectedItems[0].Index + 1].SubItems[1].Text =
                        listViewLectionList.Items[listViewLectionList.SelectedItems[0].Index].SubItems[1].Text;

                    //将上一条记录赋值给当前选中的
                    listViewLectionList.Items[listViewLectionList.SelectedItems[0].Index].SubItems[1].Text = strOldItemText;

                    listViewLectionList.Items[listViewLectionList.SelectedItems[0].Index + 1].Selected = true;

                }

            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            //if (comboBoxLectionList.SelectedIndex <= 0)
            //{
            //    MessageBox.Show("请先选择经文列表！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            if (this.listViewLectionList.SelectedIndices.Count == 0)
            {
                MessageBox.Show("请选择要删除的经文！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.listViewLectionList.Items[this.listViewLectionList.SelectedIndices[0]].Remove();

            //重新编号
            if (this.listViewLectionList.Items.Count > 0)
            {
                for (int i = 0; i < this.listViewLectionList.Items.Count; i++)
                {
                    this.listViewLectionList.Items[i].SubItems[0].Text = (i + 1).ToString();
                }
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            string strSelectedFileName = "";

            if (comboBoxLectionList.SelectedIndex <= 0 || comboBoxLectionList.Items[comboBoxLectionList.SelectedIndex].ToString() == _strCreateNewList)
            {
                //MessageBox.Show("请选择要保存的经文列表！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;

                strSelectedFileName = CreateNewList();

            }
            else
            {
                strSelectedFileName = comboBoxLectionList.Items[comboBoxLectionList.SelectedIndex].ToString();
            }

            string strFileContent = "";

            //if (strSelectedFileName == "")
            //{
            //    //MessageBox.Show("请选择要保存的经文列表！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //return;

            //    strSelectedFileName = CreateNewList();

            //    if (strSelectedFileName == "")
            //    {
            //        return;

            //    }
            //}

            if (this.listViewLectionList.Items.Count > 0)
            {
                for (int i = 0; i < this.listViewLectionList.Items.Count; i++)
                {
                    strFileContent += this.listViewLectionList.Items[i].SubItems[1].Text;
                    strFileContent += "|";
                }

                if (strFileContent.EndsWith("|"))
                {
                    strFileContent = strFileContent.Remove(strFileContent.Length - 1);
                }

                //保存经文列表
                bool bResult = ListHelper.WriteFileContent(strSelectedFileName + ".txt", strFileContent);

                if (bResult)
                {
                    MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //重新加载经文列表


                    StrLastLectionList = strSelectedFileName;

                    InitLectionList();

                    //for(int j=0;j<comboBoxLectionList.Items.Count;j++)
                    //{
                    //    if (comboBoxLectionList.Items[j].ToString() == strSelectedFileName)
                    //    {
                    //        comboBoxLectionList.SelectedIndex = j;
                    //        break;
                    //    }
                    //}
                }
                else
                {
                    MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("经文列表为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            comboBoxLectionList_SelectedIndexChanged(sender, e);
        }

        private void comboBoxTitleFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            StrTitleFontName = comboBoxTitleFontName.Items[comboBoxTitleFontName.SelectedIndex].ToString();
            _configHelper.Write("TitleFontName", comboBoxTitleFontName.Items[comboBoxTitleFontName.SelectedIndex].ToString());
            ReloadConfigParameters();
            ShowLectionContent();
        }

        private void comboBoxTitleFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntTitleFontSize = int.Parse(comboBoxTitleFontSize.Items[comboBoxTitleFontSize.SelectedIndex].ToString());
            _configHelper.Write("TitleFontSize", comboBoxTitleFontSize.Items[comboBoxTitleFontSize.SelectedIndex].ToString());
            ReloadConfigParameters();
            ShowLectionContent();
        }

        private void comboBoxTitleFontStyle_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (comboBoxTitleFontStyle.Items[comboBoxTitleFontStyle.SelectedIndex].ToString())
            {
                case "正常":
                    FontStyleTitle = FontStyle.Regular;
                    break;
                case "加粗":
                    FontStyleTitle = FontStyle.Bold;
                    break;
                case "倾斜":
                    FontStyleTitle = FontStyle.Italic;
                    break;
                default:
                    FontStyleTitle = FontStyle.Regular;
                    break;
            }

            //FontStyleTitle = (FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), comboBoxTitleFontStyle.Items[comboBoxTitleFontStyle.SelectedIndex].ToString());

            _configHelper.Write("TitleFontStyle", comboBoxTitleFontStyle.Items[comboBoxTitleFontStyle.SelectedIndex].ToString());
            ReloadConfigParameters();
            ShowLectionContent();
        }

        private void labelTitleColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = labelTitleColor.BackColor;
            cd.ShowDialog();

            ColorTitle = cd.Color;
            labelTitleColor.BackColor = ColorTitle;
            cd.Dispose();
            cd = null;

            _configHelper.Write("TitleColor", ColorTitle.Name.ToString());
            ReloadConfigParameters();
            ShowLectionContent();
        }

        private void labelBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = labelBackColor.BackColor;
            cd.ShowDialog();

            ColorBack = cd.Color;
            labelBackColor.BackColor = ColorBack;
            cd.Dispose();
            cd = null;

            _configHelper.Write("BackColor", ColorBack.Name.ToString());
            ReloadConfigParameters();
            ShowLectionContent();
        }

        private void comboBoxLectionFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            StrLectionFontName = comboBoxLectionFontName.Items[comboBoxLectionFontName.SelectedIndex].ToString();
            _configHelper.Write("LectionFontName", comboBoxLectionFontName.Items[comboBoxLectionFontName.SelectedIndex].ToString());
            ReloadConfigParameters();
            ShowLectionContent();
        }

        private void comboBoxLectionFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntLectionFontSize = int.Parse(comboBoxLectionFontSize.Items[comboBoxLectionFontSize.SelectedIndex].ToString());
            _configHelper.Write("LectionFontSize", comboBoxLectionFontSize.Items[comboBoxLectionFontSize.SelectedIndex].ToString());
            ReloadConfigParameters();
            ShowLectionContent();
        }

        private void comboBoxLectionFontStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxLectionFontStyle.Items[comboBoxLectionFontStyle.SelectedIndex].ToString())
            {
                case "正常":
                    FontStyleLection = FontStyle.Regular;
                    break;
                case "加粗":
                    FontStyleLection = FontStyle.Bold;
                    break;
                case "倾斜":
                    FontStyleLection = FontStyle.Italic;
                    break;
                default:
                    FontStyleLection = FontStyle.Regular;
                    break;
            }

            //FontStyleLection = (FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), comboBoxLectionFontStyle.Items[comboBoxLectionFontStyle.SelectedIndex].ToString());
            _configHelper.Write("LectionFontStyle", comboBoxLectionFontStyle.Items[comboBoxLectionFontStyle.SelectedIndex].ToString());
            ReloadConfigParameters();
            ShowLectionContent();
        }

        private void labelLectionColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = labelLectionColor.BackColor;
            cd.ShowDialog();

            ColorLection = cd.Color;
            labelLectionColor.BackColor = ColorLection;
            cd.Dispose();
            cd = null;

            _configHelper.Write("LectionColor", ColorLection.Name.ToString());
            ReloadConfigParameters();
            ShowLectionContent();
        }

        private void comboBoxLineSpacing_SelectedIndexChanged(object sender, EventArgs e)
        {
            FloatLineSpaceScale = float.Parse(comboBoxLineSpacing.Items[comboBoxLineSpacing.SelectedIndex].ToString());
            _configHelper.Write("LineSpaceScale", comboBoxLineSpacing.Items[comboBoxLineSpacing.SelectedIndex].ToString());
            ReloadConfigParameters();
            ShowLectionContent();
        }

        private void buttonScrollUp_Click(object sender, EventArgs e)
        {
            _formScreen.ScrollScreen(50);
        }

        private void buttonScrollDown_Click(object sender, EventArgs e)
        {
            _formScreen.ScrollScreen(-50);

        }

        private void buttonPreviousLection_Click(object sender, EventArgs e)
        {
            if (listViewLectionList.Items.Count <= 0)
            {
                return;
            }

            try
            {
                if (listViewLectionList.SelectedItems.Count <= 0) //判断有没有选择项
                {
                    listViewLectionList.Items[0].Selected = true;
                }

                //计算选择项的位置
                int iPageUpIndex = listViewLectionList.SelectedItems[0].Index - 1 < 0 ? 0 : listViewLectionList.SelectedItems[0].Index - 1;
                listViewLectionList.Items[iPageUpIndex].Selected = true;
            }
            catch (Exception)
            {
            }
        }

        private void buttonNextLection_Click(object sender, EventArgs e)
        {
            if (listViewLectionList.Items.Count <= 0)
            {
                return;
            }

            try
            {
                if (listViewLectionList.SelectedItems.Count <= 0) //判断有没有选择项
                {
                    listViewLectionList.Items[0].Selected = true;
                    return;
                }

                //计算选择项的位置
                int iPageDownIndex = listViewLectionList.SelectedItems[0].Index + 1 >= listViewLectionList.Items.Count ? listViewLectionList.SelectedItems[0].Index : listViewLectionList.SelectedItems[0].Index + 1;
                listViewLectionList.Items[iPageDownIndex].Selected = true;
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxFontSpacing_SelectedIndexChanged(object sender, EventArgs e)
        {
            FloatFontSpaceScale = float.Parse(comboBoxFontSpacing.Items[comboBoxFontSpacing.SelectedIndex].ToString());
            _configHelper.Write("FontSpaceScale", comboBoxFontSpacing.Items[comboBoxFontSpacing.SelectedIndex].ToString());
            ReloadConfigParameters();
            ShowLectionContent();
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\圣经投影工具使用说明.doc");

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            Process.Start("http://Elijah.com.cn/");
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            Process.Start("http://Elijah.com.cn/");
        }

        private void toolStripStatusLabel4_Click(object sender, EventArgs e)
        {
            Process.Start("http://Elijah.com.cn/");
        }

        private void comboBoxLectionLeftSpace_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntLectionLeftSpace = int.Parse(comboBoxLectionLeftSpace.Items[comboBoxLectionLeftSpace.SelectedIndex].ToString());
            _configHelper.Write("LectionLeftSpace", comboBoxLectionLeftSpace.Items[comboBoxLectionLeftSpace.SelectedIndex].ToString());
            ReloadConfigParameters();
            ShowLectionContent();
        }

        private void labelVerseNumberColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = labelVerseNumberColor.BackColor;
            cd.ShowDialog();

            ColorVerseNumber = cd.Color;
            labelVerseNumberColor.BackColor = ColorVerseNumber;
            cd.Dispose();
            cd = null;

            _configHelper.Write("VerseNumberColor", ColorVerseNumber.Name.ToString());
            ReloadConfigParameters();
            ShowLectionContent();
        }


    }
}
