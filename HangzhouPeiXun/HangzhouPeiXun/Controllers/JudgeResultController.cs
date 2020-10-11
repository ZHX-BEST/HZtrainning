using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using HangzhouPeiXun.DAL;
using HangzhouPeiXun.Helper;

namespace HangzhouPeiXun.Controllers
{
    public class JudgeResultController : ApiController
    {
        /// <summary>
        /// 将结果数组转为Json并返回 
        /// </summary>
        /// <param name="resLength"></param>
        /// <returns></returns>
        public string getJudgeResult(int resLength)
        {
            JudgeResult judgeResult = new JudgeResult();
            string[] resultStr = judgeResult.readRes(resLength);
            string result = new Helper.jstodt().rAtoJs(resultStr);
            return result;
        }
    }
}
