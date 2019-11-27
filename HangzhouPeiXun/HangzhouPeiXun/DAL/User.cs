using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HangzhouPeiXun.DAL
{
    public class User
    {
        private static User myUser = new User();
        public static User MyUser { get { return myUser; } }
        public User() { }

        public DataTable getUsers(string teacherID)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@teacherID", teacherID) };
            string sql = "SELECT * FROM TB_User where User_Teacher = @teacherID and User_Del = 0";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }

        public string postuser(string name, string pwd, string teacher)
        {
            string res = "False";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@name", name), new SqlParameter("@pwd", pwd), new SqlParameter("@teacher", teacher) };
            string sql = "insert into TB_User (User_Name,User_PWD,User_Teacher) values (@name ,@pwd , @teacher)";
            int flag = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.Text);
            if (flag > 0)
                res = "True";
            return res;
        }

        public string postchangeuser(string name,string pwd,string teacher,string id)
        {
            string res = "False";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@name", name), new SqlParameter("@pwd", pwd), new SqlParameter("@teacher", teacher), new SqlParameter("@UserID", id), };
            string sql = "update TB_User set User_Name = @name, User_PWD = @pwd, User_Teacher = @teacher  where User_ID = @UserID";
            int flag = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.Text);
            if (flag > 0)
                res = "True";
            return res;
        }

        public string postdeluser(string UserID)
        {
            string res = "False";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@UserID", UserID) };
            string sql = "update TB_User set User_Del = 1 where User_ID = @UserID";
            int flag = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.Text);
            if (flag > 0)
                res = "True";
            return res;
        }
    }

    
}