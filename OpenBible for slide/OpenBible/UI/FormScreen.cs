using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using OpenBible.BLL;

namespace OpenBible.UI
{
    public partial class FormScreen : Form
    {
        //////////////////////////////私有变量//////////////////////////////////////

        //书名
        private ucLinkPanel uclpVolumeName;
        //章号
        private ucLinkPanel uclpChapterNumber;
        //开始节
        private ucLinkPanel uclpVerseNumberBegin;
        //结束节
        private ucLinkPanel uclpVerseNumberEnd;


        //标题文本的位置
        PointF[] _positionsTitle;

        //节数文本的位置
        PointF[] _positionsVerseNumber;

        //经文文本的位置
        PointF[] _positionsLection;

        //系统标题栏高度
        private int _iCaptionHeight;


        //用户点选的变量
        //书名
        string _strVolumeName = "";
        int _iChapterNumber = 0;
        int _iVerseNumberBegin = 0;
        int _iVerseNumberEnd = 0;

        //位移
        private float _fOffsetX = 0;
        private float _fOffsetY = 0;


        //临时图片
        Image _imageTemp = null;


        FormControl _formControl;

        private int _iScrollIndex;

        private SizeF _sizeTitle;

        private SizeF _sizeLection;


        private bool bIsNeedReCalculatePosition;


        private int _iMaxHeight;


        private int _iMouseScreenX;
        private int _iMouseScreenY;



        private int _iMouseScrollX;
        private int _iMouseScrollY;

        private bool _bMouseScroll;



        /// <summary>
        /// 卷动
        /// </summary>
        /// <param name="iScroll"></param>
        public void ScrollScreen(int iScroll)
        {
            if ((_iScrollIndex + iScroll) > (this.Height / 3))
            {
                return;
            }

            if (_iMaxHeight != 0)
            {
                if (Math.Abs(_iScrollIndex + iScroll) > _iMaxHeight - (this.Height / 3))
                {
                    return;
                }
            }

            _iScrollIndex = _iScrollIndex + iScroll;
            this.Invalidate(true);
        }

        /// <summary>
        /// 卷动到指定位置
        /// </summary>
        /// <param name="iScroll"></param>
        public void ScrollScreenTo(int iScroll)
        {
            _iScrollIndex = iScroll;

            this.Invalidate(true);
        }

        public FormScreen(FormControl formControl)
        {
            InitializeComponent();

            _iScrollIndex = 0;
            _formControl = formControl;

            //鼠标滚轮
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.FormScreen_MouseWheel);

        }

        private void FormScreen_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.ScrollScreen(e.Delta / 5);
        }

        private void FormScreen_Load(object sender, EventArgs e)
        {

            InitParameter();

        }

        public void InitParameter()
        {

            _iCaptionHeight = SystemInformation.CaptionHeight;

            this.BackColor = _formControl.ColorBack;

            this.bIsNeedReCalculatePosition = false;


            #region 鼠标点选，选择经文控件
            //卷
            uclpVolumeName = new ucLinkPanel();
            uclpVolumeName.Selected += new ucLinkPanel.SelectedEventHandler(this.uclpVolumeNameSelected);
            //uclpVolumeName.Enter += new System.EventHandler(this.uclpVolumeName_Enter);
            uclpVolumeName.MouseMove += new MouseEventHandler(this.uclpVolumeName_MouseMove);

            uclpVolumeName.BackColor = Color.LightGray;
            //uclpVolumeName.TabStop = false;
            uclpVolumeName.Font = new Font("", 10);


            //章
            uclpChapterNumber = new ucLinkPanel();
            uclpChapterNumber.Selected += new ucLinkPanel.SelectedEventHandler(this.uclpChapterNumberSelected);
            //uclpChapterNumber.Enter += new System.EventHandler(this.uclpChapterNumber_Enter);
            uclpChapterNumber.MouseMove += new MouseEventHandler(this.uclpChapterNumber_MouseMove);
            uclpChapterNumber.BackColor = Color.LightCyan;
            //uclpChapterNumber.TabStop = false;
            uclpChapterNumber.Font = new Font("", 10);

            //开始节
            uclpVerseNumberBegin = new ucLinkPanel();
            uclpVerseNumberBegin.Selected += new ucLinkPanel.SelectedEventHandler(this.uclpVerseNumberBeginSelected);
            //uclpVerseNumberBegin.Enter += new System.EventHandler(this.uclpVerseNumberBegin_Enter);
            uclpVerseNumberBegin.MouseMove += new MouseEventHandler(this.uclpVerseNumberBegin_MouseMove);

            uclpVerseNumberBegin.BackColor = Color.LightCoral;
            //uclpVerseNumberBegin.TabStop = false;
            uclpVerseNumberBegin.Font = new Font("", 10);


            //结束节
            uclpVerseNumberEnd = new ucLinkPanel();
            uclpVerseNumberEnd.Selected += new ucLinkPanel.SelectedEventHandler(this.uclpVerseNumberEndSelected);
            //uclpVerseNumberEnd.Enter += new System.EventHandler(this.uclpVerseNumberEnd_Enter);
            //uclpVerseNumberEnd.MouseLeave += new System.EventHandler(this.uclpVerseNumberEnd_Enter);

            uclpVerseNumberEnd.BackColor = Color.LightBlue;
            //uclpVerseNumberEnd.TabStop = false;
            uclpVerseNumberEnd.Font = new Font("", 10);


            //新旧约切换按钮
            buttonNewTestament.Visible = false;
            buttonOldTestament.Visible = false;


            panelPinyin.Height = 20;
            panelPinyin.SendToBack();
            panelPinyin.Visible = false;


            #endregion

        }


        #region 用户点选选择经文控件的事件


        private void uclpVolumeName_MouseMove(object sender, MouseEventArgs e)
        {
            _iMouseScreenX = e.X + uclpVolumeName.Left;
            _iMouseScreenY = e.Y + uclpVolumeName.Top;
        }

        private void uclpChapterNumber_MouseMove(object sender, MouseEventArgs e)
        {
            _iMouseScreenX = e.X + uclpChapterNumber.Left;
            _iMouseScreenY = e.Y + uclpChapterNumber.Top;
        }

        private void uclpVerseNumberBegin_MouseMove(object sender, MouseEventArgs e)
        {
            _iMouseScreenX = e.X + uclpVerseNumberBegin.Left;
            _iMouseScreenY = e.Y + uclpVerseNumberBegin.Top;
        }


        //private void uclpVolumeName_Enter(object sender, EventArgs e)
        //{
        //}

        //private void uclpChapterNumber_Enter(object sender, EventArgs e)
        //{
        //}

        //private void uclpVerseNumberBegin_Enter(object sender, EventArgs e)
        //{
        //}

        //private void uclpVerseNumberEnd_Enter(object sender, EventArgs e)
        //{
        //}

        private void uclpVerseNumberEnd_MouseLeave(object sender, EventArgs e)
        {
            uclpVolumeName.Enabled = false;
            uclpVolumeName.Enabled = true;

            uclpChapterNumber.Enabled = false;
            uclpChapterNumber.Enabled = true;

            uclpVerseNumberBegin.Enabled = false;
            uclpVerseNumberBegin.Enabled = true;

            uclpVerseNumberEnd.Enabled = false;
            uclpVerseNumberEnd.Enabled = true;

        }


        private void uclpVolumeNameSelected(object sender, EventArgs e)
        {
            this.Controls.Remove(uclpChapterNumber);
            this.Controls.Remove(uclpVerseNumberBegin);
            this.Controls.Remove(uclpVerseNumberEnd);

            _strVolumeName = ((LinkLabel)sender).Tag.ToString();
            int iMaxChapterNum = BibleHelper.GetChapterCountByVolumeSN(BibleHelper.GetVolumeSNbyFullName(_strVolumeName));
            string[,] strArray = new string[iMaxChapterNum, 2];
            for (int i = 0; i < iMaxChapterNum; i++)
            {
                strArray[i, 0] = (i + 1).ToString() + "章";
                strArray[i, 1] = (i + 1).ToString();
            }

            if (this.Controls.Contains(uclpChapterNumber))
            {
                this.Controls.Remove(uclpChapterNumber);
            }

            uclpChapterNumber.LoadData(strArray);
            this.Controls.Add(uclpChapterNumber);

            if (_iMouseScreenX + 20 < uclpVolumeName.Right && _iMouseScreenX + 20 > uclpVolumeName.Left)
            {
                uclpChapterNumber.Left = _iMouseScreenX + 20;
            }
            else
            {
                uclpChapterNumber.Left = uclpVolumeName.Right + 0;

            }
            uclpChapterNumber.Top = 22;


            uclpChapterNumber.BringToFront();

            this.Refresh();
        }


        //根据选择的章显示节数
        private void uclpChapterNumberSelected(object sender, EventArgs e)
        {
            this.Controls.Remove(uclpVerseNumberBegin);
            this.Controls.Remove(uclpVerseNumberEnd);

            _iChapterNumber = int.Parse(((LinkLabel)sender).Tag.ToString());
            int iMaxVerseNum = BibleHelper.GetVerseCountByVolumeSNAndChapterSN(BibleHelper.GetVolumeSNbyFullName(_strVolumeName), _iChapterNumber);

            string[,] strArray = new string[iMaxVerseNum, 2];
            for (int i = 0; i < iMaxVerseNum; i++)
            {
                strArray[i, 0] = (i + 1).ToString() + "节";
                strArray[i, 1] = (i + 1).ToString();
            }


            if (this.Controls.Contains(uclpVerseNumberBegin))
            {
                this.Controls.Remove(uclpVerseNumberBegin);
            }

            uclpVerseNumberBegin.LoadData(strArray);
            this.Controls.Add(uclpVerseNumberBegin);

            if (_iMouseScreenX + 20 < uclpChapterNumber.Right && _iMouseScreenX + 20 > uclpChapterNumber.Left)
            {
                uclpVerseNumberBegin.Left = _iMouseScreenX + 20;
            }
            else
            {
                uclpVerseNumberBegin.Left = uclpChapterNumber.Right + 0;
            }
            uclpVerseNumberBegin.Top = 22;

            uclpVerseNumberBegin.BringToFront();

            this.Refresh();

        }


        //根据选择的开始节数显示将结束的节数
        private void uclpVerseNumberBeginSelected(object sender, EventArgs e)
        {
            this.Controls.Remove(uclpVerseNumberEnd);

            _iVerseNumberBegin = int.Parse(((LinkLabel)sender).Tag.ToString());

            int iMaxVerseNum = BibleHelper.GetVerseCountByVolumeSNAndChapterSN(BibleHelper.GetVolumeSNbyFullName(_strVolumeName), _iChapterNumber);


            string strTemp = _strVolumeName + " " + _iChapterNumber + ":" + _iVerseNumberBegin;


            ShowLectionByVolumeNameEtc(strTemp, false);

            //添加到经文列表中
            _formControl.listViewLectionList.Items.Add(new ListViewItem(new string[] { (_formControl.listViewLectionList.Items.Count + 1).ToString(), strTemp }));


            //如果没有结束经文，则直接显示经文
            if ((iMaxVerseNum - _iVerseNumberBegin) <= 0)
            {


            }
            else
            {
                string[,] strArray = new string[iMaxVerseNum - _iVerseNumberBegin, 2];
                for (int i = 0; i < iMaxVerseNum - _iVerseNumberBegin; i++)
                {
                    strArray[i, 0] = "->" + (i + 1 + _iVerseNumberBegin).ToString() + "节";
                    strArray[i, 1] = (i + 1 + _iVerseNumberBegin).ToString();
                }

                if (this.Controls.Contains(uclpVerseNumberEnd))
                {
                    this.Controls.Remove(uclpVerseNumberEnd);
                }

                uclpVerseNumberEnd.LoadData(strArray);
                this.Controls.Add(uclpVerseNumberEnd);


                if (_iMouseScreenX + 20 < uclpVerseNumberBegin.Right && _iMouseScreenX + 20 > uclpVerseNumberBegin.Left)
                {
                    uclpVerseNumberEnd.Left = _iMouseScreenX + 20;
                }
                else
                {
                    uclpVerseNumberEnd.Left = uclpVerseNumberBegin.Right + 0;
                }

                uclpVerseNumberEnd.Top = 22;

                uclpVerseNumberEnd.BringToFront();

                this.Refresh();
            }
        }


        //根据选择的结束节数显示经文
        private void uclpVerseNumberEndSelected(object sender, EventArgs e)
        {
            _iVerseNumberEnd = int.Parse(((LinkLabel)sender).Tag.ToString());

            string strTemp = _strVolumeName + " " + _iChapterNumber + ":" + _iVerseNumberBegin + "-" + _iVerseNumberEnd;

            ShowLectionByVolumeNameEtc(strTemp, false);

            //添加到经文列表中
            _formControl.listViewLectionList.Items.Add(new ListViewItem(new string[] { (_formControl.listViewLectionList.Items.Count + 1).ToString(), strTemp }));

        }

        //验证是否为有效的
        private bool ValidatePinYin(string strPinYin)
        {
            //拆解卷名，章号，起始节号，结束节号
            string strVolumeName = "";
            int iChapterSN = -1;
            int iVerseSNBegin = -1;
            int iVerseSNEnd = -1;

            int iVolumeSN = -1;

            try
            {
                if (strPinYin.Contains(' '))
                {
                    strVolumeName = strPinYin.Substring(0, strPinYin.IndexOf(' '));

                    iVolumeSN = BibleHelper.GetVolumeSNbyFullName(strVolumeName);

                    strPinYin = strPinYin.Substring(strPinYin.IndexOf(' ') + 1);

                    if (strPinYin[0] == ':')
                    {
                        return false;
                    }

                    iChapterSN = int.Parse(strPinYin.Substring(0, strPinYin.IndexOf(':')));

                    //验证章数
                    if (iChapterSN > 0 && iChapterSN <= BibleHelper.GetChapterCountByVolumeSN(iVolumeSN))
                    {
                        //继续向下验证
                    }
                    else
                    {
                        return false;
                    }

                    strPinYin = strPinYin.Substring(strPinYin.IndexOf(':') + 1);

                    if (strPinYin.Contains('-'))
                    {
                        //路 1:1-2

                        iVerseSNBegin = int.Parse(strPinYin.Substring(0, strPinYin.IndexOf('-')));
                        strPinYin = strPinYin.Substring(strPinYin.IndexOf('-') + 1);
                        iVerseSNEnd = int.Parse(strPinYin);

                        //_formControl.StrTitle = strVolumeName + " " + iChapterSN + ":" + iVerseSNBegin + "-" + iVerseSNEnd;
                    }
                    else
                    {
                        //路 1:3
                        iVerseSNBegin = int.Parse(strPinYin);
                        iVerseSNEnd = iVerseSNBegin;

                        //_formControl.StrTitle = strVolumeName + " " + iChapterSN + ":" + iVerseSNBegin;
                    }

                    //if (iVerseSNEnd >= iVerseSNBegin)
                    //{

                    int iVerseCount = -1;
                    iVerseCount = BibleHelper.GetVerseCountByVolumeSNAndChapterSN(iVolumeSN, iChapterSN);

                    if (iVerseCount <= 0)
                    {
                        return false;
                    }


                    //验证开始节数
                    //验证终止节数
                    if (iVerseSNBegin > 0 && iVerseSNBegin <= iVerseCount && iVerseSNEnd > 0 && iVerseSNEnd <= iVerseCount)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    //}
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
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

                    _formControl.StrTitle = strVolumeName + " " + iChapterSN + ":" + iVerseSNBegin + "-" + iVerseSNEnd;
                }
                else
                {
                    //路 1:3
                    iVerseSNBegin = int.Parse(strVolumeNameEtc);
                    iVerseSNEnd = iVerseSNBegin;

                    _formControl.StrTitle = strVolumeName + " " + iChapterSN + ":" + iVerseSNBegin;
                }

                if (iVerseSNEnd >= iVerseSNBegin)
                {
                    if (bIsShortVolumeName)
                    {
                        _formControl.StrLection = BibleHelper.GetLectionByShortNameandChapterSN(strVolumeName, iChapterSN, iVerseSNBegin, iVerseSNEnd);
                    }
                    else
                    {
                        _formControl.StrLection = BibleHelper.GetLectionByFullNameandChapterSN(strVolumeName, iChapterSN, iVerseSNBegin, iVerseSNEnd);
                    }


                    if (_formControl.StrTitle != "" && _formControl.StrLection != "")
                    {
                        ScrollScreenTo(0);
                        DrawText();
                    }

                }

                //生成节号字符串
                _formControl.CalculateVerseNumberCount(iVerseSNBegin, iVerseSNEnd);

            }

            uclpVolumeName.Enabled = false;
            uclpVolumeName.Enabled = true;

            uclpChapterNumber.Enabled = false;
            uclpChapterNumber.Enabled = true;

            uclpVerseNumberBegin.Enabled = false;
            uclpVerseNumberBegin.Enabled = true;

            uclpVerseNumberEnd.Enabled = false;
            uclpVerseNumberEnd.Enabled = true;

        }



        private void ShowFullNameList(bool bNewOrOld)
        {

            string strTemp = BibleHelper.GetFullNameListByNewOrOld(bNewOrOld);
            if (strTemp.Contains('|'))
            {
                string[] strArray1 = strTemp.Split('|');

                int iVolume = strArray1.Length;
                //iVolume += 2;

                //切换新旧约的按钮

                string[,] strArray2 = new string[iVolume, 2];
                for (int i = 0; i < iVolume; i++)
                {
                    strArray2[i, 0] = strArray1[i];
                    strArray2[i, 1] = strArray1[i];
                }

                if (this.Controls.Contains(uclpChapterNumber))
                {
                    this.Controls.Remove(uclpChapterNumber);
                }

                uclpVolumeName.LoadData(strArray2);
                this.Controls.Add(uclpVolumeName);
                uclpVolumeName.BringToFront();

                uclpVolumeName.Left = 0;
                uclpVolumeName.Top = 22;


                buttonOldTestament.Left = 0;
                buttonOldTestament.Top = 0;

                buttonNewTestament.Left = buttonOldTestament.Right;
                buttonNewTestament.Top = 0;

                buttonNewTestament.Visible = true;
                buttonOldTestament.Visible = true;
                buttonNewTestament.BringToFront();
                buttonOldTestament.BringToFront();

            }

            this.Refresh();


        }


        #endregion




        //所有绘制动作都在这里实现
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_formControl.StrTitle == null || _formControl.StrTitle == "")
            {
                return;
            }

            //关闭抗锯齿
            //e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;

            //卷动
            e.Graphics.TranslateTransform(0, _iScrollIndex);

            if (this.bIsNeedReCalculatePosition == true)
            {
                ////////////////////////绘制标题/////////////////////////
                //计算单个字的尺寸
                _sizeTitle = e.Graphics.MeasureString(_formControl.StrTitle[0].ToString(), _formControl.FontTitle);

                //文字系统的坐标原点是左下角
                _fOffsetY = (_sizeTitle.Height * (1 + _formControl.FloatFontSpaceScale)) + (_sizeTitle.Height * _formControl.FloatLineSpaceScale) + _iScrollIndex;

                _fOffsetX = 0;
                _fOffsetX = _sizeTitle.Width * _formControl.FloatFontSpaceScale + _formControl.IntLectionLeftSpace;

                if (_fOffsetX < _formControl.IntLectionLeftSpace)
                {
                    _fOffsetX = _formControl.IntLectionLeftSpace;
                }

                //实例化标题位置数组
                _positionsTitle = new PointF[_formControl.StrTitle.Length];
                for (int i = 0; i < _formControl.StrTitle.Length; i++)
                {
                    //每个字的偏移
                    _positionsTitle[i] = new PointF(_fOffsetX, _fOffsetY);

                    _fOffsetX = _fOffsetX + (_sizeTitle.Width * (1 + _formControl.FloatFontSpaceScale));
                }

                //////////////////////绘制经文////////////////////////
                //排列经文的每个字符的位置
                _fOffsetX = 0;

                //查找全角字符的尺寸
                try
                {
                    //8个字符以前会得到一个全角字符
                    for (int i = 0; i < 10; i++)
                    {
                        if (Encoding.Default.GetByteCount(_formControl.StrLection[i].ToString()) == 2)
                        {
                            _sizeLection = e.Graphics.MeasureString(_formControl.StrLection[i].ToString(), _formControl.FontLection);
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                }

                //文字系统的坐标原点是左下角
                _fOffsetX = 0;
                _fOffsetX = _sizeLection.Width * _formControl.FloatFontSpaceScale + _formControl.IntLectionLeftSpace;

                if (_fOffsetX < _formControl.IntLectionLeftSpace)
                {
                    _fOffsetX = _formControl.IntLectionLeftSpace;
                }

                _fOffsetY = _fOffsetY + (_sizeLection.Height * (1 + _formControl.FloatFontSpaceScale)) + (_sizeLection.Height * _formControl.FloatLineSpaceScale) + _iScrollIndex;


                //实例化节号位置数组
                if (_formControl.StrArrayVerseNumber.Length > 0)
                {
                    if (_positionsVerseNumber != null)
                    {
                        Array.Clear(_positionsVerseNumber, 0, _positionsVerseNumber.Length);
                    }

                    _positionsVerseNumber = new PointF[_formControl.StrArrayVerseNumber.Length];
                }

                //节号位置数组的索引
                int iPositionsVerseIndex = 0;

                //实例化标题位置数组
                _positionsLection = new PointF[_formControl.StrLection.Length];
                for (int i = 0; i < _formControl.StrLection.Length; i++)
                {

                    //遇到" "自动换行，预备节号位置数组
                    if (_formControl.StrLection[i] == ' ')
                    {
                        _fOffsetX = 0;
                        _fOffsetX = _sizeLection.Width * _formControl.FloatFontSpaceScale + _formControl.IntLectionLeftSpace;

                        if (_fOffsetX < _formControl.IntLectionLeftSpace)
                        {
                            _fOffsetX = _formControl.IntLectionLeftSpace;
                        }

                        //第一个节号，y位置不变
                        if (i > 0)
                        {
                            _fOffsetY = _fOffsetY + (_sizeLection.Height * (1 + _formControl.FloatFontSpaceScale)) + (_sizeLection.Height * _formControl.FloatLineSpaceScale);
                        }

                        //取得一个节号的第一位字符的位置，如119节，得到第一个1的位置
                        while (_formControl.StrArrayVerseNumber[iPositionsVerseIndex] != ' ')
                        {
                            _positionsVerseNumber[iPositionsVerseIndex] = new PointF(_fOffsetX, _fOffsetY);
                            iPositionsVerseIndex++;
                            
                            _fOffsetX = _fOffsetX + (_sizeLection.Width / 2 * (1 + _formControl.FloatFontSpaceScale));
                        }

                        //自增一次得到空格的位置
                        _positionsVerseNumber[iPositionsVerseIndex] = new PointF(_fOffsetX, _fOffsetY);
                        iPositionsVerseIndex++;

                    }
                    else
                    {
                        //超出屏幕范围自动换行
                        if (_fOffsetX + (_sizeLection.Width * (1 + _formControl.FloatFontSpaceScale)) > this.Width)
                        {
                            _fOffsetX = 0;
                            _fOffsetX = _sizeLection.Width * _formControl.FloatFontSpaceScale + _formControl.IntLectionLeftSpace;

                            if (_fOffsetX < _formControl.IntLectionLeftSpace)
                            {
                                _fOffsetX = _formControl.IntLectionLeftSpace;
                            }

                            _fOffsetY = _fOffsetY + (_sizeLection.Height * (1 + _formControl.FloatFontSpaceScale)) + (_sizeLection.Height * _formControl.FloatLineSpaceScale);
                        }

                    }


                    //每个字的偏移
                    _positionsLection[i] = new PointF(_fOffsetX, _fOffsetY);

                    _fOffsetX = _fOffsetX + (_sizeLection.Width * (1 + _formControl.FloatFontSpaceScale));

                }
           

                //将经文中的"|"替换为空格
                //_formControl.StrLection = _formControl.StrLection.Replace('|', ' ');

                _iMaxHeight = (int)_positionsLection[_formControl.StrLection.Length - 1].Y;

                this.bIsNeedReCalculatePosition = false;
            }


            //绘制标题
            GdiplusMethods.DrawDriverString(e.Graphics, _formControl.StrTitle, _formControl.FontTitle, _formControl.BrushTitle, _positionsTitle);
            //Array.Clear(_positionsTitle, 0, _positionsTitle.Length);

            //绘制经文
            GdiplusMethods.DrawDriverString(e.Graphics, _formControl.StrLection, _formControl.FontLection, _formControl.BrushLection, _positionsLection);
            //Array.Clear(_positionsLection, 0, _positionsLection.Length);

            //绘制节号
            GdiplusMethods.DrawDriverString(e.Graphics, _formControl.StrArrayVerseNumber, _formControl.FontLection, _formControl.BrushVerseNumber, _positionsVerseNumber);

        }


        //绘制文本
        public void DrawText()
        {
            this.BackColor = _formControl.ColorBack;
            if (_formControl.StrTitle == null || _formControl.StrTitle == "")
            {
                return;
            }

            this.bIsNeedReCalculatePosition = true;
            this.ScrollScreenTo(0);
            this.Invalidate(true);

        }

        private void FormScreen_Resize(object sender, EventArgs e)
        {
            this.bIsNeedReCalculatePosition = true;

            this.Invalidate(true);
        }

        private void FormScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            string strTemp = "";
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

                        this.ScrollScreenTo(0);
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

                        if (panelPinyin.Visible == true)
                        {
                            //清空拼音搜索字符串
                            _formControl.StrSearchByPinYin = "";

                            labelPinyin.Text = "";
                            listBoxPinyin.Items.Clear();
                            panelPinyin.SendToBack();
                            panelPinyin.Visible = false;

                            //this.panelLectionContent.Focus();
                        }
                        else
                        {

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

                            this.ScrollScreenTo(0);
                            this.Invalidate(true);


                        }
                        ////重新计算高度
                        //ReCalculateHeight();
                        break;
                    case Keys.Up:
                        this.ScrollScreen(50);
                        break;
                    case Keys.Down:
                        this.ScrollScreen(-50);
                        break;
                    case Keys.Home:
                        this.ScrollScreenTo(0);
                        this.Invalidate(true);

                        //_iScrollIndex = 0;
                        //this.Invalidate(true);
                        //this.richTextBoxLectionContent.Top = 0;
                        break;
                    case Keys.PageDown:
                        if (_formControl.listViewLectionList.Items.Count <= 0)
                        {
                            return;
                        }

                        if (_formControl.listViewLectionList.Items.Count > 0 && _formControl.listViewLectionList.SelectedItems.Count == 0) //判断有没有选择项
                        {
                            _formControl.listViewLectionList.Items[0].Selected = true;
                        }

                        //计算选择项的位置
                        int iPageDownIndex = _formControl.listViewLectionList.SelectedItems[0].Index + 1 >= _formControl.listViewLectionList.Items.Count ? _formControl.listViewLectionList.SelectedItems[0].Index : _formControl.listViewLectionList.SelectedItems[0].Index + 1;
                        _formControl.listViewLectionList.Items[iPageDownIndex].Selected = true;

                        break;
                    case Keys.PageUp:
                        if (_formControl.listViewLectionList.Items.Count <= 0)
                        {
                            return;
                        }

                        if (_formControl.listViewLectionList.Items.Count > 0 && _formControl.listViewLectionList.SelectedItems.Count == 0) //判断有没有选择项
                        {
                            _formControl.listViewLectionList.Items[0].Selected = true;
                        }

                        //计算选择项的位置
                        int iPageUpIndex = _formControl.listViewLectionList.SelectedItems[0].Index - 1 < 0 ? 0 : _formControl.listViewLectionList.SelectedItems[0].Index - 1;
                        _formControl.listViewLectionList.Items[iPageUpIndex].Selected = true;
                        break;
                    case Keys.Enter:
                        //清空拼音搜索字符串
                        _formControl.StrSearchByPinYin = "";

                        //添加经文到列表
                        if (this.labelPinyin.Text != "" && this.labelPinyin.Text.Contains(":")
                            && !this.labelPinyin.Text.EndsWith(":") && !this.labelPinyin.Text.EndsWith("-"))
                        {
                            _formControl.listViewLectionList.Items.Add(new ListViewItem(
                                    new string[]{(_formControl.listViewLectionList.Items.Count + 1).ToString(),
                                    this.labelPinyin.Text}));
                        }

                        labelPinyin.Text = "";
                        listBoxPinyin.Items.Clear();
                        panelPinyin.SendToBack();
                        panelPinyin.Visible = false;

                        //this.panelLectionContent.Focus();

                        break;
                    case Keys.Back:
                        //如果拼音中有值
                        if (_formControl.StrSearchByPinYin.Length > 0)
                        {
                            //如果控件中有值，并且不以空格结尾
                            if (labelPinyin.Text != "" && !labelPinyin.Text.EndsWith(" "))
                            {
                                labelPinyin.Text = labelPinyin.Text.Remove(labelPinyin.Text.Length - 1);

                                if (labelPinyin.Text.Contains(":") && !labelPinyin.Text.EndsWith(":") && !labelPinyin.Text.EndsWith("-"))
                                {
                                    ShowLectionByVolumeNameEtc(labelPinyin.Text, false);
                                }
                            }
                            else
                            {
                                _formControl.StrSearchByPinYin = _formControl.StrSearchByPinYin.Remove(_formControl.StrSearchByPinYin.Length - 1);
                                if (_formControl.StrSearchByPinYin.Length > 0)
                                {
                                    ShowVolumeNameByPinYin();
                                }
                                else
                                {
                                    labelPinyin.Text = "";
                                    panelPinyin.SendToBack();
                                    panelPinyin.Visible = false;

                                    _formControl.StrSearchByPinYin = "";
                                }
                            }


                        }
                        //else
                        //{
                        //    //如果拼音中没有值


                        //}

                        break;
                    case Keys.Oem1:
                        ////_formControl.StrSearchByPinYin += ":";
                        if (labelPinyin.Text.Contains(":"))
                        {
                            return;
                        }

                        strTemp = "";
                        strTemp = labelPinyin.Text + ":";

                        if (!strTemp.Contains("::") && !strTemp.Contains("--") && !strTemp.Contains(":-") && !strTemp.Contains(" 0") && !strTemp.Contains(" -"))
                        {
                            labelPinyin.Text += ":";
                        }

                        break;
                    case Keys.OemMinus:
                        if (labelPinyin.Text.Contains("-"))
                        {
                            return;
                        }

                        //_formControl.StrSearchByPinYin += "-";
                        strTemp = "";
                        strTemp = labelPinyin.Text + "-";

                        if (strTemp.Contains(":") && !strTemp.Contains(":-") && !strTemp.Contains("-:") && !strTemp.Contains("--") && !strTemp.EndsWith(":") && !strTemp.Contains("--") && !strTemp.Contains(" 0") && !strTemp.Contains(" -"))
                        {
                            labelPinyin.Text += "-";
                        }

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

                        int iKey = e.KeyValue;

                        switch (iKey)
                        {
                            //数字0 - 9
                            case 48:
                            case 49:
                            case 50:
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                            case 56:
                            case 57:
                                //case 32:
                                //case 45:
                                //case 58:
                                //case 186:
                                //case 189:
                                //如果labelPinyin中有经卷名称，则根据数字显示章节
                                //MessageBox.Show(e.KeyChar.ToString());
                                if (labelPinyin.Text != "")
                                {
                                    strTemp = "";
                                    strTemp = labelPinyin.Text + (char)e.KeyValue;

                                    if (strTemp.Contains(" 0") || strTemp.Contains(" -") || strTemp.Contains("||") || strTemp.Contains("::"))
                                    {
                                        break;
                                    }

                                    if (strTemp.Contains(":") && !strTemp.EndsWith(":") && !strTemp.EndsWith("-"))
                                    {
                                        //验证是否为有效的拼音和章节
                                        if (ValidatePinYin(strTemp))
                                        {
                                            labelPinyin.Text += (char)e.KeyValue;

                                            //if (labelPinyin.Text.Contains(":") && !labelPinyin.Text.EndsWith(":") && !labelPinyin.Text.EndsWith("-"))
                                            //{
                                            ShowLectionByVolumeNameEtc(labelPinyin.Text, false);
                                            //}
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        labelPinyin.Text += (char)e.KeyValue;
                                    }

                                }
                                else
                                {
                                    return;
                                }
                                //如果labelPinyin中没有经卷名称，则无操作

                                break;
                            case 13:


                                break;
                            default:
                                //a --- z 之间
                                if (65 <= e.KeyValue && e.KeyValue <= 90)
                                {
                                    _formControl.StrSearchByPinYin += e.KeyCode.ToString();
                                    ShowVolumeNameByPinYin();

                                }

                                break;
                        }

                        break;
                }

                //this.panelLectionContent.Focus();
            }
            catch (Exception)
            {
            }
        }



        private void ShowVolumeNameByPinYin()
        {
            string strTemp = BibleHelper.GetFullNameListAndPinYinbyVolumePinYin(_formControl.StrSearchByPinYin);

            if (strTemp.Length > 0)
            {
                //显示在列表中
                panelPinyin.BringToFront();
                panelPinyin.Visible = true;

                labelPinyin.Text = "";

                if (strTemp.Contains('|'))
                {
                    //listBoxPinyin.Dock = DockStyle.Top;
                    listBoxPinyin.Visible = true;
                    listBoxPinyin.Items.Clear();
                    listBoxPinyin.Items.AddRange(strTemp.Split('|'));
                    panelPinyin.Height = 245;

                    strTemp = listBoxPinyin.Items[0].ToString().Substring(listBoxPinyin.Items[0].ToString().IndexOf(' ') + 1);
                    labelPinyin.Text = strTemp + " "; ;

                }
                else
                {
                    strTemp = strTemp.Substring(strTemp.IndexOf(' ') + 1);
                    labelPinyin.Text = strTemp + " "; ;
                    listBoxPinyin.Items.Clear();
                    listBoxPinyin.Visible = false;
                    //listBoxPinyin.Dock = DockStyle.None;
                    panelPinyin.Height = 20;
                }

            }
            else
            {
                labelPinyin.Text = "";
                panelPinyin.SendToBack();
                panelPinyin.Visible = false;

                _formControl.StrSearchByPinYin = "";
            }

        }




        private void buttonNewTestament_Click(object sender, EventArgs e)
        {
            ShowFullNameList(true);

            buttonNewTestament.Enabled = false;
            buttonNewTestament.Enabled = true;
        }

        private void buttonOldTestament_Click(object sender, EventArgs e)
        {
            ShowFullNameList(false);

            buttonOldTestament.Enabled = false;
            buttonOldTestament.Enabled = true;
        }

        private void listBoxPinyin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPinyin.SelectedItems.Count > 0)
            {
                string strTemp = listBoxPinyin.Items[listBoxPinyin.SelectedIndex].ToString();
                strTemp = strTemp.Substring(strTemp.IndexOf(' ') + 1);
                labelPinyin.Text = strTemp + " ";

                listBoxPinyin.Enabled = false;
                listBoxPinyin.Enabled = true;

                //this.panelLectionContent.Focus();
            }
        }


        private void FormScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > 100)
            {
                this.Controls.Remove(uclpVolumeName);
                this.Controls.Remove(uclpChapterNumber);
                this.Controls.Remove(uclpVerseNumberBegin);
                this.Controls.Remove(uclpVerseNumberEnd);

                buttonNewTestament.Visible = false;
                buttonOldTestament.Visible = false;


                if (_bMouseScroll)
                {
                    ScrollScreen(e.Y - _iMouseScreenY);

                    this.Invalidate();
                }

                _iMouseScreenY = e.Y;


                return;
            }

            if (e.X < 40 && e.Y < 100)
            {
                if (!this.Controls.Contains(uclpVolumeName))
                {
                    ShowFullNameList(true);
                }
                return;
            }


            if (e.X < 100 && e.Y < 40)
            {
                if (!this.Controls.Contains(uclpVolumeName))
                {
                    ShowFullNameList(false);
                }

                return;

            }

        }

        private void FormScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
            {
                _bMouseScroll = true;
            }
        }

        private void FormScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
            {
                _bMouseScroll = false;
            }
            else if (e.Button == MouseButtons.Right)
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

            this.ScrollScreenTo(0);
            this.Invalidate(true);
        }

        private void ToolStripMenuItemPreviousLection_Click(object sender, EventArgs e)
        {

            if (_formControl.listViewLectionList.Items.Count <= 0)
            {
                return;
            }

            if (_formControl.listViewLectionList.Items.Count > 0 && _formControl.listViewLectionList.SelectedItems.Count == 0) //判断有没有选择项
            {
                _formControl.listViewLectionList.Items[0].Selected = true;
            }

            //计算选择项的位置
            int iPageUpIndex = _formControl.listViewLectionList.SelectedItems[0].Index - 1 < 0 ? 0 : _formControl.listViewLectionList.SelectedItems[0].Index - 1;
            _formControl.listViewLectionList.Items[iPageUpIndex].Selected = true;
        }

        private void ToolStripMenuItemNextLection_Click(object sender, EventArgs e)
        {
            if (_formControl.listViewLectionList.Items.Count <= 0)
            {
                return;
            }

            if (_formControl.listViewLectionList.Items.Count > 0 && _formControl.listViewLectionList.SelectedItems.Count == 0) //判断有没有选择项
            {
                _formControl.listViewLectionList.Items[0].Selected = true;
            }

            //计算选择项的位置
            int iPageDownIndex = _formControl.listViewLectionList.SelectedItems[0].Index + 1 >= _formControl.listViewLectionList.Items.Count ? _formControl.listViewLectionList.SelectedItems[0].Index : _formControl.listViewLectionList.SelectedItems[0].Index + 1;
            _formControl.listViewLectionList.Items[iPageDownIndex].Selected = true;

        }

        private void ToolStripMenuItemCopyLectionToClipBoard_Click(object sender, EventArgs e)
        {
            if (_formControl.StrTitle == null || _formControl.StrTitle == "")
            {
                return;
            }


            System.Windows.Forms.Clipboard.SetText(_formControl.StrTitle + _formControl.StrLection);
        }


    }
}
