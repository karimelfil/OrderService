using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public string CreatedBy { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;



        [NotMapped]
        public ICollection<OrderItem> Items { get; set; }
    }
}
