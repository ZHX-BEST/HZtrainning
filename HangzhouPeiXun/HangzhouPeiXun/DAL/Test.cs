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

        #region 提交答题卡
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