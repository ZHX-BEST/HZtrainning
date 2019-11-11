using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HangzhouPeiXun.DAL
{
    /// <summary>
    /// 测试DAL
    /// </summary>
    public class Test
    {
        private static Test mytest = new Test();
        public static Test MyTest { get { return mytest; } }
        public Test() { }

        #region 获取试卷信息
        public DataTable getTestList()
        {
            
            string sql = "SELECT Test_ID,Test_Date,Test_Time,Test_Fin FROM TB_Test order by Test_ID desc";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql,  CommandType.Text);
            return dt;
        }
        #endregion

        public DataTable gettest(string testID)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@testID", testID) };
            string sql = "SELECT * FROM TB_Test where Test_ID = @testID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql,paras, CommandType.Text);
            return dt;

        }

        #region 获取练习题ID
        public DataTable getproblem(string upperid)//根据所属教师获取课堂练习
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@upperid", upperid) };
            string sql = "select data.*,Que.Que_Contest,I.I_96Date,U.U_96Date,W.W_96Date from TB_Data data " +
               "inner join TB_Question Que on data.Data_UpperID =Que.Que_UpperID " +
               "inner join TB_I I on data.Data_AbID =I.I_DataID " +
               "inner join TB_U U on data.Data_AbID =U.U_DataID " +
               "inner join TB_W W on data.Data_AbID =W.W_DataID " +
               "where data.Data_UpperID = @upperID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion

        #region 提交答题卡
        public string postTestCard(string testID, string result, string time,  string userID)
        {
            string flag = "False";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@testID", testID),
                                                        new SqlParameter("@result", result),
                                                        new SqlParameter("@time", time),//上传到达时间                                                       
                                                        new SqlParameter("@userID", userID)};
            string sql = "INSERT INTO TB_DoTest(DoTest_TestID,DoTest_UserID,DoTest_Result,DoTest_Time) VALUES (@testID, @userID,@result,@time)";
            int res = new Helper.SQLHelper().ExecuteNonQuery(sql, para, CommandType.Text);
            if (res > 0)
                flag = "True";
            return flag;

        }
        #endregion

        #region 获取考试结果
        public DataTable getResult(string testID)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@testID", testID) };
            string sql = "SELECT TB_DoTest.*, TB_User.User_Name FROM TB_DoTest INNER JOIN TB_User ON TB_DoTest.DoTest_UserID=TB_User.User_ID where DoTest_TestID = @testID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion
    }
}