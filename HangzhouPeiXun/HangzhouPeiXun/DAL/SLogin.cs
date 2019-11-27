using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HangzhouPeiXun.DAL
{
    /// <summary>
    /// 登录
    /// </summary>
    public class SLogin
    {
        private static SLogin mylogin = new SLogin();
        public static SLogin MyLogin { get { return mylogin; } }
        public SLogin() { }

        /// <summary>
        /// 学员登录
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public DataTable getlogin(string ID,string pwd)
        {
            //string sql = "select * from TB_User where (User_ID = @ID or User_Name =@ID ) and User_PWD = @pwd and User_Teacher !=0";
            string sql = "select * from TB_User where (User_Name =@ID ) and User_PWD = @pwd and User_Teacher !=0";
            SqlParameter[] paras = {new SqlParameter("@ID",ID),new SqlParameter("@pwd",pwd) }; 
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 教师登录
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public DataTable getteacherlogin(string ID, string pwd)
        {
            string sql = "select * from TB_User where (User_Name =@ID) and User_PWD = @pwd and User_Teacher = 0";
            SqlParameter[] paras = { new SqlParameter("@ID", ID), new SqlParameter("@pwd", pwd) };
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
    }
}