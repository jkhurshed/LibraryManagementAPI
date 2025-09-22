using Library.Models.Entities;

namespace Library.Dtos.BookDtos;

public class BookGetDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? ISBN { get; set; }
    public DateTime? PublishedDate { get; set; }
    public string Category { get; set; }
    public List<string> Authors { get; set; }
    public double? Rating { get; set; }
    public int? BookCount { get; set; }
    public int? ReservedCount { get; set; }
}