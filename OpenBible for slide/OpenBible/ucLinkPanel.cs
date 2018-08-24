using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenBible
{
    public partial class ucLinkPanel : UserControl
    {

        //绘制LinkLabel的位移
        private int _xOffset;
        private int _yOffset;

        //最大的x位移
        private int _maxXOffset;


        //返回结果
        private string _strReturnResult;

        //最大宽度和高度
        private int _maxWidth;
        private int _maxHeight;

        //默认的背景颜色
        private Color _colorBackColor;

        public ucLinkPanel()
        {
            InitializeComponent();
        }

        private void ucLinkPanel_Load(object sender, EventArgs e)
        {
            this._maxWidth = 0;
            this._maxHeight = 0;

            this._xOffset = 0;
            this._yOffset = 0;

            this._maxXOffset = 0;

        }

        //选择事件委托
        public delegate void SelectedEventHandler(object sender, EventArgs e);
        //选择事件
        public event SelectedEventHandler Selected;

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="input"></param>
        public void LoadData(string[,] inputArray)
        {
            //重置LinkLabel的绘制位置
            this._maxWidth = 0;
            this._maxHeight = 0;

            _xOffset = 0;
            _yOffset = 0;

            _maxXOffset = 0;

            //清除已有LinkLabel
            while (this.Controls.Count > 0)
            {
                ((LinkLabel)this.Controls[this.Controls.Count - 1]).LinkClicked -= new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
                ((LinkLabel)this.Controls[this.Controls.Count - 1]).MouseLeave -= new System.EventHandler(this.linkLabel_MouseLeave);
                ((LinkLabel)this.Controls[this.Controls.Count - 1]).MouseEnter -= new System.EventHandler(this.linkLabel_MouseEnter);

                this.Controls[this.Controls.Count - 1].Dispose();
                //this.Controls.RemoveAt(this.Controls.Count - 1);
            }

            //动态创建LinkLabel
            if (inputArray != null)
            {
                int i = 1;

                for(int j=0;j<inputArray.GetLength(0);j++)
                {
                    //第一列数据用于显示
                    LinkLabel linkLabel = new LinkLabel();
                    linkLabel.AutoSize = true;
                    linkLabel.Text = inputArray[j,0];
                    linkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
                    linkLabel.Name = "linkLabel" + linkLabel.Text;
                    linkLabel.TabIndex = 0;
                    linkLabel.TabStop = false;
                    linkLabel.LinkColor = Color.Black;


                    //第二列数据用于返回
                    linkLabel.Tag = inputArray[j,1];

                    this.Controls.Add(linkLabel);
                    linkLabel.Left = _xOffset;
                    linkLabel.Top = _yOffset;

                    linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
                    linkLabel.MouseLeave += new System.EventHandler(this.linkLabel_MouseLeave);
                    linkLabel.MouseEnter += new System.EventHandler(this.linkLabel_MouseEnter);

                    //计算控件尺寸
                    //if (linkLabel.Right > _maxWidth)
                    //{
                    //    _maxWidth = linkLabel.Right;
                    //}

                    if (_xOffset > _maxWidth)
                    {
                        _maxWidth = _xOffset;
                    }

                    if (_yOffset > _maxHeight)
                    {
                        _maxHeight = _yOffset;
                    }

                    if (linkLabel.Right > _maxXOffset)
                    {
                        _maxXOffset = linkLabel.Right;
                    }

                    if (i > 20)
                    {
                        _xOffset = _maxXOffset + 0;
                        _yOffset = 0;

                        i = 1;
                    }
                    else
                    {
                        _yOffset = linkLabel.Bottom + 2;
                        i++;
                    }
                }

                this.Width = this._maxWidth;
                this.Height = this._maxHeight;
            }

        }

        ///// <summary>
        ///// 加载数据
        ///// </summary>
        ///// <param name="reader"></param>
        //public void LoadData(IDataReader reader)
        //{
        //    //重置LinkLabel的绘制位置
        //    _xOffset = 0;
        //    _yOffset = 0;

        //    //清除已有LinkLabel
        //    while (this.Controls.Count > 0)
        //    {
        //        int iRemoveIndex = this.Controls.Count - 1;
        //        ((LinkLabel)this.Controls[iRemoveIndex]).LinkClicked -= new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
        //        ((LinkLabel)this.Controls[iRemoveIndex]).MouseLeave -= new System.EventHandler(this.linkLabel_MouseLeave);
        //        ((LinkLabel)this.Controls[iRemoveIndex]).MouseEnter -= new System.EventHandler(this.linkLabel_MouseEnter);

        //        this.Controls[iRemoveIndex].Dispose();
        //        //this.Controls.RemoveAt(iRemoveIndex);
        //    }

        //    //动态创建LinkLabel
        //    if (reader != null)
        //    {
        //        int i = 1;

        //        while(reader.Read())
        //        {
        //            //第一列数据用于显示
        //            LinkLabel linkLabel = new LinkLabel();
        //            linkLabel.AutoSize = true;
        //            linkLabel.Text = reader[0].ToString();
        //            linkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
        //            linkLabel.Name = "linkLabel" + linkLabel.Text;
        //            linkLabel.TabIndex = 0;
        //            linkLabel.TabStop = false;
        //            linkLabel.LinkColor = Color.Black;


        //            //第二列数据用于返回
        //            linkLabel.Tag = reader[1].ToString();

        //            this.Controls.Add(linkLabel);
        //            linkLabel.Left = _xOffset;
        //            linkLabel.Top = _yOffset;

        //            linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
        //            linkLabel.MouseLeave += new System.EventHandler(this.linkLabel_MouseLeave);
        //            linkLabel.MouseEnter += new System.EventHandler(this.linkLabel_MouseEnter);

        //            //计算控件尺寸
        //            if (_xOffset > _maxWidth)
        //            {
        //                _maxWidth = _xOffset;
        //            }

        //            if (_yOffset > _maxHeight)
        //            {
        //                _maxHeight = _yOffset;
        //            }

        //            if (i > 20)
        //            {
        //                _xOffset += linkLabel.Width + 40;
        //                _yOffset = 0;

        //                i = 1;
        //            }
        //            else
        //            {
        //                _yOffset += linkLabel.Height + 5;
        //                i++;
        //            }
        //        }

        //        this.Width = this._maxWidth;
        //        this.Height = this._maxHeight;
        //    }
        //}

        //鼠标移出后变色
        private void linkLabel_MouseLeave(object sender, EventArgs e)
        {
            ((LinkLabel)sender).LinkColor = Color.Black;
            ((LinkLabel)sender).BackColor = _colorBackColor;
        }

        //鼠标移入后变色
        private void linkLabel_MouseEnter(object sender, EventArgs e)
        {
            _colorBackColor = ((LinkLabel)sender).BackColor;

            ((LinkLabel)sender).LinkColor = Color.Red;
            ((LinkLabel)sender).BackColor = Color.Yellow;
        }

        //鼠标点击后返回tag
        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (((LinkLabel)sender).Tag != null)
            {
                _strReturnResult = ((LinkLabel)sender).Tag.ToString();

                if (Selected != null)
                {
                    Selected(sender, e);
                }
            }
        }

        //获取返回值
        public string GetSelectedValue()
        {
            return _strReturnResult;
        }

    }
}
