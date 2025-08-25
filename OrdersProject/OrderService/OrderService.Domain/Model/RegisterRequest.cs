using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Model
{
    public class RegisterRequest
    {
        [Required]
        [MinLength(8)]
        [MaxLength(15)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(15)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = "user"; // Default role
    }
}
