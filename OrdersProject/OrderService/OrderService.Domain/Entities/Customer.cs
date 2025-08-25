using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Customer
    {

        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        [MaxLength(50)]
        public string FirstName { get; set; }


        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public string CreatedBy { get; set; }


        [Required(ErrorMessage = "This Field is required")]
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;

        [NotMapped]
        public virtual ICollection<Orderr> Orderss { get; set; }

    }

}
