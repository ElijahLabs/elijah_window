using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Windows.Forms;

namespace Elijah_Window
{
    /// <summary>
    /// 公有DBHelper类
    /// </summary>
    public class DBHelper
    {
        private static OleDbConnection conn = null;
        private static string str = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Database\database.mdb;Persist Security Info=True";

        public static OleDbConnection GetDbConnection
        {
            get
            {
                if (conn == null)
                {
                    conn = new OleDbConnection(str);
                }
                switch (conn.State)
                {
                    case ConnectionState.Broken:
                        conn.Close();
                        break;

                    case ConnectionState.Open:
                        conn.Close();
                        break;
                }
                conn.Open();
                return DBHelper.conn;
            }
        }
        /// <summary>
        /// 执行OleDbCommand(增删改)
        /// </summary>
        /// <param name="safeSql">sql语句</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteCommand(string safeSql)
        {
            OleDbCommand cmd = new OleDbCommand(safeSql, GetDbConnection);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        /// <summary>
        /// 执行OleDbCommand(增删改)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="value">sql语句单个参数(对应表中字段)</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteCommand(string sql, OleDbParameter value)
        {
            OleDbCommand cmd = new OleDbCommand(sql, GetDbConnection);
            cmd.Parameters.Add(value);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        /// <summary>
        /// 执行OleDbCommand(增删改)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="values">sql语句多个参数(对应表中字段)</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteCommand(string sql, OleDbParameter[] values)
        {
            int result = 0;
            try
            {
                OleDbCommand cmd = new OleDbCommand(sql, GetDbConnection);
                cmd.Parameters.AddRange(values);
                result = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

                result = 0;
            }
            return result;
        }
        /// <summary>
        /// 执行OleDbCommand(Count(*)查询)
        /// </summary>
        /// <param name="safeSql">sql语句</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteScalar(string safeSql)
        {
            OleDbCommand cmd = new OleDbCommand(safeSql, GetDbConnection);
            int result = (int)cmd.ExecuteScalar();
            return result;
        }
        /// <summary>
        /// 执行OleDbCommand(Count(*)查询)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="value">sql语句单个参数(对应表中字段)</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteScalar(string sql, OleDbParameter value)
        {
            OleDbCommand cmd = new OleDbCommand(sql, GetDbConnection);
            cmd.Parameters.Add(value);
            int result = (int)cmd.ExecuteScalar();
            return result;
        }
        /// <summary>
        /// 执行OleDbCommand(Count(*)查询)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="values">sql语句多个参数(对应表中字段)</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteScalar(string sql, OleDbParameter[] values)
        {
            OleDbCommand cmd = new OleDbCommand(sql, GetDbConnection);
            cmd.Parameters.AddRange(values);
            int result = (int)cmd.ExecuteScalar();
            return result;
        }
        /// <summary>
        /// 执行结果集(非外键表)
        /// </summary>
        /// <param name="safeSql">sql语句</param>
        /// <returns>集合</returns>
        public static OleDbDataReader ExecuteReader(string safeSql)
        {
            OleDbCommand cmd = new OleDbCommand(safeSql, GetDbConnection);
            OleDbDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        /// <summary>
        /// 执行结果集(非外键表)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="value">sql语句单个参数(对应表中字段)</param>
        /// <returns>集合</returns>
        public static OleDbDataReader ExecuteReader(string sql, OleDbParameter value)
        {
            OleDbCommand cmd = new OleDbCommand(sql, GetDbConnection);
            cmd.Parameters.Add(value);
            OleDbDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        /// <summary>
        /// 执行结果集(非外键表)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="values">sql语句多个参数(对应表中字段)</param>
        /// <returns></returns>
        public static OleDbDataReader ExecuteReader(string sql, params OleDbParameter[] values)
        {
            OleDbCommand cmd = new OleDbCommand(sql, GetDbConnection);
            cmd.Parameters.AddRange(values);
            OleDbDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        /// <summary>
        /// 返回一张表
        /// </summary>
        /// <param name="safeSql">sql语句</param>
        /// <returns>内存中数据的一个表</returns>
        public static DataTable GetDataSet(string safeSql)
        {
            DataSet ds = new DataSet();
            OleDbCommand cmd = new OleDbCommand(safeSql, GetDbConnection);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
        /// <summary>
        /// 返回一张表
        /// </summary>
        /// <param name="safeSql">sql语句</param>
        /// <param name="value">sql语句单个参数(对应表中字段)</param>
        /// <returns>内存中数据的一个表</returns>
        public static DataTable GetDataSet(string safeSql, OleDbParameter value)
        {
            DataSet ds = new DataSet();
            OleDbCommand cmd = new OleDbCommand(safeSql, GetDbConnection);
            cmd.Parameters.Add(value);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
        /// <summary>
        /// 返回一张表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="values">sql语句多个参数(对应表中字段)</param>
        /// <returns>内存中数据的一个表</returns>
        public static DataTable GetDataSet(string sql, params OleDbParameter[] values)
        {
            DataSet ds = new DataSet();
            OleDbCommand cmd = new OleDbCommand(sql, GetDbConnection);
            cmd.Parameters.AddRange(values);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
}

