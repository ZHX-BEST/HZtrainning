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
                DataTable dt =  new Helper.SQLHelper().ExcuteQuery(sql, paras, CommandType.StoredProcedure);//使用存储过程
            }catch{
                return res;//报错返回False
            }
            if(UpperID == "")//若为获取UpperID返回False
                UpperID = res;
            return UpperID;
        }

        #region 数据生成
        public string create96I(string UserType)
        {
            string data = "";
            DataTable dtI = new DataTable();
            switch (UserType)
            {
                default:
                    break;
            }
            data = new Helper.jstodt().ToJson(dtI);//转换成json字符串
            return data;
        }
        #endregion

        #region 数据拟合
        public bool abnoral(string ABType)//异常数据拟合分项处理
        {
            bool res = false;           
            switch (ABType)
            {
                case "123":
                    res = true;
                    break;
                default:
                    break;
            }            
            return res;
        }

        #endregion
    }
}