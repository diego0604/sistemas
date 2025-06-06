using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateUserDto
    {
        public string Name { get; set; } = null!;
        public string? Password { get; set; }      
        public string? Email { get; set; }      
        public string Role { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
