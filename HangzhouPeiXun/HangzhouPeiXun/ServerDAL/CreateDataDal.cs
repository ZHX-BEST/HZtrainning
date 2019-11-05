using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HangzhouPeiXun.ServerDAL
{
    /// <summary>
    /// 曲线拟合层实现
    /// </summary>
    public class CreateDataDal
    {
        private static CreateDataDal mycreate = new CreateDataDal();
        public static CreateDataDal MyCreate { get { return mycreate; } }
        public CreateDataDal() { }

        public string InsertIUW(string NorID, string DataI, string DataU, string DataW)
        {
            string flag = "False"; //默认返回False         
            string sql = "InsertIUW";//插入数据存储过程
            SqlParameter[] paras = { new SqlParameter("@NorID", NorID), 
                                      new SqlParameter("@DataI", DataI), 
                                      new SqlParameter("@DataU", DataU), 
                                      new SqlParameter("@DataW", DataW) };//参数 NorID，电流96点Json，电压96点Json，电能96点Json，
            int res = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.StoredProcedure);//使用存储过程
            if(res >0)
                flag ="True";//插入成功返回True        
            return flag;
        }

        #region 获取固定典型正常数据
        public DataTable getNormalData(string TB_Name, string option)
        {
            string sql = "";
            switch (option)
            {
                case "I":
                    sql = "select 时间,A相电流,A相电流,C相电流 from @TBname";
                    break;
                case "U":
                    sql = "select 时间,A相电压,A相电压,C相电压 from @TBname";
                    break;
                case "W":
                    sql = "select 时间,用电量,变压器容量,倍率 from @TBname where 用电量 is not NULL";
                    break;
                default:
                    break;
            }
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@TBname", TB_Name) };
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion


    }
}