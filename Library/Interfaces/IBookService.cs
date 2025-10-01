using Library.Dtos.BookDtos;

namespace Library.Interfaces;

public interface IBookService
    : ICrudService<BookGetAllDto, BookCreateDto, BookCreateDto>
{
    Task<BookGetDetailDto?> GetBookById(Guid id);
}