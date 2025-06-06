namespace Application.DTOs
{
    public class SaleRequestDto
    {
        public List<SaleItemDto> Items { get; set; } = new();
    }
}
