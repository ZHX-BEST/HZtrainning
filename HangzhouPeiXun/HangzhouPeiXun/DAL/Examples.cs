using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HangzhouPeiXun.DAL
{
    /// <summary>
    /// 典型行业曲线DAL
    /// </summary>
    public class Examples
    {
        private static Examples myexamples = new Examples();
        public static Examples MyExamples { get { return myexamples; } }
        public Examples() { }

        #region 获取正常数据
        public DataTable getNormalData(string User_type, string option)
        {
            string table = "";
            string table_id = "";
            switch (option)
            {
                case "I":
                    table = "TB_I";
                    table_id = "TB_I.I_DataID";
                    break;
                case "U":
                    table = "TB_U";
                    table_id = "TB_U.U_DataID";
                    break;
                case "W":
                    table = "TB_W";
                    table_id = "TB_W.W_DataID";
                    break;
            }
            DataTable dt = new DataTable();
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@type", User_type) };
            // string sqllogin = "select * from TB_I where I_DataID=(select Data_UpperID from TB_Data where Data_UserType=@type and Data_NorID=1)";
            string sqllogin = "select "+ table +".* from "+ table +" inner join TB_Data on "+ table_id + "=TB_Data.Data_UpperID where Data_UserType=@type and Data_NorID=1";
            dt = new Helper.SQLHelper().ExcuteQuery(sqllogin, paras, CommandType.Text);
            return dt;
        }
        #endregion

        #region 获取异常数据
        public DataTable getAbnormalData(string User_type, string option)
        {
            string table = "";
            string table_id = "";
            switch (option)
            {
                case "I":
                    table = "TB_I";
                    table_id = "TB_I.I_DataID";
                    break;
                case "U":
                    table = "TB_U";
                    table_id = "TB_U.U_DataID";
                    break;
                case "W":
                    table = "TB_W";
                    table_id = "TB_W.W_DataID";
                    break;
            }
            DataTable dt = new DataTable();
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@type", User_type) };
            // string sqllogin = "select * from TB_I where I_DataID=(select Data_UpperID from TB_Data where Data_UserType=@type and Data_NorID=1)";
            string sqllogin = "select " + table + ".* from " + table + " inner join TB_Data on " + table_id + "=TB_Data.Data_UpperID where Data_UserType=@type and Data_AbID=1";
            dt = new Helper.SQLHelper().ExcuteQuery(sqllogin, paras, CommandType.Text);
            return dt;
        }
        #endregion
    }
}