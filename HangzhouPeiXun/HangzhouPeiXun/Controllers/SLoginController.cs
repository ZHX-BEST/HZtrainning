using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

namespace HangzhouPeiXun.Controllers
{
    public class SLoginController : ApiController
    {
        public string getlogin(string id, string pwd)
        {
            string res = "False";
            DataTable dt = new DAL.SLogin().getlogin(id, pwd);
            res = new Helper.jstodt().ToJson(dt);
            if (!DAL.Videos.t.IsAlive)
            {
                DAL.Videos.t.Start();
            }
            return res;
        }

        public string getteacherlogin(string id, string pwd)
        {
            string res = "False";
            DataTable dt = new DAL.SLogin().getteacherlogin(id, pwd);
            res = new Helper.jstodt().ToJson(dt);
            if (!DAL.Videos.t.IsAlive)
            {
                DAL.Videos.t.Start();
            }
            return res;
        }
    }
}
