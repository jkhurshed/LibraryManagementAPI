namespace Library.Dtos.BookDtos;

public record BooksByCategoryDto
{
    public Guid CategoryId { get; init; }
    public string? Category { get; init; }
    public string BookTitle { get; init; }
    public DateTime? PublishedDate { get; init; }
}