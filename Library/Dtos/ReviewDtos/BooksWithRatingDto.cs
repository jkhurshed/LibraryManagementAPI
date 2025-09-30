namespace Library.Dtos.ReviewDtos;

public record BooksWithRatingDto
{
    public Guid BookId { get; init; }
    public string BookTitle { get; init; }
    public int Rating { get; init; }
}