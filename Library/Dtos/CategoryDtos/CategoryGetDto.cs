namespace Library.Dtos.CategoryDtos;

public class CategoryGetDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public ICollection<CategoryGetDto> SubCategories { get; set; } = new List<CategoryGetDto>();
}