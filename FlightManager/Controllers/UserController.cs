using FlightManager.Services.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

using FlightManager.Data.Models;
using FlightManager.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            
            var user = _userService.Login(username, password);

            if (user == null)
            {
                ViewBag.Error = "Грешно потребителско име или парола";
                return View();
            }

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult List()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return Unauthorized();

            var users = _userService.GetAll();
            return View(users);
        }
    }
}
