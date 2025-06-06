using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstractions
{
    public interface ISalesService
    {
        Task<SaleSummaryDto> CreateSaleAsync(SaleRequestDto dto);
        Task<IEnumerable<SaleSummaryDto>> GetAllSalesAsync(DateTime? from = null, DateTime? to = null);
    }
}
