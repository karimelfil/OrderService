using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Model.Order
{
    public class CustomerUpdateDto
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        [StringLength(20, ErrorMessage = "FirstName should be between 5 and 20", MinimumLength = 5)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        [StringLength(20, ErrorMessage = "LastName should be between 5 and 20", MinimumLength = 5)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        public string Phone { get; set; }
    }
}
