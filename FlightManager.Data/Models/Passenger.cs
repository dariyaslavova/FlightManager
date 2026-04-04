using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace FlightManager.Data.Models
{
    public class Passenger
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string MiddleName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string EGN { get; set; } = null!;

        [Phone]
        public string Phone { get; set; } = null!;

        [Required]
        public string Nationality { get; set; } = null!;

        [Required]
        public string TicketType { get; set; } = null!; // Business / Economy

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;
    }
}
