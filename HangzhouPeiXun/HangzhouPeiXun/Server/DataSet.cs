using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace HangzhouPeiXun.Server
{
    /// <summary>
    /// 曲线拟合层对外接口封装类
    /// </summary>
    public class DataSet
    {
        private static DataSet mydata = new DataSet();
        public static DataSet MyData { get { return mydata; } }
        public DataSet() { }

        #region Examples        

        /// <summary>
        /// 异常数据叠加接口  
        /// </summary>
        /// <param name="dtI"></param>
        /// <param name="dtU"></param>
        /// <param name="dtW"></param>
        /// <param name="AbType"></param>
        /// <param name="abCount"></param>
        /// <returns></returns>
        public Tuple<DataTable, DataTable, DataTable> SetStaticAbData(DataTable dtI, DataTable dtU, DataTable dtW, DataTable AbType, int abCount)//设置典型数据异常
        {

            DataTable abdtI = dtI;
            DataTable abdtU = dtU;
            DataTable abdtW = dtW;
            int Icount = dtI.Rows.Count;
            int Ucount = dtU.Rows.Count;
            int Wcount = dtW.Rows.Count;
            for (int i = 0; i < abCount; i++)
            {
                string timeStart = AbType.Rows[i]["timeStart"].ToString();
                string timeEnd = AbType.Rows[i]["timeEnd"].ToString();
                switch (AbType.Rows[i]["abType"].ToString())
                {
                    case "A相电流断相":
                        abdtI = AXDLDX(abdtI, timeStart, timeEnd);
                        break;
                    case "B相电流断相":
                        abdtI = BXDLDX(abdtI, timeStart, timeEnd);
                        break;
                    case "C相电流断相":
                        abdtI = CXDLDX(abdtI, timeStart, timeEnd);
                        break;
                    case "A相电流串二极管":
                        abdtU = AXDLEJG(abdtI, timeStart, timeEnd);
                        break;
                    case "B相电流串二极管":
                        abdtU = BXDLEJG(abdtI, timeStart, timeEnd);
                        break;
                    case "C相电流串二极管":
                        abdtU = CXDLEJG(abdtI, timeStart, timeEnd);
                        break;
                    case "A相电流串电阻":
                        abdtU = AXDLDZ(abdtI, timeStart, timeEnd);
                        break;
                    case "B相电流串电阻":
                        abdtU = BXDLDZ(abdtI, timeStart, timeEnd);
                        break;
                    case "C相电流串电阻":
                        abdtU = CXDLDZ(abdtI, timeStart, timeEnd);
                        break;
                    case "A相电流反向":
                        abdtU = AXDLFX(abdtI, timeStart, timeEnd);
                        break;
                    case "B相电流反向":
                        abdtU = BXDLFX(abdtI, timeStart, timeEnd);
                        break;
                    case "C相电流反向":
                        abdtU = CXDLFX(abdtI, timeStart, timeEnd);
                        break;
                    case "A相电压断相":
                        abdtU = AXDYDX(abdtU, timeStart, timeEnd);
                        break;
                    case "B相电压断相":
                        abdtU = BXDYDX(abdtU, timeStart, timeEnd);
                        break;
                    case "C相电压断相":
                        abdtU = CXDYDX(abdtU, timeStart, timeEnd);
                        break;
                    case "A相电压串二极管":
                        abdtU = AXDYEJG(abdtU, timeStart, timeEnd);
                        break;
                    case "B相电压串二极管":
                        abdtU = BXDYEJG(abdtU, timeStart, timeEnd);
                        break;
                    case "C相电压串二极管":
                        abdtU = CXDYEJG(abdtU, timeStart, timeEnd);
                        break;
                    case "A相电压串电阻":
                        abdtU = AXDYDZ(abdtU, timeStart, timeEnd);
                        break;
                    case "B相电压串电阻":
                        abdtU = BXDYDZ(abdtU, timeStart, timeEnd);
                        break;
                    case "C相电压串电阻":
                        abdtU = CXDYDZ(abdtU, timeStart, timeEnd);
                        break;
                    default:
                        break;
                }
            }
            Tuple<DataTable, DataTable, DataTable> tup = new Tuple<DataTable, DataTable, DataTable>(abdtI, abdtU, abdtW);
            return tup;
        }
        #region 异常数据叠加yyyy-MM-dd HH:mm:ss.fff
        /// <summary>
        /// A相电流断相
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDLDX(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy-MM-dd HH:mm:ss.fff");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                I.Rows[i]["A相电流"] = "0";
            }
            return I;
        }

        /// <summary>
        /// B相电流断相
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDLDX(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy-MM-dd HH:mm:ss.fff");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                I.Rows[i]["B相电流"] = "0";
            }
            return I;
        }
        /// <summary>
        /// C相电流断相
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDLDX(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy-MM-dd HH:mm:ss.fff");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                I.Rows[i]["C相电流"] = "0";
            }
            return I;
        }

        /// <summary>
        /// A相电流二极管
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDLEJG(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double A = Convert.ToDouble(I.Rows[i]["A相电流"]);//二极管变成根号二分之一
                A = A * 0.7071;
                I.Rows[i]["A相电流"] = A.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// B相电流二极管
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDLEJG(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double B = Convert.ToDouble(I.Rows[i]["B相电流"]);//二极管变成根号二分之一
                B = B * 0.7071;
                I.Rows[i]["B相电流"] = B.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// C相电流二极管
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDLEJG(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double C = Convert.ToDouble(I.Rows[i]["C相电流"]);//二极管变成根号二分之一
                C = C * 0.7071;
                I.Rows[i]["C相电流"] = C.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// A相电流电阻
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDLDZ(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double A = Convert.ToDouble(I.Rows[i]["A相电流"]);//二极管变成根号二分之一
                A = A * 0.5;
                I.Rows[i]["A相电流"] = A.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// B相电流电阻
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDLDZ(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double B = Convert.ToDouble(I.Rows[i]["B相电流"]);//二极管变成根号二分之一
                B = B * 0.5;
                I.Rows[i]["B相电流"] = B.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// C相电流电阻
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDLDZ(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double C = Convert.ToDouble(I.Rows[i]["C相电流"]);//二极管变成根号二分之一
                C = C * 0.5;
                I.Rows[i]["C相电流"] = C.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// A相电流反向
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDLFX(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double A = Convert.ToDouble(I.Rows[i]["A相电流"]);//二极管变成根号二分之一
                A = A * -1;
                I.Rows[i]["A相电流"] = A.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// B相电流反向
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDLFX(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double B = Convert.ToDouble(I.Rows[i]["B相电流"]);//二极管变成根号二分之一
                B = B * -1;
                I.Rows[i]["B相电流"] = B.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// C相电流反向
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDLFX(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double C = Convert.ToDouble(I.Rows[i]["C相电流"]);//二极管变成根号二分之一
                C = C * -1;
                I.Rows[i]["C相电流"] = C.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// A相电压断相
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDYDX(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy-MM-dd HH:mm:ss.fff");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                U.Rows[i]["A相电压"] = "0";
            }
            return U;
        }

        /// <summary>
        /// B相电压断相
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDYDX(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy-MM-dd HH:mm:ss.fff");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                U.Rows[i]["B相电压"] = "0";
            }
            return U;
        }

        /// <summary>
        /// C相电压断相
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDYDX(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy-MM-dd HH:mm:ss.fff");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy-MM-dd HH:mm:ss.fff");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                U.Rows[i]["C相电压"] = "0";
            }
            return U;
        }
        /// <summary>
        /// A相电压二极管
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDYEJG(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double A = Convert.ToDouble(U.Rows[i]["A相电压"]);//二极管变成根号二分之一
                A = A * 0.7071;
                U.Rows[i]["A相电压"] = A.ToString("#0.000");
            }
            return U;
        }

        /// <summary>
        /// B相电压二极管
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDYEJG(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double B = Convert.ToDouble(U.Rows[i]["B相电压"]);//二极管变成根号二分之一
                B = B * 0.7071;
                U.Rows[i]["B相电压"] = B.ToString("#0.000");
            }
            return U;
        }

        /// <summary>
        /// C相电压二极管
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDYEJG(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double C = Convert.ToDouble(U.Rows[i]["C相电压"]);//二极管变成根号二分之一
                C = C * 0.7071;
                U.Rows[i]["C相电压"] = C.ToString("#0.000");
            }
            return U;
        }
        /// <summary>
        /// A相电压电阻
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDYDZ(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double A = Convert.ToDouble(U.Rows[i]["A相电压"]);//二极管变成根号二分之一
                A = A * 0.5;
                U.Rows[i]["A相电压"] = A.ToString("#0.000");
            }
            return U;
        }

        /// <summary>
        /// B相电压电阻
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDYDZ(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double B = Convert.ToDouble(U.Rows[i]["B相电压"]);//二极管变成根号二分之一
                B = B * 0.5;
                U.Rows[i]["B相电压"] = B.ToString("#0.000");
            }
            return U;
        }

        /// <summary>
        /// C相电压电阻
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDYDZ(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double C = Convert.ToDouble(U.Rows[i]["C相电压"]);//二极管变成根号二分之一
                C = C * 0.5;
                U.Rows[i]["C相电压"] = C.ToString("#0.000");
            }
            return U;
        }
        #endregion
        #endregion

        #region Exercise Test
        //生成新的正常数据接口
        //public string SetNorData(string UserType)
        //{
        //    string UpperID = ServerDAL.DataSetDal.MyData.SetData(UserType);//插入Data一行，生成UpperID，NorID，AbID；

        //    string flag = Server.CreateData.MyCreate.CreateNorData(UpperID, UserType);//生成 I，U，W正常数据
        //    return UpperID;
        //}


        //异常数据叠加接口       
        public Tuple<DataTable, DataTable, DataTable> SetAbData(DataTable dtI, DataTable dtU, DataTable dtW, DataTable AbType, int abCount)//设置典型数据异常
        {
            DataTable abdtI = dtI;
            DataTable abdtU = dtU;
            DataTable abdtW = dtW;           
            for (int i = 0; i < abCount; i++)
            {
                string timeStart = AbType.Rows[i]["timeStart"].ToString();
                string timeEnd = AbType.Rows[i]["timeEnd"].ToString();
                switch (AbType.Rows[i]["abType"].ToString())
                {
                    case "A相电流断相":
                        abdtI = AXDLDX1(abdtI, timeStart, timeEnd);
                        break;
                    case "B相电流断相":
                        abdtI = BXDLDX1(abdtI, timeStart, timeEnd);
                        break;
                    case "C相电流断相":
                        abdtI = CXDLDX1(abdtI, timeStart, timeEnd);
                        break;
                    case "A相电流串二极管":
                        abdtU = AXDLEJG1(abdtI, timeStart, timeEnd);
                        break;
                    case "B相电流串二极管":
                        abdtU = BXDLEJG1(abdtI, timeStart, timeEnd);
                        break;
                    case "C相电流串二极管":
                        abdtU = CXDLEJG1(abdtI, timeStart, timeEnd);
                        break;
                    case "A相电流串电阻":
                        abdtU = AXDLDZ1(abdtI, timeStart, timeEnd);
                        break;
                    case "B相电流串电阻":
                        abdtU = BXDLDZ1(abdtI, timeStart, timeEnd);
                        break;
                    case "C相电流串电阻":
                        abdtU = CXDLDZ1(abdtI, timeStart, timeEnd);
                        break;
                    case "A相电流反向":
                        abdtU = AXDLFX1(abdtI, timeStart, timeEnd);
                        break;
                    case "B相电流反向":
                        abdtU = BXDLFX1(abdtI, timeStart, timeEnd);
                        break;
                    case "C相电流反向":
                        abdtU = CXDLFX1(abdtI, timeStart, timeEnd);
                        break;
                    case "A相电压断相":
                        abdtU = AXDYDX1(abdtU, timeStart, timeEnd);
                        break;
                    case "B相电压断相":
                        abdtU = BXDYDX1(abdtU, timeStart, timeEnd);
                        break;
                    case "C相电压断相":
                        abdtU = CXDYDX1(abdtU, timeStart, timeEnd);
                        break;
                    case "A相电压串二极管":
                        abdtU = AXDYEJG1(abdtU, timeStart, timeEnd);
                        break;
                    case "B相电压串二极管":
                        abdtU = BXDYEJG1(abdtU, timeStart, timeEnd);
                        break;
                    case "C相电压串二极管":
                        abdtU = CXDYEJG1(abdtU, timeStart, timeEnd);
                        break;
                    case "A相电压串电阻":
                        abdtU = AXDYDZ1(abdtU, timeStart, timeEnd);
                        break;
                    case "B相电压串电阻":
                        abdtU = BXDYDZ1(abdtU, timeStart, timeEnd);
                        break;
                    case "C相电压串电阻":
                        abdtU = CXDYDZ1(abdtU, timeStart, timeEnd);
                        break;
                    default:
                        break;
                }
            }
            Tuple<DataTable, DataTable, DataTable> tup = new Tuple<DataTable, DataTable, DataTable>(abdtI, abdtU, abdtW);
            return tup;
        }
        #region 异常数据叠加yyyy/M/d H:mm:ss
        /// <summary>
        /// A相电流断相
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDLDX1(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                I.Rows[i]["A相电流"] = "0";
            }
            return I;
        }

        /// <summary>
        /// B相电流断相
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDLDX1(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                I.Rows[i]["B相电流"] = "0";
            }
            return I;
        }
        /// <summary>
        /// C相电流断相
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDLDX1(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                I.Rows[i]["C相电流"] = "0";
            }
            return I;
        }

        /// <summary>
        /// A相电流二极管
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDLEJG1(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double A = Convert.ToDouble(I.Rows[i]["A相电流"]);//二极管变成根号二分之一
                A = A * 0.7071;
                I.Rows[i]["A相电流"] = A.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// B相电流二极管
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDLEJG1(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double B = Convert.ToDouble(I.Rows[i]["B相电流"]);//二极管变成根号二分之一
                B = B * 0.7071;
                I.Rows[i]["B相电流"] = B.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// C相电流二极管
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDLEJG1(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double C = Convert.ToDouble(I.Rows[i]["C相电流"]);//二极管变成根号二分之一
                C = C * 0.7071;
                I.Rows[i]["C相电流"] = C.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// A相电流电阻
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDLDZ1(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double A = Convert.ToDouble(I.Rows[i]["A相电流"]);//二极管变成根号二分之一
                A = A * 0.5;
                I.Rows[i]["A相电流"] = A.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// B相电流电阻
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDLDZ1(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double B = Convert.ToDouble(I.Rows[i]["B相电流"]);//二极管变成根号二分之一
                B = B * 0.5;
                I.Rows[i]["B相电流"] = B.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// C相电流电阻
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDLDZ1(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double C = Convert.ToDouble(I.Rows[i]["C相电流"]);//二极管变成根号二分之一
                C = C * 0.5;
                I.Rows[i]["C相电流"] = C.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// A相电流反向
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDLFX1(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double A = Convert.ToDouble(I.Rows[i]["A相电流"]);//二极管变成根号二分之一
                A = A * -1;
                I.Rows[i]["A相电流"] = A.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// B相电流反向
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDLFX1(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double B = Convert.ToDouble(I.Rows[i]["B相电流"]);//二极管变成根号二分之一
                B = B * -1;
                I.Rows[i]["B相电流"] = B.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// C相电流反向
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDLFX1(DataTable I, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = I.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(I.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(I.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return I; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                flagStart = I.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                I.PrimaryKey = new System.Data.DataColumn[] { I.Columns["时间"] };
                DataRow row = I.Rows.Find(sT);
                FlagEnd = I.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double C = Convert.ToDouble(I.Rows[i]["C相电流"]);//二极管变成根号二分之一
                C = C * -1;
                I.Rows[i]["C相电流"] = C.ToString("#0.000");
            }
            return I;
        }

        /// <summary>
        /// A相电压断相
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDYDX1(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                U.Rows[i]["A相电压"] = "0";
            }
            return U;
        }

        /// <summary>
        /// B相电压断相
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDYDX1(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                U.Rows[i]["B相电压"] = "0";
            }
            return U;
        }

        /// <summary>
        /// C相电压断相
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDYDX1(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                U.Rows[i]["C相电压"] = "0";
            }
            return U;
        }
        /// <summary>
        /// A相电压二极管
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDYEJG1(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double A = Convert.ToDouble(U.Rows[i]["A相电压"]);//二极管变成根号二分之一
                A = A * 0.7071;
                U.Rows[i]["A相电压"] = A.ToString("#0.000");
            }
            return U;
        }

        /// <summary>
        /// B相电压二极管
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDYEJG1(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double B = Convert.ToDouble(U.Rows[i]["B相电压"]);//二极管变成根号二分之一
                B = B * 0.7071;
                U.Rows[i]["B相电压"] = B.ToString("#0.000");
            }
            return U;
        }

        /// <summary>
        /// C相电压二极管
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDYEJG1(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double C = Convert.ToDouble(U.Rows[i]["C相电压"]);//二极管变成根号二分之一
                C = C * 0.7071;
                U.Rows[i]["C相电压"] = C.ToString("#0.000");
            }
            return U;
        }
        /// <summary>
        /// A相电压电阻
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable AXDYDZ1(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double A = Convert.ToDouble(U.Rows[i]["A相电压"]);//二极管变成根号二分之一
                A = A * 0.5;
                U.Rows[i]["A相电压"] = A.ToString("#0.000");
            }
            return U;
        }

        /// <summary>
        /// B相电压电阻
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable BXDYDZ1(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double B = Convert.ToDouble(U.Rows[i]["B相电压"]);//二极管变成根号二分之一
                B = B * 0.5;
                U.Rows[i]["B相电压"] = B.ToString("#0.000");
            }
            return U;
        }

        /// <summary>
        /// C相电压电阻
        /// </summary>
        /// <param name="I"></param>
        /// <param name="timeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        DataTable CXDYDZ1(DataTable U, string timeStart, string TimeEnd)
        {
            int flagStart = 0;
            int FlagEnd = U.Rows.Count;
            int CountI = FlagEnd;
            DateTime dtStart = Convert.ToDateTime(timeStart);
            DateTime dtEnd = Convert.ToDateTime(TimeEnd);
            dtStart = set15minute(dtStart);//设置分钟为15的整数倍
            dtEnd = set15minute(dtEnd);
            DateTime dtDataS = Convert.ToDateTime(U.Rows[0]["时间"].ToString());//数据开始时间
            DateTime dtDataE = Convert.ToDateTime(U.Rows[CountI - 1]["时间"].ToString());//数据结束时间
            if (DateTime.Compare(dtDataE, dtStart) < 0 || DateTime.Compare(dtEnd, dtDataS) < 0)//异常时间不在数据时间中间
            { return U; }
            if (DateTime.Compare(dtStart, dtDataS) > 0)//异常起始时间在数据起始时间之后
            {
                string sT = dtStart.ToString("yyyy/M/d H:mm:ss");//2018-10-23 00:00:00.000
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                flagStart = U.Rows.IndexOf(row);
            }
            if (DateTime.Compare(dtDataE, dtEnd) > 0)//异常结束时间在数据结束时间之前
            {
                string sT = dtEnd.ToString("yyyy/M/d H:mm:ss");
                U.PrimaryKey = new System.Data.DataColumn[] { U.Columns["时间"] };
                DataRow row = U.Rows.Find(sT);
                FlagEnd = U.Rows.IndexOf(row);
            }
            for (int i = flagStart; i < FlagEnd; i++)
            {
                double C = Convert.ToDouble(U.Rows[i]["C相电压"]);//二极管变成根号二分之一
                C = C * 0.5;
                U.Rows[i]["C相电压"] = C.ToString("#0.000");
            }
            return U;
        }
        #endregion

        #endregion
        DateTime set15minute(DateTime dt)//化为整数时刻
        {            
            int m = ((dt.Minute / 15+1)%4)*15;
            string T = dt.ToString("u");//2007-04-24 15:52:19
            string DayHour = T.Split(':')[0];//2007-04-24 15
            string Tnow = T;
            if (m==0)
                Tnow = DayHour + ":00:00";
           else
                Tnow = DayHour + ":"+m+":00";
            dt = Convert.ToDateTime(Tnow);
            return dt;
        }

    }
}