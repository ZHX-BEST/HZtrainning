 using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HangzhouPeiXun.DAL
{
    public class AbList
    {
        private static AbList myAbList = new AbList();
        public static AbList MyAbList { get { return myAbList; } }
        public AbList() { }

        #region 获取异常表
        public DataTable GetAbList()
        {
            string sql = "select * from TB_AbList";
            /*
             * IsSubmit列为1时说明解决表单已经提交，查看工单按钮
             * IsSubmit列为0时说明解决表单还未提交，提交工单按钮
             */
            DataTable dt=new Helper.SQLHelper().ExcuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
    }
}