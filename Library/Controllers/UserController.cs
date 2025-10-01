using Library.Interfaces;
using Library.Models;
using Library.Models.Dtos;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// Get all existing users
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<UserGetAllDto>>> GetAllUsers()
    {
        return Ok(await userService.GetAllAsync());
    }

    /// <summary>
    /// Provide wallet id to see detail info about this wallet.
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> UserGetDetail(Guid id)
    {
        var user = await userService.GetByIdAsync(id);
        return Ok(user);
    }

    /// <summary>
    /// Create users
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(UserCreateDto userDto)
    {
        var user = await userService.CreateAsync(userDto);
        return StatusCode(statusCode: 201, value: user);
    }

    /// <summary>
    /// Provide the users id to see detail info about this user
    /// </summary>
    /// <param name="id"></param>
    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUser(Guid id, UserCreateDto userDto)
    {
        var user = await userService.UpdateAsync(id, userDto);
        return Ok(user);
    }
    
    /// <summary>
    /// Provide the users id to see delete this user
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> DeleteUser(Guid id)
    {
        var user = await userService.DeleteAsync(id);
        return StatusCode(StatusCodes.Status204NoContent, new { message = "User deleted successfully!" });

    }

    /// <summary>
    /// Provide the users id to see all loans for this user
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}/loans")]
    public async Task<ActionResult<List<User>>> GetUserLoans(Guid id)
    {
        var userLoans = await userService.GetUserLoans(id);
        
        return StatusCode(StatusCodes.Status200OK, userLoans);
    }
    
    /// <summary>
    /// Provide the users id to see all active loans for this user
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}/activeLoans")]
    public async Task<ActionResult<List<User>>> GetUsersActiveLoans(Guid id)
    {
        var userLoans = await userService.GetUserActiveLoans(id);
        
        return StatusCode(StatusCodes.Status200OK, userLoans);
    }
}