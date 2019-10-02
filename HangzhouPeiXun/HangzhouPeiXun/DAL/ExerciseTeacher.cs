using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HangzhouPeiXun.DAL
{
    /// <summary>
    /// 教师课堂练习DAL
    /// </summary>
    public class ExerciseTeacher
    {
        private static  ExerciseTeacher myexerciseteacher = new ExerciseTeacher();
        public static ExerciseTeacher MyExerciseTeacher { get { return myexerciseteacher; } }
        public ExerciseTeacher() { }

        public string postExercise(string upperID, string userID)
        {
            string res = "False";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@upperID", upperID), new SqlParameter("@userID", userID) };
            string sql = "insert into TB_Exercise (Exe_DataID , Exe_UserID) values (@upperID,@userID)";
            int flag = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.Text);
            if (flag > 0)
                res = "True";
            return res;
        }

        public DataTable getresult(string exeID)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@exeID", exeID) };
            string sql = "select * from TB_DoEXE where Do_ExeID = @exeID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
    }
}