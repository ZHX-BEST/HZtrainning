using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HangzhouPeiXun.DAL
{
    /// <summary>
    /// 测试教师端DAL
    /// </summary>
    public class TestTeacher
    {
        private static TestTeacher mytestteacher = new TestTeacher();
        public static TestTeacher MyTestTeacher { get { return mytestteacher; } }
        public TestTeacher() { }

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

        /// <summary>
        /// 异常存入数据库
        /// </summary>
        /// <param name="abID"></param>
        /// <param name="abI"></param>
        /// <param name="abU"></param>
        /// <param name="abW"></param>
        /// <returns></returns>
        public string postabdata(string abID, string abI, string abU, string abW)
        {
            string res = "false";
            string sql = "insert into TB_I (I_DataID,I_96Date) values(@id,@I); " +
                "insert into TB_U (I_DataID,I_96Date) values(@id,@U); " +
                "insert into TB_I (I_DataID,I_96Date) values(@id,@W); ";
            SqlParameter[] paras = { new SqlParameter("@id", abID), new SqlParameter("@I", abI), new SqlParameter("@U", abU), new SqlParameter("@W", abW) };
            int flag = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.Text);
            if (flag > 0)
                res = "true";
            return res;
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


        #region 获取练习题ID
        public DataTable getproblem(string upperid)//根据所属教师获取课堂练习
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@upperid", upperid) };
            string sql = "select data.*,I.I_96Date,U.U_96Date,W.W_96Date from TB_Data data " +
               "inner join TB_I I on nor.Nor_ID =I.I_DataID " +
               "inner join TB_U U on nor.Nor_ID =U.U_DataID " +
               "inner join TB_W W on nor.Nor_ID =W.W_DataID " +
               "where data.Data_UpperID = @upperID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion

        #region 出卷子
        public string postTest(string userID, string testdata, string Test_Date, string Test_Time, string Test_Start)
        {
            string res = "False";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@userID", userID), new SqlParameter("@testdata", testdata),
                                                        new SqlParameter("@testdate", Test_Date), new SqlParameter("@testtime", Test_Time),
                                                        new SqlParameter("@teststart", Test_Start)};
            string sql = "insert into TB_Test (Test_User , Test_Data, Test_Date, Test_Time, Test_Start) values (@userID, @testdata, @testdate, @testtime, @teststart)";
            int flag = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.Text);
            if (flag > 0)
                res = "True";
            return res;
        }
        #endregion

        #region 获取考试结果
        public DataTable getResult(string testID)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@testID", testID) };
            string sql = "SELECT TB_DoTest.*, TB_User.User_Name FROM TB_DoTest INNER JOIN TB_User ON TB_DoTest.DoTest_UserID=TB_User.User_ID where DoTest_ID = @testID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion

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