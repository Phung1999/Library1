using LibraryCommanCore;
using System.Web.Mvc;
using System.Web.Routing;
using static LibraryCommanCore.CommonConstants;

namespace Library.Areas.Admin.Controllers
{
    public class BaseController : Controller 
    {
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (USER_SESSION != null)
                {
                    var session = (UserLogin)Session[USER_SESSION];
                    if (session == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(new
                            RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
                    }
                }

                base.OnActionExecuting(filterContext);
            }
            catch (System.Exception e)
            {
                Logger.Savefile("" + e);
            }
            
        }
    }
}