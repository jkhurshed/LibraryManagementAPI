using AutoMapper;
using Library.Dtos.BookDtos;
using Library.Dtos.CategoryDtos;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController(LibDbContext context, IMapper mapper) : ControllerBase
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
        return Ok(mapper.Map<List<CategoryGetDto>>(rootCategories));
    }
    
    /// <summary>
    /// Create category
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(CategoryCreateDto categoryDto)
    {
        var category = mapper.Map<Category>(categoryDto);
        
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
        
        var result = mapper.Map<CategoryGetDto>(category);
        
        return StatusCode(statusCode:201, value: result);
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
        
        mapper.Map(categoryDto, category);
        
        await context.SaveChangesAsync();
        return Ok(mapper.Map<CategoryGetDto>(category));
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
    
    /// <summary>
    /// Books list under a specific category
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/books")]
    public async Task<ActionResult<List<Category>>> GetBooksByCategoryId(Guid id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if(null == category) return NotFound();
        
        var books = await context.Books
            .Where(b => b.CategoryId == id)
            .Select(b => new BooksByCategoryDto()
            {
                CategoryId = b.CategoryId,
                Category = b.Category.Title,
                BookTitle = b.Title,
                PublishedDate = b.CreatedAt
            })
            .ToListAsync();
        
        return StatusCode(StatusCodes.Status200OK,
            mapper.Map<List<BooksByCategoryDto>>(books));
    }
}