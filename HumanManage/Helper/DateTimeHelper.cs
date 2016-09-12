using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanManage.Helpers
{
    public static class DateTimeHelper
    {
        public static string GetStartTimeState(DateTime? dt)
        {
            DateTime dt1 = Convert.ToDateTime(dt);
            if (dt > dt1.Date.Add(TimeSpan.Parse("08:31:00")))
                return "迟到";
            else
                return "正常";
        }

        public static int GetDutyState(DateTime? dt)
        {
            DateTime dt1 = Convert.ToDateTime(dt);
            if (dt > dt1.Date.Add(TimeSpan.Parse("08:31:00")))
                return 0;
            else
                return 1;
        }

        public static string GetEndTimeState(DateTime? dt)
        {
            DateTime dt1 = Convert.ToDateTime(dt);
            if (dt > dt1.Date.Add(TimeSpan.Parse("17:31:00")))
                return "正常";
            else
                return "早退";
        }

        public static bool GetSubDate(DateTime? starttime, DateTime? endtime)
        {
            DateTime dtstart = (DateTime)starttime;
            DateTime dtend = (DateTime)endtime;
            TimeSpan ts = dtend - dtstart;
            int days = ts.Days;
            if (days >= 0)
                return true;
            else
                return false;
        }

        public static bool GetSubDate(DateTime? starttime, DateTime? endtime, int? type)
        {
            bool reuslt = false;
            switch (type)
            {
                case 0:
                    reuslt = GetSubDate(starttime, DateTime.Now);
                    break;
                case 1:
                    reuslt = GetSubDate(endtime, DateTime.Now);
                    break;
                case 2:
                    reuslt = GetSubDate(endtime, DateTime.Now) && GetSubDate(starttime, DateTime.Now);
                    break;
            }
            return reuslt;
        }

        public static string GetApplyDuty(int? type, DateTime? starttime, DateTime? endtime)
        {
            string msg = "";
            switch (type)
            {
                case 0:
                    msg = String.Format("{0:yyyy-MM-dd HH:mm:ss}", starttime);
                    break;
                case 1:
                    msg = String.Format("{0:yyyy-MM-dd HH:mm:ss}", endtime);
                    break;
                case 2:
                    msg = String.Format("{0:yyyy-MM-dd HH:mm:ss}", starttime) + "~" + String.Format("{0:yyyy-MM-dd HH:mm:ss}", endtime);
                    break;
            }
            return msg;
        }
    }
}
