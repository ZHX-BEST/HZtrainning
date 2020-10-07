using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace HangzhouPeiXun.DAL
{
    #region 获取模型判断结果
    public class ReadRes
    {
        string path = "/res.txt";
        public string readRes(string path,string reslength)
        {
            StreamReader res = new StreamReader(path, Encoding.UTF8);
            string result = res.ReadToEndAsync().Result;
            return result;
        }
        public string readRes(string path)
        {
            
            StreamReader res = new StreamReader(path, Encoding.UTF8);
            string result = res.ReadToEndAsync().ToString();
            return result;
        }
    }
    #endregion

}