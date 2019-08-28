using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace LibraryCommanCore
{
   public static class Logger
    {
       public static bool Createfile(this string path)
       {
           bool kq=false;
           if (!File.Exists(path))
           {
               File.CreateText(path);
               
               kq = true;
           }
           return kq;
       }
       public static bool Savefile(this string path,string noidung)
       {          
           bool kq = false;
           path = HttpContext.Current.Server.MapPath(path);
           StreamWriter file = new StreamWriter(path,true,Encoding.UTF8);
           file.WriteLine(DateTime.Now.ToString()+noidung);
           file.Close();             
           return kq;
       }
      public static void Savefile(this object noidung)
        {
            string datenow = DateTime.Now.ToString("yyyy-MM-dd");
            string pathfile = "~/Files/Logger/" + datenow + ".txt";
          
           string path = HttpContext.Current.Server.MapPath(pathfile);
            StreamWriter file = new StreamWriter(path, true, Encoding.UTF8);
            file.WriteLine(DateTime.Now.ToString() + noidung);
            file.Close();          
        }
    }
}
