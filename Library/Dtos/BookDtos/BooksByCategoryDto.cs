namespace Library.Dtos.BookDtos;

public class BooksByCategoryDto
{
    public Guid CategoryId { get; set; }
    public string? Category { get; set; }
    public string BookTitle { get; set; }
    public DateTime? PublishedDate { get; set; }
}