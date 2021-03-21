using Microsoft.AspNetCore.Mvc;
using SportsMeeting.Server.Services;
using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportsMeeting.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> getCategories()
        {
            return Ok(await _categoryService.getAllCategories());
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> getCategory([FromRoute] int id)
        {
            return Ok(await _categoryService.getCategory(id));
        }

        // POST api/<CategoryController>
        [HttpPost("new_category")]
        public async Task<IActionResult> createParticiapnt([FromBody] CreateCategoryDto dto)
        {
            await _categoryService.createCategory(dto);
            return StatusCode(201);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> updateCategory([FromRoute] int id, [FromBody] CategoryDto dto)
        {
            await _categoryService.updateCategory(id, dto);
            return Ok();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> deleteCategory([FromRoute] int id)
        {
            await _categoryService.deleteCategory(id);
            return Ok();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> editParticiapnt([FromBody] CategoryDto dto)
        {
            var categoryId = dto.Id;
            await _categoryService.updateCategory(categoryId, dto);
            return Ok();
        }
    }
}
