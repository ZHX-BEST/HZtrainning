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
        public DataTable getTestInfo(string teacherID)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@teacherID", teacherID) };
            string sql = "SELECT Test_ID,Test_Date,Test_Time FROM TB_Test WHERE Test_User=@teacherID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, para, CommandType.Text);
            return dt;
        }
        #endregion

        #region 开始作答生成答题卡
        public string postTestCard(string testID, string userID)
        {
            string flag = "False";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@testID", testID),                                                        
                                                        new SqlParameter("@userID", userID)};
            string sql = "INSERT INTO TB_DoTest(DoTest_TestIDDoTest_UserID) VALUES (@testID, @userID)";
            int res = new Helper.SQLHelper().ExecuteNonQuery(sql, para, CommandType.Text);
            if (res > 0)
                flag = "True";
            return flag;

        }
        #endregion

        #region 获取试题
        public DataTable getTestItem(string dataupperID, string option)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@dataupperID", dataupperID) };
            string sql = "SELECT * FROM TB_Data INNER JOIN TB_" + option + " ON TB_Data.Data_AbID=TB_" + option + "." + option + "_DataID WHERE Data_UpperID = @dataupperID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, para, CommandType.Text);
            return dt;
        }
        #endregion

        #region 题目作答
        public string postTestCard(string testID, string result, string time,  string userID)
        {
            string flag = "False";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@testID", testID),
                                                        new SqlParameter("@result", result),
                                                        new SqlParameter("@time", time),//上传到达时间                                                       
                                                        new SqlParameter("@userID", userID)};
            string sql = "INSERT INTO TB_DoTest(DoTest_TestID,DoTest_UserID,DoTest_Result,DoTest_Time) VALUES (@testID, @userID,@result,@time)";
            int res = new Helper.SQLHelper().ExecuteNonQuery(sql, para, CommandType.StoredProcedure);
            if (res > 0)
                flag = "True";
            return flag;

        }
        #endregion
    }
}