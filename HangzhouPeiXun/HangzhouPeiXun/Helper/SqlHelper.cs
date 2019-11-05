
/*
 * 创建人：姜铁民
 * 说明：数据库助手类
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web;

namespace HangzhouPeiXun.Helper
{
    public class SQLHelper
    {
        private SqlConnection conn = null;
        private SqlCommand cmd = null;        

        public SQLHelper()
        {//建立SQL链接          

            string connstr = ConfigurationManager.ConnectionStrings["connstrs"].ConnectionString;
            conn = new SqlConnection(connstr);
        }

        private SqlConnection getconn()
        {//打开链接            
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;

        }

        /// <summary>
        /// 执行不带参数的增删改SQL语句或存储过程
        /// </summary>
        /// <param name="cmdtext">增删改SQL语句或存储过程</param>
        /// <param name="ct">命令类型（SQL语句或存储过程）</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string cmdtext, CommandType ct)
        {//该方法执行传入的SQL语句
            int res = 0;
            try
            {
                using (cmd = new SqlCommand(cmdtext, getconn()))
                {
                    cmd.CommandType = ct;
                    res = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return res;
        }

        /// <summary>
        ///  执行带参数的增删改SQL语句或存储过程
        /// </summary>
        /// <param name="cmdtext">增删改SQL语句或存储过程</param>
        /// <param name="paras">参数集合</param>
        /// <param name="ct">命令类型（SQL语句或存储过程）</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string cmdtext, SqlParameter[] paras, CommandType ct)
        {//该方法执行传入的SQL语句
            int res = 0;
            try
            {
                using (cmd = new SqlCommand(cmdtext, getconn()))
                {
                    cmd.CommandType = ct;
                    cmd.Parameters.AddRange(paras);
                    res = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return res;
        }

        /// <summary>
        /// 执行带参数查询SQL语句或存储过程
        /// </summary>
        /// <param name="cmdtext">查询SQL语句或存储过程</param>
        /// <param name="paras">参数集合</param>
        /// <param name="ct">命令类型（SQL语句或存储过程）</param>
        /// <returns></returns>
        public DataTable ExcuteQuery(string cmdtext, SqlParameter[] paras, CommandType ct)
        {//执行查询
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand(cmdtext, getconn());
                cmd.CommandType = ct;
                cmd.Parameters.AddRange(paras);
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dt;
        }

        /// <summary>
        /// 执行不带参数查询SQL语句或存储过程
        /// </summary>
        /// <param name="cmdtext">查询SQL语句或存储过程</param>
        /// <param name="ct">命令类型（SQL语句或存储过程）</param>
        /// <returns></returns>
        public DataTable ExcuteQuery(string cmdtext, CommandType ct)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand(cmdtext, getconn());
                cmd.CommandType = ct;
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dt;
        }
    }
}




