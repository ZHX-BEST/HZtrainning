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