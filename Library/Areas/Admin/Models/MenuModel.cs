using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Areas.Admin.Models
{
    public class MenuModel
    {
        public  int Id { get; set; }
        public string MenuName { get; set; }
        public  string NameUrl { get; set; }
        public int ParentId { get; set; }
        public  List<MenuModel> List { get; set; }
    }
}