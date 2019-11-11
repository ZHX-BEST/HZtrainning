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

        public string CreateNorData(string UpperID, string UserType)
        {
            #region 生成曲线数据
            DataTable dt = ServerDAL.CreateDataDal.MyCreate.getNormalData(UserType);            
            #region 创建数据格式
            DataTable DtI = new DataTable();
            DtI.Columns.Add("时间",Type.GetType("System.String"));
            DtI.Columns.Add("A相电流", Type.GetType("System.String"));
            DtI.Columns.Add("B相电流", Type.GetType("System.String"));
            DtI.Columns.Add("C相电流", Type.GetType("System.String"));
            DataTable DtU = new DataTable();
            DtU.Columns.Add("时间", Type.GetType("System.String"));
            DtU.Columns.Add("A相电压", Type.GetType("System.String"));
            DtU.Columns.Add("B相电压", Type.GetType("System.String"));
            DtU.Columns.Add("C相电压", Type.GetType("System.String"));
            DataTable DtW = new DataTable();
            DtW.Columns.Add("时间", Type.GetType("System.String"));
            DtW.Columns.Add("用电量", Type.GetType("System.String"));
            DtW.Columns.Add("倍率", Type.GetType("System.String"));
            #endregion
            int rowscpount = dt.Rows.Count;
            int RW = 0;
            for (int i = 0; i < rowscpount; i++)
            {
                DtI.Rows.Add();
                DtI.Rows[i]["时间"] = dt.Rows[i]["时间"].ToString();
                DtI.Rows[i]["A相电流"] = dt.Rows[i]["A相电流"].ToString();
                DtI.Rows[i]["B相电流"] = dt.Rows[i]["B相电流"].ToString();
                DtI.Rows[i]["C相电流"] = dt.Rows[i]["C相电流"].ToString();
                DtU.Rows.Add();
                DtU.Rows[i]["时间"] = dt.Rows[i]["时间"].ToString();
                DtU.Rows[i]["A相电压"] = dt.Rows[i]["A相电压"].ToString();
                DtU.Rows[i]["B相电压"] = dt.Rows[i]["B相电压"].ToString();
                DtU.Rows[i]["C相电压"] = dt.Rows[i]["C相电压"].ToString();
                if (dt.Rows[i]["用电量"] != null && dt.Rows[i]["用电量"].ToString() != "")
                {
                    DtW.Rows.Add();
                    DtW.Rows[RW]["时间"] = dt.Rows[i]["时间"].ToString();
                    DtW.Rows[RW]["用电量"] = dt.Rows[i]["用电量"].ToString();
                    DtW.Rows[RW]["倍率"] = dt.Rows[i]["倍率"].ToString();
                    RW++;
                }
            }
            #endregion
            string DataI = new Helper.jstodt().ToJson(DtI);
            string DataU = new Helper.jstodt().ToJson(DtU);
            string DataW = new Helper.jstodt().ToJson(DtW);
            string flag = ServerDAL.CreateDataDal.MyCreate.InsertIUW(UpperID, DataI,DataU,DataW);//插入生成的IUW
            return flag;
        }        
       

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