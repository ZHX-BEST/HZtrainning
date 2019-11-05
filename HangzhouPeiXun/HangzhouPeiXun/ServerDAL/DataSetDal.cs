using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HangzhouPeiXun.ServerDAL
{
    /// <summary>
    /// 曲线生成与拟合封装类
    /// </summary>
    public class DataSetDal
    {
        private static DataSetDal mydata = new DataSetDal();
        public static DataSetDal MyData { get { return mydata; } }
        public DataSetDal() { }

        /// <summary>
        /// 插入一条TB_Data,生成UpperID，NorID，AbIDe，返回 UpperID
        /// </summary>
        /// <param name="UserType">用户类别</param>
        /// <returns></returns>
        public string SetData(string UserType)
        {
        	string res = "False";//异常失败返回False
            string sql = "InsertTB_Data"; //插入一条TB_Data,生成UpperID，NorID，AbIDe，返回 UpperID
            string UpperID = "";
            try
            {
                SqlParameter[] paras = { new SqlParameter("@UserType", UserType)};
                DataTable dt =  new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.StoredProcedure);//使用存储过程
                res = dt.Rows[0]["Data_UpperID"].ToString();
            }catch{
                return res;//报错返回False
            }
            if(UpperID == "")//若为获取UpperID返回False
                UpperID = res;
            return UpperID;
        }   
    }
}