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
                res = dt.Rows[0]["Data_UpperID"].ToString();
            }catch{
                return res;//报错返回False
            }
            if(UpperID == "")//若为获取UpperID返回False
                UpperID = res;
            return UpperID;
        }

<<<<<<< HEAD
        //异常数据叠加接口
        public DataTable SetAbData(string UpperID, string AbTime, string AbType)
        {
           //TODO添加异常数据
            SqlParameter[] paras = { new SqlParameter("@UpperID", UpperID+"_1") };
            string sql = "select * from TB_I where I_DataID = @UpperID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql,paras,CommandType.Text);
            return dt;
        }
=======
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
>>>>>>> fa79032e2ea852b6bc9fabbe4e05617e84e4d6fb
    }
}