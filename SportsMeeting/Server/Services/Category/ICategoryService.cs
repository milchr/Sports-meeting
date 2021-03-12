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

        public Task createCategory(CreateCategoryDto dto);
        public Task<Category> deleteCategory(int Id);
        public Task<Category> updateConversation(Category category);

    }
}
