namespace Library.Dtos.ReviewDtos;

public record ReviewsGetAllDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string? ReviewContent { get; init; }
    public int Rating { get; init; }
    public DateTime ReviewDate { get; init; }
    public Guid UserId { get; init; }
    public Guid BookId { get; init; }
}