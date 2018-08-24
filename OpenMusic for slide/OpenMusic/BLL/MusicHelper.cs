using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenMusic.DAL;
using OpenMusic.Properties;


namespace OpenMusic.BLL
{
    public class MusicHelper
    {

        //程序运行路径
        private static string _strStartupPath = Application.StartupPath;

        //音乐家目录
        private static string _strMusicianListPath = _strStartupPath + "\\" + Settings.Default.MusicFolderName;
        
        //根据文件名搜索歌曲
        public static string SearchMusicByName(string strName)
        {
            return FileHelper.GetFileFullNameList(_strMusicianListPath, "*" + strName + "*.txt", System.IO.SearchOption.AllDirectories);
        }

        public static string SearchMusicByLyrics(string strLyrics)
        {
            return FileHelper.GetFileFullNameListByKeyword(_strMusicianListPath, strLyrics, "*.txt", System.IO.SearchOption.AllDirectories);
        }


        //根据歌词文件路径得到歌词
        public static string GetLyricsByLyricFilePath(string strLyricFilePath)
        {
            return FileHelper.ReadFileContent(strLyricFilePath);
        }

        //根据文件路径设置歌词
        public static bool SetLyricsByLyricFilePath(string strLyricFilePath, string strLyric)
        {
            return FileHelper.WriteFileContent(strLyricFilePath, strLyric);
        }


        //根据歌词文件名得到歌词文件路径
        public static string GetLyricFilePathByLyricFileName(string strMusicianName, string strAlbumName, string strLyricFileName)
        {
            return _strMusicianListPath + "\\" + strMusicianName + "\\" + strAlbumName + "\\" + strLyricFileName;
        }

        //根据歌词文件路径得到音频文件路径
        public static string GetAudioFilePathByLyricFilePath(string strLyricFilePath)
        {
            string strResult = "";
            string strAudioFilePathTemp = strLyricFilePath.Remove(strLyricFilePath.Length - 3);


            if (FileHelper.FileExists(strAudioFilePathTemp + "mp3"))
            {
                strResult = strAudioFilePathTemp + "mp3";
                return strResult;

            }

            if (FileHelper.FileExists(strAudioFilePathTemp + "wma"))
            {
                strResult = strAudioFilePathTemp + "wma";
                return strResult;
            }

            if (FileHelper.FileExists(strAudioFilePathTemp + "mid"))
            {
                strResult = strAudioFilePathTemp + "mid";
                return strResult;
            }

            return strResult;

        }

    }
}
