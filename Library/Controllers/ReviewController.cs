using Library.Dtos.ReviewDtos;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController(LibDbContext context) : ControllerBase
{
    /// <summary>
    /// Get all Reviews
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<Review>>> GetAllReviews()
    {
        return Ok(
            await context.Reviews
                .Select(r => new Review
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Rating = r.Rating,
                    BookId = r.BookId,
                    UserId = r.UserId,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                }).ToListAsync()
        );
    }
    
    /// <summary>
    /// Create Reviews
    /// </summary>
    /// <param name="reviewDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Review>> CreateReview(ReviewCreateDto reviewDto)
    {
        var review = new Review
        {
            Title = reviewDto.Title,
            Description = reviewDto.ReviewContent,
            Rating = reviewDto.Rating,
            BookId = reviewDto.BookId,
            UserId = reviewDto.UserId
        };
        
        await context.Reviews.AddAsync(review);
        await context.SaveChangesAsync();
        return StatusCode(statusCode: 201, value: review);
    }

    /// <summary>
    /// Update Review by providing its id
    /// </summary>
    /// <param name="reviewDto"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Review>> UpdateReview(ReviewCreateDto reviewDto, Guid id)
    {
        var review = await context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        
        if (review == null) return NotFound();
        review.Title = reviewDto.Title;
        review.Description = reviewDto.ReviewContent;
        review.Rating = reviewDto.Rating;
        review.BookId = reviewDto.BookId;
        review.UserId = reviewDto.UserId;
        
        await context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status200OK, review);
    }
    
    /// <summary>
    /// Provide Review id to delete that review
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Review>> DeleteReview(Guid id)
    {
        var review = await context.Reviews.FindAsync(id);
        if (review == null) return NotFound();
        context.Reviews.Remove(review);
        
        await context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent, new { message = "Review deleted successfully!" });
    }

    [HttpGet("top")]
    public async Task<ActionResult<List<Review>>> GetTopRatedBooksReviews()
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
        
        return Ok(topBooks);
    }
}