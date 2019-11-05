using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HangzhouPeiXun.DAL
{
    public class Videos
    {
        private static Videos myVideos = new Videos();
        public static Videos MyVideos { get { return myVideos; } }
        public Videos() { }

        public DataTable getvideolist()
        {
            string sql = "select * from TB_Video order by Video_ID desc";
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, CommandType.Text);
            return dt;
        }

        public DataTable getvideo(string videoid)
        {
            string sql = "select * from TB_Video where Video_ID = @id";
            SqlParameter[] paras = new SqlParameter[] {new SqlParameter("@id",videoid) };
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql,paras ,CommandType.Text);
            return dt;

        }

        public string postvideo(string userid, string problem,string contest,string url,string hash)
        {
            string res = "false";
            string sql = "insert into TB_Video (Video_User,Video_Problem,Video_Contest,Video_URL,Video_Hash) values (@userid,@problem,@contest,@url,@hash)";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@userid", userid),
                new SqlParameter("@problem", problem) , new SqlParameter("@contest", contest) ,
                new SqlParameter("@url", url) , new SqlParameter("@hash", hash) };
            int flag = new Helper.SQLHelper().ExecuteNonQuery(sql, paras , CommandType.Text);
            if (flag>0)
            {
                res = "true";
            }
            return res;

        }
    }
}