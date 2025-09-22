namespace Library.Models.Entities;

public class BookAuthors
{
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public Guid AuthorId { get; set; }
    public Author Author { get; set; }
}