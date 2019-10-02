using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HangzhouPeiXun.DAL
{
    /// <summary>
    /// 学员课堂练习DAL
    /// </summary>
    public class Exercise
    {
        private static Exercise myexercise = new Exercise();
        public static Exercise MyExercise { get { return myexercise; } }
        public Exercise() { }

        #region 获取练习题
        public DataTable getExercise(string exeID, string option)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@exeID", exeID) };
            string sql = "SELECT TB_" + option + ".* FROM (TB_Exercise INNER JOIN TB_Data ON TB_Exercise.Exe_DataID=TB_Data.Data_UpperID) INNER JOIN TB_" + option + " ON TB_Data.Data_AbID=TB_" + option + "." + option + "_DataID WHERE Exe_ID = @exeID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion

        #region 提交答题卡
        public string postExerciseCard(string exeID, string time, string result, string userID)
        {
            string flag = "False";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@exeID", exeID),
                                                        new SqlParameter("@time", time),
                                                        new SqlParameter("@result", result),
                                                        new SqlParameter("@userID", userID)};
            string sql = "INSERT INTO TB_DoEXE(Do_ExeID, Do_Time, Do_Result, Do_UserID) VALUES (@exeID, @time, @result, @userID)";
            int res = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.Text);
            if (res > 0)
                flag = "True";
            return flag;
        }
        #endregion
    }
}