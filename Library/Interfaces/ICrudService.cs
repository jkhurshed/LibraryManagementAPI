namespace Library.Interfaces;

public interface ICrudService<TGetDto, in TCreateDto, in TUpdateDto>
{
    Task<List<TGetDto>> GetAllAsync();
    Task<TGetDto?> GetByIdAsync(Guid id);
    Task<TGetDto> CreateAsync(TCreateDto dto);
    Task<TGetDto?> UpdateAsync(Guid id, TUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}
