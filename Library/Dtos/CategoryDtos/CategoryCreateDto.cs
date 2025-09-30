namespace Library.Dtos.CategoryDtos;

public record CategoryCreateDto
{
    public string Title { get; init; }
    public string? Description { get; init; }
    public Guid? ParentCategoryId { get; init; }
}