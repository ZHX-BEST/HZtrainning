using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Data;

namespace HangzhouPeiXun.Controllers
{
    public class VideosController : ApiController
    {
        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <returns></returns>
        public string getvideolist()
        {
            string res = "false";
            DataTable dt = DAL.Videos.MyVideos.getvideolist();
            res = new Helper.jstodt().ToJson(dt);
            return res;
        }

        /// <summary>
        /// 获取视频详情
        /// </summary>
        /// <param name="videoid"></param>
        /// <returns></returns>
        public string getvideo(string videoid)
        {
            string res = "false";
            DataTable dt = DAL.Videos.MyVideos.getvideo(videoid);
            res = new Helper.jstodt().ToJson(dt);
            return res;

        }

        public string postvideofile(string userid,string problem)
        {
            string time = DateTime.Now.ToFileTime().ToString();//日期字符串格式化
            HttpPostedFile file0 = HttpContext.Current.Request.Files[0];
            string contest = HttpContext.Current.Request["contest"].ToString();
            string fi0 = file0.FileName;
            string str0 = fi0.Substring(fi0.LastIndexOf("."), fi0.Length - fi0.LastIndexOf("."));//切出后缀
            string ymd = DateTime.Now.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);//精确到日，用于文件夹名
            string ymds = DateTime.Now.ToString("yyyyMMddHHmmssffff", DateTimeFormatInfo.InvariantInfo);//精确到毫秒时间，用于文件名
            string url = @"/Videos/" + ymd;
            string strPath = HttpContext.Current.Server.MapPath(url);
            string hash = "";//计算文件hash值
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);//如文件夹不存在创建当日文件夹
            }
            url = url + ymds + str0;
            strPath += ymds + str0;
            try
            {
                file0.SaveAs(strPath);
                hash = ComputeSHA1(strPath);//计算hash
            }
            catch (Exception)
            {
                return "error";//文件保存报错
                throw;
            }
            string flag = DAL.Videos.MyVideos.postvideo(userid, problem, contest, url, hash);
            return flag;


        }

        /// <summary>
        ///  计算指定文件的SHA1值
        /// </summary>
        /// <param name="fileName">指定文件的完全限定名称</param>
        /// <returns>返回值的字符串形式</returns>
        public  string ComputeSHA1(string fileName)
        {
            string hashSHA1 = string.Empty;
            //检查文件是否存在，如果文件存在则进行计算，否则返回空值
            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    //计算文件的SHA1值
                    System.Security.Cryptography.SHA1 calculator = System.Security.Cryptography.SHA1.Create();
                    Byte[] buffer = calculator.ComputeHash(fs);
                    calculator.Clear();
                    //将字节数组转换成十六进制的字符串形式
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        stringBuilder.Append(buffer[i].ToString("x2"));
                    }
                    hashSHA1 = stringBuilder.ToString();
                }//关闭文件流
            }
            return hashSHA1;
        }//ComputeSHA1
    }

}
