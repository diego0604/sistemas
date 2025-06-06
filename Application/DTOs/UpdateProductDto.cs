using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool RequiresPrescription { get; set; }
    }
}
