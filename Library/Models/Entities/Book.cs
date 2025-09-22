using Library.Models.BaseEntities;

namespace Library.Models.Entities;

public class Book : BaseTitleDescriptionEntity
{
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public string? ISBN { get; set; }
    public Inventory? Inventory { get; set; }
    
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<BookAuthors> Authors { get; set; } = new List<BookAuthors>();
}