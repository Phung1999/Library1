using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Areas.Admin.Models
{
    public class User
    {
        [Key]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; internal set; }
        public DateTime CreateDate { get; set; }

        public DateTime? LastEdit { get; set; }

     

      
        public bool IsBlocked { get; internal set; }
        public int? IDRole { get; set; }

    }
}