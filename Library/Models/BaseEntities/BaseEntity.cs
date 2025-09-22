namespace Library.Models.BaseEntities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
}

public class BaseTitleDescriptionEntity : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
}