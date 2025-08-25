using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Model
{
    public class UpdateOrderItemDto : OrderItemDto
    {
        [Required(ErrorMessage = "Updated by is required")]
        [StringLength(50, ErrorMessage = "Updated by cannot exceed 50 characters")]
        public string UpdatedBy { get; set; }
    }
}
