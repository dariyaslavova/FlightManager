using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } = null!; // имейл за потвърждение

        public bool IsConfirmed { get; set; } = false;

        public int FlightId { get; set; }
        public Flight Flight { get; set; } = null!;

        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
    }
}
