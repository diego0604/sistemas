using Application.DTOs;
using Application.Services.Abstractions;
using Domain.Entities;
using Domain.Repositories.Abstractions;

namespace Application.Services.Implementations;

public class SalesService : ISalesService
{
    private readonly  ISalesRepository _salesRepository;
    private readonly IProductRepository _productRepository;

    public SalesService(ISalesRepository salesRepository, IProductRepository productRepository)
    {
        _salesRepository = salesRepository;
        _productRepository = productRepository;
    }

    public async Task<SaleSummaryDto> CreateSaleAsync(SaleRequestDto dto)
    {
        var sale = new Sale();

        foreach (var itemDto in dto.Items)
        {
            var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
            if (product == null)
                throw new Exception($"Producto con ID {itemDto.ProductId} no existe.");

            if (product.Stock < itemDto.Quantity)
                throw new Exception($"Stock insuficiente para el producto {product.Name}.");

            var saleItem = new SaleItem
            {
                ProductId = product.Id,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price
            };
            sale.Items.Add(saleItem);

            product.Stock -= itemDto.Quantity;
            await _productRepository.UpdateAsync(product);
        }

        sale.TotalAmount = sale.Items.Sum(i => i.Quantity * i.UnitPrice);
        var created = await _salesRepository.CreateSaleAsync(sale);

        return new SaleSummaryDto
        {
            SaleId = created.Id,
            SaleDate = created.SaleDate,
            TotalAmount = created.TotalAmount,
            Items = created.Items.Select(i => new SaleItemDetailDto
            {
                ProductId = i.ProductId,
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };
    }

    public async Task<IEnumerable<SaleSummaryDto>> GetAllSalesAsync(DateTime? from = null, DateTime? to = null)
    {
        var sales = await _salesRepository.GetAllSalesAsync(from, to);
        return sales.Select(s => new SaleSummaryDto
        {
            SaleId = s.Id,
            SaleDate = s.SaleDate,
            TotalAmount = s.TotalAmount,
            Items = s.Items.Select(i => new SaleItemDetailDto
            {
                ProductId = i.ProductId,
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        });
    }
}
