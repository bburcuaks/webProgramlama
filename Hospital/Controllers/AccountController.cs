using Microsoft.AspNetCore.Mvc;
using Hospital.Models;
using Hospital.Entities;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        public AccountController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }


        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                string hashedPassword = DoMD5HashedString(model.Password);

                User user = _databaseContext.Users.SingleOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == hashedPassword);
                if (user != null)
                {
                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.Username), "Kullanıcı adı kilitli.");
                        return View(model);
                    }
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim("Username", user.Username));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));

                    ClaimsIdentity identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre doğru değil.");
                }
            }
            return View(model);

        }

        private string DoMD5HashedString(string s)
        {
            string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            string salted = s+ md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {

                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Kullanıcı adı zaten ekli.");
                    View(model);
                }
                string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                string saltedPassword = model.Password + md5Salt;
                string hashedPassword = saltedPassword.MD5();

                User user = new()
                {
                    Username = model.Username,
                    Password = hashedPassword
                };
                _databaseContext.Users.Add(user);
                int affectedRowCount = _databaseContext.SaveChanges();
                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "Kullanıcı eklenemedi.");
                }

                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            return View(model);
        }

        public IActionResult Profile()
        {
            ProfileInfoLoader();
            return View();
        }

        private void ProfileInfoLoader()
        {
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);
            ViewData["Username"] = user.Username;
        }

        [HttpPost]
        public IActionResult ProfileChangeFullname([Required][StringLength(50)]string? username)
        {

            if (ModelState.IsValid)
            {
                Guid userid=new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.SingleOrDefault(x=>x.Id==userid);
                user.Username = username;
                _databaseContext.SaveChanges();
                return RedirectToAction(nameof(Profile));
            }
            ProfileInfoLoader();
            return View("Profile");
           
        }

        [HttpPost]
        public IActionResult ProfileChangePassword([Required][MinLength(6)][MaxLength(16)] string? password)
        {

            if (ModelState.IsValid)
            {
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);
                user.Password = password;
                _databaseContext.SaveChanges();
                ViewData["results"] = "Şifre Değiştirildi";
            }
            ProfileInfoLoader();
            return View("Profile");

        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
