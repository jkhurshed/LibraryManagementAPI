using AutoMapper;
using Library.Dtos.AuthorDtos;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController(LibDbContext context, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Get all authors
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<Author>>> GetAllAuthors()
    {
        var authors = await context.Authors.ToListAsync();
        return Ok(mapper.Map<List<AuthorGetDto>>(authors));
    }

    /// <summary>
    /// Get author by providing its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthorById(Guid id)
    {
        var authors = await context.Authors
            .Include(a => a.Books)
            .ThenInclude(b => b.Book)
            .FirstOrDefaultAsync(a => a.Id == id);
        
        return Ok(mapper.Map<AuthorGetDto>(authors));
    }
    
    /// <summary>
    /// Create books authors
    /// </summary>
    /// <param name="authorDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Author>> CreateAuthor(AuthorCreateDto authorDto)
    {
        var author = mapper.Map<Author>(authorDto);
        
        if (author == null) return BadRequest();
        
        await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();
        
        var result = mapper.Map<AuthorGetDto>(author);
        return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, result);
    }

    /// <summary>
    /// Update author by providing its id
    /// </summary>
    /// <param name="authorDto"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Author>> UpdateAuthor(AuthorCreateDto authorDto, Guid id)
    {
        var author = await context.Authors.FindAsync(id);
        
        if (author == null) return NotFound();
        
        mapper.Map(authorDto, author);
        await context.SaveChangesAsync();
        
        return Ok(mapper.Map<AuthorGetDto>(author));
    }
    
    /// <summary>
    /// Delete a specific Author by providing its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Author>> DeleteAuthor(Guid id)
    {
        var author = await context.Authors.FindAsync(id);
        if (author == null) return NotFound();
        context.Authors.Remove(author);
        await context.SaveChangesAsync();
        return NoContent();
    }
    
    /// <summary>
    /// Provide author id to get this author's books
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/books")]
    public async Task<ActionResult<Author>> GetBooksByAuthorId(Guid id)
    {
        var author = await context.Authors
            .Where(a=>a.Id==id)
            .Select(a=>new AuthorGetBooksDto()
            {
                AuthorName =a.FullName,
                BooksList = a.Books.Select(b => new AuthorBookDto(){Title = b.Book.Title, Description = b.Book.Description}).ToList(),
            }).FirstOrDefaultAsync();
        
        if(author == null) return NotFound();
        
        return StatusCode(StatusCodes.Status200OK,
            mapper.Map<AuthorGetBooksDto>(author));
    }
    
    /// <summary>
    /// Find authors by partial name match.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("search/{name}")]
    public async Task<ActionResult<Author>> AuthorSearchByName(string name)
    {
        var author = await context.Authors
            .Where(a => a.FullName.ToLower().Contains(name.ToLower()))
            .FirstOrDefaultAsync();
        
        if(author == null) return NotFound();
        
        return Ok(mapper.Map<AuthorGetDto>(author));
    }
}