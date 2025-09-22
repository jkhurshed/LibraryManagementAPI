using Library.Models.BaseEntities;

namespace Library.Models.Entities;

public class Inventory : BaseEntity
{
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public int BookCount { get; set; }
    public int ReservedCount { get; set; } = 0;
}