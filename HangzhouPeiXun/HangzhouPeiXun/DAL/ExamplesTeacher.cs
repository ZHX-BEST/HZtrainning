using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangzhouPeiXun.DAL
{
    /// <summary>
    /// 典型行业曲线教师端接口DAL
    /// </summary>
    public class ExamplesTeacher
    {
        private static ExamplesTeacher myexampleteacher = new ExamplesTeacher();
        public static ExamplesTeacher MyExampleTeacher { get { return myexampleteacher; } }
        public ExamplesTeacher() { }

        #region 获取正常数据
        public DataTable getNormalData(string NorID, string option)
        {           
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@type", User_type),new SqlParameter("@NorID", NorID) };
            string sql = "select * from TB_"+ option +" where "+ option +"_DataID = @NorID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sqllogin, paras, CommandType.Text);
            return dt;
        }
        #endregion

        #region 获取异常数据
        public DataTable getAbnormalData(string AbID, string option)
        {            
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@type", User_type),new SqlParameter("@AbID", AbID) };
            string sql = "select * from TB_"+ option +" where "+ option +"_DataID = @AbID";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sqllogin, paras, CommandType.Text);
            return dt;
        }
        #endregion
    }
}