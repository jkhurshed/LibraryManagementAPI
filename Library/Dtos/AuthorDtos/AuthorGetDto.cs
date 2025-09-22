namespace Library.Dtos.AuthorDtos;

public class AuthorGetDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string? Country { get; set; }
    public string? Biography { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? deathDate { get; set; }
}