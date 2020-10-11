using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;

namespace HangzhouPeiXun.Controllers
{
    public class AbExamplesController : ApiController
    {
        // GET: AbExamples
        //获取正常数据曲线接口
        public string getNormalData(string User_type)//User_TYpe 为1-10
        {
            if(User_type != "01JCC" && User_type != "02CLC" &&
                User_type != "03ZZC" && User_type != "04HGC" &&
                User_type != "05ZGC" && User_type != "06Hospital" &&
                User_type != "07ZJDX" && User_type != "08FZC" &&
                User_type != "09CKC" && User_type != "10SC")
                return "FalseUserType";//获取用户类别错误
            string res = "error";//默认报错
            string TB_Name = "class" + User_type;
            DataTable dtI = DAL.AbExamples.MyAbExample.getNormalData(TB_Name, "I");
            DataTable dtU = DAL.AbExamples.MyAbExample.getNormalData(TB_Name, "U");
            DataTable dtW = DAL.AbExamples.MyAbExample.getNormalData(TB_Name, "W");
            string NordataI = new Helper.jstodt().ToJson(dtI);//数据打成json返回
            string NordataU = new Helper.jstodt().ToJson(dtU);//数据打成json返回
            string NordataW = new Helper.jstodt().ToJson(dtW);//数据打成json返回
            #region 处理data数据
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", Type.GetType("System.String"));
            dt.Columns.Add("DataI", Type.GetType("System.String"));
            dt.Columns.Add("DataU", Type.GetType("System.String"));
            dt.Columns.Add("DataW", Type.GetType("System.String"));
            dt.Rows.Add();
            dt.Rows[0]["ID"] = User_type;
            dt.Rows[0]["DataI"] = NordataI;
            dt.Rows[0]["DataU"] = NordataU;
            dt.Rows[0]["DataW"] = NordataW;
            #endregion
            res = new Helper.jstodt().ToJson(dt);
            return res;//固定死正常数据
        }

        //获取异常数据曲线接口
        public string getAbnormalData(string User_type, string AbType)//abType为json 异常类型，异常开始时间，异常结束时间
        {
            if(User_type != "01JCC" && User_type != "02CLC" &&
                User_type != "03ZZC" && User_type != "04HGC" &&
                User_type != "05ZGC" && User_type != "06Hospital" &&
                User_type != "07ZJDX" && User_type != "08FZC" &&
                User_type != "09CKC" && User_type != "10SC")
                return "FalseUserType";//获取用户类别错误
            string res = "error";//默认报错
            string TB_Name = "class" + User_type;
            DataTable dtI = DAL.AbExamples.MyAbExample.getNormalData(TB_Name, "I");
            DataTable dtU = DAL.AbExamples.MyAbExample.getNormalData(TB_Name, "U");
            DataTable dtW = DAL.AbExamples.MyAbExample.getNormalData(TB_Name, "W");
            DataTable abtable = new Helper.jstodt().ToDataTable(AbType);//异常表
            int abcount = abtable.Rows.Count;
            if(abcount != 0)
            {
                var ab = DAL.AbExamples.MyAbExample.SetAbData(dtI, dtU, dtW, abtable, abcount);//异常叠加
            }
            string abNordataI = new Helper.jstodt().ToJson(dtI);//数据打成json返回
            string abNordataU = new Helper.jstodt().ToJson(dtU);//数据打成json返回
            string abNordataW = new Helper.jstodt().ToJson(dtW);//数据打成json返回
            #region 处理data数据
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", Type.GetType("System.String"));
            dt.Columns.Add("abDataI", Type.GetType("System.String"));
            dt.Columns.Add("abDataU", Type.GetType("System.String"));
            dt.Columns.Add("abDataW", Type.GetType("System.String"));
            dt.Rows.Add();
            dt.Rows[0]["ID"] = User_type;
            dt.Rows[0]["abDataI"] = abNordataI;
            dt.Rows[0]["abDataU"] = abNordataU;
            dt.Rows[0]["abDataW"] = abNordataW;
            #endregion
            res = new Helper.jstodt().ToJson(dt);
            return res;//固定死正常数据            
        }
    }
}