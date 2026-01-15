using AutoMapper;
using DigitalProductsApi.src.Application.DTOs.Products;
using DigitalProductsApi.src.Application.Interfaces;
using DigitalProductsApi.src.Domain.Entities;
using DigitalProductsApi.src.Domain.Exceptions;
using DigitalProductsApi.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DigitalProductsApi.src.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GetAllAsync
        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        // GetByIdAsync
        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null)
                //Custom Exception Class
                throw new NotFoundException(nameof(Product), id);

            return _mapper.Map<ProductDto>(product);
        }
        // CreateAsync
        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            _context.Products.Add(product);

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }
        //UpdateAsync
        public async Task<bool> UpdateAsync(Guid id, UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();
            return true;
        }
        //DeleteAsync
        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
