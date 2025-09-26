using AutoMapper;
using Library.Dtos.AuthorDtos;
using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class CrudService<TEntity, TGetDto, TCreateDto, TUpdateDto> 
    : ICrudService<TGetDto, TCreateDto, TUpdateDto>
    where TEntity : class
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;
    private readonly DbSet<TEntity> _dbSet;

    public CrudService(DbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<List<TGetDto>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return _mapper.Map<List<TGetDto>>(entities);
    }

    public async Task<TGetDto?> GetByIdAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity == null ? default : _mapper.Map<TGetDto>(entity);
    }

    public async Task<TGetDto> CreateAsync(TCreateDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<TGetDto>(entity);
    }

    public async Task<TGetDto?> UpdateAsync(Guid id, TUpdateDto dto)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) return default;

        _mapper.Map(dto, entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<TGetDto>(entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
