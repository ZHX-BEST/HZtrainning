﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangzhouPeiXun.DAL
{
    /// <summary>
    /// 典型行业曲线教师端接口DAL
    /// </summary>
    public class ExamplesTeacher
    {
        private static ExamplesTeacher myexampleteacher = new ExamplesTeacher();
        public static ExamplesTeacher MyExampleTeacher { get { return myexampleteacher; } }
        public ExamplesTeacher() { }
    }
}