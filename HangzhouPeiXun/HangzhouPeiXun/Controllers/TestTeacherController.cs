using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

namespace HangzhouPeiXun.Controllers
{
    public class TestTeacherController : ApiController
    {
        //出卷子
        public string postTest(string userID, string testdata)
        {
            string res = DAL.TestTeacher.MyTestTeacher.postTest(userID, testdata);
            return res;
        }

        //获取考试结果
        public string getResult(string testID)
        {
            DataTable dt = DAL.TestTeacher.MyTestTeacher.getResult(testID);
            string res = new Helper.jstodt().ToJson(dt);
            return res;
        }
    }
}
