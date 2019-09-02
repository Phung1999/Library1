using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCommanCore
{
    public static  class StringCore
    {
        //public static string[] CutString(string chuoi, char kytucat)
        //{
        //    if (kytucat != null)
        //    {
        //        string[] str = chuoi.Split(kytucat);
        //        return str;
        //    }
        //}
        //public static string Up(this string chuoi)
        //{
        //    chuoi.();
        //}

        public static string LowCase(this string chuoi)
        {
            return chuoi.ToLower();
        }
    }
}
