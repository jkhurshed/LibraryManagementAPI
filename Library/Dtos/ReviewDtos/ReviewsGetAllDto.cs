namespace Library.Dtos.ReviewDtos;

public class ReviewsGetAllDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? ReviewContent { get; set; }
    public int Rating { get; set; }
    public DateTime ReviewDate { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
}