using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Model.Order
{
    public class CreateOrderResponseDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<int> ItemIds { get; set; }
    }
}
