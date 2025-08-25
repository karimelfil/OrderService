using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Model.Order
{
    public class OrderUpdateRequestDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public CustomerUpdateDto Customer { get; set; }
        public List<ItemOrderUpdateDto> Items { get; set; }
        public string UpdatedBy { get; set; }
    }
}
