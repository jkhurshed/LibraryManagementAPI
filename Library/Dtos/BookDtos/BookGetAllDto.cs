namespace Library.Dtos.BookDtos;

public class BookGetAllDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? ISBN { get; set; }
    public string? Description { get; set; }
    public DateTime? PublishedDate { get; set; }
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
    public string? Category { get; set; }
}