using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HangzhouPeiXun.DAL
{
    /// <summary>
    /// 曲线生成与拟合封装类
    /// </summary>
    public class DataSet
    {
        private static DataSet mydata = new DataSet();
        public static DataSet MyData { get { return mydata; } }
        public DataSet() { }

        public string SetData(string UserType)
        {

        	string res = "False";//异常失败返回False
            string sql = "InsertTB_Data"; //插入一条TB_Data,生成UpperID，NorID，AbIDe，返回 UpperID
            string UpperID = "";
            try
            {
                SqlParameter[] paras = { new SqlParameter("@UserType", UserType)};
                UpperID =  new Helper.SQLHelper().ExcuteNonQuery(sql, paras, CommandType.StoredProcedure);//使用存储过程
            }catch{
                return res;//报错返回False
            }
            if(UpperID == "")//若为获取UpperID返回False
                UpperID = res;
            return UpperID;
        }
    }
}