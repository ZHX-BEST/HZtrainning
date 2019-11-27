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

        #region 获取练习题ID
        public DataTable getExercise(string ExeID)//根据所属教师获取课堂练习
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@ExeID", ExeID) };
            string sql = "SELECT exe.Exe_ID,I.I_96Date,U.U_96Date,W.W_96Date,exe.Exe_Fin,data.Data_AbTypeTime FROM TB_Exercise exe " + 
                            "inner join TB_Data data on data.Data_UpperID = exe.Exe_DataID "+
                            "inner join TB_I I on data.Data_AbID = I.I_DataID "+
                            "inner join TB_U U on data.Data_AbID = U.U_DataID "+
                            "inner join TB_W W on data.Data_AbID = W.W_DataID "+
                            "WHERE exe.Exe_ID = @ExeID ";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }

        public DataTable getExerciseRes(string exeID,string userID)//根据所属教师获取课堂练习
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@exeID", exeID), new SqlParameter("@userID", userID) };
            string sql = "Pro_GetExeRes";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.StoredProcedure);
            return dt;
        }
        #endregion


        #region 提交答题卡
        public string postExerciseCard(string exeID, string result, string userID,string time)
        {
            string flag = "False";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@exeID", exeID),                                                        
                                                        new SqlParameter("@result", result),
                                                        new SqlParameter("@userID", userID),
                                                        new SqlParameter("@time", time)};
            string sql = "UPDATE TB_DoEXE SET Do_Result = @result,Do_Time = @time WHERE Do_UserID= @userID AND Do_ExeID = @exeID ";
            int res = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.Text);
            if (res > 0)
                flag = "True";
            return flag;
        }
        #endregion

        #region 获取练习题答案
        public DataTable getExerciseres(string ExeID)//根据所属教师获取课堂练习
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@EXEID", ExeID) };
            string sql = "SELECT exe.Exe_ID,data.Data_AbTypeTime,I.I_96Date,U.U_96Date,W.W_96Date ,exe.Exe_Fin FROM TB_Exercise exe " +
                            "inner join TB_Data data on data.Data_UpperID = exe.Exe_DataID " +
                            "inner join TB_I I on data.Data_NorID = I.I_DataID " +
                            "inner join TB_U U on data.Data_NorID = U.U_DataID " +
                            "inner join TB_W W on data.Data_NorID = W.W_DataID " +
                            "WHERE Exe_ID = @EXEID AND exe.Exe_Fin =1  ";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion

        #region 获取练习题
        public DataTable getExercisetea(string TeacherID)//根据所属教师获取课堂练习
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@teacherID", TeacherID) };
            string sql = "SELECT * FROM TB_Exercise exe " +
                            "WHERE Exe_UserID = @teacherID ORDER BY Exe_ID DESC";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
       
        #endregion
    }
}