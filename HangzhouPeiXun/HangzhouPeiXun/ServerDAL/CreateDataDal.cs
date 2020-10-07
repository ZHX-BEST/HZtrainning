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

        /// <summary>
        /// 插入生成的数据
        /// </summary>
        /// <param name="NorID"></param>
        /// <param name="DataI"></param>
        /// <param name="DataU"></param>
        /// <param name="DataW"></param>
        /// <returns></returns>
        public string InsertIUW(string UpperID, string DataI, string DataU, string DataW)
        {
            string flag = "False"; //默认返回False         
            string sql = "InsertIUW";//插入数据存储过程
            SqlParameter[] paras = { new SqlParameter("@UpperID", UpperID), 
                                      new SqlParameter("@DataI", DataI), 
                                      new SqlParameter("@DataU", DataU), 
                                      new SqlParameter("@DataW", DataW) };//参数 NorID，电流96点Json，电压96点Json，电能96点Json，
            int res = new Helper.SQLHelper().ExecuteNonQuery(sql, paras, CommandType.StoredProcedure);//使用存储过程
            if(res >0)
                flag ="True";//插入成功返回True        
            return flag;
        }

        #region 获取拟合源数据
        /// <summary>
        /// 获取拟合源数据
        /// </summary>
        /// <param name="TB_Name"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public DataTable getNormalData(string usertype)
        {
            string sql = "ProCreateNew";
            SqlParameter[] paras = new SqlParameter[] {new SqlParameter("@tablename", "class"+ usertype) };           
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql,paras, CommandType.StoredProcedure);
            return dt;
        }
        #endregion


    }
}