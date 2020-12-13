using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DataObj
{
    public static class MyUtils
    {

        public static int GetAmountInPIPS(double amount)
        {
            return (int)(amount * 10000);
        }

        public static double ConvertPipsToAmount(int PIPS)
        {
            return (double)(PIPS * 0.0001);
        }


        public static string GetFormattedAmount(double amount)
        {
            return string.Format("{0:0.00000}", amount);
        }


        public static string GetName(Expression<Func<object>> exp)
        {
            MemberExpression body = exp.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)exp.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }



        public static string ShowResults(object obj)
        {

            string result = string.Empty; 

            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo pi in props)
            {
                string name = pi.Name;
                object[] args = new object[0];
                object value = pi.GetValue(obj, args);
                string row = name + "=" + value.ToString() + Environment.NewLine;
                result = result + row;
            }

            return result;

        }

        static  DateTime START = new DateTime(1970, 1, 1);
        public static  DateTime GetDateByTick(int tick)
        {

            DateTime result = START + TimeSpan.FromHours(tick + 2);
            return result;

        }

        public static int GetTickFromDate(DateTime date)
        {


            TimeSpan ts = date - START;

            int tick = (int)ts.TotalHours - 1;

            return tick;


        }


    }
}
