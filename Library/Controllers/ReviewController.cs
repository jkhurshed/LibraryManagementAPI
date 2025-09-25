using AutoMapper;
using Library.Dtos.ReviewDtos;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController(LibDbContext context, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Get all Reviews
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<Review>>> GetAllReviews()
    {
        return Ok(await context.Reviews.ToListAsync());
    }
    
    /// <summary>
    /// Create Reviews
    /// </summary>
    /// <param name="reviewDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Review>> CreateReview(ReviewCreateDto reviewDto)
    {
        var userId = await context.Users.AnyAsync(u => u.Id == reviewDto.UserId);
        var bookId = await context.Books.AnyAsync(b => b.Id == reviewDto.BookId);
        
        if (!userId || !bookId) return NotFound(new { message = "User or Book not found!" });
        if (reviewDto.Rating < 1 || reviewDto.Rating > 5) return BadRequest(new { message = "Rating must be between 1 and 5" });
        
        var review = mapper.Map<Review>(reviewDto);
        
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
        
        if (review == null) return NotFound(new { message = "Review not found!" });
        review.Title = reviewDto.Title;
        review.Description = reviewDto.ReviewContent;
        review.Rating = reviewDto.Rating;
        review.BookId = reviewDto.BookId;
        review.UserId = reviewDto.UserId;
        
        await context.SaveChangesAsync();
        return Ok(review);
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