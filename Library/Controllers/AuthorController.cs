using Library.Dtos.AuthorDtos;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController(LibDbContext context) : ControllerBase
{
    /// <summary>
    /// Get all authors
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<Author>>> GetAllAuthors()
    {
        return Ok(
            await context.Authors
                .Select(a => new Author
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Country = a.Country,
                    Biography = a.Biography,
                    BirthDate = a.BirthDate,
                    deathDate = a.deathDate
                }).ToListAsync()
            );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthorById(Guid id)
    {
        var authors = await context.Authors
            .Include(a => a.Books)
            .ThenInclude(b => b.Book)
            .FirstOrDefaultAsync(a => a.Id == id);
        
        return Ok(authors);
    }
    
    /// <summary>
    /// Create books authors
    /// </summary>
    /// <param name="authorDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Author>> CreateAuthor(AuthorCreateDto authorDto)
    {
        var author = new Author
        {
            FullName = authorDto.FullName,
            Country = authorDto.Country,
            Biography = authorDto.Biography,
            BirthDate = authorDto.BirthDate,
            deathDate = authorDto.deathDate
        };
        await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();
        return StatusCode(statusCode: 201, value: author);
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
        author.FullName = authorDto.FullName;
        author.Country = authorDto.Country;
        author.Biography = authorDto.Biography;
        author.BirthDate = authorDto.BirthDate;
        author.deathDate = authorDto.deathDate;
        await context.SaveChangesAsync();
        return Ok(author);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Author>> DeleteAuthor(Guid id)
    {
        var author = await context.Authors.FindAsync(id);
        if (author == null) return NotFound();
        context.Authors.Remove(author);
        await context.SaveChangesAsync();
        return Ok(author);
    }
}