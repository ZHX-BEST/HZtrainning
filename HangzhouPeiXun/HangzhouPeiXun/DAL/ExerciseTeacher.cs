﻿using System;
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
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("usertype", usertype) };
            string sql = "select nor.*,I.I_96Date,U.U_96Date,W.W_96Date from TB_NorList nor " +
                "inner join TB_I I on nor.Nor_ID =I.I_DataID " +
                "inner join TB_U U on nor.Nor_ID =U.U_DataID " +
                "inner join TB_W W on nor.Nor_ID =W.W_DataID " +
                "where Nor_UserType = @usertype";
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
            return res;
        }

        public DataTable getnordata(string upperID)//获取列表
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@upperID", upperID) };
            string sql = "select data.*,I.I_96Date,U.U_96Date,W.W_96Date from TB_Data data " +
                "inner join TB_I I on nor.Nor_ID =I.I_DataID " +
                "inner join TB_U U on nor.Nor_ID =U.U_DataID " +
                "inner join TB_W W on nor.Nor_ID =W.W_DataID " +
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

       
    }
}
