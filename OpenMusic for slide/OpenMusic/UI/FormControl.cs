using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenMusic.Properties;
using OpenMusic.DAL;
using OpenMusic.BLL;
using System.Threading;
using System.Diagnostics;

namespace OpenMusic.UI
{
    public partial class FormControl : Form
    {

        //是否显示标题
        public bool BoolIsShowTitle;

        //标题
        public string StrTitle;
        //页号
        public string StrPageNumber;
        //歌词
        public string StrLyric;

        ////开始节号
        //public int IntVerseNumberBegin;

        ////结束节号
        //public int IntVerseNumberEnd;

        /////所有节号字符串
        //public string StrArrayVerseNumber;

        ////使用拼音查找经文
        //public string StrSearchByPinYin;

        //标题字体名
        public string StrTitleFontName;
        //歌词字体名
        public string StrLyricFontName;

        //页号字体名
        public string StrPageNumberFontName;

        //标题颜色
        public Color ColorTitle;
        //页号颜色
        public Color ColorPageNumber;
        //歌词颜色
        public Color ColorLyric;
        //背景颜色
        public Color ColorBack;

        //是否显示背景图片

        public bool IsShowBackgroundImage;
        //背景图片
        public string StrBackgroundImagePath;

        //标题字号
        public int IntTitleFontSize;
        //页号字号
        public int IntPageNumberFontSize;
        //歌词字号
        public int IntLyricFontSize;
        //标题样式
        public FontStyle FontStyleTitle;
        //页号样式
        public FontStyle FontStylePageNumber;
        //歌词样式
        public FontStyle FontStyleLyric;


        //标题画刷
        public Brush BrushTitle;
        //标题字体
        public Font FontTitle;

        //页码画刷
        public Brush BrushPageNumber;

        //页码字体
        public Font FontPageNumber;


        //歌词画刷
        public Brush BrushLyric;
        //歌词字体
        public Font FontLyric;


        //字体间距倍数
        public int IntFontSpace;

        //歌词行间距倍数
        public int IntLineSpace;

        public int IntLyricLeftSpace;

        //上一次选择的音乐家列表
        public string StrLastMusicianList;

        //上一次选择的专辑列表
        public string StrLastAlbumList;


        //投影窗口
        FormScreen _formScreen = null;


        //字符串
        //private static string _strCreateNewList = "[新建列表]";

        //配置文件写入类
        ConfigHelper _configHelper = null;

        //安装到当前机器中的字体
        private System.Drawing.Text.InstalledFontCollection _objFont = new System.Drawing.Text.InstalledFontCollection();

        //灵歌推荐 窗体
        FormPop formPop;

        public FormControl()
        {
            InitializeComponent();
        }

        private void FormControl_Load(object sender, EventArgs e)
        {

            //读取配置文件的初始值
            //字体名称
            StrTitleFontName = Settings.Default.TitleFontName;
            StrLyricFontName = Settings.Default.LyricFontName;
            StrPageNumberFontName = Settings.Default.PageNumberFontName;

            //颜色
            ColorTitle = Settings.Default.TitleColor;
            ColorPageNumber = Settings.Default.PageNumberColor;
            ColorLyric = Settings.Default.LyricColor;
            ColorBack = Settings.Default.BackColor;



            //是否显示背景图片
            IsShowBackgroundImage = Settings.Default.IsShowBackgroundImage;
            
            //背景图片
            StrBackgroundImagePath = Settings.Default.BackgroundImagePath;


            //字号
            IntTitleFontSize = Settings.Default.TitleFontSize;
            IntLyricFontSize = Settings.Default.LyricFontSize;
            IntPageNumberFontSize = Settings.Default.PageNumberFontSize;
            //IntLyricLeftSpace = Settings.Default.LyricLeftSpace;

            //加载上次打开的列表
            StrLastMusicianList = Settings.Default.LastMusicianList;
            StrLastAlbumList = Settings.Default.LastAlbumList;


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
            switch (Settings.Default.LyricFontStyle)
            {
                case "正常":
                    FontStyleLyric = FontStyle.Regular;
                    break;
                case "加粗":
                    FontStyleLyric = FontStyle.Bold;
                    break;
                case "倾斜":
                    FontStyleLyric = FontStyle.Italic;
                    break;
                default:
                    FontStyleLyric = FontStyle.Regular;
                    break;
            }

            //字符间距及行间距
            IntFontSpace = Settings.Default.FontSpace;
            IntLineSpace = Settings.Default.LineSpace;

            //是否显示标题
            BoolIsShowTitle = Settings.Default.IsShowTitle;
            if (BoolIsShowTitle == false)
            {
                buttonHideOrShowTitle.Text = "显示标题";
            }
            else
            {
                buttonHideOrShowTitle.Text = "隐藏标题";
            }

            //重新加载画刷等参数
            ReloadConfigParameters();

            _configHelper = new ConfigHelper();


            //显示投影窗口
            ShowScreen();

            //移动投影窗口到右上角
            _formScreen.Left = Screen.GetWorkingArea(this).Right - _formScreen.Width;
            _formScreen.Top = Screen.GetWorkingArea(this).Top;


            #region 初始化控件数据
            //初始化系列列表下拉列表控件
            InitMusicianList();

            //初始化标题字体，经文字体控件
            foreach (System.Drawing.FontFamily i in _objFont.Families)
            {
                comboBoxLyricFontName.Items.Add(i.Name.ToString());
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


            comboBoxLyricFontSize.Items.Add("12");
            comboBoxLyricFontSize.Items.Add("20");
            comboBoxLyricFontSize.Items.Add("30");
            comboBoxLyricFontSize.Items.Add("35");
            comboBoxLyricFontSize.Items.Add("40");
            comboBoxLyricFontSize.Items.Add("45");
            comboBoxLyricFontSize.Items.Add("50");
            comboBoxLyricFontSize.Items.Add("60");
            comboBoxLyricFontSize.Items.Add("70");
            comboBoxLyricFontSize.Items.Add("80");
            comboBoxLyricFontSize.Items.Add("90");
            comboBoxLyricFontSize.Items.Add("100");
            comboBoxLyricFontSize.Items.Add("110");
            comboBoxLyricFontSize.Items.Add("120");
            comboBoxLyricFontSize.Items.Add("130");
            comboBoxLyricFontSize.Items.Add("140");
            comboBoxLyricFontSize.Items.Add("150");
            comboBoxLyricFontSize.Items.Add("200");


            //初始化标题、经文的字体样式
            //comboBoxLectionFontStyle.Items.Add(FontStyle.Bold.ToString());
            //comboBoxLectionFontStyle.Items.Add(FontStyle.Italic.ToString());
            //comboBoxLectionFontStyle.Items.Add(FontStyle.Regular.ToString());
            //comboBoxLectionFontStyle.Items.Add(FontStyle.Strikeout.ToString());
            //comboBoxLectionFontStyle.Items.Add(FontStyle.Underline.ToString());

            comboBoxLyricFontStyle.Items.Add("正常");
            comboBoxLyricFontStyle.Items.Add("加粗");
            comboBoxLyricFontStyle.Items.Add("倾斜");

            //comboBoxTitleFontStyle.Items.Add(FontStyle.Bold.ToString());
            //comboBoxTitleFontStyle.Items.Add(FontStyle.Italic.ToString());
            //comboBoxTitleFontStyle.Items.Add(FontStyle.Regular.ToString());
            //comboBoxTitleFontStyle.Items.Add(FontStyle.Strikeout.ToString());
            //comboBoxTitleFontStyle.Items.Add(FontStyle.Underline.ToString());

            comboBoxTitleFontStyle.Items.Add("正常");
            comboBoxTitleFontStyle.Items.Add("加粗");
            comboBoxTitleFontStyle.Items.Add("倾斜");


            //初始化行间距
            comboBoxLineSpacing.Items.Add("-100");
            comboBoxLineSpacing.Items.Add("-50");
            comboBoxLineSpacing.Items.Add("-30");
            comboBoxLineSpacing.Items.Add("-20");
            comboBoxLineSpacing.Items.Add("-10");
            comboBoxLineSpacing.Items.Add("-5");
            comboBoxLineSpacing.Items.Add("0");
            comboBoxLineSpacing.Items.Add("5");
            comboBoxLineSpacing.Items.Add("10");
            comboBoxLineSpacing.Items.Add("15");
            comboBoxLineSpacing.Items.Add("20");
            comboBoxLineSpacing.Items.Add("30");
            comboBoxLineSpacing.Items.Add("40");
            comboBoxLineSpacing.Items.Add("50");
            comboBoxLineSpacing.Items.Add("60");
            comboBoxLineSpacing.Items.Add("70");
            comboBoxLineSpacing.Items.Add("80");
            comboBoxLineSpacing.Items.Add("90");
            comboBoxLineSpacing.Items.Add("100");
            comboBoxLineSpacing.Items.Add("150");
            comboBoxLineSpacing.Items.Add("200");
            comboBoxLineSpacing.Items.Add("250");
            comboBoxLineSpacing.Items.Add("300");
            comboBoxLineSpacing.Items.Add("400");
            comboBoxLineSpacing.Items.Add("500");
            comboBoxLineSpacing.Items.Add("600");
            comboBoxLineSpacing.Items.Add("700");


            //初始化字间距
            comboBoxFontSpacing.Items.Add("-100");
            comboBoxFontSpacing.Items.Add("-80");
            comboBoxFontSpacing.Items.Add("-50");
            comboBoxFontSpacing.Items.Add("-20");
            comboBoxFontSpacing.Items.Add("-10");
            comboBoxFontSpacing.Items.Add("-5");
            comboBoxFontSpacing.Items.Add("0");
            comboBoxFontSpacing.Items.Add("5");
            comboBoxFontSpacing.Items.Add("10");
            comboBoxFontSpacing.Items.Add("15");
            comboBoxFontSpacing.Items.Add("20");
            comboBoxFontSpacing.Items.Add("30");
            comboBoxFontSpacing.Items.Add("40");
            comboBoxFontSpacing.Items.Add("50");
            comboBoxFontSpacing.Items.Add("60");
            comboBoxFontSpacing.Items.Add("70");
            comboBoxFontSpacing.Items.Add("80");
            comboBoxFontSpacing.Items.Add("90");
            comboBoxFontSpacing.Items.Add("100");
            comboBoxFontSpacing.Items.Add("150");
            comboBoxFontSpacing.Items.Add("200");
            comboBoxFontSpacing.Items.Add("250");
            comboBoxFontSpacing.Items.Add("300");
            comboBoxFontSpacing.Items.Add("400");
            comboBoxFontSpacing.Items.Add("500");
            comboBoxFontSpacing.Items.Add("600");
            comboBoxFontSpacing.Items.Add("700");


            //comboBoxLyricLeftSpace.Items.Add("-100");
            //comboBoxLyricLeftSpace.Items.Add("-80");
            //comboBoxLyricLeftSpace.Items.Add("-50");
            //comboBoxLyricLeftSpace.Items.Add("-20");
            //comboBoxLyricLeftSpace.Items.Add("-10");
            //comboBoxLyricLeftSpace.Items.Add("-5");
            //comboBoxLyricLeftSpace.Items.Add("0");
            //comboBoxLyricLeftSpace.Items.Add("5");
            //comboBoxLyricLeftSpace.Items.Add("10");
            //comboBoxLyricLeftSpace.Items.Add("20");
            //comboBoxLyricLeftSpace.Items.Add("30");
            //comboBoxLyricLeftSpace.Items.Add("50");
            //comboBoxLyricLeftSpace.Items.Add("70");
            //comboBoxLyricLeftSpace.Items.Add("100");
            //comboBoxLyricLeftSpace.Items.Add("150");
            //comboBoxLyricLeftSpace.Items.Add("200");
            //comboBoxLyricLeftSpace.Items.Add("300");
            //comboBoxLyricLeftSpace.Items.Add("400");


            comboBoxBackgroundImageLayout.Items.Add("适应");
            comboBoxBackgroundImageLayout.Items.Add("平铺");
            comboBoxBackgroundImageLayout.Items.Add("居中");
            comboBoxBackgroundImageLayout.Items.Add("拉伸");
            comboBoxBackgroundImageLayout.Items.Add("无");


            #endregion



            #region 尺寸与颜色，其他默认值

            //用设计器设置这里居然有bug，只好用代码。。。
            //设置panel的最小尺寸
            //this.splitContainer1.Panel2MinSize = 300;
            //this.splitContainer1.Panel1MinSize = 400;


            //画布背景色
            //this.panelLectionContent.BackColor = ColorBack;

            //经文背景色
            //this.richTextBoxLectionContent.BackColor = ColorBack;

            //背景颜色
            this.labelBackColor.BackColor = ColorBack;


            //背景图片
            if (IsShowBackgroundImage)
            {
                checkBoxIsShowBackgroundImage.Checked = true;
            }
            else
            {
                checkBoxIsShowBackgroundImage.Checked = false;
            }



            //标题颜色
            this.labelTitleColor.BackColor = ColorTitle;

            //节号颜色
            this.labelPageNumberColor.BackColor = ColorPageNumber;

            //经文颜色
            this.labelLyricColor.BackColor = ColorLyric;

            //标题、经文字体名称
            comboBoxLyricFontName.Text = StrLyricFontName;
            comboBoxTitleFontName.Text = StrTitleFontName;

            //标题、经文字号控件
            comboBoxLyricFontSize.Text = IntLyricFontSize.ToString();
            comboBoxTitleFontSize.Text = IntTitleFontSize.ToString();

            //标题、经文字样式
            comboBoxTitleFontStyle.Text = Settings.Default.TitleFontStyle;// FontStyleTitle.ToString();
            comboBoxLyricFontStyle.Text = Settings.Default.LyricFontStyle;// FontStyleLection.ToString();

            //行间距
            comboBoxLineSpacing.Text = IntLineSpace.ToString();

            comboBoxFontSpacing.Text = IntFontSpace.ToString();

            //comboBoxLyricLeftSpace.Text = IntLyricLeftSpace.ToString();


            //背景图片位置（布局）
            switch (Settings.Default.BackgroundImageLayout)
            {
                case ImageLayout.Zoom:
                    //适应
                    comboBoxBackgroundImageLayout.Text = "适应";
                    break;
                case ImageLayout.Tile:
                    //平铺
                    comboBoxBackgroundImageLayout.Text = "平铺";

                    break;
                case ImageLayout.Center:
                    //居中
                    comboBoxBackgroundImageLayout.Text = "居中";

                    break;
                case ImageLayout.Stretch:
                    //拉伸
                    comboBoxBackgroundImageLayout.Text = "拉伸";

                    break;
                case ImageLayout.None:
                    //无
                    comboBoxBackgroundImageLayout.Text = "无";

                    break;
                default:
                    comboBoxBackgroundImageLayout.Text = "适应";
                    break;

            }



            //是否忽略换行符
            if (Settings.Default.IsIgnoreReturn)
            {
                checkBoxIsIgnoreReturn.Checked = true;
            }
            else
            {
                checkBoxIsIgnoreReturn.Checked = false;
            }


            //显示歌词
            StrTitle = "";
            StrLyric = "";
            StrPageNumber = "";



            #endregion



            //显示“灵歌推荐”窗体
            formPop = new FormPop();
            formPop.StartPosition = FormStartPosition.Manual;
            formPop.Left = this.Right;
            formPop.Top = this.Top; ;
            //formPop.Show();

            //this.axWindowsMediaPlayer1.settings.autoStart = true;
            //this.axWindowsMediaPlayer1.settings.setMode("Loop", true);


        }


        //初始化与配置文件有关的参数
        private void ReloadConfigParameters()
        {
            #region 配置文件相关参数

            //标题画刷和字体
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

            //歌词画刷和字体
            if (BrushLyric != null)
            {
                BrushLyric.Dispose();
            }
            BrushLyric = new SolidBrush(ColorLyric);

            if (BrushPageNumber != null)
            {
                BrushPageNumber.Dispose();
            }
            BrushPageNumber = new SolidBrush(ColorPageNumber);

            //页码画刷和字体
            if (FontPageNumber != null)
            {
                FontPageNumber.Dispose();
            }
            FontPageNumber = new Font(StrPageNumberFontName, IntPageNumberFontSize, FontStylePageNumber);

            if (FontLyric != null)
            {
                FontLyric.Dispose();
            }
            FontLyric = new Font(StrLyricFontName, IntLyricFontSize, FontStyleLyric);
            
            #endregion

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

        private void listViewMusicList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                //显示音乐歌词内容
                if (listViewMusicList.SelectedIndices.Count > 0)
                {

                    //停止正在播放的歌曲
                    this.axWindowsMediaPlayer1.Ctlcontrols.stop();
                    
                    textBoxLyric.Text = "";


                    //将目录和文件名拼接起来，找到文件，并打开、显示
                    string strLyricFilePath = "";
                    strLyricFilePath = listViewMusicList.Items[listViewMusicList.SelectedIndices[0]].Tag.ToString();
                    strLyricFilePath = strLyricFilePath.Remove(strLyricFilePath.Length - 4);
                    strLyricFilePath = strLyricFilePath + ".txt";

                    //显示歌词
                    StrTitle = "";
                    StrLyric = "";
                    StrPageNumber = "";


                    //如果[忽略换行]，则去除换行符

                    StrLyric = MusicHelper.GetLyricsByLyricFilePath(strLyricFilePath).Trim();

                    StrTitle = listViewMusicList.Items[listViewMusicList.SelectedIndices[0]].Text;
                    //如果前n位为阿拉伯数字，则移除前n位

                    int iNumCount = 0;
                    for (int i = 0; i < StrTitle.Length; i++)
                    {
                        if (IsNumeric(StrTitle[i].ToString()))
                        {
                            iNumCount++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    StrTitle = StrTitle.Substring(iNumCount);

                    textBoxLyric.Text = StrLyric;


                    //逐行去除行前空格和行后空格，并将换行符替换为空格

                    string[] strsLyric = StrLyric.Replace("\r\n", "\\").Split('\\');

                    StrLyric = "";

                    for (int iRowNum = 0; iRowNum < strsLyric.Length; iRowNum++)
                    {
                        strsLyric[iRowNum] = strsLyric[iRowNum].Trim();

                        StrLyric += strsLyric[iRowNum];

                        if (iRowNum != strsLyric.Length - 1)
                        {
                            //如果忽略换行，则用空格连接
                            if (Settings.Default.IsIgnoreReturn)
                            {
                                //StrLyric += " ";
                            }
                            else
                            {
                                StrLyric += "\r\n";
                            }

                        }
                    }


                    //播放音乐
                    //音频文件，自动匹配mp3，wma，mid格式
                    string strAudioFilePath = "";
                    strAudioFilePath = listViewMusicList.Items[listViewMusicList.SelectedIndices[0]].Tag.ToString();

                    axWindowsMediaPlayer1.currentPlaylist.clear();

                    axWindowsMediaPlayer1.currentPlaylist.appendItem(axWindowsMediaPlayer1.newMedia(listViewMusicList.Items[listViewMusicList.SelectedIndices[0]].Tag.ToString()));

                    //this.axWindowsMediaPlayer1.Ctlcontrols.playItem(axWindowsMediaPlayer1.currentPlaylist.get_Item(listViewMusicList.SelectedIndices[0]));

                    if (!listViewMusicList.Items[listViewMusicList.SelectedIndices[0]].Tag.ToString().EndsWith(".txt"))
                    {
                        this.axWindowsMediaPlayer1.Ctlcontrols.playItem(axWindowsMediaPlayer1.currentPlaylist.get_Item(0));
                    }

                    DrawLyric();


                    //_formScreen.BIsNeedReCalculatePosition = true;

                    ////强制重绘
                    //_formScreen.Invalidate(true);
                    //
                    //MessageBox.Show(this.axWindowsMediaPlayer1.currentMedia.name);

                }



            }
            catch (Exception)
            {
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




        //初始化系列列表下拉列表控件
        private void InitMusicianList()
        {
            comboBoxMusicianList.Items.Clear();

            string strList = ListHelper.GetMusicianList();
            if (strList != "")
            {
                string[] strsMusician = strList.Split('|');
                for (int i = 0; i < strsMusician.Length; i++)
                {
                    if (strsMusician[i] == "")
                    {

                    }
                    else
                    {
                        comboBoxMusicianList.Items.Add(strsMusician[i].ToLower());
                    }
                }
                Array.Clear(strsMusician, 0, strsMusician.Length);
            }

            //定位到刚创建的系列列表
            for (int i = 0; i < this.comboBoxMusicianList.Items.Count; i++)
            {
                if (StrLastMusicianList == this.comboBoxMusicianList.Items[i].ToString())
                {
                    this.comboBoxMusicianList.SelectedIndex = i;

                    //定位到某个专辑
                    InitAlbumList();


                    break;
                }
            }

        }

        //初始化专辑列表控件，用于定位到上一次选择的专辑列表
        private void InitAlbumList()
        {
            comboBoxAlbumList.Items.Clear();

            if (comboBoxMusicianList.Items.Count <= 0 || comboBoxMusicianList.SelectedIndex < 0)
            {
                return;
            }

            if (comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString() == "")
            {
                return;
            }

            string strList = ListHelper.GetAlbumList(comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString());

            if (strList == "")
            {
                return;
            }

            string[] strsAlbum = strList.Split('|');
            for (int i = 0; i < strsAlbum.Length; i++)
            {
                if (strsAlbum[i] == "")
                {

                }
                else
                {
                    comboBoxAlbumList.Items.Add(strsAlbum[i].ToLower());
                }
            }
            Array.Clear(strsAlbum, 0, strsAlbum.Length);


            //定位到刚创建的系列列表
            for (int i = 0; i < this.comboBoxAlbumList.Items.Count; i++)
            {
                if (StrLastAlbumList == this.comboBoxAlbumList.Items[i].ToString())
                {
                    this.comboBoxAlbumList.SelectedIndex = i;
                    break;
                }
            }

        }


        //编辑音乐列表
        private void buttonEditMusicList_Click(object sender, EventArgs e)
        {
            FormList formList = new FormList(this);
            formList.ShowDialog();
            formList.Dispose();
            formList = null;

            InitMusicianList();

        }



        //选择系列后，填充专辑列表
        private void comboBoxMusicianList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBoxAlbumList.Items.Clear();

            comboBoxAlbumList.Items.Add("");

            if (comboBoxMusicianList.Items.Count <= 0 || comboBoxMusicianList.SelectedIndex < 0)
            {
                return;
            }


            string strList = ListHelper.GetAlbumList(comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString());
            comboBoxAlbumList.Items.Clear();

            if (strList != "")
            {
                string[] strsAlbum = strList.Split('|');
                for (int i = 0; i < strsAlbum.Length; i++)
                {
                    if (strsAlbum[i] == "")
                    {

                    }
                    else
                    {
                        comboBoxAlbumList.Items.Add(strsAlbum[i].ToLower());
                    }
                }
                Array.Clear(strsAlbum, 0, strsAlbum.Length);
            }

        }


        //选择专辑后，显示音乐列表
        private void comboBoxAlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAlbumList.SelectedIndex < 0)
            {
                return;
            }

            StrLastMusicianList = comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString();
            StrLastAlbumList = comboBoxAlbumList.Items[comboBoxAlbumList.SelectedIndex].ToString();

            _configHelper.Write("LastMusicianList", StrLastMusicianList);
            _configHelper.Write("LastAlbumList", StrLastAlbumList);

            if (StrLastAlbumList == "")
            {
                return;
            }

            listViewMusicList.Items.Clear();


            //显示音乐列表
            string strMusicList = ListHelper.GetMusicList(StrLastMusicianList, StrLastAlbumList);

            //清除WMP中的音乐列表
            axWindowsMediaPlayer1.currentPlaylist.clear();

            if (strMusicList != "")
            {
                string[] strsContent = strMusicList.Split('|');

                for (int i = 0; i < strsContent.Length; i++)
                {
                    if (strsContent[i] != "")
                    {
                        //ListViewItem item = new ListViewItem((i + 1).ToString());
                        //item.SubItems.Add(strsContent[i].Remove(strsContent[i].Length - 4, 4));

                        //string strID = strsContent[i].Substring(0, 3);
                        //string strName = strsContent[i].Remove(strsContent[i].Length - 4, 4).Substring(3);

                        string strName = strsContent[i].Remove(strsContent[i].Length - 4, 4);

                        ////去掉开头的0
                        //while (strID[0] == '0')
                        //{
                        //    strID = strID.Substring(1, strID.Length - 1);
                        //}

                        ListViewItem item = new ListViewItem(strName);
                        //item.SubItems.Add(strName);

                        //将音频文件路径存入tag属性
                        //将目录和文件名拼接起来，自动匹配mp3，wma，mid格式
                        string strAudioFilePath = null;
                        strAudioFilePath = MusicHelper.GetAudioFilePathByLyricFilePath(MusicHelper.GetLyricFilePathByLyricFileName(comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString(),
                        comboBoxAlbumList.Items[comboBoxAlbumList.SelectedIndex].ToString(), strsContent[i]));


                        if (strAudioFilePath == "")
                        {
                            item.Tag = MusicHelper.GetLyricFilePathByLyricFileName(comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString(),
                        comboBoxAlbumList.Items[comboBoxAlbumList.SelectedIndex].ToString(), strsContent[i]);
                        }
                        else
                        {
                            item.Tag = strAudioFilePath;
                        }


                        listViewMusicList.Items.Add(item);
                        //axWindowsMediaPlayer1.currentPlaylist.appendItem(axWindowsMediaPlayer1.newMedia(strAudioFilePath));


                    }
                }

                Array.Clear(strsContent, 0, strsContent.Length);

            }




            /*

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
                    ////弹出窗口让用户输入播经文列表名


                    ShowNewList(CreateNewList());



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

             */



        }

        public void buttonPreviousMusic_Click(object sender, EventArgs e)
        {
            if (listViewMusicList.Items.Count <= 0)
            {
                return;
            }

            try
            {
                if (listViewMusicList.SelectedItems.Count <= 0) //判断有没有选择项
                {
                    listViewMusicList.Items[0].Selected = true;
                }

                //计算选择项的位置
                int iPageUpIndex = listViewMusicList.SelectedItems[0].Index - 1 < 0 ? 0 : listViewMusicList.SelectedItems[0].Index - 1;
                listViewMusicList.Items[iPageUpIndex].Selected = true;
            }
            catch (Exception)
            {
            }
        }

        public void buttonNextMusic_Click(object sender, EventArgs e)
        {
            if (listViewMusicList.Items.Count <= 0)
            {
                return;
            }

            try
            {
                if (listViewMusicList.SelectedItems.Count <= 0) //判断有没有选择项
                {
                    listViewMusicList.Items[0].Selected = true;
                    return;
                }

                //计算选择项的位置
                int iPageDownIndex = listViewMusicList.SelectedItems[0].Index + 1 >= listViewMusicList.Items.Count ? listViewMusicList.SelectedItems[0].Index : listViewMusicList.SelectedItems[0].Index + 1;
                listViewMusicList.Items[iPageDownIndex].Selected = true;
            }
            catch (Exception)
            {
            }
        }

        private void axWindowsMediaPlayer1_CurrentItemChange(object sender, AxWMPLib._WMPOCXEvents_CurrentItemChangeEvent e)
        {

            //在歌曲列表中查找同名歌曲，选中

            //MessageBox.Show(this.axWindowsMediaPlayer1.name);


        }

        private void buttonHideOrShowTitle_Click(object sender, EventArgs e)
        {
            if (buttonHideOrShowTitle.Text == "隐藏标题")
            {
                BoolIsShowTitle = false;
                buttonHideOrShowTitle.Text = "显示标题";
            }
            else
            {
                BoolIsShowTitle = true;
                buttonHideOrShowTitle.Text = "隐藏标题";
            }

            //更新


            DrawLyric();


            //_formScreen.BIsNeedReCalculatePosition = true;

            ////强制重绘
            //_formScreen.Invalidate(true);

            //保存到配制文件
            _configHelper.Write("IsShowTitle", BoolIsShowTitle.ToString());

        }

        private void comboBoxTitleFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            StrTitleFontName = comboBoxTitleFontName.Items[comboBoxTitleFontName.SelectedIndex].ToString();
            _configHelper.Write("TitleFontName", comboBoxTitleFontName.Items[comboBoxTitleFontName.SelectedIndex].ToString());
            ReloadConfigParameters();
            DrawLyric();
        }

        private void buttonScrollDown_Click(object sender, EventArgs e)
        {
            _formScreen.TurnPage(1);
        }

        private void buttonScrollUp_Click(object sender, EventArgs e)
        {
            _formScreen.TurnPage(-1);
        }

        private void buttonKeyboardControl_Click(object sender, EventArgs e)
        {
            _formScreen.Focus();
        }

        private void buttonCancelModify_Click(object sender, EventArgs e)
        {
            textBoxLyric.Text = "";
            textBoxLyric.Text = StrLyric;
        }

        private void buttonSaveLyric_Click(object sender, EventArgs e)
        {
            string strLyricFilePath = "";

            try
            {

                strLyricFilePath = listViewMusicList.Items[listViewMusicList.SelectedIndices[0]].Tag.ToString();
                strLyricFilePath = strLyricFilePath.Remove(strLyricFilePath.Length - 4);
                strLyricFilePath = strLyricFilePath + ".txt";

                StrLyric = textBoxLyric.Text;
                //StrLyric = MusicHelper.GetLyricsByLyricFilePath(strLyricFilePath).Trim();

                //StrLyric = MusicHelper.GetLyricsByLyricFilePath(strLyricFilePath).Trim();

                string[] strsLyric = StrLyric.Replace("\r\n", "\\").Split('\\');

                StrLyric = "";

                for (int iRowNum = 0; iRowNum < strsLyric.Length; iRowNum++)
                {
                    strsLyric[iRowNum] = strsLyric[iRowNum].Trim();

                    StrLyric += strsLyric[iRowNum];

                    if (iRowNum != strsLyric.Length - 1)
                    {
                        //如果忽略换行，则用空格连接
                        if (Settings.Default.IsIgnoreReturn)
                        {
                            //StrLyric += " ";
                        }
                        else
                        {
                            StrLyric += "\r\n";
                        }

                    }
                }


                MusicHelper.SetLyricsByLyricFilePath(strLyricFilePath, StrLyric);

                DrawLyric();


                //_formScreen.BIsNeedReCalculatePosition = true;

                ////强制重绘
                //_formScreen.Invalidate(true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        //显示歌词
        private void DrawLyric()
        {

            //显示投影屏幕
            ShowScreen();

            if (StrLyric == null || StrLyric == "")
            {
                return;
            }

            _formScreen.BackColor = ColorBack;

            //重新计算歌词文字的位置
            _formScreen.BIsNeedReCalculatePosition = true;

            //强制重绘
            _formScreen.Invalidate(true);

        }

        private void comboBoxTitleFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntTitleFontSize = int.Parse(comboBoxTitleFontSize.Items[comboBoxTitleFontSize.SelectedIndex].ToString());
            _configHelper.Write("TitleFontSize", comboBoxTitleFontSize.Items[comboBoxTitleFontSize.SelectedIndex].ToString());
            ReloadConfigParameters();
            DrawLyric();
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
            DrawLyric();
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
            DrawLyric();
        }

        private void labelLyricColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = labelLyricColor.BackColor;
            cd.ShowDialog();

            ColorLyric = cd.Color;
            labelLyricColor.BackColor = ColorLyric;
            cd.Dispose();
            cd = null;

            _configHelper.Write("LyricColor", ColorLyric.Name.ToString());
            ReloadConfigParameters();
            DrawLyric();
        }

        private void labelPageNumberColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = labelPageNumberColor.BackColor;
            cd.ShowDialog();

            ColorPageNumber= cd.Color;
            labelPageNumberColor.BackColor = ColorPageNumber;
            cd.Dispose();
            cd = null;

            _configHelper.Write("PageNumberColor", ColorPageNumber.Name.ToString());
            ReloadConfigParameters();
            DrawLyric();
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
            DrawLyric();
        }

        private void comboBoxLyricFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            StrLyricFontName = comboBoxLyricFontName.Items[comboBoxLyricFontName.SelectedIndex].ToString();
            _configHelper.Write("LyricFontName", comboBoxLyricFontName.Items[comboBoxLyricFontName.SelectedIndex].ToString());
            ReloadConfigParameters();
            DrawLyric();
        }

        private void comboBoxLyricFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntLyricFontSize = int.Parse(comboBoxLyricFontSize.Items[comboBoxLyricFontSize.SelectedIndex].ToString());
            _configHelper.Write("LyricFontSize", comboBoxLyricFontSize.Items[comboBoxLyricFontSize.SelectedIndex].ToString());
            ReloadConfigParameters();
            DrawLyric();
        }

        private void comboBoxLyricFontStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxLyricFontStyle.Items[comboBoxLyricFontStyle.SelectedIndex].ToString())
            {
                case "正常":
                    FontStyleLyric = FontStyle.Regular;
                    break;
                case "加粗":
                    FontStyleLyric = FontStyle.Bold;
                    break;
                case "倾斜":
                    FontStyleLyric = FontStyle.Italic;
                    break;
                default:
                    FontStyleLyric = FontStyle.Regular;
                    break;
            }

            _configHelper.Write("LyricFontStyle", comboBoxLyricFontStyle.Items[comboBoxLyricFontStyle.SelectedIndex].ToString());
            ReloadConfigParameters();
            DrawLyric();
        }

        private void comboBoxFontSpacing_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntFontSpace = int.Parse(comboBoxFontSpacing.Items[comboBoxFontSpacing.SelectedIndex].ToString());
            _configHelper.Write("FontSpace", comboBoxFontSpacing.Items[comboBoxFontSpacing.SelectedIndex].ToString());
            ReloadConfigParameters();
            DrawLyric();
        }

        private void comboBoxLineSpacing_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntLineSpace = int.Parse(comboBoxLineSpacing.Items[comboBoxLineSpacing.SelectedIndex].ToString());
            _configHelper.Write("LineSpace", comboBoxLineSpacing.Items[comboBoxLineSpacing.SelectedIndex].ToString());
            ReloadConfigParameters();
            DrawLyric();
        }

        //如果是纯数字还可以采用ASCII码进行判断
        /// <summary>   
        /// 判断是否是数字   
        /// </summary>   
        /// <param name="str">字符串</param>   
        /// <returns>bool</returns>   
        public bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }

        //搜索
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == "")
            {
                return;
            }

            FormSearchResult formSearchResult = new FormSearchResult(textBoxSearch.Text, radioButtonSearchByName.Checked);
            if (formSearchResult.ShowDialog() == DialogResult.OK)
            {
                if (formSearchResult.StrMusicianName == "" || formSearchResult.StrAlbumName == "" || formSearchResult.StrMusicName == "")
                {
                    return;
                }

                //listViewMusicList.Items.Clear();

                //将搜索到的结果显示出来，定位到系列、专辑和音乐，自动播放
                //

                for (int i = 0; i < comboBoxMusicianList.Items.Count; i++)
                {
                    if (comboBoxMusicianList.Items[i].ToString() == formSearchResult.StrMusicianName)
                    {
                        comboBoxMusicianList.SelectedIndex = i;
                        break;
                    }
                }

                //Thread.Sleep(1000);


                for (int i = 0; i < comboBoxAlbumList.Items.Count; i++)
                {
                    if (comboBoxAlbumList.Items[i].ToString() == formSearchResult.StrAlbumName)
                    {
                        comboBoxAlbumList.SelectedIndex = i;
                        break;
                    }
                }
                //Thread.Sleep(1000);

                for (int i = 0; i < listViewMusicList.Items.Count; i++)
                {
                    if (listViewMusicList.Items[i].Text == formSearchResult.StrMusicName)
                    {
                        listViewMusicList.Items[i].Selected = true;
                        break;
                    }
                }
                //Thread.Sleep(1000);

            }

            formSearchResult.Dispose();
        }

        private void buttonShowNews_Click(object sender, EventArgs e)
        {
            //显示“灵歌推荐”窗体
            formPop = new FormPop();
            formPop.StartPosition = FormStartPosition.Manual;
            formPop.Left = this.Right;
            formPop.Top = this.Top; ;
            formPop.Show();

        }

        private void textBoxLyric_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }

        private void textBoxSearch_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = this.buttonSearch;
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.jidulove.com/");
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.kwork.cn/");
        }

        private void toolStripStatusLabel4_Click(object sender, EventArgs e)
        {
            Process.Start("http://hi.baidu.com/opensoulblog/home/");
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\灵歌投影工具使用说明.doc");
        }

        private void buttonSelectBackImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            try
            {
                openFileDialog.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog.FileName != "")
                    {
                        _configHelper.Write("BackgroundImagePath", openFileDialog.FileName);

                        Settings.Default.Reload();

                        checkBoxIsShowBackgroundImage_CheckedChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                openFileDialog.Dispose();
                openFileDialog = null;
            }
           
        }

        public void checkBoxIsShowBackgroundImage_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIsShowBackgroundImage.Checked)
            {
                if (Settings.Default.BackgroundImagePath != "")
                {
                    _configHelper.Write("IsShowBackgroundImage", "True");


                    _formScreen.BackgroundImage = Bitmap.FromFile(Settings.Default.BackgroundImagePath);
                    _formScreen.Invalidate();
                }
            }
            else
            {
                _configHelper.Write("IsShowBackgroundImage", "False");

                _formScreen.BackgroundImage = null;
                _formScreen.Invalidate();
            }
        }

        private void checkBoxIsIgnoreReturn_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIsIgnoreReturn.Checked)
            {
                _configHelper.Write("IsIgnoreReturn", "True");
            }
            else
            {
                _configHelper.Write("IsIgnoreReturn", "False");
            }

            Settings.Default.Reload();

            listViewMusicList_SelectedIndexChanged(null, null);

        }

        private void comboBoxBackgroundImageLayout_SelectedIndexChanged(object sender, EventArgs e)
        {

            //背景图片位置（布局）
            switch (comboBoxBackgroundImageLayout.Items[comboBoxBackgroundImageLayout.SelectedIndex].ToString())
            {
                case "适应":
                    _formScreen.BackgroundImageLayout = ImageLayout.Zoom;
                    break;
                case "平铺":
                    _formScreen.BackgroundImageLayout = ImageLayout.Tile;

                    break;
                case "居中":
                    _formScreen.BackgroundImageLayout = ImageLayout.Center;
                    break;
                case "拉伸":
                    _formScreen.BackgroundImageLayout = ImageLayout.Stretch;
                    break;
                case "无":
                    _formScreen.BackgroundImageLayout = ImageLayout.None;
                    break;
            }

            //Settings.Default.BackgroundImageLayout = _formScreen.BackgroundImageLayout;
            //Settings.Default.Save();
            _configHelper.Write("BackgroundImageLayout", _formScreen.BackgroundImageLayout.ToString());
            _formScreen.Invalidate();
        }

    }
}
