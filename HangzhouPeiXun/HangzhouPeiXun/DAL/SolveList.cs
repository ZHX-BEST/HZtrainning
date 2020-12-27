using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HangzhouPeiXun.DAL
{
    public class SolveList
    {
        private static SolveList mySolveList = new SolveList();
        public static SolveList MySolveList { get { return mySolveList; } }
        public SolveList() { }

        #region 获取解决工单内容
        /// <summary>
        /// 查询解决工单表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSolveList()
        {
            string sql = "select * from TB_SolveList";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 根据异常Id查询工单记录
        /// </summary>
        /// <param name="abId">异常Id</param>
        /// <returns></returns>
        public DataTable GetSolve(int abId)
        {
            string sql = "select * from TB_SolveList where AbId=@id";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@id",abId)
            };
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion

        #region 插入解决工单记录
        /// <summary>
        /// 插入解决工单
        /// </summary>
        /// <param name="abId">异常记录ID</param>
        /// <param name="voltage">电压</param>
        /// <param name="electricity">电流</param>
        /// <param name="wattage">用电量</param>
        /// <param name="scription">描述</param>
        /// <param name="suberName">提交者姓名</param>
        /// <returns></returns>
        public string InsertSolve(string abId, string voltage, string electricity, string wattage, string scription, string suberName)
        {

            string res = "false";
            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@AbId",abId),
                new SqlParameter("@Voltage",voltage),
                new SqlParameter("@Electricity",electricity),
                new SqlParameter("@Wattage",wattage),
                new SqlParameter("@Scription",scription),
                new SqlParameter("@SuberName",suberName)
            };
            string sql1 = "insert into TB_SolveList(AbId,Voltage_V,Electricity_A,Wattage,Scription,SuberName) values (@AbId,@Voltage,@Electricity,@Wattage,@Scription,@SuberName)";


            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@AbId",abId)
            };
            string sql2 = "update TB_AbList set IsSubmit='true'where AbId=@AbId";

            try
            {
                int flag1 = new Helper.SQLHelper().ExecuteNonQuery(sql1, paras1, CommandType.Text);
                int flag2 = new Helper.SQLHelper().ExecuteNonQuery(sql2, paras2, CommandType.Text);
                if(flag1 > 0 && flag2 > 0)
                    res = "True";
            }
            catch(Exception exception)
            {

                throw exception;
            }
            return res;
        }
        #endregion
    }
}