using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Areas.Admin.Models
{
    public class DeparmentModel
    {
        //public int DeparmentID { get; set; }
        [Required]
        [Display(Name="Tên phòng ban")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "ID Cha")]
        public int IdParent { get; set; }
        [Required]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Ngày tạo")]
        public string CreateDate { get; set; }
        [Required]
        [Display(Name = "Ngày sửa")]
        public string LastEdit { get; set; }
        [Required]
        [Display(Name = "Nhóm dùng")]
        public int IDGroupUser { get; set; }
    }
}