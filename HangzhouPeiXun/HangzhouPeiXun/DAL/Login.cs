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
    public class Login
    {
        private Login mylogin = new Login();
        public Login MyLogin { get { return mylogin; } }
        public Login() { }

        /// <summary>
        /// 学员登录
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public DataTable getlogin(string ID,string pwd)
        {
            string sql = "select * from TB_User where User_ID = @ID and User_PWD = @pwd and User_Teacher =0";
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
            string sql = "select * from TB_User where User_ID = @ID and User_PWD = @pwd and User_Teacher =1";
            SqlParameter[] paras = { new SqlParameter("@ID", ID), new SqlParameter("@pwd", pwd) };
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
    }
}