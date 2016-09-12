using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanManage.Helpers
{
    public static class TimeHelper
    {
        public static DateTime GetFirstMinuteByMonth(string YYMM)
        {
            return Convert.ToDateTime(YYMM).AddMinutes(1);
        }

        public static DateTime GetLastDayByMonth(string YYMM)
        {
            return Convert.ToDateTime(YYMM).AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59);
        }

        public static DateTime GetLastMinuteByMonth(string YYMM)
        {
            return Convert.ToDateTime(YYMM).AddMonths(1).AddMinutes(-1);
        }
    }
}