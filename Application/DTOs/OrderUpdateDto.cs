using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class OrderUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Product { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
