using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightManager.Data;
using FlightManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Services.Services
{
    public class FlightService : IFlightService
    {
        private readonly ApplicationDbContext _context;

        public FlightService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Flight> GetAll()
        {
            return _context.Flights.ToList();
        }

        public Flight? GetById(int id)
        {
            return _context.Flights
                .Include(f => f.Reservations)
                .ThenInclude(r => r.Passengers)
                .FirstOrDefault(f => f.Id == id);
        }

        public void Create(Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();
        }

        public void Update(Flight flight)
        {
            _context.Flights.Update(flight);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.Id == id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Flight> Search(string? from, string? to, DateTime? date)
        {
            var query = _context.Flights.AsQueryable();

            if (!string.IsNullOrWhiteSpace(from))
                query = query.Where(f => f.FromLocation.Contains(from));

            if (!string.IsNullOrWhiteSpace(to))
                query = query.Where(f => f.ToLocation.Contains(to));

            if (date.HasValue)
                query = query.Where(f => f.DepartureTime.Date == date.Value.Date);

            return query.ToList();
        }

        public bool HasAvailableSeats(int flightId, int economyCount, int businessCount)
        {
            var flight = _context.Flights
                .Include(f => f.Reservations)
                .ThenInclude(r => r.Passengers)
                .FirstOrDefault(f => f.Id == flightId);

            if (flight == null)
                return false;

            int usedEconomy = flight.Reservations
                .SelectMany(r => r.Passengers)
                .Count(p => p.TicketType == "Economy");

            int usedBusiness = flight.Reservations
                .SelectMany(r => r.Passengers)
                .Count(p => p.TicketType == "Business");

            return usedEconomy + economyCount <= flight.CapacityEconomy
                && usedBusiness + businessCount <= flight.CapacityBusiness;
        }
        public IQueryable<Flight> GetAllFlights()
        {
            return _context.Flights.AsQueryable();
        }
    }
}
