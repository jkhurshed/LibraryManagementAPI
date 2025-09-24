using Library.Models.BaseEntities;

namespace Library.Models.Entities;

public class Review : BaseTitleDescriptionEntity
{
    public int Rating { get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}