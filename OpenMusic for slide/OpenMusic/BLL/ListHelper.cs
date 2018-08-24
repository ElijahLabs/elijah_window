using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OpenMusic.DAL;
using OpenMusic.Properties;

namespace OpenMusic.BLL
{
    public class ListHelper
    {

        //程序运行路径
        private static string _strStartupPath = Application.StartupPath;

        //音乐家目录
        private static string _strMusicianListPath = _strStartupPath + "\\" + Settings.Default.MusicFolderName;

        ////专辑目录
        //private static string _strAlbumsListPath = _strStartupPath + "\\music";


        //获取音乐家列表
        public static string GetMusicianList()
        {
            return FileHelper.GetFolderNameList(_strMusicianListPath, "*", SearchOption.TopDirectoryOnly);
        }

        //根据音乐家的名字，获取其所有专辑列表
        public static string GetAlbumList(string strMusician)
        {
            return FileHelper.GetFolderNameList(_strMusicianListPath + "\\" + strMusician, "*", SearchOption.TopDirectoryOnly);
        }

        //根据音乐家名字，专辑名字，获取音乐列表
        public static string GetMusicList(string strMusician, string strAlbum)
        {
            return FileHelper.GetFileNameList(_strMusicianListPath + "\\" + strMusician + "\\" + strAlbum, "*.txt", SearchOption.TopDirectoryOnly);
        }

        //在音乐家下创建专辑
        public static string CreateAlbum(string strMusician, string strAlbum)
        {
            string strResult = "新建专辑失败！";

            //判断是否存在相同专辑
            if (FileHelper.FolderExists(_strMusicianListPath + "\\" + strMusician + "\\" + strAlbum))
            {
                strResult = "已存在同名专辑，请使用其他名称尝试。";
            }
            else
            {
                if (FileHelper.CreateFolder(_strMusicianListPath + "\\" + strMusician + "\\" + strAlbum))
                {
                    strResult = "在[" + strMusician + "]系列中新建专辑[" + strAlbum + "]成功！";
                }
            }

            return strResult;
        }

        //删除专辑下所有文件及专辑目录
        public static string DeleteAlbum(string strMusician, string strAlbum)
        {
            string strResult = "删除专辑失败！";

            if (FileHelper.DeleteFolder(_strMusicianListPath + "\\" + strMusician + "\\" + strAlbum))
            {
                strResult = "删除专辑成功！";
            }
            else
            {
                strResult = "删除专辑失败！";

            }
            return strResult;
        }

        //更改专辑名称
        public static string RenameAlbum(string strMusician, string strSourceFolderPath, string strDestFolderPath)
        {
            string strResult = "专辑改名失败！";

            //判断是否存在相同专辑
            if (FileHelper.FolderExists(_strMusicianListPath + "\\" + strMusician + "\\" + strDestFolderPath))
            {
                strResult = "已存在同名专辑，请使用其他名称尝试。";
            }
            else
            {
                if (FileHelper.RenameFolder(_strMusicianListPath + "\\" + strMusician + "\\" + strSourceFolderPath, _strMusicianListPath + "\\" + strMusician + "\\" + strDestFolderPath))
                {
                    strResult = "[" + strMusician + "]系列中的专辑[" + strSourceFolderPath + "]已成功改名为[" + strDestFolderPath + "]！";
                }
            }

            return strResult;

        }

        ////向专辑插入音乐
        //public static bool AddMusicToAlbum(string strSourceMusician, string strSourceAlbum, string strSourceMusic, string strDestMusician, string strDestAlbum, string strDestMusic)
        //{
        //    //拷贝音乐文件
        //    if (strSourceMusic.Substring(strSourceMusic.Length - 4) != ".txt")
        //    {
        //        FileHelper.CopyFile(_strMusicianListPath + "\\" + strSourceMusician + "\\" + strSourceAlbum + "\\" + strSourceMusic, _strMusicianListPath + "\\" + strDestMusician + "\\" + strDestAlbum + "\\" + strDestMusic);
        
        //        //得到歌词文件
        //        strSourceMusic = strSourceMusic.Substring(strSourceMusic.Length - 3) + "txt";
        //        strDestMusic = strDestMusic.Substring(strDestMusic.Length - 3) + "txt";
        //    }

        //    //拷贝歌词文件
        //    FileHelper.CopyFile(_strMusicianListPath + "\\" + strSourceMusician + "\\" + strSourceAlbum + "\\" + strSourceMusic, _strMusicianListPath + "\\" + strDestMusician + "\\" + strDestAlbum + "\\" + strDestMusic);

        //    return true;
        //}

        //向专辑插入音乐
        public static bool AddMusicToAlbum(string strSourceMusicFilePath, string strDestMusician, string strDestAlbum, string strDestMusic)
        {
            //拷贝音乐文件
            if (strSourceMusicFilePath.Substring(strSourceMusicFilePath.Length - 4) != ".txt")
            {
                FileHelper.CopyFile(strSourceMusicFilePath, _strMusicianListPath + "\\" + strDestMusician + "\\" + strDestAlbum + "\\" + strDestMusic);

                //得到歌词文件
                strSourceMusicFilePath = strSourceMusicFilePath.Substring(0,strSourceMusicFilePath.Length - 3) + "txt";
                strDestMusic = strDestMusic.Substring(0, strDestMusic.Length - 3) + "txt";
            }

            if (FileHelper.FileExists(strSourceMusicFilePath))
            {
                //拷贝歌词文件
                FileHelper.CopyFile(strSourceMusicFilePath, _strMusicianListPath + "\\" + strDestMusician + "\\" + strDestAlbum + "\\" + strDestMusic);
            }
            return true;
        }



    }
}
