using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
        public string getTestItem(string dataupperID, string option)//注option必须为I，U，W
        {
            if (option != "I" && option != "U" && option != "W")
                return "FalseOption";//获取选项错误
            string res;
            DataTable dt = DAL.Test.MyTest.getTestItem(dataupperID, option);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //提交答题卡
        public string postTestCard(string testID, string result, string time, string point, string userID)
        {
            string res = DAL.Test.MyTest.postTestCard(testID, result, time, point, userID);
            return res;
        }
    }
}