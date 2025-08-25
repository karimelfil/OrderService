using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Login
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        [MaxLength(255)] 
        public string Password { get; set; }

        [Required]
        [MaxLength(100)] 
        public string Salt { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "user"; 

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public bool IsDeleted { get; set; } = false;

    }
}
