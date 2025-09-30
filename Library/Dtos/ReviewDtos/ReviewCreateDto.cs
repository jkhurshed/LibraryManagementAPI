namespace Library.Dtos.ReviewDtos;

public record ReviewCreateDto
{
    public string Title { get; init; }
    public string? ReviewContent { get; init; }
    public int Rating { get; init; }
    public Guid BookId { get; init; }
    public Guid UserId { get; init; }
}