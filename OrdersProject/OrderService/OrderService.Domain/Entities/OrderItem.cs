using System.ComponentModel.DataAnnotations;


namespace OrderService.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "This Field is required")]
        public string CreatedBy { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public DateTime CreatedDate { get; set; }



        public string? UpdatedBy { get; set; }


        public DateTime? UpdatedDate { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "This Field is required")]
        public bool IsDeleted { get; set; } = false;

        public Order Order { get; set; }
    }
}
