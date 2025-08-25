using System.ComponentModel.DataAnnotations;


namespace OrderService.Domain.Model
{
    public class OrderDto
    {
        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Order date is required.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Total amount is required.")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Created by is required.")]
        public string CreatedBy { get; set; }
    }
}
