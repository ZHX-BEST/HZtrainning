using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HangzhouPeiXun.DAL
{
    public class Login
    {
        public DataTable getlogin(string ID,string pwd)
        {
            string sql = "select * from TB_User where User_ID = @ID and User_PWD = @pwd";
            SqlParameter[] paras = {new SqlParameter("@ID",ID),new SqlParameter("@pwd",pwd) }; 
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
    }
}