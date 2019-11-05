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

        //获取试题数据
        public string getExerciseID(string TeacherID)
        {
            string res;
            DataTable dt = DAL.Exercise.MyExercise.getExercise(TeacherID);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }
        
        //做题
        public string postExerciseCard(string exeID, string time, string result, string userID)
        {
            string res = DAL.Exercise.MyExercise.postExerciseCard(exeID, time, result, userID);
            return res;
        }

        //获取试题答案
        public string getExerciseres(string EXEID)//若为fin1公布结果
        {
            string res;
            DataTable dt = DAL.Exercise.MyExercise.getExerciseres(EXEID);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }
    }
}
