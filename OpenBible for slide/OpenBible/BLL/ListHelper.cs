using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace OpenBible.BLL
{
    public class ListHelper
    {
        //程序运行路径
        private static string _strStartupPath = Application.StartupPath + "\\历史列表";

        public static string GetFileList()
        {
            string strResult = "";

            //读取HistoryList目录下的文本文件，形成字符串
            //File. "|DataDirectory|";

            DirectoryInfo directoryInfo = new DirectoryInfo(_strStartupPath);

            //按文件名称升序排列
            FileInfo[] fileInfos = directoryInfo.GetFiles("*.txt",SearchOption.TopDirectoryOnly).OrderBy(p =>p.Name).ToArray();

            for (int i = 0; i < fileInfos.Length; i++)
            {
                strResult += fileInfos[i].Name + "|";
            }

            Array.Clear(fileInfos, 0, fileInfos.Length);

            return strResult;
        }

        //获得文件内容
        public static string ReadFileContent(string strFileName)
        {
            string strResult = "";

            string strFilePath = _strStartupPath + "\\" + strFileName;
            if (File.Exists(strFilePath))
            {
                strResult = File.ReadAllText(strFilePath,Encoding.Default).Replace("\r\n","|");
            }

            return strResult;
        }

        //保存文件内容
        public static bool WriteFileContent(string strFileName,string strContent)
        {
            bool bResult = false;

            string strFilePath = _strStartupPath + "\\" + strFileName;
            if (File.Exists(strFilePath))
            {
                File.WriteAllText(strFilePath, strContent.Replace("|", "\r\n"), Encoding.Default);
                bResult = true;
            }

            return bResult;
        }


        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <returns></returns>
        public static bool CreateFile(string strFileName)
        {
            try
            {
                FileStream fs = File.Create(_strStartupPath + "\\" + strFileName);
                fs.Close();
                fs.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public static bool DeleteFile(string strFileName)
        {
            try
            {
                File.Delete(_strStartupPath + "\\" + strFileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 是否存在此文件
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <returns></returns>
        public static bool FileExists(string strFileName)
        {
            return File.Exists(_strStartupPath + "\\" + strFileName);

        }

    }
}
