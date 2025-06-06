using Domain.Entities;

namespace Domain.Repositories.Abstractions
{
    public interface ISalesRepository
    {
        Task<Sale> CreateSaleAsync(Sale sale);
        Task<IEnumerable<Sale>> GetAllSalesAsync(DateTime? from = null, DateTime? to = null);
        Task<IEnumerable<SaleItem>> GetSaleItemsBySaleIdAsync(int saleId);
    }
}
