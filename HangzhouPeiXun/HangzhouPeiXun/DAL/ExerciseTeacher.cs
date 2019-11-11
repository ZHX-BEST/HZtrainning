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

        public DataTable getnorlist(string usertype)//获取列表
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@usertype", usertype) };
            string sql = "select nor.*from TB_NorList nor " +
                                "where Nor_UserType = @usertype order by Nor_ID desc";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }

        public DataTable getnordatabyid(string id)//获取列表
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@id", id) };
            string sql = "select nor.* ,I.I_96Date ,U.U_96Date ,W.W_96Date from TB_NorList nor " +
                "inner join TB_I I on nor.Nor_ID =I.I_DataID " +
                "inner join TB_U U on nor.Nor_ID =U.U_DataID " +
                "inner join TB_W W on nor.Nor_ID =W.W_DataID " +
                "where Nor_ID = @id";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }

        //选中
        public string getUpperID(string NorID, string User_type)
        {
            string res = "False";//异常失败返回False
            string sql = "InsertTB_DatawithNorID"; //插入一条TB_Data,生成UpperID，NorID，AbIDe，返回 UpperID
            string UpperID = "";
            try
            {
                SqlParameter[] paras = { new SqlParameter("@NorID", NorID), new SqlParameter("@UserType", User_type) };
                DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.StoredProcedure);//使用存储过程
                res = dt.Rows[0]["Data_UpperID"].ToString();
            }
            catch
            {
                return res;//报错返回False
            }
            if (UpperID == "")//若为获取UpperID返回False
                UpperID = res;
            return UpperID;
           
        }
        //新建
        public string getnewUpperID(string User_type)
        {
            string res = Server.DataSet.MyData.SetNorData(User_type);//调用数据仿真接口
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@upperID", res) };
            string sql = "select data.*,I.I_96Date,U.U_96Date,W.W_96Date from TB_Data data " +
                "inner join TB_I I on data.Data_NorID =I.I_DataID " +
                "inner join TB_U U on data.Data_NorID =U.U_DataID " +
                "inner join TB_W W on data.Data_NorID =W.W_DataID " +
                "where data.Data_UpperID = @upperID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        /// <summary>
        /// 异常存入数据库
        /// </summary>
        /// <param name="abID"></param>
        /// <param name="abI"></param>
        /// <param name="abU"></param>
        /// <param name="abW"></param>
        /// <returns></returns>
        public string postabdata(string upperID,string abI,string abU, string abW,string AbType)
        {
            string res = "false";
            string abID = upperID + "_1";
            string sql = "update TB_I set I_96Date = @I  where I_DataID = @abid; " +
                "update TB_U set U_96Date = @I  where U_DataID = @abid; " +
                "update TB_W set W_96Date = @I  where W_DataID = @abid; " +
                "update TB_Data set Data_AbTypeTime = @abtype where Data_UpperID = @id;";
            SqlParameter[] paras = { new SqlParameter("@id", upperID), new SqlParameter("@abid", abID), new SqlParameter("@I", abI), new SqlParameter("@U", abU), new SqlParameter("@W", abW), new SqlParameter("@abtype", AbType) };
            int flag = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.Text);
            if (flag > 0)
                res = "true";
            return res;
        }

        public DataTable getnordata(string upperID)//获取列表
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@upperID", upperID) };
            string sql = "select data.*,I.I_96Date,U.U_96Date,W.W_96Date from TB_Data data " +
                "inner join TB_I I on data.Data_NorID =I.I_DataID " +
                "inner join TB_U U on data.Data_NorID =U.U_DataID " +
                "inner join TB_W W on data.Data_NorID =W.W_DataID " +
                "where data.Data_UpperID = @upperID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }

        #region 获取异常数据
        public Tuple<DataTable, DataTable, DataTable> SetAbData(DataTable dtI, DataTable dtU, DataTable dtW, DataTable AbType, int abcount)
        {
            var ab = Server.DataSet.MyData.SetAbData(dtI, dtU, dtW, AbType, abcount);
            DataTable abdtI = ab.Item1;
            DataTable abdtU = ab.Item2;
            DataTable abdtW = ab.Item3;
            Tuple<DataTable, DataTable, DataTable> tup = new Tuple<DataTable, DataTable, DataTable>(abdtI, abdtU, abdtW);
            return tup;
        }
        #endregion

        public string postExercise(string upperID, string userID)
        {
            string res = "False";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@upperID", upperID), new SqlParameter("@userID", userID) };
            string sql = "insert into TB_Exercise (Exe_DataID , Exe_UserID) values (@upperID, @userID)";
            int flag = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.Text);
            if (flag > 0)
                res = "True";
            return res;
        }

        public DataTable getresult(string exeID)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@exeID", exeID) };
            string sql = "SELECT TB_DoEXE.*, TB_User.User_Name FROM TB_DoEXE INNER JOIN TB_User ON TB_DoEXE.Do_UserID=TB_User.User_ID where Do_ExeID = @exeID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }

        public string postfin(string exeID)
        {
            string res = "false";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@exeID", exeID) };
            string sql = "UPDATE TB_Exercise SET Exe_Fin = 1 where Exe_ID = @exeID";
            int flag = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.Text);
            if (flag>0)
            {
                res = "true";
            }
            return res;
        }
        #region 获取练习题ID
        public DataTable getExercise(string TeacherID)//根据所属教师获取课堂练习
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@teacherID", TeacherID) };
            string sql = "SELECT * FROM TB_Exercise exe " +
                            "WHERE Exe_UserID = @teacherID ORDER BY Exe_ID DESC";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }

        public DataTable getExerciseByID(string ExeID)//根据所属教师获取课堂练习
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@ExeID", ExeID) };
            string sql = "SELECT exe.*,data.Data_AbTypeTime,I.I_96Date,U.U_96Date,W.W_96Date FROM TB_Exercise exe " +
                            "inner join TB_Data data on data.Data_UpperID = exe.Exe_DataID " +
                            "inner join TB_I I on data.Data_AbID = I.I_DataID " +
                            "inner join TB_U U on data.Data_AbID = U.U_DataID " +
                            "inner join TB_W W on data.Data_AbID = W.W_DataID " +
                            "WHERE exe.Exe_ID = @ExeID ORDER BY Exe_ID DESC";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion

    }
}
