using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HangzhouPeiXun.Server
{
    /// <summary>
    /// 曲线拟合层实现
    /// </summary>
    public class CreateData
    {
        private static CreateData mycreate = new CreateData();
        public static CreateData MyCreate { get { return mycreate; } }
        public CreateData() { }

        public string CreateNorData(string NorID,string UserType)
        {
            #region 生成曲线数据
            string DataI = CreateI(UserType);
            string DataU = CreateU(UserType);
            string DataW = CreateW(UserType); 
            #endregion

            string flag = ServerDAL.CreateDataDal.MyCreate.InsertIUW(NorID,DataI,DataU,DataW);//插入生成的IUW
            return flag;
        }
        
        #region 生成曲线数据
        private string CreateI(string UserType)
        {
            DataTable dt = ServerDAL.CreateDataDal.MyCreate.getNormalData(UserType, "I");
            string Data = new Helper.jstodt().ToJson(dt);//数据打成json返回
            return Data;
        } 
         private string CreateU(string UserType)
        {
            DataTable dt = ServerDAL.CreateDataDal.MyCreate.getNormalData(UserType, "U");
            string Data = new Helper.jstodt().ToJson(dt);//数据打成json返回
            return Data;
        } 
         private string CreateW(string UserType)
        {
            DataTable dt = ServerDAL.CreateDataDal.MyCreate.getNormalData(UserType, "W");
            string Data = new Helper.jstodt().ToJson(dt);//数据打成json返回
            return Data;
        }
        #endregion

        #region 数据拟合
        public DataTable Simulate(DataTable dt)
        {
            DataTable dtsim = dt;
            //TODO:数据模拟
            return dtsim;
        }
        #endregion


    }
}