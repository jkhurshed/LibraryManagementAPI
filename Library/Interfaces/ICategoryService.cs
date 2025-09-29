using Library.Dtos.BookDtos;
using Library.Dtos.CategoryDtos;

namespace Library.Interfaces;

public interface ICategoryService
    : ICrudService<CategoryGetDto, CategoryCreateDto, CategoryCreateDto>
{
    Task<CategoryGetDto?> GetAllCategories();
    Task<List<BooksByCategoryDto>?> GetBooksByCategoryId(Guid id);
}