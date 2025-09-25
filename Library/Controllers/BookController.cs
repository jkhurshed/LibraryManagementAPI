using AutoMapper;
using Library.Dtos.BookDtos;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController(LibDbContext context, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Get all existing books
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<BookGetAllDto>>> GetAllBooks()
    {
        var books = await context.Books.ToListAsync();
        return Ok(mapper.Map<List<BookGetAllDto>>(books));
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
        
        return Ok(mapper.Map<BookGetDetailDto>(book));
    }
    
    /// <summary>
    /// Create books
    /// </summary>
    /// <param name="bookDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(BookCreateDto bookDto)
    {
        var book = mapper.Map<Book>(bookDto);
        
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();

        var result = mapper.Map<BookGetDetailDto>(book);
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, result);
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
        
        mapper.Map(bookDto, book);
        
        await context.SaveChangesAsync();
        return Ok(mapper.Map<BookGetDetailDto>(book));
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