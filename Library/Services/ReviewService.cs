using AutoMapper;
using Library.Dtos.ReviewDtos;
using Library.Interfaces;
using Library.Models;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class ReviewService(LibDbContext context, IMapper mapper)
    : CrudService<Review, ReviewsGetAllDto, ReviewCreateDto, ReviewCreateDto>
(context, mapper), IReviewService
{
    public new async Task<ReviewCreateDto> CreateAsync(ReviewCreateDto reviewDto)
    {
        var userId = await context.Users.AnyAsync(u => u.Id == reviewDto.UserId);
        var bookId = await context.Books.AnyAsync(b => b.Id == reviewDto.BookId);
        
        if (!userId || !bookId)
        {
            throw new ArgumentException("User or Book not found!");
        }
        if (reviewDto.Rating < 1 || reviewDto.Rating > 5)
        {
            throw new ArgumentException("Rating must be between 1 and 5");
        }
        
        var review = mapper.Map<Review>(reviewDto);
        
        await context.Reviews.AddAsync(review);
        await context.SaveChangesAsync();
        
        return mapper.Map<ReviewCreateDto>(review);
    }
    
    public async Task<List<BooksWithRatingDto>> GetTopRatedBooksReviews()
    {
        var topBooks = await context.Reviews
            .Include(r => r.Book)
            .OrderByDescending(r => r.Rating)
            .Select(b => new BooksWithRatingDto()
            {
                BookId = b.BookId,
                BookTitle = b.Title,
                Rating = b.Rating
            })
            .ToListAsync();
        
        return mapper.Map<List<BooksWithRatingDto>>(topBooks);
    }
}