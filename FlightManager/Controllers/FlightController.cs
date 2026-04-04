using FlightManager.Data.Models;
using FlightManager.Services.Services;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;

namespace FlightManager.Web.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        // LIST + FILTERS + PAGING
        public IActionResult Index(string from, string to, int page = 1)
        {
            // Only logged users (Admin or Employee)
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "User");

            var flights = _flightService.GetAllFlights();

            if (!string.IsNullOrEmpty(from))
                flights = flights.Where(f => f.FromLocation.Contains(from));

            if (!string.IsNullOrEmpty(to))
                flights = flights.Where(f => f.ToLocation.Contains(to));

            flights = flights.OrderBy(f => f.DepartureTime);

            var pagedFlights = flights.ToPagedList(page, 10);

            return View(pagedFlights);
        }

        // CREATE GET
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "User");

            return View();
        }

        // CREATE POST
        [HttpPost]
        public IActionResult Create(Flight flight)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "User");

            if (!ModelState.IsValid)
                return View(flight);

            _flightService.Create(flight);
            return RedirectToAction("Index");
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "User");

            var flight = _flightService.GetById(id);
            if (flight == null)
                return NotFound();

            return View(flight);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(Flight flight)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "User");

            if (!ModelState.IsValid)
                return View(flight);

            _flightService.Update(flight);
            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "User");

            _flightService.Delete(id);
            return RedirectToAction("Index");
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "User");

            var flight = _flightService.GetById(id);
            if (flight == null)
                return NotFound();

            return View(flight);
        }
    }
}
