using Library.Models;
using Library.Models.Dtos;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(LibDbContext context) : ControllerBase
{
    /// <summary>
    /// Get all existing users
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<UserGetAllDto>>> GetAllUsers()
    {
        return Ok(
            await context.Users
                .Select(u => new UserGetAllDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    UserType = u.UserType,
                    IsActive = u.IsActive,
                    RegisteredAt = u.CreatedAt
                }).ToListAsync()
            );
    }

    /// <summary>
    /// Provide wallet id to see detail info about this wallet.
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> UserGetDetail(Guid id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound();
        var userDetail = await (
            from u in context.Users
            join l in context.Loans on u.Id equals l.UserId
            join r in context.Reviews on u.Id equals r.UserId
            join b in context.Books on l.BookId equals b.Id
            where u.Id == id
                select new UserGetDetailDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    UserType = u.UserType,
                    IsActive = u.IsActive,
                    RegisteredAt = u.CreatedAt,
                    BookTitle = b.Title,
                    IsReturned = l.IsReturned,
                    LoanDate = l.LoanDate,
                    ReturnDate = l.ReturnDate,
                    Title = r.Title,
                    ReviewDescription = r.Description,
                    Rating = r.Rating
                }
            ).ToListAsync();
        return Ok(userDetail);
    }

    /// <summary>
    /// Create users
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(UserCreateDto userDto)
    {
        var user = new User
        {
            UserName = userDto.UserName,
            Email = userDto.Email,
            Password = userDto.Password,
            UserType = userDto.UserType,
            IsActive = userDto.IsActive,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return StatusCode(statusCode: 201, value: user);
    }

    /// <summary>
    /// Provide the users id to see detail info about this user
    /// </summary>
    /// <param name="id"></param>
    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUser(Guid id, UserUpdateDto userdto)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound();
        user.UserName = userdto.UserName;
        user.Email = userdto.Email;
        user.UserType = userdto.UserType;
        user.IsActive = userdto.IsActive;
        user.UpdatedAt = DateTime.Now;
        await context.SaveChangesAsync();
        return Ok(user);
    }
    
    /// <summary>
    /// Provide the users id to see delete this user
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> DeleteUser(Guid id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound();
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent, new { message = "User deleted successfully!" });

    }

    /// <summary>
    /// Provide the users id to see all loans for this user
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}/loans")]
    public async Task<ActionResult<List<User>>> GetUserLoans(Guid id)
    {
        var userLoans = await context.Loans
            .Where(l => l.UserId == id)
            .ToListAsync();
        
        return StatusCode(StatusCodes.Status200OK, userLoans);
    }
    
    /// <summary>
    /// Provide the users id to see all active loans for this user
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}/activeLoans")]
    public async Task<ActionResult<List<User>>> GetUsersActiveLoans(Guid id)
    {
        var userLoans = await context.Loans
            .Where(l => (l.UserId == id && l.IsReturned == false))
            .ToListAsync();
        
        return StatusCode(StatusCodes.Status200OK, userLoans);
    }
}