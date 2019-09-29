using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

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

        //生成正常数据接口
        public string SetNorData(string UserType)
        {
        	string UpperID = DAL.DataSet.MyData.SetData(UserType);//插入Data一行，生成UpperID，NorID，AbID；
        	string NorID = UpperID+"_0";//NorID = UpperID+_0; AbID = UpperID + _1;
        	string flag = CreateDate.MyCreate.CreateNorData(NorID,UserType);//生成 I，U，W正常数据
        	return UpperID;
        }

        //异常数据叠加接口
        public string SetAbData(string UpperID,string AbTime,string AbType)
        {
        	string flag="";

        	return flag;
        }
    }
}