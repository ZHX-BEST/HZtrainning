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
        #region 出题
        //获取已生成正常数据曲线列表
        public string getNormalDatalist(string User_type)//User_TYpe 为1-10
        {
            if (User_type != "01JCC" && User_type != "02CLC" &&
                User_type != "03ZZC" && User_type != "04HGC" &&
                User_type != "05ZGC" && User_type != "06Hospital" &&
                User_type != "07ZJDX" && User_type != "08FZC" &&
                User_type != "09CKC" && User_type != "10SC")
                return "FalseUserType";//获取用户类别错误
            string res = "error";//默认报错
            DataTable dt = DAL.ExerciseTeacher.MyExerciseTeacher.getnorlist(User_type);          
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //获取已生成正常数据曲线数据
        public string getNormalData(string Norid)//User_TYpe 为1-10
        {            
            string res = "error";//默认报错
            DataTable dt = DAL.ExerciseTeacher.MyExerciseTeacher.getnordatabyid(Norid);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //选中
        public string getUpperID(string NorID,string  usertype)
        {
            string res = DAL.ExerciseTeacher.MyExerciseTeacher.getUpperID(NorID, usertype);
            return res;
        }

        //新建
        public string getnewUpperID(string usertype)
        {
            string res = DAL.ExerciseTeacher.MyExerciseTeacher.getnewUpperID(usertype);
            return res;
        }

        //获取异常数据曲线接口
        public string getAbnormalData(string upperID, string AbType,int story)//abType为json 异常类型，异常开始时间，异常结束时间  story 0为不存，1为存
        {
            string res = "error";//默认报错     
            DataTable data = DAL.ExerciseTeacher.MyExerciseTeacher.getnordata(upperID);           
            DataTable abtable = new Helper.jstodt().ToDataTable(AbType);//异常表
            int abcount = abtable.Rows.Count;
            int count = data.Rows.Count;
            DataTable dtI = new Helper.jstodt().ToDataTable(data.Rows[0]["I_96Date"].ToString());
            DataTable dtU = new Helper.jstodt().ToDataTable(data.Rows[0]["U_96Date"].ToString());
            DataTable dtW = new Helper.jstodt().ToDataTable(data.Rows[0]["W_96Date"].ToString());
            if (count != 0 && abcount != 0 )
            {
                var ab = DAL.ExerciseTeacher.MyExerciseTeacher.SetAbData(dtI, dtU, dtW, abtable, abcount);//异常叠加 
                dtI = ab.Item1;
                dtU = ab.Item2;
                dtW = ab.Item3;
            }
            string abNordataI = new Helper.jstodt().ToJson(dtI);//数据打成json返回
            string abNordataU = new Helper.jstodt().ToJson(dtU);//数据打成json返回
            string abNordataW = new Helper.jstodt().ToJson(dtW);//数据打成json返回
            if (story == 1 )
            {
                DAL.ExerciseTeacher.MyExerciseTeacher.postabdata(upperID,abNordataI,abNordataU,abNordataW, AbType);//存入数据库
            }
            #region 处理data数据
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", Type.GetType("System.String"));
            dt.Columns.Add("abDataI", Type.GetType("System.String"));
            dt.Columns.Add("abDataU", Type.GetType("System.String"));
            dt.Columns.Add("abDataW", Type.GetType("System.String"));
            dt.Rows.Add();
            dt.Rows[0]["ID"] = upperID;
            dt.Rows[0]["abDataI"] = abNordataI;
            dt.Rows[0]["abDataU"] = abNordataU;
            dt.Rows[0]["abDataW"] = abNordataW;
            #endregion
            res = new Helper.jstodt().ToJson(dt);
            return res;            
        }

        //出题
        public string postExercise(string upperID, string TeacherID)
        {
            string res = DAL.ExerciseTeacher.MyExerciseTeacher.postExercise(upperID, TeacherID);
            return res;
        }
        #endregion

        //获取试题数据列表
        public string getExercise(string TeacherID)
        {
            string res;
            DataTable dt = DAL.ExerciseTeacher.MyExerciseTeacher.getExercise(TeacherID);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //获取试题详情
        public string getExercisebyID(string ExeID)
        {
            string res;
            DataTable dt = DAL.ExerciseTeacher.MyExerciseTeacher.getExerciseByID(ExeID);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //获取课堂测试结果异步刷新
        public string getresult(string ExeID)
        {
            DataTable dt = DAL.ExerciseTeacher.MyExerciseTeacher.getresult(ExeID);
            string res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        //结束测试
        public string postfinexe(string exeID)
        {
            string res = DAL.ExerciseTeacher.MyExerciseTeacher.postfin(exeID);

            return res;
        }
    }
}
