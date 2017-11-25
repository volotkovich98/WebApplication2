using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    public class UsersController : Controller
    {
        private readonly sportcomplexContext _context;

        public UsersController(sportcomplexContext context)
        {
            _context = context;
        }


        // GET: /<controller>/ 
        public IActionResult Index()
        {
            return View();
        }

        // Get method for registration 
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        //Post method for registration 
        [HttpPost]
        public IActionResult Registration(string name, string surname, string email, string password)
        {
            if (_context.Users.Where(u => u.Email.Equals(email)).Count() == 0)
            {
                _context.Users.Add(new Users { Name = name, Surname = surname, Email = email, IsAdmin = false, Password = password });
                _context.SaveChanges();
                return View("Login");
            }
            else
            {
                ViewBag.Message = "User with this email exists, use other email or login with correct data!";
                return View("Error");
            }
        }

        //Get method for login 
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Method for user authentificate 
        private void Authentificate(Users user)
        {
            var claims = new List<Claim>
{
new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
new Claim(ClaimsIdentity.DefaultRoleClaimType, user.IsAdmin ? "admin" : "user")
};
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        //Post method for login 
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            Users user = _context.Users.FirstOrDefault(u => u.Email == login && u.Password == password);
            if (user != null)
            {
                Authentificate(user);
                
                    return RedirectToAction("Index", "Groups");  
            }
            else
            {
                ViewBag.Message = "No thats user. Try again or choose registration!";
                return View("Error");
            }
        }

        //Get method for login out 
        [Authorize(Roles = "admin, user")]
        public IActionResult Logout()
        {
            // HttpContext.Session.Clear(); 
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Index");
        }
    }
} 