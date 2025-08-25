using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Model.Order
{
    public class ItemOrderUpdateDto
    {
        public int ItemId { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        [Range(1, 5000, ErrorMessage = "Quantity should be between 1 and 5000")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        [Range(1, 5000, ErrorMessage = "UnitPrice should be between 1 and 5000")]
        public decimal UnitPrice { get; set; }
    }
}
