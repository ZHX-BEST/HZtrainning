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
        public string getTestInfo(string userID)
        {
            string res;
            DataTable dt = DAL.Test.MyTest.getTestInfo(userID);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //获取试题
        public string getproblem(string dataupperID)
        {
            string res;
            DataTable dt = DAL.Test.MyTest.getproblem(dataupperID);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        public string postanswer(string time, string answer)
        {
            string timelast = HttpContext.Current.Session["Time"].ToString();
            DateTime dtlast = Convert.ToDateTime(timelast);
            DateTime dtthis = Convert.ToDateTime(time); 
            if (DateTime.Compare(dtthis, dtlast) > 0)
            {
                HttpContext.Current.Session["Time"] = time;//时间戳
                HttpContext.Current.Session["answer"] = answer;
            }
            return "true";
        }

        //提交答题卡
        public string postTestCard(string testID, string result, string time,  string userID)
        {
            string res = DAL.Test.MyTest.postTestCard(testID, result, time,  userID);
            return res;
        }
    }
}