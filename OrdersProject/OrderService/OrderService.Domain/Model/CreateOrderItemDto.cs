using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Model
{
    public class CreateOrderItemDto : OrderItemDto
    {
        [Required(ErrorMessage = "Created by is required")]
        [StringLength(50, ErrorMessage = "Created by cannot exceed 50 characters")]
        public string CreatedBy { get; set; }
    }

}
