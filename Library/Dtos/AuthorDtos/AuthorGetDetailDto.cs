using Library.Models.Entities;

namespace Library.Dtos.AuthorDtos;

public class AuthorGetDetailDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string? Country { get; set; }
    public string? Biography { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? deathDate { get; set; }
    public ICollection<BookAuthors> Books { get; set; } = new List<BookAuthors>();
}