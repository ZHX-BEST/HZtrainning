using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HangzhouPeiXun.Controllers
{
    public class AbListController : ApiController
    {
        /// <summary>
        /// 获取异常列表
        /// </summary>
        /// <returns></returns>
        public string GetAbList()
        {
            string res = "false";
            DataTable dt = DAL.AbList.MyAbList.GetAbList();
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }
    }
}
