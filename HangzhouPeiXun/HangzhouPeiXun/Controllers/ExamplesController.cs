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
        //获取正常数据曲线接口
        public string getNormalData(string User_type, string option)//注option必须为I，U，W
        {
            if(option!="I"&&option!="U"&&option!="W")
                return "FalseOption";//获取选项错误
            string res;
            string UpperID = Server.DataSet.MyData.SetNorData(User_type);//获取UpperID，生成数据
            string NorID = UpperID+"_0";
            DataTable dt = DAL.Examples.MyExamples.getNormalData(NorID, option);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //获取异常数据曲线接口
        public string getAbnormalData(string UpperID,string AbType, string AbTime,string option)
        {
            if(option!="I"&&option!="U"&&option!="W")
                return "FalseOption";//获取选项错误
            string res;
            string flag = Server.DataSet.MyData.SetAbData(UpperID,AbTime,AbType);//设置异常
            string AbID = UpperID+"_1";
            DataTable dt = DAL.Examples.MyExamples.getAbnormalData(AbID, option);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }
    }
}
