using Library.Dtos.CategoryDtos;
using Library.Interfaces;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController
    (ICategoryService categoryService)
    : ControllerBase
{
    /// <summary>
    /// Get all existing Categories
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAllCategories()
    {
        return Ok(await categoryService.GetAllAsync());
    }
    
    /// <summary>
    /// Create category
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(CategoryCreateDto categoryDto)
    {
        var category = await categoryService.CreateAsync(categoryDto);
        return StatusCode(201, category);
    }

    /// <summary>
    /// Update the category by providing its id
    /// </summary>
    /// <param name="categoryDto"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Category>> UpdateCategory(CategoryCreateDto categoryDto, Guid id)
    {
        var category = await categoryService.UpdateAsync(id, categoryDto);
        return category == null ? NotFound() : Ok(category);
    }
    
    /// <summary>
    /// Delete the category by providing its id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Category>> DeleteCategory(Guid id)
    {
        var category = await categoryService.DeleteAsync(id);
        return category ? NoContent() : NotFound();
    }
    
    /// <summary>
    /// Books list under a specific category
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/books")]
    public async Task<ActionResult<List<Category>>> GetBooksByCategoryId(Guid id)
    {
        var category = await categoryService.GetBooksByCategoryId(id);
        return category == null ? NotFound() : Ok(category);
    }
}