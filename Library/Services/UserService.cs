using AutoMapper;
using Library.Dtos.LoanDtos;
using Library.Interfaces;
using Library.Models;
using Library.Models.Dtos;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class UserService(LibDbContext context, IMapper mapper)
    : CrudService<User, UserGetAllDto, UserCreateDto, UserCreateDto>
        (context, mapper), IUserService
{

    public async Task<UserGetDetailDto?> GetById(Guid id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return null;
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
        ).FirstOrDefaultAsync();
        
        return userDetail;
    }

    public async Task<List<LoanGetAllDto>> GetUserLoans(Guid id)
    {
        var userLoans = await context.Loans
            .Where(l => l.UserId == id)
            .ToListAsync();
        return mapper.Map<List<LoanGetAllDto>>(userLoans);
    }

    public async Task<List<LoanGetAllDto>> GetUserActiveLoans(Guid id)
    {
        var userLoans = await context.Loans
            .Where(l => (l.UserId == id && l.IsReturned == false))
            .ToListAsync();
        return mapper.Map<List<LoanGetAllDto>>(userLoans);
    }
}