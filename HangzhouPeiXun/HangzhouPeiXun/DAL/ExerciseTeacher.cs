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

        #region 获取正常数据
        public DataTable getNormalData(string NorID, string option)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@NorID", NorID) };
            string sql = "select * from TB_" + option + " where " + option + "_DataID = @NorID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion

        #region 获取异常数据
        public DataTable getAbnormalData(string AbID, string option)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@AbID", AbID) };
            string sql = "select * from TB_" + option + " where " + option + "_DataID = @AbID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion
    }
}