using Library.Dtos.AuthorDtos;
using Library.Models.Entities;

namespace Library.Interfaces;

public interface IAuthorService 
    : ICrudService<AuthorGetDto, AuthorCreateDto, AuthorCreateDto>
{
    Task<AuthorGetBooksDto?> GetBooksByAuthorId(Guid id);
    Task<List<AuthorGetDto>> AuthorSearchByName(string name);
}