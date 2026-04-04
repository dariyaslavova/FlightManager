using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManager.Data.Models { 
public class User
{
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string EGN { get; set; } = null!;

        public string Address { get; set; } = null!;

        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string Role { get; set; } = "Employee"; // Admin / Employein
    }
}
