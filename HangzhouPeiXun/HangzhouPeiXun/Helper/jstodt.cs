using System;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;
using System.Web.Script.Serialization;

namespace HangzhouPeiXun.Helper
{
    public class jstodt
    {
        #region 转换为string字符串类型
        /// <summary>
        ///  转换为string字符串类型
        /// </summary>
        /// <param name="s">获取需要转换的值</param>
        /// <param name="format">需要格式化的位数</param>
        /// <returns>返回一个新的字符串</returns>
        public string ToStr(object s, string format = "")
        {
            string result = "";
            try
            {
                if(format == "")
                {
                    result = s.ToString();
                }
                else
                {
                    result = string.Format("{0:" + format + "}", s);
                }
            }
            catch
            {
            }
            return result;
        }
        #endregion
        #region Json 字符串 转换为 DataTable数据集合
        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public DataTable ToDataTable(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if(arrayList.Count > 0)
                {
                    foreach(Dictionary<string, object> dictionary in arrayList)
                    {
                        if(dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if(dataTable.Columns.Count == 0)
                        {
                            foreach(string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach(string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }

                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
        #endregion
        #region DataTable 转换为Json 字符串
        /// <summary>
        /// DataTable 对象 转换为Json 字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ToJson(DataTable dt)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayList = new ArrayList();
            foreach(DataRow dataRow in dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                foreach(DataColumn dataColumn in dt.Columns)
                {
                    string aa = ToStr(dataRow[dataColumn.ColumnName]);
                    dictionary.Add(dataColumn.ColumnName, aa);
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值
            }

            return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
        }
        #endregion
        #region 数组转Json
        /// <summary>
        /// 字符串数组转json
        /// </summary>
        /// <param name="result">字符串数组</param>
        /// <returns></returns>
        public string rAtoJs(string[] result)
        {
            string resultJson = "[";
            int length = result.Length;
            if(length > 0)
            {
                for(int i = 0; i < length; i++)
                {
                    result[i] = "{result" + i.ToString() + ":" + result[i] + "}";
                }
                for(int i = 0; i < length - 1; i++)
                {
                    resultJson = resultJson + result[i] + ",";
                }
                resultJson = resultJson + result[length - 1] + "]";
            }
            if(resultJson.Length > 2)
            {
                return resultJson;
            }
            else
            {
                return resultJson+"{result:true}]";
            }

        }
        #endregion
    }
}