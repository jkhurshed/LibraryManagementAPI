using Library.Dtos.CategoryDtos;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController(LibDbContext context) : ControllerBase
{
    /// <summary>
    /// Get all existing Categories
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAllCategories()
    {
        var categories = await context.Categories
            .Include(c => c.SubCategories)
            .ToListAsync();

        var categoryDict = categories.ToDictionary(c => c.Id);
        foreach (var category in categories)
        {
            category.SubCategories = categories
                .Where(c => c.ParentCategoryId == category.Id)
                .ToList();
        }
        
        var rootCategories = categories
            .Where(c => c.ParentCategoryId == null)
            .ToList();
        return Ok(rootCategories);
    }
    
    /// <summary>
    /// Create category
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(CategoryCreateDto categoryDto)
    {
        var category = new Category
        {
            Title = categoryDto.Title,
            Description = categoryDto.Description,
            ParentCategoryId = categoryDto.ParentCategoryId
        };
        
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
        
        return Ok(category);
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
        var category = await context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        category.Title = categoryDto.Title;
        category.Description = categoryDto.Description;
        category.ParentCategoryId = categoryDto.ParentCategoryId;
        await context.SaveChangesAsync();
        return Ok(category);
    }
    
    /// <summary>
    /// Delete the category by providing its id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Category>> DeleteCategory(Guid id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return Ok(category);
    }
}