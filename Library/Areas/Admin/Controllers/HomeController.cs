using System.Web.Mvc;
using LibraryDatabase.Database;
using System.Linq;

namespace Library.Areas.Admin.Controllers
{
    public class HomeController :BaseController
    {
        // GET: Admin/Home
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
           // Session.Remove("USER_SESSION");
           // Session.Abandon();
            return RedirectToAction("Index", "LoginAjax");
        }
        //private LibraryDBEntities db =new LibraryDBEntities();
        [HasCredentialAtrribute(RoleID ="VIEW_USER")]
        public ActionResult ListUser()
        {
            
            return View();
        }
    }
}