using Application.DTOs;
using Application.Services.Abstractions;
using Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Application.Services.Implementations;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _dashboardRepository;

    public DashboardService(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        var totalSales = await _dashboardRepository.GetTotalSalesAsync();

        var totalProductsSold = await _dashboardRepository.GetTotalProductsSoldAsync();

        // 3) Cálculo de ticket promedio
        decimal averageTicket = 0;
        var saleCount = await _dashboardRepository.GetSalesCountAsync();
        if (saleCount > 0)
        {
            averageTicket = totalSales / saleCount;
        }
       


        return new DashboardSummaryDto
        {
            TotalSales = totalSales,
            TotalProductsSold = totalProductsSold,
            AverageTicket = averageTicket,
        };
    }
}