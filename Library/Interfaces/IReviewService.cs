using Library.Dtos.ReviewDtos;

namespace Library.Interfaces;

public interface IReviewService
    : ICrudService<ReviewsGetAllDto, ReviewCreateDto, ReviewCreateDto>
{
    Task<List<BooksWithRatingDto>> GetTopRatedBooksReviews();
}