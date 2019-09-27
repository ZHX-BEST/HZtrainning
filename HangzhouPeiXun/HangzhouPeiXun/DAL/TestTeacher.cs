using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangzhouPeiXun.DAL
{
    /// <summary>
    /// 测试教师端DAL
    /// </summary>
    public class TestTeacher
    {
        private static TestTeacher mytestteacher = new TestTeacher();
        public static TestTeacher MyTestTeacher { get { return mytestteacher; } }
        public TestTeacher() { }
    }
}