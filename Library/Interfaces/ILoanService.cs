using Library.Dtos.LoanDtos;
using Library.Models.Dtos.LoanDtos;

namespace Library.Interfaces;

public interface ILoanService
    : ICrudService<LoanGetAllDto, LoanCreateDto, LoanCreateDto>
{
    new Task<List<LoanGetAllDto>?> GetAllAsync();
    new Task<LoanGetAllDto> CreateAsync(LoanCreateDto dto);
}