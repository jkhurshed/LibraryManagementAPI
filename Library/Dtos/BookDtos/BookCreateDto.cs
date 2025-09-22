namespace Library.Dtos.BookDtos;

public class BookCreateDto
{
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string? ISBN { get; set; }
    public string? Description { get; set; }
}