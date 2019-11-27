 using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace HangzhouPeiXun.Controllers
{   
    public class TestController : ApiController
    {        
        //获取试卷信息
        public string getTestlist()
        {
            string res;
            DataTable dt = DAL.Test.MyTest.getTestList();
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        /// <summary>
        /// 获取试卷详情
        /// </summary>
        /// <param name="testID"></param>
        /// <returns></returns>
        public string gettest(string testID, string time)
        {
            HttpContext.Current.Session["Time"] = time;
            string sessionid = HttpContext.Current.Session.SessionID;
            DataTable dt = DAL.Test.MyTest.gettest(testID);
            string res = new Helper.jstodt().ToJson(dt)+ "#ASP.NET_SessionId=" + sessionid;
            return res;
        }
        //获取试题
        public string getproblem(string upperID)
        {
            string res;
            DataTable dt = DAL.Test.MyTest.getproblem(upperID);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        
        public string postanswer(string time)
        {
            string answer = HttpContext.Current.Request["answer"].ToString();
            string timelast = HttpContext.Current.Session["Time"].ToString();            
            DateTime dtlast = Convert.ToDateTime(timelast);
            DateTime dtthis = Convert.ToDateTime(time);
            if (DateTime.Compare(dtthis, dtlast) > 0)
            {
                HttpContext.Current.Session["Time"] = time;//时间戳
                HttpContext.Current.Session["answer"] = answer;
            }
            return "true";
            //return timelast;
        }

        //提交答题卡
        public string postTestCard(string testID,  string time,  string userID)
        {
            string answer = HttpContext.Current.Request["answer"].ToString();
            string res = DAL.Test.MyTest.postTestCard(testID, answer, time,  userID);
            HttpContext.Current.Session["answer"] = "";
            HttpContext.Current.Session["minutes"] = "0";
            return res;
        }

        //获取考试结果
        public string getResult(string testID)
        {
            DataTable dt = DAL.TestTeacher.MyTestTeacher.getResult(testID);
            string res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        public string getcleartest()
        {
            HttpContext.Current.Session["minutes"] = "0";
            HttpContext.Current.Session["answer"] = "";
            return "True";
        }

        [HttpGet]
        public string getsetminutes(string minutes)
        {
            HttpContext.Current.Session["minutes"] = minutes;
            return minutes;
        }

        public string getminutes()
        {
            string minutes = "0";
            try
            {
                 minutes = HttpContext.Current.Session["minutes"].ToString();
            }
            catch (Exception)
            {

                throw;
            }
            
            return minutes;
        }

        public string getanswer()
        {
            string res = "NULL";
            try
            {
                res = HttpContext.Current.Session["answer"].ToString();
            }
            catch (Exception)
            {
;
            }
            return res;
   
        }
    }
}