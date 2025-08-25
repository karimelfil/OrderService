using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Model.Order
{
    public class CreateOrderRequestDto
    {
        [Required(ErrorMessage = "This Field is required")]
        [StringLength(20, ErrorMessage = " Customer FirstName should be between 5 and 20", MinimumLength = 5)]
        public string CustomerFirstName { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        [StringLength(20, ErrorMessage = "Customer LastName should be between 5 and 20", MinimumLength = 5)]
        public string CustomerLastName { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string CustomerEmail { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        [StringLength(20, ErrorMessage = "Customer LastName should be between 7 and 20", MinimumLength = 7)]
        public string CustomerPhone { get; set; }
        [Required(ErrorMessage = "This Field is required")]

        [StringLength(20, ErrorMessage = "CreatedBy should be between 5 and 20", MinimumLength = 5)]
        public string CreatedBy { get; set; }
        public List<CreateOrdersItemDto> Items { get; set; }
    }
}
