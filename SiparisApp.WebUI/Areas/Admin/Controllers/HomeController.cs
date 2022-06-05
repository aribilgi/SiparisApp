using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiparisApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize] // .net core da admin sayfalarında bu attribute ü yazmazsak sayfa açılmıyor!
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
