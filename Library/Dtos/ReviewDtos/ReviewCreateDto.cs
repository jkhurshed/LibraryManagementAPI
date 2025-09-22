namespace Library.Dtos.ReviewDtos;

public class ReviewCreateDto
{
    public string Title { get; set; }
    public string? ReviewContent { get; set; }
    public int Rating { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
}