using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

namespace HangzhouPeiXun.Controllers
{
    public class LoginController : ApiController
    {
        public string getlogin(string id, string pwd)
        {
            string res = "False";
            DataTable dt = new DAL.Login().getlogin(id, pwd);
            if (dt.Rows.Count != 0)
                res = "Ture";
            return res;
        }
    }
}
