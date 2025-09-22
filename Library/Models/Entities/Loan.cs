using Library.Models.BaseEntities;

namespace Library.Models.Entities;

public class Loan : BaseEntity
{
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int LoanCount { get; set; } = 1;
    public bool IsReturned { get; set; } = false;
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}