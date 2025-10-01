using AutoMapper;
using Library.Dtos.BookDtos;
using Library.Interfaces;
using Library.Models;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class BookService(LibDbContext context, IMapper mapper)
    :CrudService<Book, BookGetAllDto, BookCreateDto, BookCreateDto>
        (context, mapper), IBookService
{
    public async Task<BookGetDetailDto?> GetBookById(Guid id)
    {
        var book = await context.Books
            .Select(b => new BookGetDetailDto()
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ISBN = b.ISBN,
                Category = b.Category.Title,
                Rating = b.Reviews.Average(r => r.Rating),
                BookCount = b.Inventory.BookCount,
                ReservedCount = b.Inventory.ReservedCount,
                PublishedDate = b.CreatedAt,
                Authors = b.Authors.Select(a => a.Author.FullName).ToList()
            })
            .FirstOrDefaultAsync(b=>b.Id==id);
        
        return book == null ? default : mapper.Map<BookGetDetailDto>(book);
    }
}