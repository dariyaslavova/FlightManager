using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FlightManager.Data.Models
{
    public class Flight
    {
        public int Id { get; set; }

        [Required]
        public string FromLocation { get; set; } = null!;

        [Required]
        public string ToLocation { get; set; } = null!;

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public string PlaneType { get; set; } = null!;

        [Required]
        public string PlaneNumber { get; set; } = null!;

        [Required]
        public string PilotName { get; set; } = null!;

        public int CapacityEconomy { get; set; }
        public int CapacityBusiness { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}