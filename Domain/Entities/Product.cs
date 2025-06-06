namespace Domain.Entities
{
 

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool RequiresPrescription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
