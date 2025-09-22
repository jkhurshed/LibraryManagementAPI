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
}