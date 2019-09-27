using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HangzhouPeiXun.Controllers
{
    public class ExamplesController : ApiController
    {
        public string getNormalData(string User_type, string option)
        {
            string res;
            DataTable dt = new DataTable();
            dt = new DAL.Examples().getNormalData(User_type, option);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        public string getAbnormalData(string User_type, string option)
        {
            string res;
            DataTable dt = new DataTable();
            dt = new DAL.Examples().getAbnormalData(User_type, option);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }
    }
}
