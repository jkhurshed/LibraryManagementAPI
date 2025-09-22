using Library.Dtos.LoanDtos;
using Library.Models;
using Library.Models.Dtos.LoanDtos;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class LoanController(LibDbContext context) : ControllerBase
{
    /// <summary>
    /// Get all loans
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<LoanGetAllDto>>> GetAllLoans()
    {
        return Ok(
            await context.Loans
                .Select(l => new LoanGetAllDto
                {
                    Id = l.Id,
                    BookId = l.BookId,
                    LoanCount = l.LoanCount,
                    UserId = l.UserId,
                    IsReturned = l.IsReturned,
                    LoanDate = l.CreatedAt,
                    ReturnDate = l.ReturnDate
                })
            .ToListAsync());
    }
    
    /// <summary>
    /// For Loan creation
    /// </summary>
    /// <param name="loanDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<List<Loan>>> LoanBook(LoanCreateDto loanDto)
    {
        var LoanCount = loanDto.LoanCount;
        var BookID = loanDto.BookId;
        var BookCount = context.Inventories
            .Where(i => i.BookId == BookID)
            .Select(i => i.BookCount)
            .FirstOrDefault();
        var bookIsActive = await context.Inventories
            .Where(i => i.IsActive == true)
            .FirstOrDefaultAsync(i => i.BookId == BookID);
        if (bookIsActive != null && BookCount >= LoanCount)
        {
            var loan = new Loan
            {
                BookId = BookID,
                LoanCount = LoanCount,
                UserId = loanDto.UserId,
                LoanDate = DateTime.Now
            };
            context.Loans.Add(loan);
            await context.SaveChangesAsync();
            return StatusCode(statusCode:201, value: loan);
        }

        return StatusCode(StatusCodes.Status400BadRequest, "Book is not available!");
    }

    /// <summary>
    /// Updating Loan by providing its id
    /// </summary>
    /// <param name="loanDto"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Loan>> UpdateLoan(LoanCreateDto loanDto, Guid id)
    {
        var loan = await context.Loans.FirstOrDefaultAsync(l => l.Id == id);
        
        if (loan == null) return NotFound();
        loan.BookId = loanDto.BookId;
        loan.UserId = loanDto.UserId;
        loan.LoanCount = loanDto.LoanCount;
        
        await context.SaveChangesAsync();
        return Ok(loan);
    }
    
    /// <summary>
    /// Deleting Loan by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Loan>> DeleteLoan(Guid id)
    {
        var loan = await context.Loans.FirstOrDefaultAsync(l => l.Id == id);
        
        if (loan == null) return NotFound(new { message = "Loan not found!" });
        context.Loans.Remove(loan);
        
        await context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent, new { message = "Loan deleted successfully!" });
    }
}