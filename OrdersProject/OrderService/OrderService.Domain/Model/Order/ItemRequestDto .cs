using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Model.Order
{
    public class ItemRequestDto
    {
        [Required(ErrorMessage = "This Field is required")]
        [StringLength(20, ErrorMessage = "Name should be between 5 and 20", MinimumLength = 5)]
        public string Name { get; set; } = default!;
        [Required(ErrorMessage = "This Field is required")]
        [StringLength(20, ErrorMessage = "Description should be between 5 and 20", MinimumLength = 5)]

        public string? Description { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        [Range(1, 5000, ErrorMessage = "Price should be between 1 and 5000")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        [StringLength(20, ErrorMessage = "UpdatedBy should be between 5 and 20", MinimumLength = 5)]
        public string UpdatedBy { get; set; } = default!;
    }
}
