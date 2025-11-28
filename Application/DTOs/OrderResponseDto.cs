namespace Application.DTOs
{
    public class OrderResponseDto
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

