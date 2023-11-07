using Microsoft.AspNetCore.Mvc;

namespace NoixMagicroquanteWebsite.Controllers
{
    public class BaseController : Controller
    {
        protected NoixMagicroquanteWebsiteContext db;

        public BaseController(NoixMagicroquanteWebsiteContext context)
        {
            db = context;
        }

    }
}
