using Library.Models.Entities;

namespace Library.Dtos.AuthorDtos;

public class AuthorGetBooksDto
{
    public string AuthorName { get; set; }
    public List<AuthorBookDto> BooksList { get; set; }
}

public class AuthorBookDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
}
