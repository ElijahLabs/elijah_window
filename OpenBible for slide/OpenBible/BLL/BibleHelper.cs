using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using OpenSoul.DAL;
using System.Data;

namespace OpenBible.BLL
{
    public class BibleHelper
    {

        /// <summary>
        /// 获取完整卷名的列表
        /// </summary>
        /// <returns>卷名列表</returns>
        public static string[] GetFullNameList()
        {
            int i = 0;
            string[] arrayResult = new string[66];

            IDataReader reader = DBHelper.ExecuteReader("select fullname from bibleid order by sn");
            while (reader.Read())
            {
                arrayResult[i] = reader[0].ToString();
                i++;
            }

            reader.Close();
            reader.Dispose();

            return arrayResult;
        }

        /// <summary>
        /// 获取BibleId表的所有信息
        /// </summary>
        /// <returns>BibleId表的所有信息的DataSet</returns>
        public static DataTable GetBibleIdList()
        {
            DataTable dataTable = DBHelper.GetDataSet("Select * from bibleid order by sn");
            return dataTable;
        }

        /// <summary>
        /// 由卷ID得到章总数
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public static int GetChapterCountByVolumeSN(int sn)
        {
            int i = -1;

            IDataReader reader = DBHelper.ExecuteReader("select ChapterNumber from bibleid where sn = " + sn.ToString());
            if (reader.Read())
            {
                i = int.Parse(reader[0].ToString());
            }

            reader.Close();
            reader.Dispose();
            return i;
        }


        /// <summary>
        /// 由卷ID和章ID得到节总数
        /// </summary>
        /// <param name="VolumeSN"></param>
        /// <param name="ChapterSN"></param>
        /// <returns></returns>
        public static int GetVerseCountByVolumeSNAndChapterSN(int VolumeSN, int ChapterSN)
        {
            int i = -1;

            IDataReader reader = DBHelper.ExecuteReader("select [VerseSN] from bible where VolumeSN = " + VolumeSN.ToString() + " and ChapterSN = " + ChapterSN.ToString() + " order by VerseSN desc");
            if (reader.Read())
            {
                i = int.Parse(reader[0].ToString());
            }

            reader.Close();
            reader.Dispose();
            return i;

        }


        ///// <summary>
        ///// 由卷ID和章ID得到经文
        ///// </summary>
        ///// <param name="VolumeSN"></param>
        ///// <param name="ChapterSN"></param>
        ///// <returns></returns>
        //public static StringBuilder GetLectionByVolumeSNandChapterSN(int VolumeSN, int ChapterSN, int VerseSN)
        //{
        //    StringBuilder sbLection = new StringBuilder();

        //    IDataReader reader = DBHelper.ExecuteReader("select Lection from bible where VolumeSN = " + VolumeSN.ToString() + " and ChapterSN = " + ChapterSN.ToString() + " and VerseSN = " + VerseSN.ToString() + " order by VerseSN");
        //    while (reader.Read())
        //    {
        //        sbLection.Append(ChapterSN.ToString());
        //        sbLection.Append(":");
        //        sbLection.Append(VerseSN.ToString());
        //        sbLection.Append(" ");
        //        sbLection.Append(reader["Lection"].ToString());
        //        sbLection.Append("\n");
        //    }

        //    reader.Close();
        //    reader.Dispose();
        //    return sbLection;
        //}



        /// <summary>
        /// 由卷ID和章ID得到经文
        /// </summary>
        /// <param name="VolumeSN"></param>
        /// <param name="ChapterSN"></param>
        /// <returns></returns>
        public static string GetLectionByVolumeSNandChapterSN(int VolumeSN, int ChapterSN, int VerseSN)
        {
            string strResult = "";
            StringBuilder sbLection = new StringBuilder();

            IDataReader reader = DBHelper.ExecuteReader("select Lection from bible where VolumeSN = " + VolumeSN.ToString() + " and ChapterSN = " + ChapterSN.ToString() + " and VerseSN = " + VerseSN.ToString() + " order by VerseSN");
            while (reader.Read())
            {
                //sbLection.Append(ChapterSN.ToString());
                //sbLection.Append(":");
                //sbLection.Append(VerseSN.ToString());
                sbLection.Append(" ");
                sbLection.Append(reader["Lection"].ToString());
                //sbLection.Append("\n");
                //sbLection.Append("");
            }

            reader.Close();
            reader.Dispose();

            strResult = sbLection.ToString();
            sbLection.Remove(0, sbLection.Length);
            sbLection = null;

            //strResult = strResult.Remove(0, 3);
            return strResult;
        }


        /// <summary>
        /// 由卷ID，章ID，节起始ID，节结束ID得到经文
        /// </summary>
        /// <param name="VolumeSN"></param>
        /// <param name="ChapterSN"></param>
        /// <returns></returns>
        public static string GetLectionByVolumeSNandChapterSN(int VolumeSN, int ChapterSN, int VerseSNBegin, int VerseSNEnd)
        {
            string strResult = "";

            StringBuilder sbLection = new StringBuilder();
            IDataReader reader = null;

            for (int i = VerseSNBegin; i <= VerseSNEnd; i++)
            {
                reader = DBHelper.ExecuteReader("select Lection,VerseSN from bible where VolumeSN = " + VolumeSN.ToString() + " and ChapterSN = " + ChapterSN.ToString() + " and VerseSN = " + i.ToString() + " order by VerseSN");
                while (reader.Read())
                {
                    //sbLection.Append(ChapterSN.ToString());
                    //sbLection.Append(":");
                    //sbLection.Append(i.ToString());
                    sbLection.Append(" ");
                    sbLection.Append(reader["Lection"].ToString());
                    //sbLection.Append("\r\n");
                    //sbLection.Append(" ");
                }
                reader.Close();
            }

            reader.Dispose();

            strResult = sbLection.ToString();
            sbLection.Remove(0, sbLection.Length);

            return strResult;
        }

        /// <summary>
        /// 由卷名称缩写得到卷ID
        /// </summary>
        /// <param name="strShortName">卷名缩写</param>
        /// <returns></returns>
        public static int GetVolumeSNbyShortName(string strShortName)
        {
            int iResult = -1;

            IDataReader reader = null;

            reader = DBHelper.ExecuteReader("select SN from bibleid where ShortName = '" + strShortName +"'");
            if (reader.Read())
            {
                iResult = int.Parse(reader["SN"].ToString());
            }

            reader.Close();
            reader.Dispose();

            return iResult;
        }


        /// <summary>
        /// 卷名全称得到卷ID
        /// </summary>
        /// <param name="strFullName">卷名全称</param>
        /// <returns></returns>
        public static int GetVolumeSNbyFullName(string strFullName)
        {
            int iResult = -1;

            IDataReader reader = null;

            reader = DBHelper.ExecuteReader("select SN from bibleid where FullName = '" + strFullName + "'");
            if (reader.Read())
            {
                iResult = int.Parse(reader["SN"].ToString());
            }

            reader.Close();
            reader.Dispose();

            return iResult;
        }


        ///// <summary>
        ///// 由卷拼音得到卷ID
        ///// </summary>
        ///// <param name="strPinYin">拼音</param>
        ///// <returns></returns>
        //public static int GetVolumeSNbyPinYin(string strPinYin)
        //{
        //    int iResult = -1;

        //    IDataReader reader = null;

        //    reader = DBHelper.ExecuteReader("select SN from bibleid where PinYin like '" + strPinYin + "%' ");
        //    while (reader.Read())
        //    {
        //        iResult = int.Parse(reader["SN"].ToString());
        //    }

        //    reader.Close();
        //    reader.Dispose();

        //    return iResult;

        //}


        /// <summary>
        /// 由新旧约得到卷全名列表
        /// </summary>
        /// <param name="bNewOrOld"></param>
        /// <returns></returns>
        public static string GetFullNameListByNewOrOld(bool bNewOrOld)
        {
            string strResult = "";

            IDataReader reader = null;
            reader = DBHelper.ExecuteReader("select '' + FullName,FullName from bibleid where NewOrOld = " + bNewOrOld + " order by SN ");
            while (reader.Read())
            {
                strResult += reader["FullName"].ToString();
                strResult += "|";
            }

            if (strResult.EndsWith("|"))
            {
                strResult = strResult.Remove(strResult.Length - 1);
            }

            reader.Close();
            reader.Dispose();

            return strResult;

        }


        /// <summary>
        /// 由卷拼音得到卷全名
        /// </summary>
        /// <param name="strPinYin"></param>
        /// <returns></returns>
        public static string GetFullNameListbyVolumePinYin(string strPinYin)
        {
            string strResult = "";

            IDataReader reader = null;

            reader = DBHelper.ExecuteReader("select FullName from bibleid where PinYin like '" + strPinYin + "%' order by SN ");
            while (reader.Read())
            {
                strResult += reader["FullName"].ToString();
                strResult += "|";
            }

            if (strResult.EndsWith("|"))
            {
                strResult = strResult.Remove(strResult.Length - 1);
            }

            reader.Close();
            reader.Dispose();

            return strResult;

        }

        public static string GetFullNameListAndPinYinbyVolumePinYin(string strPinYin)
        {
            string strResult = "";

            IDataReader reader = null;

            reader = DBHelper.ExecuteReader("select PinYin,FullName from bibleid where PinYin like '" + strPinYin + "%' order by SN ");
            while (reader.Read())
            {
                strResult += reader["PinYin"].ToString();
                strResult += " ";
                strResult += reader["FullName"].ToString();
                strResult += "|";
            }

            if (strResult.EndsWith("|"))
            {
                strResult = strResult.Remove(strResult.Length - 1);
            }

            reader.Close();
            reader.Dispose();

            return strResult;

        }


        //public static StringBuilder GetLectionByShortNameandChapterSN(string strShortName, int ChapterSN, int VerseSNBegin, int VerseSNEnd)
        //{
        //    return GetLectionByVolumeSNandChapterSN(GetVolumeSNbyShortName(strShortName), ChapterSN, VerseSNBegin, VerseSNEnd);
        //}

        public static string GetLectionByShortNameandChapterSN(string strShortName, int ChapterSN, int VerseSNBegin, int VerseSNEnd)
        {
            return GetLectionByVolumeSNandChapterSN(GetVolumeSNbyShortName(strShortName), ChapterSN, VerseSNBegin, VerseSNEnd);
        }

        public static string GetLectionByFullNameandChapterSN(string strFullName, int ChapterSN, int VerseSNBegin, int VerseSNEnd)
        {
            return GetLectionByVolumeSNandChapterSN(GetVolumeSNbyFullName(strFullName), ChapterSN, VerseSNBegin, VerseSNEnd);
        }

    }
}
