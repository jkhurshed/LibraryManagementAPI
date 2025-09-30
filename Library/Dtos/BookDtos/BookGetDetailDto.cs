using Library.Models.Entities;

namespace Library.Dtos.BookDtos;

public record BookGetDetailDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public string? ISBN { get; init; }
    public DateTime? PublishedDate { get; init; }
    public string Category { get; init; }
    public List<string> Authors { get; init; }
    public double? Rating { get; init; }
    public int? BookCount { get; init; }
    public int? ReservedCount { get; init; }
}