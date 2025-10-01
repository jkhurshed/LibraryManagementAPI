using AutoMapper;
using Library.Dtos.LoanDtos;
using Library.Interfaces;
using Library.Models;
using Library.Models.Dtos.LoanDtos;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class LoanService(LibDbContext context, IMapper mapper)
    : CrudService<Loan, LoanGetAllDto, LoanCreateDto, LoanCreateDto>
        (context, mapper), ILoanService
{
    public async Task<List<LoanGetAllDto>> GetAllLoans()
    {
        return (
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

    public async Task<LoanGetAllDto> CreateAsync(LoanCreateDto loanDto)
    {
        var BookID = loanDto.BookId;
        var bookInventory = await context.Inventories
            .FirstOrDefaultAsync(i => i.BookId == BookID && i.IsActive);
        if (bookInventory == null || bookInventory.BookCount < loanDto.LoanCount)
            return null;

        var loan = new Loan
        {
            BookId = loanDto.BookId,
            LoanCount = loanDto.LoanCount,
            UserId = loanDto.UserId,
            LoanDate = DateTime.Now
        };

        context.Loans.Add(loan);
        await context.SaveChangesAsync();

        return mapper.Map<LoanGetAllDto>(loan);
    }
}