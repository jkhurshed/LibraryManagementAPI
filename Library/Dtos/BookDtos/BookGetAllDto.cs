namespace Library.Dtos.BookDtos;

public record BookGetAllDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string? ISBN { get; init; }
    public string? Description { get; init; }
    public DateTime? PublishedDate { get; init; }
    public Guid AuthorId { get; init; }
    public Guid CategoryId { get; init; }
    public string? Category { get; init; }
}