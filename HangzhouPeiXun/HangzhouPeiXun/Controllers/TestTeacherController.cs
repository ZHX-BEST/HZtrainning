using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Web;

namespace HangzhouPeiXun.Controllers
{
    public class TestTeacherController : ApiController
    {
        /// <summary>
        /// 获取测试列表
        /// </summary>
        /// <returns></returns>
        public string gettestlist()//Test_Data要传给详情页
        {
            DataTable dt = DAL.TestTeacher.MyTestTeacher.gettestlist();
            string res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        #region 出题
        //获取已生成正常数据曲线列表
        public string getNormalDatalist(string User_type)//User_TYpe 为1-10
        {
            if (User_type != "01JCC" && User_type != "02CLC" &&
                User_type != "03ZZC" && User_type != "04HGC" &&
                User_type != "05ZGC" && User_type != "06Hospital" &&
                User_type != "07ZJDX" && User_type != "08FZC" &&
                User_type != "09CKC" && User_type != "10SC")
                return "FalseUserType";//获取用户类别错误
            string res = "error";//默认报错
            DataTable dt = DAL.TestTeacher.MyTestTeacher.getnorlist(User_type);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //获取已生成正常数据曲线数据
        public string getNormalData(string Norid)//User_TYpe 为1-10
        {
            string res = "error";//默认报错
            DataTable dt = DAL.TestTeacher.MyTestTeacher.getnordatabyid(Norid);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //新建
        public string getnewUpperID(string usertype)
        {
            string res = DAL.TestTeacher.MyTestTeacher.getnewUpperID(usertype);
            return res;
        }

        //选中
        public string getUpperID(string NorID, string usertype)
        {
            string res = DAL.TestTeacher.MyTestTeacher.getUpperID(NorID, usertype);
            return res;
        }



        //获取异常数据曲线接口
        [HttpPost]
        public string postAbnormalData(string upperID, string AbType,string context )//abType为json 异常类型，异常开始时间，异常结束时间  story 0为不存，其他为存，传现有的考卷题号Test_Date
        {
            string res = "error";//默认报错     
            string story = HttpContext.Current.Request["story"].ToString();
            DataTable data = DAL.TestTeacher.MyTestTeacher.getnordata(upperID);
            DataTable abtable = new Helper.jstodt().ToDataTable(AbType);//异常表
            int abcount = abtable.Rows.Count;
            int count = data.Rows.Count;
            DataTable dtI = new Helper.jstodt().ToDataTable(data.Rows[0]["I_96Date"].ToString());
            DataTable dtU = new Helper.jstodt().ToDataTable(data.Rows[0]["U_96Date"].ToString());
            DataTable dtW = new Helper.jstodt().ToDataTable(data.Rows[0]["W_96Date"].ToString());
            if (count != 0 && abcount != 0)
            {
                var ab = DAL.TestTeacher.MyTestTeacher.SetAbData(dtI, dtU, dtW, abtable, abcount);//异常叠加 
                dtI = ab.Item1;
                dtU = ab.Item2;
                dtW = ab.Item3;
            }
            string abNordataI = new Helper.jstodt().ToJson(dtI);//数据打成json返回
            string abNordataU = new Helper.jstodt().ToJson(dtU);//数据打成json返回
            string abNordataW = new Helper.jstodt().ToJson(dtW);//数据打成json返回
            if (story != "0")
            {
                DAL.TestTeacher.MyTestTeacher.postabdata(upperID, abNordataI, abNordataU, abNordataW, AbType,context);//存入数据库
                HttpContext.Current.Session["TestCash"] = story;//session缓存已出题目
            }
            #region 处理data数据
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", Type.GetType("System.String"));
            dt.Columns.Add("abDataI", Type.GetType("System.String"));
            dt.Columns.Add("abDataU", Type.GetType("System.String"));
            dt.Columns.Add("abDataW", Type.GetType("System.String"));
            dt.Rows.Add();
            dt.Rows[0]["ID"] = upperID;
            dt.Rows[0]["abDataI"] = abNordataI;
            dt.Rows[0]["abDataU"] = abNordataU;
            dt.Rows[0]["abDataW"] = abNordataW;
            #endregion
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        #endregion

        //获取试题数据
        public string getproblem(string upperid)
        {
            string res;
            DataTable dt = DAL.TestTeacher.MyTestTeacher.getproblem(upperid);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //出卷子
        public string postTest(string userID,  string TestDate, string TestTime, string TestID)//出题人ID，考试开始时间，考试时长，标记位，新建传0其他传testID
        {
            string testdata = HttpContext.Current.Request["testdata"].ToString();
            string res = DAL.TestTeacher.MyTestTeacher.postTest(userID, testdata, TestDate, TestTime, TestID);
            HttpContext.Current.Session["TestCash"] = "";//session缓存已出题目
            return res;
        }

        //获取考试结果
        public string getResult(string testID)
        {
            DataTable dt = DAL.Test.MyTest.getResult(testID);
            string res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        public string getfin(string testID)
        {
            string res = DAL.TestTeacher.MyTestTeacher.getfin(testID);
            return res;
        }
               
    }
}
