using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HangzhouPeiXun.DAL
{
    /// <summary>
    /// 曲线拟合层实现
    /// </summary>
    public class CreateData
    {
        private static CreateData mycreate = new CreateData();
        public static CreateData MyCreate { get { return mycreate; } }
        public CreateData() { }

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
        
       
    }
}