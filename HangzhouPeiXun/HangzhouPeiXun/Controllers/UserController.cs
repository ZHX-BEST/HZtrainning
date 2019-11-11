using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HangzhouPeiXun.Controllers
{
    public class UserController : ApiController
    {
        public string getUsers(string teacherID)
        {
            DataTable dt = DAL.User.MyUser.getUsers(teacherID);
            string res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        public string postuser(string name, string pwd, string teacher)
        {
            string res = DAL.User.MyUser.postuser(name,pwd,teacher);           
            return res;
        }

        public string postchangeuser(string name, string pwd, string teacher, string id)
        {
            string res = DAL.User.MyUser.postchangeuser(name, pwd, teacher,id);
            return res;
        }

        public string postdeleuser(string UserID)
        {
            string res = DAL.User.MyUser.postdeluser(UserID);
            return res;
        }
    }
}
