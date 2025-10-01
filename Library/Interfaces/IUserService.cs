using Library.Dtos.LoanDtos;
using Library.Models.Dtos;

namespace Library.Interfaces;

public interface IUserService
    : ICrudService<UserGetAllDto, UserCreateDto, UserCreateDto>
{
    Task<UserGetDetailDto?> GetById(Guid id);
    Task<List<LoanGetAllDto>> GetUserLoans(Guid id);
    Task<List<LoanGetAllDto>> GetUserActiveLoans(Guid id);
}