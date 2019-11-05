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
            for (int i = 0; i < abCount; i++)
            {
                switch (AbType.Rows[i]["abType"].ToString())
                {
                    //TODO：添加异常处理
                    default:
                        break;
                }
            }
            Tuple<DataTable, DataTable, DataTable> tup = new Tuple<DataTable, DataTable, DataTable>(abdtI, abdtU, abdtW);
            return tup;
        }
        #endregion

        #region Exercise Test
        //生成新的正常数据接口
        public string SetNorData(string UserType)
        {
            string UpperID = ServerDAL.DataSetDal.MyData.SetData(UserType);//插入Data一行，生成UpperID，NorID，AbID；
            string NorID = UpperID + "_0";//NorID = UpperID+_0; AbID = UpperID + _1;
            string flag = Server.CreateData.MyCreate.CreateNorData(NorID, UserType);//生成 I，U，W正常数据
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
                switch (AbType.Rows[i]["abType"].ToString())
                {
                    //TODO：添加异常处理
                    default:
                        break;
                }
            }
            Tuple<DataTable, DataTable, DataTable> tup = new Tuple<DataTable, DataTable, DataTable>(abdtI, abdtU, abdtW);
            return tup;
        }
        #endregion


    }
}