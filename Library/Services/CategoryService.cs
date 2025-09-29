using AutoMapper;
using Library.Dtos.BookDtos;
using Library.Dtos.CategoryDtos;
using Library.Interfaces;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class CategoryService(LibDbContext context, IMapper mapper)
    : CrudService<Category, CategoryGetDto, CategoryCreateDto, CategoryCreateDto>
        (context, mapper), ICategoryService
{
    public async Task<CategoryGetDto?> GetAllCategories()
    {
        var categories = await context.Categories
            .Include(c => c.SubCategories)
            .ToListAsync();

        foreach (var category in categories)
        {
            category.SubCategories = categories
                .Where(c => c.ParentCategoryId == category.Id)
                .ToList();
        }
        
        var rootCategories = categories
            .Where(c => c.ParentCategoryId == null)
            .ToList();

        return mapper.Map<CategoryGetDto>(rootCategories);
    }

    public async Task<List<BooksByCategoryDto>?> GetBooksByCategoryId(Guid id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if(null == category) return null;
        
        var books = await context.Books
            .Where(b => b.CategoryId == id)
            .Select(b => new BooksByCategoryDto()
            {
                CategoryId = b.CategoryId,
                Category = b.Category!.Title,
                BookTitle = b.Title,
                PublishedDate = b.CreatedAt
            })
            .ToListAsync();
        
        return mapper.Map<List<BooksByCategoryDto>>(books);
    }
}
