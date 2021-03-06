using SportsMeeting.Server.Models;
using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Services
{
    public interface ICategoryService
    {
        public Task<List<CategoryDto>> getAllCategories();
        public Task<CategoryDto> getCategory(int id);
        public Task createCategory(CreateCategoryDto dto);
        public Task deleteCategory(int Id);
        public Task updateCategory(int id, CategoryDto category);

    }
}
