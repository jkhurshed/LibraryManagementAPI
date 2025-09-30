namespace Library.Dtos.AuthorDtos;

public record AuthorCreateDto
{
    public string FullName { get; init; }
    public string? Country { get; init; }
    public string? Biography { get; init; }
    public DateTime? BirthDate { get; init; }
    public DateTime? deathDate { get; init; }
}