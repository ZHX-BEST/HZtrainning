using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        #region 获取异常数据
        public Tuple<DataTable, DataTable, DataTable> SetAbData(DataTable dtI, DataTable dtU, DataTable dtW, string AbTime, string AbType)
        {
            var ab = Server.DataSet.MyData.SetAbData(dtI, dtU, dtW, AbTime, AbType);
            DataTable abdtI = ab.Item1;
            DataTable abdtU = ab.Item2;
            DataTable abdtW = ab.Item3;
            Tuple<DataTable, DataTable, DataTable> tup = new Tuple<DataTable, DataTable, DataTable>(abdtI, abdtU, abdtW);
            return tup;
        }
        #endregion
    }
}