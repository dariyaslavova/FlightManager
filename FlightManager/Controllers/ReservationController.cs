using Microsoft.AspNetCore.Mvc;
using FlightManager.Data;
using FlightManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST
        public IActionResult Index()
        {
            // Only Employee
            if (HttpContext.Session.GetString("Role") != "Employee")
                return Unauthorized();

            var reservations = _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passengers)
                .ToList();

            return View(reservations);
        }

        // CREATE (STEP 1)
        public IActionResult Create(int flightId)
        {
            if (HttpContext.Session.GetString("Role") != "Employee")
                return Unauthorized();

            var flight = _context.Flights.Find(flightId);
            if (flight == null) return NotFound();

            ViewBag.Flight = flight;
            return View();
        }

        [HttpPost]
        public IActionResult Create(int flightId, string email)
        {
            if (HttpContext.Session.GetString("Role") != "Employee")
                return Unauthorized();

            var flight = _context.Flights.Find(flightId);
            if (flight == null) return NotFound();

            var reservation = new Reservation
            {
                FlightId = flightId,
                Email = email,
                IsConfirmed = false
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return RedirectToAction("AddPassengers", new { id = reservation.Id });
        }

        // ADD PASSENGERS (STEP 2)
        public IActionResult AddPassengers(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Employee")
                return Unauthorized();

            var reservation = _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passengers)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null) return NotFound();

            return View(reservation);
        }

        [HttpPost]
        public IActionResult AddPassengers(int reservationId, Passenger passenger)
        {
            if (HttpContext.Session.GetString("Role") != "Employee")
                return Unauthorized();

            var reservation = _context.Reservations
                .Include(r => r.Passengers)
                .FirstOrDefault(r => r.Id == reservationId);

            if (reservation == null) return NotFound();

            passenger.ReservationId = reservationId;

            _context.Passengers.Add(passenger);
            _context.SaveChanges();

            return RedirectToAction("AddPassengers", new { id = reservationId });
        }

        // CONFIRM (STEP 3)
        public IActionResult Confirm(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Employee")
                return Unauthorized();

            var reservation = _context.Reservations
                .Include(r => r.Passengers)
                .Include(r => r.Flight)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null) return NotFound();

            reservation.IsConfirmed = true;
            _context.SaveChanges();

            return View(reservation);
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Employee")
                return Unauthorized();

            var reservation = _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passengers)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null) return NotFound();

            return View(reservation);
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Employee")
                return Unauthorized();

            var reservation = _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passengers)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null) return NotFound();

            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Employee")
                return Unauthorized();

            var reservation = _context.Reservations
                .Include(r => r.Passengers)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null) return NotFound();

            _context.Passengers.RemoveRange(reservation.Passengers);
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
