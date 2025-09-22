namespace Library.Dtos.CategoryDtos;

public class CategoryCreateDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}