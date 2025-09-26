using AutoMapper;
using Library.Dtos.AuthorDtos;
using Library.Interfaces;
using Library.Models;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class AuthorService(LibDbContext context, IMapper mapper)
    : CrudService<Author, AuthorGetDto, AuthorCreateDto, AuthorCreateDto>
        (context, mapper), IAuthorService
{
    public async Task<AuthorGetDto?> GetByIdAsync(Guid id)
    {
        var authors = await context.Authors
            .Include(a => a.Books)
            .ThenInclude(b => b.Book)
            .FirstOrDefaultAsync(a => a.Id == id);
        return authors == null ? default : mapper.Map<AuthorGetDto>(authors);
    }

    public async Task<AuthorGetBooksDto?> GetBooksByAuthorId(Guid id)
    {
        var author = await context.Authors
            .Where(a=>a.Id==id)
            .Select(a=>new AuthorGetBooksDto()
            {
                AuthorName =a.FullName,
                BooksList = a.Books
                    .Select(b => new AuthorBookDto()
                    {
                        Title = b.Book.Title,
                        Description = b.Book.Description
                    }).ToList(),
            }).FirstOrDefaultAsync();
        
        return author == null ? default : mapper.Map<AuthorGetBooksDto>(author);
    }
    
    public async Task<List<AuthorGetDto>> AuthorSearchByName(string name)
    {
        var authors = await context.Authors
            .Where(a => a.FullName.ToLower().Contains(name.ToLower()))
            .Select(a => mapper.Map<AuthorGetDto>(a))
            .ToListAsync();
        return authors;
    }
}