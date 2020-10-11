using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace HangzhouPeiXun.DAL
{
    #region 获取模型判断结果
    public class JudgeResult
    {
        string path = "/res.txt";
        /// <summary>
        /// 机器学习模型判断结果(01串)写在文件中，获取最后一位（最近一天）或最后七位（最近七天）
        /// </summary>
        /// <param name="reslength">要获取的长度（1或7）</param>
        /// <returns>返回字符串数组</returns>
        public string[] readRes(int reslength)
        {
            string resultStr = "";
            StreamReader res = new StreamReader(path, Encoding.UTF8);
            string line;//结果在多行按行读取
            while((line = res.ReadLine()) != null)//按行读取
            {
                resultStr += line;
            }
            //resultStr = res.ReadToEndAsync().Result;//结果在一行
            string result = resultStr.Substring(resultStr.Length - reslength);
            string[] resultArray = new string[result.Length];
            for(int i = 0; i < result.Length; i++)
            {
                if(result[i] == '1')
                {
                    resultArray[i] = "true";
                }
                if(result[i] == '0')
                {
                    resultArray[i] = "false";
                }
            }
            return resultArray;
        }
    }
    #endregion

}