using AutoMapper;
using DigitalProductsApi.src.Application.DTOs.Categories;
using DigitalProductsApi.src.Application.Interfaces;
using DigitalProductsApi.src.Domain.Entities;
using DigitalProductsApi.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DigitalProductsApi.src.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //GetAllAsync
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }
        //GetByIdAsync
        public async Task<CategoryDto?> GetByIdAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            // check later which kind of if statement is better to use 
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }
        //CreateAsync
        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            _context.Categories.Add(category);

            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }
        //UpdateAsync
        public async Task<bool> UpdateAsync(Guid id, UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _mapper.Map(dto, category);
            await _context.SaveChangesAsync();
            return true;
        }
        //DeleteAsync
        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
