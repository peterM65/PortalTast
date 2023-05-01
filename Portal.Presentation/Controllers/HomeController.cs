using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Portal.Presentation.Language;

namespace Portal.Presentation.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<SharedResource> stringLocalizer;

        public HomeController(IStringLocalizer<SharedResource> stringLocalizer)
        {
            this.stringLocalizer = stringLocalizer;
        }
        public IActionResult Index()
        {
            ViewBag.Dashboard = stringLocalizer["Dashboard"];
            return View();
        }
    }
}
