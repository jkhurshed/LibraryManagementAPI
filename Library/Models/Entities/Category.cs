using System.Text.Json.Serialization;
using Library.Models.BaseEntities;

namespace Library.Models.Entities;

public class Category : BaseTitleDescriptionEntity
{
    public Guid? ParentCategoryId { get; set; }
    [JsonIgnore]
    public Category? ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();
    
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
