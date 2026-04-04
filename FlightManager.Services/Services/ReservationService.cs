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
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passengers)
                .ToList();
        }

        public Reservation? GetById(int id)
        {
            return _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passengers)
                .FirstOrDefault(r => r.Id == id);
        }

        public void Create(Reservation reservation, IEnumerable<Passenger> passengers)
        {
            reservation.Passengers = passengers.ToList();

            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public void Confirm(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation != null)
            {
                reservation.IsConfirmed = true;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var reservation = _context.Reservations
                .Include(r => r.Passengers)
                .FirstOrDefault(r => r.Id == id);

            if (reservation != null && !reservation.IsConfirmed)
            {
                _context.Passengers.RemoveRange(reservation.Passengers);
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Reservation> Search(string? email, int? flightId)
        {
            var query = _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passengers)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(r => r.Email.Contains(email));

            if (flightId.HasValue)
                query = query.Where(r => r.FlightId == flightId.Value);

            return query.ToList();
        }
    }
}