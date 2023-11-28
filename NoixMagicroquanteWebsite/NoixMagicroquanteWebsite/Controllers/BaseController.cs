using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NoixMagicroquanteWebsite.Controllers
{
    public class BaseController : Controller
    {
        protected NoixMagicroquanteWebsiteContext db;
        protected int? UserId => HttpContext.Session.GetInt32("UserId");
        protected int? IsAdmin => HttpContext.Session.GetInt32("IsAdmin");
        protected int? BasketId => HttpContext.Session.GetInt32("BasketId");

        public BaseController(NoixMagicroquanteWebsiteContext context)
        {
            db = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            ViewBag.UserId = UserId;
            ViewBag.IsAdmin = IsAdmin;
            ViewBag.BasketId = BasketId;
        }
    }
}
