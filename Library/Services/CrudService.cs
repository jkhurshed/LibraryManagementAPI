using AutoMapper;
using Library.Dtos.AuthorDtos;
using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class CrudService<TEntity, TGetDto, TCreateDto, TUpdateDto>(DbContext context, IMapper mapper)
    : ICrudService<TGetDto, TCreateDto, TUpdateDto>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<List<TGetDto>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return mapper.Map<List<TGetDto>>(entities);
    }

    public async Task<TGetDto?> GetByIdAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity == null ? default : mapper.Map<TGetDto>(entity);
    }

    public async Task<TGetDto> CreateAsync(TCreateDto dto)
    {
        var entity = mapper.Map<TEntity>(dto);
        await _dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return mapper.Map<TGetDto>(entity);
    }

    public async Task<TGetDto?> UpdateAsync(Guid id, TUpdateDto dto)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) return default;

        mapper.Map(dto, entity);
        await context.SaveChangesAsync();
        return mapper.Map<TGetDto>(entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}
