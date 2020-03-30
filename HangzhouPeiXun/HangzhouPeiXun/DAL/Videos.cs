using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace HangzhouPeiXun.DAL
{
    public class Videos
    {
        private static Videos myVideos = new Videos();
        public static Videos MyVideos { get { return myVideos; } }
        public Videos() { }

        public static Thread t = new Thread(dofile);
        public static void dofile()
        {
            while (true)
            {
                DAL.Videos.MyVideos.docopy();
                Thread.Sleep(1000);
            }
        }

        public DataTable getvideolist()
        {
            string sql = "select V.*,u.User_Name from TB_Video v inner join TB_User u on v.Video_User = u.User_ID  order by Video_ID desc";           
            DataTable dt = new Helper.SQLHelper().ExcuteQuery(sql, CommandType.Text);
            return dt;
        }

        public DataTable getvideo(string videoid)
        {
            string sql = "select V.*,u.User_Name from TB_Video v inner join TB_User u on v.Video_User = u.User_ID where Video_ID = @id";
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

        public void docopy()
        {
            //4.获取程序的基目录 
            //string path4 = System.AppDomain.CurrentDomain.BaseDirectory;

            //5.获取和设置包括该应用程序的目录的名称 
            //string path5 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase; 
            
            string strPath = System.AppDomain.CurrentDomain.BaseDirectory;           
            //string sql = "update TB_Video set Video_Problem = '"+ strPath + "' where Video_ID = '36'";
            //new Helper.SQLHelper().ExecuteNonQuery(sql, CommandType.Text);
            string path2 = "F:\\项目\\HangzhouPeiXun\\HZTraining\\HangzhouPeiXun\\HangzhouPeiXun\\";           
            CopyDirectory(strPath+"Videos", path2);
            CopyDirectory(path2+"Videos", strPath);
        }

        /// <summary>
        /// 递归-拷贝文件夹及文件
        /// </summary>
        /// <param name="sourceDirectoryUrl">源路径</param>
        /// <param name="copyDirectoryUrl">复制路径</param>
        private void CopyDirectory(string sourceDirectoryUrl, string copyDirectoryUrl)
        {
            try
            {
                string folderName = sourceDirectoryUrl.Substring(sourceDirectoryUrl.LastIndexOf("\\") + 1);

                string desfolderdir = copyDirectoryUrl + "\\" + folderName;

                if (copyDirectoryUrl.LastIndexOf("\\") == (copyDirectoryUrl.Length - 1))
                {
                    desfolderdir = copyDirectoryUrl + folderName;
                }

                string[] filenames = Directory.GetFileSystemEntries(sourceDirectoryUrl);

                foreach (string file in filenames)// 遍历所有的文件和目录
                {
                    if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    {
                        
                        string currentdir = desfolderdir + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                        if (!Directory.Exists(currentdir))
                        {
                            Directory.CreateDirectory(currentdir);
                        }

                        CopyDirectory(file, desfolderdir);
                    }
                    else // 否则直接copy文件
                    {
                        string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);

                        srcfileName = desfolderdir + "\\" + srcfileName;

                        if (!Directory.Exists(desfolderdir))
                        {
                            Directory.CreateDirectory(desfolderdir);
                        }

                        File.Copy(file, srcfileName, true);

                    }
                }//foreach 



            }
            catch (Exception ex)
            {

            }

        }
    }
}