using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Library.Areas.Admin.Models
{
    public class EmployeesModel
    {
        //[Required]
        //[Display(Name = "STT")]
        //public int EmployeeID { get; set; }
        [Required]
        [Display(Name="Họ và tên nhân viên")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Chức vụ")]
        [StringLength(50)]
        public string Position { get; set; }
        
        [Display(Name = "Văn phòng tại")]
        [StringLength(50)]
        public string Office { get; set; }
        
        [Display(Name = "Tuổi ")]
        public int? Age { get; set; }
        
        [Display(Name = "Lương")]
        public int? Salary { get; set; }

        [Display(Name = "Đất nước")]
        public int? IDCountry { get; set; }
        [Display(Name = "Đất nước")]
        public string NameCountry { get; set; }
   
        [Display(Name = "Phòng ban")]
        public int? IDDeparment { get; set; }
        [Display(Name = "Phòng ban")]
        public string NameDeparment { get; set; }
    }
}