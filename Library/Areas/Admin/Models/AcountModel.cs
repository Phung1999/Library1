using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Library.Areas.Admin.Models
{
    public class AcountModel
    {
        public class Account
        {
            [Required(ErrorMessage = "Mời nhập user name")]
            [Display(Name = "User name")]
            public string UserName { set; get; }

            [Required(ErrorMessage = "Mời nhập password")]
            [Display(Name = "Password")]
            public string Password { set; get; }

            [Display(Name = "Remember me ?")]
            public bool RememberMe { set; get; }
        }
    }
}