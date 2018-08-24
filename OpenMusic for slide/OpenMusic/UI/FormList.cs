using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenMusic.BLL;
using System.IO;
using OpenMusic.DAL;

namespace OpenMusic.UI
{
    public partial class FormList : Form
    {

        private FormControl _formControl;

        public FormList(FormControl formControl)
        {
            _formControl = formControl;

            InitializeComponent();
        }

        private void FormList_Load(object sender, EventArgs e)
        {

            InitMusicianList();

        }


        //初始化控件，填充音乐家列表
        private void InitMusicianList()
        {
            listViewMusicianList.Items.Clear();

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
                        listViewMusicianList.Items.Add(strsMusician[i].ToLower());

                        comboBoxMusicianList.Items.Add(strsMusician[i].ToLower());
                    }
                }
                Array.Clear(strsMusician, 0, strsMusician.Length);
            }

            //定位到刚创建的系列列表
            for (int i = 0; i < this.comboBoxMusicianList.Items.Count; i++)
            {
                if (_formControl.StrLastMusicianList == this.comboBoxMusicianList.Items[i].ToString())
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
                if (_formControl.StrLastAlbumList == this.comboBoxAlbumList.Items[i].ToString())
                {
                    this.comboBoxAlbumList.SelectedIndex = i;
                    break;
                }
            }


        }

        private void comboBoxAlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAlbumList.SelectedIndex < 0)
            {
                return;
            }

            listViewUserMusicList.Items.Clear();

            //显示音乐列表
            string strMusicList = ListHelper.GetMusicList(comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString(), comboBoxAlbumList.Items[comboBoxAlbumList.SelectedIndex].ToString());


            if (strMusicList != "")
            {
                string[] strsContent = strMusicList.Split('|');

                for (int i = 0; i < strsContent.Length; i++)
                {
                    if (strsContent[i] != "")
                    {

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


                        listViewUserMusicList.Items.Add(item);


                    }
                }

                Array.Clear(strsContent, 0, strsContent.Length);

            }


        }

        private void listViewMusicianList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listViewAlbumList.Items.Clear();
            this.listViewMusicList.Items.Clear();

            if (listViewMusicianList.Items.Count <= 0 || listViewMusicianList.SelectedIndices.Count == 0 || listViewMusicianList.SelectedIndices[0] < 0)
            {
                return;
            }

            string strList = ListHelper.GetAlbumList(listViewMusicianList.Items[listViewMusicianList.SelectedIndices[0]].Text);
            //listViewAlbumList.Items.Clear();

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
                        listViewAlbumList.Items.Add(strsAlbum[i].ToLower());
                    }
                }
                Array.Clear(strsAlbum, 0, strsAlbum.Length);
            }
        }

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

        private void listViewAlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listViewMusicList.Items.Clear();

            if (listViewAlbumList.Items.Count <= 0 || listViewAlbumList.SelectedIndices.Count == 0 || listViewAlbumList.SelectedIndices[0] < 0)
            {
                return;
            }

            try
            {

                string strList = ListHelper.GetMusicList(listViewMusicianList.Items[listViewMusicianList.SelectedIndices[0]].Text, listViewAlbumList.Items[listViewAlbumList.SelectedIndices[0]].Text);

                if (strList != "")
                {
                    string[] strsMusic = strList.Split('|');
                    for (int i = 0; i < strsMusic.Length; i++)
                    {
                        if (strsMusic[i] == "")
                        {

                        }
                        else
                        {

                            //string strID = strsMusic[i].Substring(0, 3);
                            //string strName = strsMusic[i].Remove(strsMusic[i].Length - 4, 4).Substring(3);
                            string strName = strsMusic[i].Remove(strsMusic[i].Length - 4, 4);


                            ////去掉开头的0
                            //while (strID[0] == '0')
                            //{
                            //    strID = strID.Substring(1, strID.Length - 1);
                            //}

                            //ListViewItem item = new ListViewItem(strID);
                            //item.SubItems.Add(strName);
                            ListViewItem item = new ListViewItem(strName);


                            //将音频文件路径存入tag属性
                            //将目录和文件名拼接起来，自动匹配mp3，wma，mid格式
                            string strAudioFilePath = null;
                            strAudioFilePath = MusicHelper.GetAudioFilePathByLyricFilePath(MusicHelper.GetLyricFilePathByLyricFileName(listViewMusicianList.Items[listViewMusicianList.SelectedIndices[0]].Text,
                            listViewAlbumList.Items[listViewAlbumList.SelectedIndices[0]].Text, strsMusic[i]));
                            if (strAudioFilePath == "")
                            {
                                item.Tag = MusicHelper.GetLyricFilePathByLyricFileName(listViewMusicianList.Items[listViewMusicianList.SelectedIndices[0]].Text,
                            listViewAlbumList.Items[listViewAlbumList.SelectedIndices[0]].Text, strsMusic[i]);
                            }
                            else
                            {
                                item.Tag = strAudioFilePath;
                            }

                            listViewMusicList.Items.Add(item);
                        }
                    }
                    Array.Clear(strsMusic, 0, strsMusic.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //新建一个播放列表/专辑
        private void buttonCreateList_Click(object sender, EventArgs e)
        {
            //过用户没有选择“系列”，则提示用户先选择一个系列
            if (comboBoxMusicianList.SelectedIndex < 0)
            {
                MessageBox.Show("请先选中一个[系列]，再新建专辑。");
            }
            else
            {
                FormInput formInput = new FormInput();
                formInput.Text = "您将在[" + comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString() + "]系列中新建专辑，请输入专辑名称并[确定]";
                formInput.textBoxMessage.Text = DateTime.Now.ToString("yyyyMMdd");
                formInput.ShowDialog();

                if (formInput.StrReturnValue != "")
                {
                    //新建目录，如果重复则提示已存在同名专辑
                    MessageBox.Show(ListHelper.CreateAlbum(comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString(), formInput.StrReturnValue));

                    //刷新专辑列表
                    comboBoxMusicianList_SelectedIndexChanged(null, null);
                }

                formInput.Dispose();
            }

        }

        //插入/拷贝歌曲和歌词等
        private void buttonInsert_Click(object sender, EventArgs e)
        {
            //将当前选择的歌曲拷贝到正在编辑的专辑中

            //ListHelper.AddMusicToAlbum(listViewMusicianList.Items[listViewMusicianList.SelectedIndices[0]].Text,listViewAlbumList.Items[listViewAlbumList.SelectedIndices[0]].Text,listViewMusicList.Items[listViewMusicList.SelectedIndices[0]].Text,
            //    comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString(),comboBoxAlbumList.Items[comboBoxAlbumList.SelectedIndex].ToString(),

            if (listViewMusicList.Items.Count <= 0 || listViewMusicList.SelectedIndices.Count == 0 || listViewMusicList.SelectedIndices[0] < 0 || comboBoxAlbumList.SelectedIndex < 0)
            {
                return;
            }

            //获得目标文件的文件名编号，例如“005奇异恩典.mp3”中的“005”
            string strIndexTemp = "";
            if (listViewUserMusicList.Items.Count < 0)
            {
                //strIndexTemp = "001";
            }
            else
            {
                strIndexTemp = (listViewUserMusicList.Items.Count + 1).ToString("d3");
            }

            //如果文件名的前n位是0-9的数字，则用新的编号替换此数字
            //由tag获得文件名
            FileInfo fileInfo = new FileInfo(listViewMusicList.SelectedItems[0].Tag.ToString());
            string strFileNameTemp = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);

            int iNumCount = 0;
            for (int i = 0; i < strFileNameTemp.Length; i++)
            {
                if (IsNumeric(strFileNameTemp[i].ToString()))
                {
                    iNumCount++;
                }
                else
                {
                    break;
                }
            }
            strFileNameTemp = strFileNameTemp.Substring(iNumCount);

            strFileNameTemp = strIndexTemp + strFileNameTemp + listViewMusicList.SelectedItems[0].Tag.ToString().Substring(listViewMusicList.SelectedItems[0].Tag.ToString().Length - 4);

            ListHelper.AddMusicToAlbum(listViewMusicList.SelectedItems[0].Tag.ToString(),
                comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString(),
                comboBoxAlbumList.Items[comboBoxAlbumList.SelectedIndex].ToString(),
                strFileNameTemp
                );


            //刷新
            comboBoxAlbumList_SelectedIndexChanged(null, null);

        }

        //删除专辑
        private void buttonDeleteList_Click(object sender, EventArgs e)
        {
            try
            {
                //提示用户是否真的要删除
                if (MessageBox.Show("是否真的要删除专辑[" + comboBoxAlbumList.Items[comboBoxAlbumList.SelectedIndex].ToString() + "]？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MessageBox.Show(ListHelper.DeleteAlbum(comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString(), comboBoxAlbumList.Items[comboBoxAlbumList.SelectedIndex].ToString()));

                    //刷新专辑列表
                    comboBoxMusicianList_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //专辑改名
        private void buttonRenameList_Click(object sender, EventArgs e)
        {
            //过用户没有选择“系列”，则提示用户先选择一个系列
            if (comboBoxMusicianList.SelectedIndex < 0 || comboBoxAlbumList.SelectedIndex < 0)
            {
                MessageBox.Show("请先选中一个[专辑]！");
            }
            else
            {
                FormInput formInput = new FormInput();
                formInput.Text = "您将修改[" + comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString() + "]系列中的[" + comboBoxAlbumList.Items[comboBoxAlbumList.SelectedIndex].ToString() + "]专辑，请输入新的专辑名称并[确定]";
                formInput.textBoxMessage.Text = comboBoxAlbumList.Items[comboBoxAlbumList.SelectedIndex].ToString();
                formInput.ShowDialog();

                if (formInput.StrReturnValue != "")
                {
                    //改名，如果重复则提示已存在同名专辑
                    MessageBox.Show(ListHelper.RenameAlbum(comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString(), comboBoxAlbumList.Items[comboBoxAlbumList.SelectedIndex].ToString(), formInput.StrReturnValue));

                    //刷新专辑列表
                    comboBoxMusicianList_SelectedIndexChanged(null, null);
                }

                formInput.Dispose();

            }

        }


        private void ChangeFileOrder(int iSelectedIndex, int iIncrease)
        {
            //修改当前选择的文件名

            FileInfo fileInfo = new FileInfo(listViewUserMusicList.SelectedItems[0].Tag.ToString());
            //组成新文件名
            string strNewFileName = iSelectedIndex.ToString("d3") + fileInfo.Name.Substring(3, fileInfo.Name.Length - 3);
            //改名
            fileInfo.MoveTo(fileInfo.FullName.Replace(fileInfo.Name, strNewFileName));
            //如果是音频文件，则修改txt文件
            if (fileInfo.Extension != ".txt")
            {
                fileInfo = new FileInfo(listViewUserMusicList.SelectedItems[0].Tag.ToString().Substring(0, listViewUserMusicList.SelectedItems[0].Tag.ToString().Length - 3) + "txt");
                //组成新文件名
                strNewFileName = iSelectedIndex.ToString("d3") + fileInfo.Name.Substring(3, fileInfo.Name.Length - 3);
                //改名
                fileInfo.MoveTo(fileInfo.FullName.Replace(fileInfo.Name, strNewFileName));
            }


            //修改前一位文件的文件名
            fileInfo = new FileInfo(listViewUserMusicList.Items[listViewUserMusicList.SelectedIndices[0] - iIncrease].Tag.ToString());
            //组成新文件名
            strNewFileName = (iSelectedIndex + iIncrease).ToString("d3") + fileInfo.Name.Substring(3, fileInfo.Name.Length - 3);
            //改名
            fileInfo.MoveTo(fileInfo.FullName.Replace(fileInfo.Name, strNewFileName));
            //如果是音频文件，则修改txt文件
            if (fileInfo.Extension != ".txt")
            {
                fileInfo = new FileInfo(listViewUserMusicList.Items[listViewUserMusicList.SelectedIndices[0] - iIncrease].Tag.ToString().Substring(0, listViewUserMusicList.Items[listViewUserMusicList.SelectedIndices[0] - iIncrease].Tag.ToString().Length - 3) + "txt");
                //组成新文件名
                strNewFileName = (iSelectedIndex + iIncrease).ToString("d3") + fileInfo.Name.Substring(3, fileInfo.Name.Length - 3);
                //改名
                fileInfo.MoveTo(fileInfo.FullName.Replace(fileInfo.Name, strNewFileName));
            }

        }


        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            if (listViewUserMusicList.Items.Count <= 0 || listViewUserMusicList.SelectedItems.Count <= 0)
            {
                return;
            }

            //得到当前选择的文件序号
            int iSelectedIndex = listViewUserMusicList.SelectedIndices[0] + 1;

            //如果当前选中的序号不是第一位，则可以向上移动一位
            if (iSelectedIndex > 1)
            {
                iSelectedIndex--;

                ChangeFileOrder(iSelectedIndex, +1);

                comboBoxAlbumList_SelectedIndexChanged(null, null);
            }
            else
            {
                MessageBox.Show("已经是第一条记录！");
            }

        }

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            if (listViewUserMusicList.Items.Count <= 0 || listViewUserMusicList.SelectedItems.Count <= 0)
            {
                return;
            }

            //得到当前选择的文件序号
            int iSelectedIndex = listViewUserMusicList.SelectedIndices[0] + 1;
            //int iSelectedIndex = int.Parse(listViewUserMusicList.SelectedItems[0].SubItems[0].Text);

            //如果当前选中的序号不是第一位，则可以向上移动一位
            if (iSelectedIndex < listViewUserMusicList.Items.Count)
            {
                iSelectedIndex++;

                ChangeFileOrder(iSelectedIndex, -1);

                comboBoxAlbumList_SelectedIndexChanged(null, null);

            }
            else
            {
                MessageBox.Show("已经是最后一条记录！");
            }
        }

        private void buttonRemoveMusic_Click(object sender, EventArgs e)
        {
            if (listViewUserMusicList.Items.Count <= 0 || listViewUserMusicList.SelectedItems.Count <= 0)
            {
                return;
            }

            //记录当前选中的序号
            int iSelectedIndex = listViewUserMusicList.SelectedIndices[0] + 1;

            //删除当前选中的音频文件
            FileInfo fileInfo = new FileInfo(listViewUserMusicList.SelectedItems[0].Tag.ToString());
            fileInfo.Delete();

            //删除txt文件
            if (fileInfo.Extension != ".txt")
            {
                fileInfo = new FileInfo(listViewUserMusicList.SelectedItems[0].Tag.ToString().Substring(0, listViewUserMusicList.SelectedItems[0].Tag.ToString().Length - 3) + "txt");
                fileInfo.Delete();
            }

            //重新排序其后的文件的顺序（名字）
            for (int i = iSelectedIndex; i < listViewUserMusicList.Items.Count; i++)
            {
                fileInfo = new FileInfo(listViewUserMusicList.Items[i].Tag.ToString());
                //组成新文件名
                string strNewFileName = (i).ToString("d3") + fileInfo.Name.Substring(3, fileInfo.Name.Length - 3);
                //改名
                fileInfo.MoveTo(fileInfo.FullName.Replace(fileInfo.Name, strNewFileName));
                //如果是音频文件，则修改txt文件
                if (fileInfo.Extension != ".txt")
                {
                    fileInfo = new FileInfo(listViewUserMusicList.Items[i].Tag.ToString().Substring(0, listViewUserMusicList.Items[i].Tag.ToString().Length - 3) + "txt");
                    //组成新文件名
                    strNewFileName = (i).ToString("d3") + fileInfo.Name.Substring(3, fileInfo.Name.Length - 3);
                    //改名
                    fileInfo.MoveTo(fileInfo.FullName.Replace(fileInfo.Name, strNewFileName));

                    //fileInfo = null;
                }

            }

            comboBoxAlbumList_SelectedIndexChanged(null, null);
        }

        private void listViewMusicList_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            buttonInsert_Click(null, null);

            ////将当前选择的歌曲拷贝到正在编辑的专辑中

            //if (listViewMusicList.Items.Count <= 0 || listViewMusicList.SelectedIndices.Count == 0 || listViewMusicList.SelectedIndices[0] < 0)
            //{
            //    return;
            //}

            ////获得目标文件的文件名编号，例如“005奇异恩典.mp3”中的“005”
            //string strIndexTemp = "";
            //if (listViewUserMusicList.Items.Count <= 0)
            //{
            //    strIndexTemp = "001";
            //}
            //else
            //{
            //    strIndexTemp = (listViewUserMusicList.Items.Count + 1).ToString("d3");
            //}

            ////拷贝
            //ListHelper.AddMusicToAlbum(listViewMusicList.SelectedItems[0].Tag.ToString(),
            //    comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString(),
            //    comboBoxAlbumList.Items[comboBoxAlbumList.SelectedIndex].ToString(),
            //    strIndexTemp + listViewMusicList.SelectedItems[0].SubItems[1].Text +
            //    listViewMusicList.SelectedItems[0].Tag.ToString().Substring(listViewMusicList.SelectedItems[0].Tag.ToString().Length - 4));


            ////刷新
            //comboBoxAlbumList_SelectedIndexChanged(null, null);

        }

        private void buttonRenameMusic_Click(object sender, EventArgs e)
        {
            if (listViewUserMusicList.Items.Count <= 0 || listViewUserMusicList.SelectedIndices.Count == 0 || listViewUserMusicList.SelectedIndices[0] < 0)
            {
                return;
            }

            //得到选中的音乐的名字，并显示在文本框中供修改
            FormInput formInput = new FormInput();
            formInput.textBoxMessage.Text = listViewUserMusicList.SelectedItems[0].Text;
            formInput.ShowDialog();

            if (formInput.StrReturnValue != "")
            {
                FileInfo fileInfo = new FileInfo(listViewUserMusicList.SelectedItems[0].Tag.ToString());
                fileInfo.MoveTo(listViewUserMusicList.SelectedItems[0].Tag.ToString().Replace(listViewUserMusicList.SelectedItems[0].Text,
                    formInput.StrReturnValue));

                //如果当前编辑的不是txt文件，则自动编辑txt文件
                if (fileInfo.Extension != ".txt")
                {
                    string strTxtFile = listViewUserMusicList.SelectedItems[0].Tag.ToString().Substring(0, listViewUserMusicList.SelectedItems[0].Tag.ToString().Length - 3) + "txt";
                    fileInfo = new FileInfo(strTxtFile);
                    fileInfo.MoveTo(strTxtFile.Replace(listViewUserMusicList.SelectedItems[0].Text,
                    formInput.StrReturnValue));
                }
            }

            formInput.Dispose();

            comboBoxAlbumList_SelectedIndexChanged(null, null);
        }

        private void buttonModifyLyric_Click(object sender, EventArgs e)
        {
            //得到要修改的txt文件，显示在textBox中
            if (listViewUserMusicList.Items.Count <= 0 || listViewUserMusicList.SelectedIndices.Count == 0 || listViewUserMusicList.SelectedIndices[0] < 0)
            {
                return;
            }

            //得到选中的音乐的名字，并显示在文本框中供修改
            FormLyric formLyric = new FormLyric();
            formLyric.Text = "修改歌词后，按[保存]键确认";
            string strTxtFilePath = listViewUserMusicList.SelectedItems[0].Tag.ToString().Substring(0, listViewUserMusicList.SelectedItems[0].Tag.ToString().Length - 3) + "txt";
            formLyric.Lyric = MusicHelper.GetLyricsByLyricFilePath(strTxtFilePath);
            if (formLyric.ShowDialog() == DialogResult.OK)
            {
                if (formLyric.Lyric != "")
                {
                    MusicHelper.SetLyricsByLyricFilePath(strTxtFilePath, formLyric.Lyric);
                }
            }

            formLyric.Dispose();

        }

        private void buttonCreateLyric_Click(object sender, EventArgs e)
        {
            //提示输入文件名
            FormInput formInput = new FormInput();
            formInput.Text = "请输入音乐名称";
            if (formInput.ShowDialog() == DialogResult.OK)
            {
                if (formInput.StrReturnValue != "")
                {
                    //修改文件编号
                    string strIndexTemp = (listViewUserMusicList.Items.Count + 1).ToString("d3");
                    string strTxtFileName = strIndexTemp + formInput.StrReturnValue + ".txt";
                    string strTxtFilePath = MusicHelper.GetLyricFilePathByLyricFileName(comboBoxMusicianList.Items[comboBoxMusicianList.SelectedIndex].ToString(), comboBoxAlbumList.Items[comboBoxAlbumList.SelectedIndex].ToString(), strTxtFileName);

                    //提示输入歌词，保存
                    FormLyric formLyric = new FormLyric();

                    if (formLyric.ShowDialog() == DialogResult.OK)
                    {
                        if (formLyric.Lyric != "")
                        {
                            MusicHelper.SetLyricsByLyricFilePath(strTxtFilePath, formLyric.Lyric);

                            comboBoxAlbumList_SelectedIndexChanged(null, null);
                        }

                    }

                }
            }

        }

        private void buttonSearchMusic_Click(object sender, EventArgs e)
        {
            if (textBoxSearchCondition.Text == "")
            {
                return;
            }

            listViewMusicList.Items.Clear();

            string strList = "";
            if (radioButtonSearchByName.Checked)
            {
                strList = MusicHelper.SearchMusicByName(textBoxSearchCondition.Text);
            }
            else
            {
                strList = MusicHelper.SearchMusicByLyrics(textBoxSearchCondition.Text);
            }


            if (strList != "")
            {
                string[] strsMusic = strList.Split('|');
                for (int i = 0; i < strsMusic.Length; i++)
                {
                    if (strsMusic[i] == "")
                    {

                    }
                    else
                    {

                        FileInfo fileInfo = new FileInfo(strsMusic[i]);

                        //string strID = fileInfo.Name.Substring(0, 3);
                        //string strName = fileInfo.Name.Remove(fileInfo.Name.Length - 4, 4);

                        string strName = strsMusic[i].Remove(strsMusic[i].Length - 4, 4);
                        int iIndex = strName.LastIndexOf('\\',strName.LastIndexOf('\\',(strName.LastIndexOf('\\', strName.Length - 1) - 1)) - 1);
                        strName = strName.Substring(iIndex);


                        ////去掉开头的0
                        //while (strID[0] == '0')
                        //{
                        //    strID = strID.Substring(1, strID.Length - 1);
                        //}

                        ListViewItem item = new ListViewItem(strName);

                        string strAudioFilePath = null;
                        strAudioFilePath = MusicHelper.GetAudioFilePathByLyricFilePath(strsMusic[i]);
                        if (strAudioFilePath == "")
                        {
                            item.Tag = strsMusic[i];
                        }
                        else
                        {
                            item.Tag = strAudioFilePath;
                        }

                        //item.Tag = strsMusic[i];

                        //将音频文件路径存入tag属性
                        //将目录和文件名拼接起来，自动匹配mp3，wma，mid格式
                        //string strAudioFilePath = null;

                        //strAudioFilePath = strsMusic[i];

                        //item.Tag = strsMusic[i];

                        //strAudioFilePath = MusicHelper.GetAudioFilePathByLyricFilePath(MusicHelper.GetLyricFilePathByLyricFileName(listViewMusicianList.Items[listViewMusicianList.SelectedIndices[0]].Text,
                        //listViewAlbumList.Items[listViewAlbumList.SelectedIndices[0]].Text, strsMusic[i]));

                        //if (strAudioFilePath == "")
                        //{
                        //    item.Tag = strsMusic[i];

                        ////    item.Tag = MusicHelper.GetLyricFilePathByLyricFileName(listViewMusicianList.Items[listViewMusicianList.SelectedIndices[0]].Text,
                        ////listViewAlbumList.Items[listViewAlbumList.SelectedIndices[0]].Text, strsMusic[i]);
                        //}
                        //else
                        //{
                        //    item.Tag = strAudioFilePath;
                        //}

                        listViewMusicList.Items.Add(item);
                    }
                }
                Array.Clear(strsMusic, 0, strsMusic.Length);

            }

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

    }
}
