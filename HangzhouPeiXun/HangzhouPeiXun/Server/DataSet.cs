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
                    case "A相电压断相":
                        abdtU = AXDYDX(abdtU, timeStart, timeEnd);
                        break;
                    case "B相电压断相":
                        abdtU = BXDYDX(abdtU, timeStart, timeEnd);
                        break;
                    case "C相电压断相":
                        abdtU = CXDYDX(abdtU, timeStart, timeEnd);
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
        #endregion
        #endregion

        #region Exercise Test
        //生成新的正常数据接口
        public string SetNorData(string UserType)
        {
            string UpperID = ServerDAL.DataSetDal.MyData.SetData(UserType);//插入Data一行，生成UpperID，NorID，AbID；
            
            string flag = Server.CreateData.MyCreate.CreateNorData(UpperID, UserType);//生成 I，U，W正常数据
            return UpperID;
        }


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
                    case "A相电压断相":
                        abdtU = AXDYDX1(abdtU, timeStart, timeEnd);
                        break;
                    case "B相电压断相":
                        abdtU = BXDYDX1(abdtU, timeStart, timeEnd);
                        break;
                    case "C相电压断相":
                        abdtU = CXDYDX1(abdtU, timeStart, timeEnd);
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