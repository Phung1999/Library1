using Library.Models;
using LibraryCommanCore;
using LibraryDatabase.Dao;
using System.Web.Mvc;
using Library.Areas.Admin.Models;

namespace Library.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }      
        public ActionResult Login(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dao = new UserDao();
                    //var result = dao.Login(model.Username, Encryptor.MD5Hash(model.Password), true);
                    var result = dao.Login(model.Username, model.Password, true);
                    if (result == 1)
                    {
                        var user = dao.GetById(model.Username);
                        string Role = dao.GetRole(model.Username);
                        var userSession = new UserLogin()
                        {
                            UserName = user.UserName,
                            GroupID = user.IDUserGroup
                        };
                        var listCredentials = dao.GetListCredential(model.Username);
                        Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        // Session.Add(""+userSession.UserName);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (result == 0)
                    {
                        ModelState.AddModelError("RememberMe", "Tài khoản không tồn tại.");
                    }
                    else if (result == -1)
                    {
                        ModelState.AddModelError("RememberMe", "Tài khoản đang bị khoá.");
                    }
                    else if (result == -2)
                    {
                        ModelState.AddModelError("RememberMe", "Mật khẩu không đúng.");
                    }
                    else if (result == -3)
                    {
                        ModelState.AddModelError("RememberMe", "Tài khoản của bạn không có quyền đăng nhập.");
                    }
                    else
                    {
                        ModelState.AddModelError("RememberMe", "đăng nhập không đúng.");
                    }
                }
                catch (System.Exception error)
                {
                   
                    Logger.Savefile(""+error);
                }
               
            }
            return View("Index");
        }

        public ActionResult LoginAjax(AcountModel model)
        {

            return View();
        }

    }
}