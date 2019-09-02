using LibraryCommanCore;
using System.Web.Mvc;
using System.Web.Routing;


namespace Library.Areas.Admin.Controllers
{
    public class BaseController : Controller 
    {
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (CommonConstants.USER_SESSION != null)
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
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