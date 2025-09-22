using Library.Models.BaseEntities;

namespace Library.Models.Entities;

public class Author : BaseEntity
{
    public string FullName { get; set; }
    public string? Country { get; set; }
    public string? Biography { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? deathDate { get; set; }
    
    public ICollection<BookAuthors> Books { get; set; } = new List<BookAuthors>();
}