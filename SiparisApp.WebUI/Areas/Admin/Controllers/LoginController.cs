using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using BL;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace SiparisApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IRepository<User> _repository;

        public LoginController(IRepository<User> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexAsync(User user)
        {
            try
            {
                var kullanici = await _repository.FirstOrDefaultAsync(u => u.UserName == user.UserName & u.Password == user.Password & u.IsActive == true);
                if (kullanici == null)
                {
                    ModelState.AddModelError("", "Kullanıcı Bulunamadı!");
                }
                else
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email, kullanici.Email)
                    };
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/Admin/Home");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(user);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(); // Çıkış işlemi yap
            return RedirectToAction("Index", "Login"); // ve sayfayı logine yönlendir
        }
    }
}
