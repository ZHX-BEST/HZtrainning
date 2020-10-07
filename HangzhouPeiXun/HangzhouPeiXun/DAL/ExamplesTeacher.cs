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
            int tableflag = 0;
            switch (TB_Name)
            {
                case "class01JCC":
                    tableflag = 0;
                    break;
                case "class02CLC":
                    tableflag = 1;
                    break;
                case "class03ZZC":
                    tableflag = 2;
                    break;
                case "class04HGC":
                    tableflag = 3;
                    break;
                case "class05ZGC":
                    tableflag = 4;
                    break;
                case "class06Hospital":
                    tableflag = 5;
                    break;
                case "class07ZJDX":
                    tableflag = 6;
                    break;
                case "class08FZC":
                    tableflag = 7;
                    break;
                case "class09CKC":
                    tableflag = 8;
                    break;
                case "class10SC":
                    tableflag = 9;
                    break;

                default:
                    break;
            }
            //判断日期
            int dayofyear = DateTime.Now.DayOfYear;
            if (dayofyear != Models.Data.dayofyear)
            {
                Models.Data.time = new string[] { " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
                Models.Data.dayofyear = dayofyear;
            }
            if (Models.Data.time[tableflag] == " ")
            {
                string sqlgetdate = "ProCreateNew";
                SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@tablename", TB_Name) };
                DataTable dtgetdate = new Helper.SQLHelper().ExcuteQuery(sqlgetdate, paras, CommandType.StoredProcedure);
                Models.Data.time[tableflag] = dtgetdate.Rows[0]["时间"].ToString();
            }
            string starttime = Models.Data.time[tableflag]; ;//起始时间            

            switch (option)
            {
                case "I":
                    sql = "select top 5760 时间,A相电流,B相电流,C相电流 from " + TB_Name + " where 时间 >= '"+ starttime + "' order by 时间 asc";
                    break;
                case "U":
                    sql = "select top 5760 时间,A相电压,B相电压,C相电压 from " + TB_Name + "  where 时间 >= '" + starttime + "' order by 时间 asc";
                    break;
                case "W":
                    sql = "select top 60 时间,用电量,变压器容量,倍率 from " + TB_Name + " where 时间 >= '" + starttime + "' and 用电量 is not NULL";
                    break;
                default:
                    break;
            }
            //SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@TBname", TB_Name) };
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        #region 获取异常数据
        public Tuple<DataTable, DataTable, DataTable> SetAbData(DataTable dtI, DataTable dtU, DataTable dtW, DataTable AbType, int abcount)
        {
            var ab = Server.DataSet.MyData.SetStaticAbData(dtI, dtU, dtW, AbType, abcount);
            DataTable abdtI = ab.Item1;
            DataTable abdtU = ab.Item2;
            DataTable abdtW = ab.Item3;
            Tuple<DataTable, DataTable, DataTable> tup = new Tuple<DataTable, DataTable, DataTable>(abdtI, abdtU, abdtW);
            return tup;
        }
        #endregion
    }
}