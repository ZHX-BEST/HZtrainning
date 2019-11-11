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

        /// <summary>
        /// 获取测试列表
        /// </summary>
        /// <returns>0</returns>
        public DataTable gettestlist()//Test_Data要传给详情页
        {
            string sql = "select Test_ID,Test_Date,Test_Time,Test_Fin,Test_Data from TB_Test order by Test_ID desc";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql,  CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 获取已仿真正常数据
        /// </summary>
        /// <param name="usertype"></param>
        /// <returns>0</returns>
        public DataTable getnorlist(string usertype)//获取列表
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@usertype", usertype) };
            string sql = "select nor.*from TB_NorList nor where Nor_UserType = @usertype order by Nor_ID desc";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 获取正常数据曲线
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable getnordatabyid(string id)
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

        //新建
        /// <summary>
        /// 新建新的模拟
        /// </summary>
        /// <param name="User_type"></param>
        /// <returns></returns>
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

        //选中
        /// <summary>
        /// 选中正常曲线进行仿真获取UpperID
        /// </summary>
        /// <param name="NorID"></param>
        /// <param name="User_type"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 根据UpperID获取正常数据
        /// </summary>
        /// <param name="upperID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 异常存入数据库
        /// </summary>
        /// <param name="abID"></param>
        /// <param name="abI"></param>
        /// <param name="abU"></param>
        /// <param name="abW"></param>
        /// <returns></returns>
        public string postabdata(string upperID, string abI, string abU, string abW, string AbType,string context)
        {
            string res = "false";
            string abID = upperID + "_1";
            string sql = "update TB_I set I_96Date = @I  where I_DataID = @abid; " +
                "update TB_U set U_96Date = @U  where U_DataID = @abid; " +
                "update TB_W set W_96Date = @W  where W_DataID = @abid; " +
                "update TB_Data set Data_AbTypeTime = @abtype where Data_UpperID = @id;" +
                "insert into TB_Question (Que_UpperID,Que_Contest) values (@id,@context)";
            SqlParameter[] paras = { new SqlParameter("@id", upperID), new SqlParameter("@abid", abID), new SqlParameter("@I", abI), new SqlParameter("@U", abU), new SqlParameter("@W", abW), new SqlParameter("@abtype", AbType), new SqlParameter("@context", context) };
            int flag = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.Text);
            if (flag > 0)
                res = "true";
            return res;
        }

        #region 获取异常数据
        /// <summary>
        /// 数据异常叠加
        /// </summary>
        /// <param name="dtI"></param>
        /// <param name="dtU"></param>
        /// <param name="dtW"></param>
        /// <param name="AbType"></param>
        /// <param name="abcount"></param>
        /// <returns></returns>
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

        #region 获取问题
        public DataTable getproblem(string upperid)//根据所属教师获取课堂练习
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@upperid", upperid) };
            string sql = "select data.*,Que.Que_Contest,I.I_96Date,U.U_96Date,W.W_96Date from TB_Data data " +
               "inner join TB_Question Que on data.Data_AbID =Que.Que_UpperID " +
               "inner join TB_I I on data.Data_AbID =I.I_DataID " +
               "inner join TB_U U on data.Data_AbID =U.U_DataID " +
               "inner join TB_W W on data.Data_AbID =W.W_DataID " +
               "where data.Data_UpperID = @upperID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion

        #region 出卷子
        /// <summary>
        /// 出卷子
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="testdata"></param>
        /// <param name="Test_Date"></param>
        /// <param name="Test_Time"></param>
        /// <param name="Test_Start"></param>
        /// <returns></returns>
        public string postTest(string userID, string testdata, string TestDate, string TestTime, string TestID)
        {
            string res = "False";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@userID", userID), new SqlParameter("@testdata", testdata),
                                                        new SqlParameter("@testdate", TestDate), new SqlParameter("@testtime", TestTime),
                                                        new SqlParameter("@testID", TestID)};
            string sql = "";
            if (TestID == "0")
            {
                sql = "insert into TB_Test (Test_User , Test_Data, Test_Date, Test_Time) values (@userID, @testdata, @testdate, @testtime)";
            }
            else
            {
                sql = "update TB_Test set Test_User = @userID ,Test_Data = @testdata,Test_Date=@testdate,Test_Time=@testtime where Test_ID = @testID";
            }
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
            string sql = "SELECT TB_DoTest.*, TB_User.User_Name FROM TB_DoTest INNER JOIN TB_User ON TB_DoTest.DoTest_UserID=TB_User.User_ID where DoTest_TestID = @testID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion

        public string getfin(string testID)
        {
            string res = "False";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@testID", testID) };
            string sql = "update TB_Test set Test_Fin = 1 where Test_ID = @testID";
            int flag = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.Text);
            if (flag > 0)
                res = "True";
            return res;
        }

    }
}