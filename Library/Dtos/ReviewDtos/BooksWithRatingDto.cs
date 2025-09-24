namespace Library.Dtos.ReviewDtos;

public class BooksWithRatingDto
{
    public Guid BookId { get; set; }
    public string BookTitle { get; set; }
    public int Rating { get; set; }
}