﻿using AutoMapper;
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
            _dbContext.Category.Add(category);
            _dbContext.SaveChanges();
        }

        public async Task<List<CategoryDto>> getAllCategories()
        {
            var category = _dbContext.Category.ToListAsync();
            var categoryDto = _mapper.Map<List<CategoryDto>>(category);
            return categoryDto;
        }
    }
}
