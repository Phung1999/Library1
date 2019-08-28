using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class AccountModel
    {
        private Boolean _rememberme;
        public string Username { get; set; }
        public string  Password { get; set; }

        public bool Rememberme
        {
            get { return _rememberme; }
            set { _rememberme = value; }
        }
    }
}