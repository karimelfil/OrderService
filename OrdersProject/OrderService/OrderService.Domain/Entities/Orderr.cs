using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Orderr
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public DateTime OrderDate { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }


        [Required(ErrorMessage = "This Field is required")]
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;

        [NotMapped]
        public virtual ICollection<ItemOrder> OrderItems { get; set; }
    }
}
