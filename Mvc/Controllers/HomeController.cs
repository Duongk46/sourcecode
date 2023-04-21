using Entities.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ManageContext _db;
        public HomeController(ILogger<HomeController> logger, ManageContext dbContext)
        {
            _logger = logger;
            _db = dbContext;
        }

        public IActionResult Index()
        {
            try
            {
                var ID_1 = _db.Categories.SingleOrDefault(x => x.Status == true && x.Name == "Ban lãnh đạo viện").ID;
                var ID_2 = _db.Categories.SingleOrDefault(x => x.Status == true && x.Name == "Hội đồng khoa học và đào tạo").ID;
                var ID_3 = _db.Categories.SingleOrDefault(x => x.Status == true && x.Name == "Tuyển sinh").ID;
                var ID_4 = _db.Categories.SingleOrDefault(x => x.Status == true && x.Name == "Đào tạo").ID;
                var model = _db.Products.Where(x => x.Status == true && x.IDCategory != ID_1 && x.IDCategory != ID_2).OrderByDescending(x => x.CreatedDate).Take(6);
                ViewData["ListTuyenSinh"] = _db.Categories.Where(x => x.Status == true && x.IDParent == ID_3).ToList();
                ViewData["ListDaoTao"] = _db.Categories.Where(x => x.Status == true && x.IDParent == ID_4).ToList();
                return View(model);
            }
            catch(Exception ex)
            {
                return Redirect("/Home");
            }
            
        }
        [Route("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index", "Home");
        }

        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid) return View(loginModel);
            try
            {
                var account = _db.Accounts.SingleOrDefault(x => x.Username == loginModel.Username);
                if (account != null)
                {
                    if (account.Password == loginModel.Password)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,account.Username)
                        };
                        var identity = new ClaimsIdentity(
                            claims,CookieAuthenticationDefaults.AuthenticationScheme
                            );
                        var princial = new ClaimsPrincipal(identity);
                        var props = new AuthenticationProperties();
                        HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,princial, props).Wait();
                        return Redirect("/Admin");
                    }
                    ViewBag.Message = "Mật khẩu không chính xác";
                }
                else
                {
                    ViewBag.Message = "Tài khoản không tồn tại";
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Đăng nhập thất bại";
            }
            return View(loginModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}