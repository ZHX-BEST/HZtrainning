using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangzhouPeiXun.DAL
{
    /// <summary>
    /// 学员课堂练习DAL
    /// </summary>
    public class Exercise
    {
        private static Exercise myexercise = new Exercise();
        public static Exercise MyExercise { get { return myexercise; } }
        public Exercise() { }

    }
}