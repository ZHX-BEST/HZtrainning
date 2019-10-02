using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

namespace HangzhouPeiXun.Controllers
{
    public class ExerciseTeacherController : ApiController
    {

        //出题
        public string postExercise(string upperID, string userID)
        {
            string res = DAL.ExerciseTeacher.MyExerciseTeacher.postExercise(upperID, userID);
            return res;
        }

        //获取课堂测试结果
        public string getresult(string exeID)
        {
            DataTable dt = DAL.ExerciseTeacher.MyExerciseTeacher.getresult(exeID);
            string res = new Helper.jstodt().ToJson(dt);
            return res;
        }
    }
}
