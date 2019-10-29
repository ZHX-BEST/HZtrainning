using System;
using System.Collections.Generic;
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

            string flag = DAL.CreateData.MyCreate.InsertIUW(NorID,DataI,DataU,DataW);
            return flag;
        }
        
        #region 生成曲线数据
        private string CreateI(string UserType)
        {
            string Data = "123";//TODO
            return Data;
        } 
         private string CreateU(string UserType)
        {
            string Data = "123";//TODO
            return Data;
        } 
         private string CreateW(string UserType)
        {
            string Data = "123";//TODO
            return Data;
        }
        #endregion


        public string CreateabNorData(string abNorID)
        {
            #region 生成曲线数据
            string DataI = CreateI();
            string DataU = CreateU();
            string DataW = CreateW();
            #endregion
            
            string flag = DAL.CreateData.MyCreate.InsertIUW(abNorID, DataI, DataU, DataW);
            return flag;
        }

        #region 生成曲线数据
        private string CreateI()
        {
            string Data = "123";//TODO
            return Data;
        }
        private string CreateU()
        {
            string Data = "123";//TODO
            return Data;
        }
        private string CreateW()
        {
            string Data = "123";//TODO
            return Data;
        }
        #endregion
    }
}