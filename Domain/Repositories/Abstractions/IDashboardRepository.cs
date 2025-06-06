using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Abstractions
{
 public interface IDashboardRepository
    {
        /// <summary>
        /// Retorna la suma total de ventas (TotalAmount) de la tabla Sales.
        /// </summary>
        Task<decimal> GetTotalSalesAsync();

        /// <summary>
        /// Retorna la suma total de cantidades vendidas (Quantity) de la tabla SaleItems.
        /// </summary>
        Task<int> GetTotalProductsSoldAsync();

        /// <summary>
        /// Retorna la cantidad de ventas registradas (número de filas en Sales).
        /// </summary>
        Task<int> GetSalesCountAsync();

     
    }
}
