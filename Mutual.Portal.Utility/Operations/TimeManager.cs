using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutual.Portal.Utility.Operations
{
    public class TimeManager
    {
        public static DateTime GetServerTimeByLocalTime(DateTime localTime)
        {
            return DateTime.Now;
        }

        public static DateTime GetLocalTimeByServerTime(DateTime serverTime)
        {
            return DateTime.Now;
        }

        public static DateTime GetServerTime()
        {
            return DateTime.Now;
        }
    }
}
