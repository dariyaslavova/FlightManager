using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightManager.Data.Models;

namespace FlightManager.Services.Services
{
    public interface IFlightService
    {
        IEnumerable<Flight> GetAll();
        Flight? GetById(int id);
        void Create(Flight flight);
        void Update(Flight flight);
        void Delete(int id);
        IQueryable<Flight> GetAllFlights();

        IEnumerable<Flight> Search(string? from, string? to, DateTime? date);
        bool HasAvailableSeats(int flightId, int economyCount, int businessCount);
    }
}
