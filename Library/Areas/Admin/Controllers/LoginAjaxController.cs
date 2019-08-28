using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;
using LibraryDatabase.Database;
using System.Linq.Dynamic;
using Library.Areas.Admin.Models;
using LibraryCommanCore;

namespace Library.Areas.Admin.Controllers
{
    public class LoginAjaxController : Controller
    {
        private LibraryDBEntities db = new LibraryDBEntities();
        // GET: Admin/LoginAjax
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Login(AccountModel model)
        {
            string result = "Fail";
            try
            {
                var DataItem = db.Users.Where(x => x.UserName == model.Username && x.PassWord == model.Password)
              .FirstOrDefault();
                if (DataItem != null)
                {
                    Session["UserID"] = DataItem.UserID.ToString();
                    Session["UserName"] = DataItem.UserName.ToString();
                    Session["IDMainMenu"] = DataItem.IDMainMenu.ToString();
                    result = "Success";
                }
            }
            catch (Exception e)
            {
                //Logger.Savefile("e);
                e.Savefile();

            }
            return Json(result, JsonRequestBehavior.AllowGet);   
        }
        public ActionResult AfterLogin()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "LoginAjax");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            // Session.Remove("USER_SESSION");
            // Session.Abandon();
            return RedirectToAction("Index", "LoginAjax");
        }
        [HttpPost]
        public ActionResult GetList()
        {
            try
            {
                //Server Side Parameter
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"]?.Trim() ?? string.Empty;
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];
                //chuyển biến dữ liệu kiểu đối tượng

               // List<Employee> empList = new List<Employee>();
                using (LibraryDBEntities db = new LibraryDBEntities())
                {
                    var totalrows = db.Employees.Count();
                    IQueryable<Employee> data;
                    var totalrowsafterfiltering = 0;
                    if (string.IsNullOrEmpty(searchValue))
                    {
                        data = db.Employees;
                        totalrowsafterfiltering = totalrows;
                    }
                    else
                    {
                        data = db.Employees.
                            Where(x => x.Name.Contains(searchValue) || x.Position.Contains(searchValue) || x.Office.Contains(searchValue) || x.Age.ToString().Contains(searchValue) || x.Salary.ToString().Contains(searchValue));
                        totalrowsafterfiltering = data.Count();
                    }

                   
                    var employees = data.OrderBy(sortColumnName + " " + sortDirection).Skip(start).Take(length).ToList();
                    return Json(new { data = employees, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Logger.Savefile("" + e);
            }
            return Json("0",JsonRequestBehavior.AllowGet);
        }
        public ActionResult Menu()
        {
            try
            {
                if (Session["IDMainMenu"] != null)
                {
                    List<MenuModel> listmenu = new List<MenuModel>();
                    string cut = Session["IDMainMenu"].ToString().Trim();
                    if (cut.Count() > 1)
                    {
                        string[] cutnow = cut.Split(',');
                        for (int i = 0; i < cut.Count() - 1; i++)
                        {
                            int idmain = int.Parse(cutnow[i]);
                            var DataMenu = db.MainMenus.Where(x => x.MainMenuID == idmain).FirstOrDefault();
                            listmenu.Add(new MenuModel() { Id = idmain, MenuName = DataMenu.Name, NameUrl = DataMenu.MainURL, ParentId = 0 });
                            var DataSub = db.SubMenus.Where(x => x.IDMainMenu == idmain).ToList();
                            if (DataSub.Count > 0)
                                for (int j = 0; j < DataSub.Count; j++)
                                    listmenu.Add(new MenuModel() { Id = idmain, MenuName = DataSub[j].Name, NameUrl = DataSub[j].SubURL, ParentId = Convert.ToInt32(DataSub[j].IDMainMenu) });
                        }
                    }
                    else
                    {
                        //int idmain = int.Parse(cut);
                        if (!int.TryParse(cut, out var idmain))
                        {
                            idmain = 0;
                        }                        
                        var DataMenu = db.MainMenus.Where(x => x.MainMenuID == idmain).FirstOrDefault();
                        listmenu.Add(new MenuModel() { Id = idmain, MenuName = DataMenu.Name, NameUrl = DataMenu.MainURL, ParentId = 0 });
                        var DataSub = db.SubMenus.Where(x => x.IDMainMenu == idmain).ToList();
                        if (DataSub.Count > 0)
                            for (int j = 0; j < DataSub.Count; j++)
                                listmenu.Add(new MenuModel() { Id = idmain, MenuName = DataSub[j].Name, NameUrl = DataSub[j].SubURL, ParentId = Convert.ToInt32(DataSub[j].IDMainMenu) });
                    }
                    ViewBag.MenuLevel1 = listmenu.ToList();
                    //ViewBag.MenuLevel1 = GetMenuTree(listmenu, null);
                    //return View();
                    //return PartialView("~/Areas/Admin/Views/LoginAjax/Menu.cshtml");
                    return PartialView("~/Areas/Admin/Views/LoginAjax/MenuViewBag.cshtml");
                }
            }
            catch (Exception e)
            {
                Logger.Savefile("" + e);

            }
            return RedirectToAction("Index", "LoginAjax");

        }
        private List<MenuModel> GetMenuTree(List<MenuModel> list, int? parentId)
        {
            return list.Where(x => x.ParentId == parentId).Select(x => new MenuModel()
            {
                Id = x.Id,
                MenuName = x.MenuName,
                ParentId = x.ParentId,
                NameUrl = x.NameUrl,
                List = GetMenuTree(list, x.Id)
            }).ToList();
        }
    }
}