namespace Library.Dtos.BookDtos;

public record BookCreateDto
{
    public Guid CategoryId { get; init; }
    public string Title { get; init; }
    public string BookName { get; init; }
    public string? ISBN { get; init; }
    public string? Description { get; init; }
}