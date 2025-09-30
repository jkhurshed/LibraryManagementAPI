namespace Library.Dtos.CategoryDtos;

public record CategoryGetDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public Guid? ParentCategoryId { get; init; }
    public ICollection<CategoryGetDto> SubCategories { get; init; } = new List<CategoryGetDto>();
}