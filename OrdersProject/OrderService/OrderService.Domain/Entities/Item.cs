    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace OrderService.Domain.Entities
    {
        public class Item
        {
            [Key]
            public int ItemId { get; set; }

            [Required]
            [MaxLength(100)]
            public string Name { get; set; }

            [MaxLength(500)]
            public string Description { get; set; }

            [Required]
            public decimal Price { get; set; }



            [Required(ErrorMessage = "This Field is required")]
            public string CreatedBy { get; set; }

            [Required(ErrorMessage = "This Field is required")]
            public DateTime CreatedDate { get; set; }
            public string? UpdatedBy { get; set; }
            public DateTime? UpdatedDate { get; set; }
            public bool IsActive { get; set; } = true;

            [NotMapped]
            public virtual ICollection<ItemOrder> ItemOrder { get; set; }
        }
    }
