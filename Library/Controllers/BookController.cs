using Library.Dtos.BookDtos;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController(LibDbContext context) : ControllerBase
{
    /// <summary>
    /// Get all existing books
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<BookGetAllDto>>> GetAllBooks()
    {
        return Ok(
            await context.Books
                .Select(b => new BookGetAllDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    Description = b.Description,
                    CategoryId = b.CategoryId,
                }).ToListAsync()
        );

    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Book>> GetBookById(Guid id)
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
        
        return Ok(book);
    }
    
    /// <summary>
    /// Create books
    /// </summary>
    /// <param name="bookDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(BookCreateDto bookDto)
    {
        var book = new Book
        {
            CategoryId = bookDto.CategoryId,
            Title = bookDto.Title,
            ISBN = bookDto.ISBN,
            Description = bookDto.Description
        };
        
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
        return StatusCode(statusCode: 201, value: book);
    }
    
    /// <summary>
    /// Updating the book by its id
    /// </summary>
    /// <param name="bookDto"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Book>> UpdateBook(BookCreateDto bookDto, Guid id)
    {
        var book = await context.Books.FindAsync(id);
        if (book == null) return NotFound();
        book.CategoryId = bookDto.CategoryId;
        book.Title = bookDto.Title;
        book.ISBN = bookDto.ISBN;
        book.Description = bookDto.Description;
        
        await context.SaveChangesAsync();
        return Ok(book);
    }
    
    /// <summary>
    /// Delete the book by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Book>> DeleteBook(Guid id)
    {
        var book = await context.Books.FindAsync(id);
        if (book == null) return NotFound();
        context.Books.Remove(book);
        await context.SaveChangesAsync();
        return Ok(book);
    }
}