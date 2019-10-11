using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

namespace HangzhouPeiXun.Controllers
{
    public class ExerciseController : ApiController
    {

        //获取试题数据ID
        public string getExerciseID(string userID)
        {
            string res;
            DataTable dt = DAL.Exercise.MyExercise.getExerciseID(userID);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //获取试题数据
        public string getExercise(string exeID, string option)//注option必须为I，U，W
        {
            if (option != "I" && option != "U" && option != "W")
                return "FalseOption";//获取选项错误
            string res;
            DataTable dt = DAL.Exercise.MyExercise.getExercise(exeID, option);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }
        //做题
        public string postExerciseCard(string exeID, string time, string result, string userID)
        {
            string res = DAL.Exercise.MyExercise.postExerciseCard(exeID, time, result, userID);
            return res;
        }
    }
}
