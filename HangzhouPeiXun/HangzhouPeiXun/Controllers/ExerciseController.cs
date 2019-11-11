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
        //获取试题数据列表
        public string getExerciseList(string TeacherID)
        {
            string res;
            DataTable dt = DAL.Exercise.MyExercise.getExercisetea(TeacherID);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //获取试题答案
        public string getExerciseRes(string ExeID)//若为fin1公布结果
        {
            string res;
            DataTable dt = DAL.Exercise.MyExercise.getExerciseres(ExeID);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //获取试题数据生成答题卡
        public string getExerciseTodo(string ExeID,string userID )
        {
            string res;
            DataTable dt = DAL.Exercise.MyExercise.getExercise(ExeID);
            DataTable result = DAL.Exercise.MyExercise.getExerciseRes(ExeID, userID);//获取是否已作答数据
            string exeresult = new Helper.jstodt().ToJson(result);
            dt.Columns.Add("res", Type.GetType("System.String"));
            dt.Rows[0]["res"] = exeresult;
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }
        
        //做题
        public string postExerciseCard(string ExeID, string result, string userID,string time)
        {
            string res = DAL.Exercise.MyExercise.postExerciseCard(ExeID, result, userID,time);
            return res;
        }

        
    }
}

