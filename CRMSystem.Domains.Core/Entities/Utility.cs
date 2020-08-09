using System;
using System.Collections.Generic;
using System.Text;

namespace CRMSystem.Domains
{
    public static class Utility
    {
        public static DateTime StartOfDay(this DateTime theDate)
        {
            return theDate.Date;
        }

        public static DateTime EndOfDay(this DateTime theDate)
        {
            return theDate.Date.AddDays(1);
        }
    }
}