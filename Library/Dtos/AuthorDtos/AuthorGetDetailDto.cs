using Library.Models.Entities;

namespace Library.Dtos.AuthorDtos;

public record AuthorGetDetailDto
{
    public Guid Id { get; init; }
    public string FullName { get; init; }
    public string? Country { get; init; }
    public string? Biography { get; init; }
    public DateTime? BirthDate { get; init; }
    public DateTime? deathDate { get; init; }
    public ICollection<BookAuthors> Books { get; init; } = new List<BookAuthors>();
}