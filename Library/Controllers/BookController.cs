using AutoMapper;
using Library.Dtos.BookDtos;
using Library.Interfaces;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController(IBookService bookService) : ControllerBase
{
    /// <summary>
    /// Get all existing books
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<BookGetAllDto>>> GetAllBooks()
    {
        return Ok(await bookService.GetAllAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Book>> GetBookById(Guid id)
    {

        var result = await bookService.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Create books
    /// </summary>
    /// <param name="bookDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(BookCreateDto bookDto)
    {
        var result = await bookService.CreateAsync(bookDto);
        return CreatedAtAction(nameof(GetBookById), new {id = result.Id}, result);
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
        var result = await bookService.UpdateAsync(id, bookDto);
        return result == null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Delete the book by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Book>> DeleteBook(Guid id)
    {
        var result = await bookService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}