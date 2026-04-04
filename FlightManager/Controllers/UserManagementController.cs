using FlightManager.Data;
using FlightManager.Data.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;

namespace FlightManager.Web.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserManagementController(ApplicationDbContext context)
        {
            _context = context;
        }


        // LIST + FILTERS + PAGING
        public IActionResult Index(string email, string username, string firstName, string lastName, int page = 1)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return Unauthorized();

            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(email))
                users = users.Where(u => u.Email.Contains(email));

            if (!string.IsNullOrEmpty(username))
                users = users.Where(u => u.Username.Contains(username));

            if (!string.IsNullOrEmpty(firstName))
                users = users.Where(u => u.FirstName.Contains(firstName));

            if (!string.IsNullOrEmpty(lastName))
                users = users.Where(u => u.LastName.Contains(lastName));

            return View(users.OrderBy(u => u.Id).ToPagedList(page, 10));
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return Unauthorized();

            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // CREATE GET
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return Unauthorized();

            return View();
        }

        // CREATE POST
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return Unauthorized();

            if (!ModelState.IsValid)
                return View(user);

            if (!_context.Users.Any())
                user.Role = "Admin";
            else
                user.Role = "Employee";

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return Unauthorized();

            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return Unauthorized();

            if (!ModelState.IsValid)
                return View(user);

            _context.Users.Update(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE GET
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return Unauthorized();

            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return Unauthorized();

            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
