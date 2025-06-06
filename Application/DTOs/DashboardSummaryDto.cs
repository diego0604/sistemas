using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{

    public class DashboardSummaryDto
    {
        public decimal TotalSales { get; set; }
        public int TotalProductsSold { get; set; }
        public decimal AverageTicket { get; set; }

    }

    public class MonthlySalesDto
    {
        public string Month { get; set; } = null!;
        public decimal Amount { get; set; }
    }

    public class TopProductDto
    {
        public string ProductName { get; set; } = null!;
        public int QuantitySold { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
