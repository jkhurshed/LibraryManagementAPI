using Library.Models.Entities;

namespace Library.Dtos.AuthorDtos;

public record AuthorGetBooksDto
{
    public string AuthorName { get; init; }
    public List<AuthorBookDto> BooksList { get; init; }
}

public record AuthorBookDto
{
    public string Title { get; init; }
    public string? Description { get; init; }
}
