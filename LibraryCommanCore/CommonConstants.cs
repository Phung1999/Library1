﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCommanCore
{
   public class CommonConstants
    {
        public static string USER_SESSION = "USER_SESSION";
        public static string SESSION_CREDENTIALS = "SESSION_CREDENTIALS";
        public static string CartSession = "CartSession";
       
        public static string Role = "";
        public static string MEMBER_GROUP = "MEMVBER";
        public static string ADMIN_GROUP = "ADMIN";
        public static string MOD_GROUP = "MOD";
        public static string CurrentCulture { set; get; }
        //public static object ADMIN_GROUP { get; set; }
    }
}
