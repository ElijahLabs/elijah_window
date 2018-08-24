using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OpenMusic.DAL
{
    public class FileHelper
    {
        //根据关键字获得文件全名列表
        public static string GetFileFullNameListByKeyword(string strFolderPath, string strKeyword, string searchPattern, SearchOption searchOption)
        {
            string strResult = "";

            DirectoryInfo directoryInfo = new DirectoryInfo(strFolderPath);

            //按文件名称升序排列
            FileInfo[] fileInfos = directoryInfo.GetFiles(searchPattern, searchOption).OrderBy(p => p.Name).ToArray();

            string strContent = "";

            for (int i = 0; i < fileInfos.Length; i++)
            {
                StreamReader sr = new StreamReader(fileInfos[i].OpenRead(), Encoding.Default);

                strContent = sr.ReadToEnd();

                if (strContent.Contains(strKeyword))
                {
                    strResult += fileInfos[i].FullName + "|";
                }

                sr.Close();
                sr.Dispose();

                strContent = "";
            }

            Array.Clear(fileInfos, 0, fileInfos.Length);
            return strResult;

        }


        //获得目录列表
        public static string GetFolderNameList(string strFolderPath, string searchPattern, SearchOption searchOption)
        {
            string strResult = "";

            DirectoryInfo directoryInfo = new DirectoryInfo(strFolderPath);

            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories(searchPattern, searchOption).OrderBy(p => p.Name).ToArray();
            for (int i = 0; i < directoryInfos.Length; i++)
            {
                strResult += directoryInfos[i].Name + "|";
            }

            Array.Clear(directoryInfos, 0, directoryInfos.Length);

            return strResult;

        }

        //获得目录列表
        public static string GetFolderFullNameList(string strFolderPath, string searchPattern, SearchOption searchOption)
        {
            string strResult = "";

            DirectoryInfo directoryInfo = new DirectoryInfo(strFolderPath);

            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories(searchPattern, searchOption).OrderBy(p => p.Name).ToArray();
            for (int i = 0; i < directoryInfos.Length; i++)
            {
                strResult += directoryInfos[i].FullName + "|";
            }

            Array.Clear(directoryInfos, 0, directoryInfos.Length);

            return strResult;

        }


        //获得文件列表
        public static string GetFileNameList(string strFolderPath, string searchPattern, SearchOption searchOption)
        {
            string strResult = "";

            DirectoryInfo directoryInfo = new DirectoryInfo(strFolderPath);

            //按文件名称升序排列
            FileInfo[] fileInfos = directoryInfo.GetFiles(searchPattern, searchOption).OrderBy(p => p.Name).ToArray();

            for (int i = 0; i < fileInfos.Length; i++)
            {
                strResult += fileInfos[i].Name + "|";
            }

            Array.Clear(fileInfos, 0, fileInfos.Length);

            return strResult;
        }


        //获得文件列表
        public static string GetFileFullNameList(string strFolderPath, string searchPattern, SearchOption searchOption)
        {
            string strResult = "";

            DirectoryInfo directoryInfo = new DirectoryInfo(strFolderPath);

            //按文件名称升序排列
            FileInfo[] fileInfos = directoryInfo.GetFiles(searchPattern, searchOption).OrderBy(p => p.Name).ToArray();

            for (int i = 0; i < fileInfos.Length; i++)
            {
                strResult += fileInfos[i].FullName + "|";
            }

            Array.Clear(fileInfos, 0, fileInfos.Length);

            return strResult;
        }

        //获得文件内容
        public static string ReadFileContent(string strFilePath)
        {
            string strResult = "";

            if (File.Exists(strFilePath))
            {
                strResult = File.ReadAllText(strFilePath, Encoding.Default);
                //strResult = File.ReadAllText(strFilePath, Encoding.Default).Replace("\r\n", "|");
            }

            return strResult;
        }

        //保存文件内容
        public static bool WriteFileContent(string strFilePath, string strContent)
        {
            bool bResult = false;

            //if (File.Exists(strFilePath))
            //{
                File.WriteAllText(strFilePath, strContent.Replace("|", "\r\n"), Encoding.Default);
                bResult = true;
            //}

            return bResult;
        }


        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <returns></returns>
        public static bool CreateFile(string strFilePath)
        {
            try
            {
                FileStream fs = File.Create(strFilePath);
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
        public static bool DeleteFile(string strFilePath)
        {
            try
            {
                File.Delete(strFilePath);
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
        public static bool FileExists(string strFilePath)
        {
            return File.Exists(strFilePath);
        }

        public static bool CreateFolder(string strFolderPath)
        {
            bool bResult = false;
            try
            {
                Directory.CreateDirectory(strFolderPath);
                bResult = true;
            }
            catch (Exception ex)
            {
                bResult = false;
                throw ex;
            }

            return bResult;
        }

        /// <summary>
        /// 是否存在此目录
        /// </summary>
        /// <param name="strFolderPath"></param>
        /// <returns></returns>
        public static bool FolderExists(string strFolderPath)
        {
            return Directory.Exists(strFolderPath);
        }

        //删除目录及其下所有文件
        public static bool DeleteFolder(string strFolderPath)
        {
            bool bResult = false;
            try
            {
                Directory.Delete(strFolderPath, true);
                bResult = true;
            }
            catch (Exception ex)
            {
                bResult = false;
                throw ex;
            }

            return bResult;
        }

        //更改目录名称
        public static bool RenameFolder(string strSourceFolderPath, string strDestFolderPath)
        {
            Directory.Move(strSourceFolderPath, strDestFolderPath);
            return true;
        }


        public static void CopyFile(string strSourceFilePath, string strDestFilePath)
        {
            File.Copy(strSourceFilePath, strDestFilePath, true);
        }


        public static void RenameFile(string strSourceFilePath, string strDestFilePath)
        {
            File.Move(strSourceFilePath, strDestFilePath);
        }
    }
}
