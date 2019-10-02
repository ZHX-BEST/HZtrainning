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
        public string postTest(string userID, string testdata)
        {
            string res = "False";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@userID", userID), new SqlParameter("@testdata", testdata) };
            string sql = "insert into TB_Test (Test_User , Test_Data) values (@userID, @testdata)";
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
            string sql = "select * from TB_DoTest where DoTest_ID = @testID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion
    }
}