using Application.DTOs;
using Application.Services.Abstractions;
using Domain.Entities;
using Domain.Repositories.Abstractions;

namespace Application.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var p = await _productRepository.GetByIdAsync(id);
        if (p == null) return null;
        return MapToDto(p);
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync(string? searchTerm = null, string? category = null)
    {
        var products = await _productRepository.GetAllAsync(searchTerm, category);
        return products.Select(MapToDto);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Category = dto.Category,
            Price = dto.Price,
            Stock = dto.Stock,
            ExpirationDate = dto.ExpirationDate,
            RequiresPrescription = dto.RequiresPrescription
        };
        await _productRepository.AddAsync(product);
        return MapToDto(product);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return false;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Category = dto.Category;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.ExpirationDate = dto.ExpirationDate;
        product.RequiresPrescription = dto.RequiresPrescription;

        await _productRepository.UpdateAsync(product);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return false;
        await _productRepository.DeleteAsync(product);
        return true;
    }

    private static ProductDto MapToDto(Product p) =>
        new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Category = p.Category,
            Price = p.Price,
            Stock = p.Stock,
            ExpirationDate = p.ExpirationDate,
            RequiresPrescription = p.RequiresPrescription,
            CreatedAt = p.CreatedAt
        };
}
