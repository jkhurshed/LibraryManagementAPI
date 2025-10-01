using Library.Dtos.LoanDtos;
using Library.Interfaces;
using Library.Models;
using Library.Models.Dtos.LoanDtos;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class LoanController(ILoanService loanService) : ControllerBase
{
    /// <summary>
    /// Get all loans
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<LoanGetAllDto>>> GetAllLoans()
    {
        return Ok(await loanService.GetAllAsync());
    }
    
    /// <summary>
    /// For Loan creation
    /// </summary>
    /// <param name="loanDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<List<Loan>>> LoanBook(LoanCreateDto loanDto)
    {
        var loan = await loanService.CreateAsync(loanDto);
        return StatusCode(statusCode:201, value: loan);
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
        var loan = await loanService.UpdateAsync(id, loanDto);
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
        await loanService.DeleteAsync(id);
        return StatusCode(StatusCodes.Status204NoContent, new { message = "Loan deleted successfully!" });
    }
}