using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using HangzhouPeiXun.DAL;
using HangzhouPeiXun.Models;

namespace HangzhouPeiXun.Controllers
{
    public class SolveListController : ApiController
    {
        /// <summary>
        /// 获取工单列表
        /// </summary>
        /// <returns></returns>
        public string GetSolveList()
        {
            string res = "false";
            DataTable dt = DAL.SolveList.MySolveList.GetSolveList();
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        /// <summary>
        /// 获取指定异常ID的工单
        /// </summary>
        /// <param name="abId"></param>
        /// <returns></returns>
        public string GetSolve(string abID)
        {
            string res = "false";
            int abId = Convert.ToInt32(abID);
            DataTable dt = DAL.SolveList.MySolveList.GetSolve(abId);
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        /// <summary>
        /// 接收Post工单对象
        /// </summary>
        /// <param name="solve">工单对象</param>
        /// <returns></returns>
        //[HttpPost]
        //public void PostSolve(Solve solve)
        //{

        //}
        
        /// <summary>
        /// 插入工单表记录
        /// </summary>
        /// <param name="json">表单json</param>
        /// <returns></returns>
        public string InsertSolve([FromBody]Solve solve)
        {
            
            //string abId = dataRow["AbId"].ToString();
            //string voltage = dataRow["Voltage"].ToString();
            //string electricity = dataRow["Eletricity"].ToString();
            //string wattage = dataRow["Wattage"].ToString();
            //string scription = dataRow["Scription"].ToString();
            //string suberName = dataRow["SuberName"].ToString();

            string abId = solve.abId;
            string voltage = solve.voltage;
            string electricity = solve.electricity;
            string wattage = solve.wattage;
            string scription = solve.scription;
            string suberName = solve.suberName;
            string res = "false";
            res = DAL.SolveList.MySolveList.InsertSolve(abId, voltage, electricity, wattage, scription, suberName);
            return res;
        }

    }
}
