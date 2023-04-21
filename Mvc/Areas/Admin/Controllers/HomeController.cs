using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ManageContext _manageContext;
        public HomeController(ManageContext manageContext)
        {
            _manageContext = manageContext;
        }
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
