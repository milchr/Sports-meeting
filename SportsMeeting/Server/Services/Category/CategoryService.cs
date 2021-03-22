using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsMeeting.Server.Data;
using SportsMeeting.Server.Models;
using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Services
{
    public class CategoryService : ICategoryService
    {
        ApplicationDbContext _dbContext;
        ILogger<CategoryService> _logger;
        IMapper _mapper;

        public CategoryService(ApplicationDbContext dbContext, ILogger<CategoryService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task createCategory(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _dbContext.Category.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task deleteCategory(int Id)
        {
            var result = await _dbContext.Category
               .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _dbContext.Category.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<CategoryDto>> getAllCategories()
        {
            var category = await _dbContext.Category.ToListAsync();
            var categoryDto = _mapper.Map<List<CategoryDto>>(category);
            return categoryDto;
        }

        public async Task<CategoryDto> getCategory(int id)
        {
            var category = await _dbContext.Category.FirstOrDefaultAsync(c => c.Id == id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task updateCategory(int id, CategoryDto category)
        {
            var result = await _dbContext.Category
              .FirstOrDefaultAsync(e => e.Id == id);

            if (result != null)
            { 
                result.Name = category.Name;
                result.Description = category.Description;

                _dbContext.Category.Update(result);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
