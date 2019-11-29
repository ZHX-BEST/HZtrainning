using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangzhouPeiXun.Models
{
    public class Data
    {
        private static Data myData = new Data();
        public static Data MyData { get { return myData; } }
        public Data() { }

        public static int dayofyear = 0;
        public static string[] time = new string[] {" ", " ", " ", " ", " ", " ", " ", " ", " ", " " };//每一个选择的日期
        
    }
}