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
    public partial class FormScreen : Form
    {

        //FormControl _formControl;

        //private int _iScrollIndex;


        //private int _iMaxHeight;


        //////////////////////////////私有变量//////////////////////////////////////



        //歌词页面数据

        //标题文本的位置
        PointF[] _positionsTitle;

        //当前页歌词文本的位置
        PointF[] _positionsLyricCurrentPage;

        //页码文本的位置
        PointF[] _positionPageNumber = null;

        //歌词文本的位置
        PointF[,] _positionsLyric;


        //系统标题栏高度
        private int _iCaptionHeight;

        //本首歌的歌词总页数
        private int _iMaxPageNumber;

        //当前页码
        private int _iCurrentPageNumber;

        ////歌词原始字符串
        //private string _strOriginalLyric;


        //当前页面歌词显示字符串（去除\r\n后的字符串）
        private string _strCurrentPageLyric;


        //每一页歌词的起始字符序号
        private int[] _beginIndexNumber;


        //每一页歌词的结束字符序号
        private int[] _endIndexNumber;




        ////用户点选的变量
        ////书名
        //string _strVolumeName = "";
        //int _iChapterNumber = 0;
        //int _iVerseNumberBegin = 0;
        //int _iVerseNumberEnd = 0;

        //位移
        private float _fLyricOffsetX = 0;
        private float _fLyricOffsetY = 0;


        ////临时图片
        //Image _imageTemp = null;


        FormControl _formControl;

        //private int _iScrollIndex;

        //private SizeF _sizeTitle;

        //private SizeF _sizeLyric;

        //歌词的文字尺寸
        RectangleF boundLyric;


        //标题的文字尺寸
        RectangleF boundTitle;



        //是否需要重新计算字符串的位置
        public bool BIsNeedReCalculatePosition;


        //private int _iMaxHeight;


        //private int _iMouseScreenX;
        //private int _iMouseScreenY;


        //private int _iMouseScrollX;
        //private int _iMouseScrollY;

        //private bool _bMouseScroll;



        public FormScreen(FormControl formControl)
        {
            InitializeComponent();

            //_iScrollIndex = 0;
            _formControl = formControl;

            //鼠标滚轮
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.FormScreen_MouseWheel);

            _iCaptionHeight = SystemInformation.CaptionHeight;

            this.BackColor = _formControl.ColorBack;

            this.BIsNeedReCalculatePosition = true;


            //实例化歌词位置数组，维度分别是最大页数和最大字数，默认最大100页
            _positionsLyric = new PointF[500, 200];


            //当前页歌词文本的位置
            _positionsLyricCurrentPage = new PointF[200];

            //标题文本的位置
            _positionsTitle = new PointF[100];


            //本首歌的歌词总页数
            _iMaxPageNumber = 0;

            //当前页码
            _iCurrentPageNumber = 0;

            ////歌词原始字符串
            //_strOriginalLyric = "";


            //歌词显示字符串（去除\r\n后的字符串）
            _strCurrentPageLyric = "";


            //每一页歌词的起始字符序号
            _beginIndexNumber = new int[500];


            //每一页歌词的结束字符序号
            _endIndexNumber = new int[500];



            //单个歌词字符的尺寸
            boundLyric = new RectangleF();

            //单个标题字符的尺寸
            boundTitle = new RectangleF();


        }

        private void FormScreen_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.TurnPage(e.Delta / 5);
        }


        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="iPage"></param>
        public void TurnPage(int iPage)
        {
            //if ((_iScrollIndex + iScroll) > (this.Height / 3))
            //{
            //    return;
            //}

            //if (_iMaxHeight != 0)
            //{
            //    if (Math.Abs(_iScrollIndex + iScroll) > _iMaxHeight - (this.Height / 3))
            //    {
            //        return;
            //    }
            //}

            //_iScrollIndex = _iScrollIndex + iScroll;

            if (_iCurrentPageNumber + iPage > _iMaxPageNumber || _iCurrentPageNumber + iPage < 0)
            {
                return;
            }

            _iCurrentPageNumber = _iCurrentPageNumber + iPage;

            this.Invalidate(true);
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_formControl.StrLyric != null && _formControl.StrLyric.Length <= 0)
            {
                return;
            }

            Array.Clear(_positionsLyricCurrentPage, 0, _positionsLyricCurrentPage.Length);


            //如果需要重新计算歌词的位置
            if (this.BIsNeedReCalculatePosition == true)
            {

                //所有歌词位置
                Array.Clear(_positionsLyric, 0, _positionsLyric.Length);


                _iMaxPageNumber = 0;
                _iCurrentPageNumber = 0;


                //循环歌词的每一个字符，得到位置


                //得到全角字符的尺寸
                try
                {
                    PointF[] posZeroLyric = new PointF[1];
                    posZeroLyric[0].X = 0;
                    posZeroLyric[0].Y = 0;

                    //10个字符以前会得到一个全角字符
                    for (int i = 0; i < 10; i++)
                    {
                        if (Encoding.Default.GetByteCount(_formControl.StrLyric[i].ToString()) == 2)
                        {
                            GdiplusMethods.MeasureDriverString(e.Graphics, _formControl.StrLyric[i].ToString(),
                                _formControl.FontLyric, _formControl.BrushLyric, posZeroLyric, null, ref boundLyric);


                            break;
                        }
                    }

                }
                catch (Exception)
                {
                }



                //如果显示标题并且是第一页，则还需要加上标题的高度
                if (_formControl.BoolIsShowTitle == true && _formControl.StrTitle !=null && _formControl.StrTitle.Length > 0)
                {
                    //标题位置
                    Array.Clear(_positionsTitle, 0, _positionsTitle.Length);

                    //计算标题文字的尺寸

                    //得到全角字符的尺寸
                    try
                    {
                        PointF[] posZeroTitle = new PointF[1];
                        posZeroTitle[0].X = 0;
                        posZeroTitle[0].Y = 0;

                        //10个字符以前会得到一个全角字符
                        for (int i = 0; i < 10; i++)
                        {
                            if (Encoding.Default.GetByteCount(_formControl.StrTitle[i].ToString()) == 2)
                            {
                                GdiplusMethods.MeasureDriverString(e.Graphics, _formControl.StrTitle[i].ToString(),
                                    _formControl.FontTitle, _formControl.BrushTitle, posZeroTitle, null, ref boundTitle);

                                break;
                            }
                        }

                    }
                    catch (Exception)
                    {
                    }

                    //标题的偏移
                    float _fTitleOffsetX = 0;
                    float _fTitleOffsetY = 0;
                    _fTitleOffsetY = -boundTitle.Top + _formControl.IntLineSpace;

                    //让标题显示在一行中
                    for (int i = 0; i < _formControl.StrTitle.Length; i++)
                    {
                        _positionsTitle[i] = new PointF(_fTitleOffsetX, _fTitleOffsetY);

                        _fTitleOffsetX = _fTitleOffsetX + boundTitle.Width + _formControl.IntFontSpace;
                    }

                    //居中显示标题
                    if (_positionsTitle[_formControl.StrTitle.Length - 1].X < this.Width)
                    {
                        float fTitleMiddleOffsetTemp = 0;
                        fTitleMiddleOffsetTemp = (this.Width - _positionsTitle[_formControl.StrTitle.Length - 1].X - boundTitle.Width) / 2;

                        for (int i = 0; i < _formControl.StrTitle.Length; i++)
                        {
                            _positionsTitle[i].X = _positionsTitle[i].X + fTitleMiddleOffsetTemp;
                        }
                    }


                    //显示标题
                    _fLyricOffsetY = -boundLyric.Top + _formControl.IntLineSpace + _fTitleOffsetY;

                }
                else
                {

                    //不显示标题
                    _fLyricOffsetY = -boundLyric.Top + _formControl.IntLineSpace;
                }


                _fLyricOffsetX = 0;


                //每页将要显示的字符串的起始和结束序号
                Array.Clear(_beginIndexNumber, 0, _beginIndexNumber.Length);
                Array.Clear(_endIndexNumber, 0, _endIndexNumber.Length);

                int iNewLine2Times = 0;

                //临时索引，用于字符串位置
                int iTempIndex = 0;

                _beginIndexNumber[_iMaxPageNumber] = 0;

                if (_formControl.StrLyric == null)
                {
                    return;
                }


                for (int i = 0; i < _formControl.StrLyric.Length; i++)
                {
                    //记录当前页起始字符序号
                    if (_beginIndexNumber[_iMaxPageNumber] == 0 && i > 0 && _iMaxPageNumber > 0)
                    {
                        _beginIndexNumber[_iMaxPageNumber] = i - 1;
                    }

                    ////////////////////////////////////////换行///////////////////////////////////////////////////

                    //遇到"\r\n"自动换行，预备节号位置数组
                    if (_formControl.StrLyric[i] == '\r' && _formControl.StrLyric[i + 1] == '\n')
                    {
                        _fLyricOffsetX = 0;

                        _fLyricOffsetY = _fLyricOffsetY + _formControl.IntLineSpace + boundLyric.Height;

                        //保存4次预留的，会被删除的位置
                        iNewLine2Times = 2;

                    }
                    else if (_fLyricOffsetX + boundLyric.Width > this.Width)
                    {
                        //超出屏幕范围自动换行
                        _fLyricOffsetY = _fLyricOffsetY + _formControl.IntLineSpace + boundLyric.Height;

                        _fLyricOffsetX = 0;
                    }


                    /////////////////////////////////////////翻页//////////////////////////////////////////////////
                    if (_fLyricOffsetY + boundLyric.Height > this.Height)
                    {

                        iTempIndex = 0;
                        //x,y偏移归0
                        _fLyricOffsetX = 0;
                        _fLyricOffsetY = -boundLyric.Top + _formControl.IntLineSpace;


                        //记录当前页结束字符序号
                        _endIndexNumber[_iMaxPageNumber] = i;


                        _iMaxPageNumber++;
                    }


                    if (iNewLine2Times > 0)
                    {
                        iNewLine2Times--;
                        //存入位置数组
                        _positionsLyric[_iMaxPageNumber, iTempIndex] = new PointF(-111, -111);

                        iTempIndex++;
                    }
                    else
                    {
                        //存入位置数组

                        /////////////////////做到这里////////////////////////
                        _positionsLyric[_iMaxPageNumber, iTempIndex] = new PointF(_fLyricOffsetX, _fLyricOffsetY);

                        ////////////////////////////////////////偏移////////////////////////////////////////////////
                        _fLyricOffsetX = _fLyricOffsetX + boundLyric.Width + _formControl.IntFontSpace;


                        iTempIndex++;
                    }


                }

                //

                BIsNeedReCalculatePosition = false;


            }


            //得到当前页面字符串
            int iCharCount = 0;

            //如果是最后一页
            if (_endIndexNumber[_iCurrentPageNumber] - _beginIndexNumber[_iCurrentPageNumber] <= 0)
            {
                _strCurrentPageLyric = _formControl.StrLyric.Substring(_beginIndexNumber[_iCurrentPageNumber]).Replace("\r\n", "");

                //给最后一页的索引赋值
                //_endIndexNumber[_iCurrentPageNumber] = _formControl.StrLyric.Substring(_beginIndexNumber[_iCurrentPageNumber]).Length;

                //iCharCount = _endIndexNumber[_iCurrentPageNumber];

                iCharCount = _formControl.StrLyric.Substring(_beginIndexNumber[_iCurrentPageNumber]).Length;

            }
            else
            {
                _strCurrentPageLyric = _formControl.StrLyric.Substring(_beginIndexNumber[_iCurrentPageNumber], _endIndexNumber[_iCurrentPageNumber] - _beginIndexNumber[_iCurrentPageNumber]).Replace("\r\n", "");

                iCharCount = _endIndexNumber[_iCurrentPageNumber] - _beginIndexNumber[_iCurrentPageNumber];

            }

            //去除\r\n及其相关的位置信息
            //得到当前页面的文字位置
            int j = 0;

            for (int k = 0; k < iCharCount; k++)
            {
                if (_positionsLyric[_iCurrentPageNumber, k].X == -111 && _positionsLyric[_iCurrentPageNumber, k].Y == -111)
                {

                }
                else
                {

                    //if (_strCurrentPageLyric[k] == ' ' &&  _positionsLyric[_iCurrentPageNumber, k].X == 0)
                    //{
                    //    _positionsLyricCurrentPage[j] = new PointF(-111, -111);

                    //}
                    //else
                    //{
                        _positionsLyricCurrentPage[j] = new PointF(_positionsLyric[_iCurrentPageNumber, k].X, _positionsLyric[_iCurrentPageNumber, k].Y);

                        j++;

                    //}

                }
            }


            //计算当前页字符串居中的位置
            //记录前一个y，进行对比
            float fPreviousY = float.NaN;

            for (int iLoopMiddle = iCharCount - 1; iLoopMiddle >= 0; iLoopMiddle--)
            {

                //找到同一行的字符串
                //首先得到最后一行的最后一个字符
                float fMiddleY = _positionsLyricCurrentPage[iLoopMiddle].Y;

                //如果是第一次循环或者找到与前一个y不同时，记录前一个y
                if (fPreviousY == float.NaN || fPreviousY != fMiddleY)
                {
                    float fMiddleX = _positionsLyricCurrentPage[iLoopMiddle].X;

                    //记录成为前一个y
                    fPreviousY = fMiddleY;

                    //获取空白位置进行居中
                    float fMiddleOffsetTemp = this.Width - fMiddleX - boundLyric.Width;

                    //有空白位置进行居中处理
                    if (fMiddleOffsetTemp > 0)
                    {
                        fMiddleOffsetTemp = fMiddleOffsetTemp / 2;
                    }

                    for (int iMiddle = iLoopMiddle; iMiddle >= 0; iMiddle--)
                    {
                        //如果是同一行上的字符
                        if (_positionsLyricCurrentPage[iMiddle].Y == fMiddleY)
                        {
                            _positionsLyricCurrentPage[iMiddle].X = _positionsLyricCurrentPage[iMiddle].X + fMiddleOffsetTemp;
                        }
                        else
                        {
                            //重新开始循环
                            break;
                        }

                    }

                }


            }

            if (_iCurrentPageNumber == 0 && _formControl.BoolIsShowTitle == true && _formControl.StrTitle.Length > 0)
            {
                GdiplusMethods.DrawDriverString(e.Graphics, _formControl.StrTitle,
_formControl.FontTitle, _formControl.BrushTitle, _positionsTitle);
            }


            GdiplusMethods.DrawDriverString(e.Graphics, _strCurrentPageLyric,
        _formControl.FontLyric, _formControl.BrushLyric, _positionsLyricCurrentPage);


            //计算页数

            string strCurrentPageNumberTemp = (_iCurrentPageNumber + 1).ToString();
            string strMaxPageNumberTemp = (_iMaxPageNumber + 1).ToString();

            while (strCurrentPageNumberTemp.Length < 3)
            {
                strCurrentPageNumberTemp = " " + strCurrentPageNumberTemp;
            }

            while (strMaxPageNumberTemp.Length < 3)
            {
                strMaxPageNumberTemp = strMaxPageNumberTemp + " ";
            }


            _formControl.StrPageNumber = strCurrentPageNumberTemp + "/" + strMaxPageNumberTemp;

            //显示页码和页数
            if (_formControl.StrPageNumber.Length > 0)
            {
                GdiplusMethods.DrawDriverString(e.Graphics, _formControl.StrPageNumber,
                    _formControl.FontPageNumber, _formControl.BrushPageNumber, _positionPageNumber);
            }

        }


        private void FormScreen_Load(object sender, EventArgs e)
        {

            InitPageNumberPosition();

            _formControl.checkBoxIsShowBackgroundImage_CheckedChanged(null, null);

            this.Invalidate();
        }


        //初始化页码位置数组
        private void InitPageNumberPosition()
        {
            #region 初始化页码数组的位置

            if (_positionPageNumber == null)
            {
                _positionPageNumber = new PointF[9];
            }

            _positionPageNumber[0].X = this.Width - 80;// "";
            _positionPageNumber[1].X = this.Width - 70;// "";
            _positionPageNumber[2].X = this.Width - 60;// "1";
            _positionPageNumber[3].X = this.Width - 50;// "/";
            _positionPageNumber[4].X = this.Width - 40;// "4";
            _positionPageNumber[5].X = this.Width - 30;// "";
            _positionPageNumber[6].X = this.Width - 20;// "";


            _positionPageNumber[0].Y = this.Height - 40;// "";
            _positionPageNumber[1].Y = this.Height - 40;// "";
            _positionPageNumber[2].Y = this.Height - 40;// "1";
            _positionPageNumber[3].Y = this.Height - 40;// "/";
            _positionPageNumber[4].Y = this.Height - 40;// "4";
            _positionPageNumber[5].Y = this.Height - 40;// "";
            _positionPageNumber[6].Y = this.Height - 40;// "";

            #endregion

        }

        private void FormScreen_Resize(object sender, EventArgs e)
        {
            BIsNeedReCalculatePosition = true;

            InitPageNumberPosition();

            this.Invalidate(true);
        }

        private void FormScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //string strTemp = "";
            try
            {

                //strTemp = labelPinyin.Text + (char)e.KeyCode;
                //if (strTemp.Contains(" 0") || strTemp.Contains(" -") || strTemp.Contains("||") || strTemp.Contains("::"))
                //{
                //    return;
                //}

                switch (e.KeyCode)
                {
                    case Keys.F5:

                        //ShowScreen();

                        //if (this.FormBorderStyle == FormBorderStyle.None)
                        //{
                        //    this.FormBorderStyle = FormBorderStyle.Sizable;
                        //    this.WindowState = FormWindowState.Normal;
                        //}
                        //else
                        //{
                        if (this.FormBorderStyle == FormBorderStyle.None && this.WindowState == FormWindowState.Maximized)
                        {
                        }
                        else
                        {
                            this.FormBorderStyle = FormBorderStyle.None;
                            this.WindowState = FormWindowState.Maximized;
                        }

                        //this.ScrollScreenTo(0);
                        this.Invalidate(true);


                        //}

                        //this.panelLectionPannelBack.Controls.Remove(this.panelLectionContent);
                        //this.Controls.Add(this.panelLectionContent);
                        //this.panelLectionContent.BringToFront();
                        //this.FormBorderStyle = FormBorderStyle.None;
                        //this.WindowState = FormWindowState.Maximized;

                        //this.richTextBoxLectionContent.Top = 0;

                        ////重新计算高度
                        //ReCalculateHeight();
                        break;
                    case Keys.Escape:

                        //if (panelPinyin.Visible == true)
                        //{
                        //    //清空拼音搜索字符串
                        //    _formControl.StrSearchByPinYin = "";

                        //    labelPinyin.Text = "";
                        //    listBoxPinyin.Items.Clear();
                        //    panelPinyin.SendToBack();
                        //    panelPinyin.Visible = false;

                        //    //this.panelLectionContent.Focus();
                        //}
                        //else
                        //{

                            //this.FormBorderStyle = FormBorderStyle.Sizable;
                            //this.WindowState = FormWindowState.Normal;
                            //this.Controls.Remove(this.panelLectionContent);
                            //this.panelLectionPannelBack.Controls.Add(this.panelLectionContent);
                            //this.panelLectionContent.Dock = DockStyle.Fill;

                            //this.richTextBoxLectionContent.Top = 0;

                            if (this.FormBorderStyle == FormBorderStyle.Sizable && this.WindowState == FormWindowState.Normal)
                            {
                            }
                            else
                            {
                                this.FormBorderStyle = FormBorderStyle.Sizable;
                                this.WindowState = FormWindowState.Normal;
                            }

                            //this.ScrollScreenTo(0);
                            this.Invalidate(true);


                        //}
                        ////重新计算高度
                        //ReCalculateHeight();
                        break;
                    case Keys.Left:
                        //this.ScrollScreen(50);
                        TurnPage(-1);

                        break;
                    case Keys.Right:
                        //this.ScrollScreen(-50);
                        TurnPage(1);

                        break;
                    case Keys.Home:
                        //this.ScrollScreenTo(0);

                        TurnPage(-_iCurrentPageNumber);


                        this.Invalidate(true);

                        //_iScrollIndex = 0;
                        //this.Invalidate(true);
                        //this.richTextBoxLectionContent.Top = 0;
                        break;
                    case Keys.PageDown:

                        _formControl.buttonNextMusic_Click(null, null);

                        
                        //if (_formControl.listViewLectionList.Items.Count <= 0)
                        //{
                        //    return;
                        //}

                        //if (_formControl.listViewLectionList.Items.Count > 0 && _formControl.listViewLectionList.SelectedItems.Count == 0) //判断有没有选择项
                        //{
                        //    _formControl.listViewLectionList.Items[0].Selected = true;
                        //}

                        ////计算选择项的位置
                        //int iPageDownIndex = _formControl.listViewLectionList.SelectedItems[0].Index + 1 >= _formControl.listViewLectionList.Items.Count ? _formControl.listViewLectionList.SelectedItems[0].Index : _formControl.listViewLectionList.SelectedItems[0].Index + 1;
                        //_formControl.listViewLectionList.Items[iPageDownIndex].Selected = true;

                        break;
                    case Keys.PageUp:

                        _formControl.buttonPreviousMusic_Click(null, null);

                        //if (_formControl.listViewLectionList.Items.Count <= 0)
                        //{
                        //    return;
                        //}

                        //if (_formControl.listViewLectionList.Items.Count > 0 && _formControl.listViewLectionList.SelectedItems.Count == 0) //判断有没有选择项
                        //{
                        //    _formControl.listViewLectionList.Items[0].Selected = true;
                        //}

                        ////计算选择项的位置
                        //int iPageUpIndex = _formControl.listViewLectionList.SelectedItems[0].Index - 1 < 0 ? 0 : _formControl.listViewLectionList.SelectedItems[0].Index - 1;
                        //_formControl.listViewLectionList.Items[iPageUpIndex].Selected = true;
                        break;
                    case Keys.Enter:
                        ////清空拼音搜索字符串
                        //_formControl.StrSearchByPinYin = "";

                        ////添加经文到列表
                        //if (this.labelPinyin.Text != "" && this.labelPinyin.Text.Contains(":")
                        //    && !this.labelPinyin.Text.EndsWith(":") && !this.labelPinyin.Text.EndsWith("-"))
                        //{
                        //    _formControl.listViewLectionList.Items.Add(new ListViewItem(
                        //            new string[]{(_formControl.listViewLectionList.Items.Count + 1).ToString(),
                        //            this.labelPinyin.Text}));
                        //}

                        //labelPinyin.Text = "";
                        //listBoxPinyin.Items.Clear();
                        //panelPinyin.SendToBack();
                        //panelPinyin.Visible = false;

                        //this.panelLectionContent.Focus();

                        break;
                    case Keys.Back:
                        ////如果拼音中有值
                        //if (_formControl.StrSearchByPinYin.Length > 0)
                        //{
                        //    //如果控件中有值，并且不以空格结尾
                        //    if (labelPinyin.Text != "" && !labelPinyin.Text.EndsWith(" "))
                        //    {
                        //        labelPinyin.Text = labelPinyin.Text.Remove(labelPinyin.Text.Length - 1);

                        //        if (labelPinyin.Text.Contains(":") && !labelPinyin.Text.EndsWith(":") && !labelPinyin.Text.EndsWith("-"))
                        //        {
                        //            ShowLectionByVolumeNameEtc(labelPinyin.Text, false);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        _formControl.StrSearchByPinYin = _formControl.StrSearchByPinYin.Remove(_formControl.StrSearchByPinYin.Length - 1);
                        //        if (_formControl.StrSearchByPinYin.Length > 0)
                        //        {
                        //            ShowVolumeNameByPinYin();
                        //        }
                        //        else
                        //        {
                        //            labelPinyin.Text = "";
                        //            panelPinyin.SendToBack();
                        //            panelPinyin.Visible = false;

                        //            _formControl.StrSearchByPinYin = "";
                        //        }
                        //    }


                        //}
                        //else
                        //{
                        //    //如果拼音中没有值


                        //}

                        break;
                    case Keys.Oem1:
                        //////_formControl.StrSearchByPinYin += ":";
                        //if (labelPinyin.Text.Contains(":"))
                        //{
                        //    return;
                        //}

                        //strTemp = "";
                        //strTemp = labelPinyin.Text + ":";

                        //if (!strTemp.Contains("::") && !strTemp.Contains("--") && !strTemp.Contains(":-") && !strTemp.Contains(" 0") && !strTemp.Contains(" -"))
                        //{
                        //    labelPinyin.Text += ":";
                        //}

                        break;
                    case Keys.OemMinus:
                        //if (labelPinyin.Text.Contains("-"))
                        //{
                        //    return;
                        //}

                        ////_formControl.StrSearchByPinYin += "-";
                        //strTemp = "";
                        //strTemp = labelPinyin.Text + "-";

                        //if (strTemp.Contains(":") && !strTemp.Contains(":-") && !strTemp.Contains("-:") && !strTemp.Contains("--") && !strTemp.EndsWith(":") && !strTemp.Contains("--") && !strTemp.Contains(" 0") && !strTemp.Contains(" -"))
                        //{
                        //    labelPinyin.Text += "-";
                        //}

                        break;
                    default:
                        //拼音查询功能

                        //_formControl.StrSearchByPinYin += e.KeyCode.ToString();
                        //_formControl.StrSearchByPinYin = _formControl.StrSearchByPinYin.ToLower();

                        //if (_formControl.StrSearchByPinYin.Length > 0)
                        //{
                        //    ShowVolumeNameByPinYin();
                        //    return;
                        //}

                        //int iKey = e.KeyValue;

                        //switch (iKey)
                        //{
                        //    //数字0 - 9
                        //    case 48:
                        //    case 49:
                        //    case 50:
                        //    case 51:
                        //    case 52:
                        //    case 53:
                        //    case 54:
                        //    case 55:
                        //    case 56:
                        //    case 57:
                        //        //case 32:
                        //        //case 45:
                        //        //case 58:
                        //        //case 186:
                        //        //case 189:
                        //        //如果labelPinyin中有经卷名称，则根据数字显示章节
                        //        //MessageBox.Show(e.KeyChar.ToString());
                        //        if (labelPinyin.Text != "")
                        //        {
                        //            strTemp = "";
                        //            strTemp = labelPinyin.Text + (char)e.KeyValue;

                        //            if (strTemp.Contains(" 0") || strTemp.Contains(" -") || strTemp.Contains("||") || strTemp.Contains("::"))
                        //            {
                        //                break;
                        //            }

                        //            if (strTemp.Contains(":") && !strTemp.EndsWith(":") && !strTemp.EndsWith("-"))
                        //            {
                        //                //验证是否为有效的拼音和章节
                        //                if (ValidatePinYin(strTemp))
                        //                {
                        //                    labelPinyin.Text += (char)e.KeyValue;

                        //                    //if (labelPinyin.Text.Contains(":") && !labelPinyin.Text.EndsWith(":") && !labelPinyin.Text.EndsWith("-"))
                        //                    //{
                        //                    ShowLectionByVolumeNameEtc(labelPinyin.Text, false);
                        //                    //}
                        //                }
                        //                else
                        //                {
                        //                    break;
                        //                }
                        //            }
                        //            else
                        //            {
                        //                labelPinyin.Text += (char)e.KeyValue;
                        //            }

                        //        }
                        //        else
                        //        {
                        //            return;
                        //        }
                        //        //如果labelPinyin中没有经卷名称，则无操作

                            //    break;
                            //case 13:


                            //    break;
                            //default:
                            //    //a --- z 之间
                            //    //if (65 <= e.KeyValue && e.KeyValue <= 90)
                            //    //{
                            //    //    _formControl.StrSearchByPinYin += e.KeyCode.ToString();
                            //    //    ShowVolumeNameByPinYin();

                            //    //}

                            //    break;
                        //}

                        break;
                }

                //this.panelLectionContent.Focus();
            }
            catch (Exception)
            {
            }
        }

        private void ToolStripMenuItemFullScreen_Click(object sender, EventArgs e)
        {
            if (this.FormBorderStyle == FormBorderStyle.None && this.WindowState == FormWindowState.Maximized)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }

            this.Invalidate(true);
        }

        private void FormScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.FormBorderStyle == FormBorderStyle.None && this.WindowState == FormWindowState.Maximized)
                {
                    this.ToolStripMenuItemFullScreen.Text = "退出全屏 (Esc)";
                }
                else
                {
                    this.ToolStripMenuItemFullScreen.Text = "全屏 (F5)";
                }

                Point p = new Point(e.X, e.Y);
                p = this.PointToScreen(p);
                this.contextMenuStripMouseRightClick.Show(p);
            }
        }

        private void ToolStripMenuItemPageUp_Click(object sender, EventArgs e)
        {
            TurnPage(-1);
        }

        private void ToolStripMenuItemPageDown_Click(object sender, EventArgs e)
        {
            TurnPage(1);
        }

        private void ToolStripMenuItemPreviousMusic_Click(object sender, EventArgs e)
        {
            _formControl.buttonPreviousMusic_Click(null, null);
        }

        private void ToolStripMenuItemNextMusic_Click(object sender, EventArgs e)
        {
            _formControl.buttonNextMusic_Click(null, null);
        }


    }
}
