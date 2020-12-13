using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObj
{
    public static class Extensions
    {

        public static DateTime GetDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }
    
    }
}
