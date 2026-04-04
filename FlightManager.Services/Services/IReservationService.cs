using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightManager.Data.Models;

namespace FlightManager.Services.Services
{
    public interface IReservationService
    {
        IEnumerable<Reservation> GetAll();
        Reservation? GetById(int id);
        void Create(Reservation reservation, IEnumerable<Passenger> passengers);
        void Confirm(int id);
        void Delete(int id);
        IEnumerable<Reservation> Search(string? email, int? flightId);
    }
}
