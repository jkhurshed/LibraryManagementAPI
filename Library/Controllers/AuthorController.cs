using AutoMapper;
using Library.Dtos.AuthorDtos;
using Library.Interfaces;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController(LibDbContext context, IMapper mapper, IAuthorService authorService) : ControllerBase
{
    /// <summary>
    /// Get all authors
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<Author>>> GetAllAuthors()
    {
        return Ok(await authorService.GetAllAsync());
    }

    /// <summary>
    /// Get author by providing its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthorById(Guid id)
    {
        var result = await authorService.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Create books authors
    /// </summary>
    /// <param name="authorDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Author>> CreateAuthor(AuthorCreateDto authorDto)
    {
        var result = await authorService.CreateAsync(authorDto);
        return CreatedAtAction(nameof(GetAuthorById), new {id = result.Id}, result);
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
        var result = await authorService.UpdateAsync(id, authorDto);
        return result == null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Delete a specific Author by providing its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Author>> DeleteAuthor(Guid id)
    {
        var result = await authorService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
    
    /// <summary>
    /// Provide author id to get this author's books
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/books")]
    public async Task<ActionResult<Author>> GetBooksByAuthorId(Guid id)
    {
        var author = await authorService.GetBooksByAuthorId(id);
        return author == null ? NotFound() : Ok(author);
    }
    
    /// <summary>
    /// Find authors by partial name match.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("search/{name}")]
    public async Task<ActionResult<Author>> AuthorSearchByName(string name)
    {
        var author = await authorService.AuthorSearchByName(name);
        return Ok(author);
    }
}