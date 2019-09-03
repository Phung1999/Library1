using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.IO;
using LibraryCommanCore;
namespace Library.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //string s="";
            //string folderPath = Server.MapPath("~/Files/");

            ////Check whether Directory (Folder) exists.
            //if (!Directory.Exists(folderPath))
            //{
            //    //If Directory (Folder) does not exists. Create it.
            //    Directory.CreateDirectory(folderPath);
            //    s = "Succsess";
            //}

            //   string path = Server.MapPath("~/Files/Logger");
            //   if (!Directory.Exists(path))
            //{
            //       Directory.CreateDirectory(path);
            //       s = "Succsess 1";
            //}

            //HttpRuntime.AppDomainAppPath;
           // string folderparent= CreateFolder.FolderParent("Files");
            //string folderchildren = CreateFolder.FolderChildren(folderparent + "/" + "Logger");
            string datenow = DateTime.Now.ToString("yyyy-MM-dd");
           // string pathfile =Server.MapPath("~/Tuyen/Logger/"+datenow+".txt") ;
            string pathfile = "~/Files/Logger/" + datenow + ".txt";
            //bool kq = Logger.Createfile(pathfile);
            //Save the File to the Directory (Folder).
            //FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

            //Save vào File
            try
            {
                int a = 3, b = 0;
                int c = a / b;
            }
            catch (Exception e)
            {

                bool kq1 = Logger.Savefile(pathfile, ""+e);
                ViewBag.s = kq1;
            }
            
          
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}