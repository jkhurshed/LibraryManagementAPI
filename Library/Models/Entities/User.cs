using Library.Models.BaseEntities;
using Library.Models.Enums;

namespace Library.Models.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public UserType UserType { get; set; } = UserType.Member;
    
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}