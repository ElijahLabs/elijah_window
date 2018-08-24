using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OpenMusic.BLL;

namespace OpenMusic.UI
{
    public partial class FormSearchResult : Form
    {
        private string _strCondition = "";
        private bool _bIsSearchByName = true;
                                
        public  string StrMusicianName= "";
        public string StrAlbumName = "";
        public string StrMusicName = "";

        public FormSearchResult(string strCondition,bool bIsSearchByName)
        {
            InitializeComponent();


            _strCondition = strCondition;
            _bIsSearchByName = bIsSearchByName;

        }

        private void listViewMusicList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonOK_Click(null, null);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (listViewMusicList.Items.Count <= 0 || listViewMusicList.SelectedItems.Count <= 0)
            {
                return;
            }

            StrMusicianName = listViewMusicList.SelectedItems[0].SubItems[0].Text;
            StrAlbumName = listViewMusicList.SelectedItems[0].SubItems[1].Text;
            StrMusicName = listViewMusicList.SelectedItems[0].SubItems[2].Text;

            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }

        private void FormSearchResult_Load(object sender, EventArgs e)
        {
            //得到查询结果，并显示
            if (_strCondition == "")
            {
                return;
            }

            listViewMusicList.Items.Clear();

            string strList = "";
            if (_bIsSearchByName)
            {
                strList = MusicHelper.SearchMusicByName(_strCondition);
            }
            else
            {
                strList = MusicHelper.SearchMusicByLyrics(_strCondition);
            }


            if (strList != "")
            {

                strList = strList.ToLower();

                string[] strsMusic = strList.Split('|');
                for (int i = 0; i < strsMusic.Length; i++)
                {
                    if (strsMusic[i] == "")
                    {

                    }
                    else
                    {

                        FileInfo fileInfo = new FileInfo(strsMusic[i]);

                        string strName = strsMusic[i].Remove(strsMusic[i].Length - 4, 4);
                        int iIndex = strName.LastIndexOf('\\', strName.LastIndexOf('\\', (strName.LastIndexOf('\\', strName.Length - 1) - 1)) - 1);
                        strName = strName.Substring(iIndex);

                        //拆解得到系列、专辑、音乐名称
                        string str1= "";
                        string str2= "";
                        string str3= "";
                        
                        string[] strsNames = strName.Split('\\');

                        try
                        {
                            str1 = strsNames[1];
                            str2 = strsNames[2];
                            str3 = strsNames[3];
                        }
                        catch(Exception)
                        {

                        }

                        ListViewItem item = new ListViewItem(str1);
                        item.SubItems.Add(str2);
                        item.SubItems.Add(str3);

                        item.Tag = strsMusic[i];

                        //将音频文件路径存入tag属性
                        //将目录和文件名拼接起来，自动匹配mp3，wma，mid格式
                        //string strAudioFilePath = null;

                        //strAudioFilePath = strsMusic[i];

                        item.Tag = strsMusic[i];

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
    }
}
