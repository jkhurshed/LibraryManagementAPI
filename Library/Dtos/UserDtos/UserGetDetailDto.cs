using Library.Models.Enums;

namespace Library.Models.Dtos;

public class UserGetDetailDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public UserType UserType { get; set; }
    public bool IsActive {get; set; }
    public DateTime RegisteredAt { get; set; }
    public string? BookTitle { get; set; }
    public bool IsReturned { get; set; }
    public DateTime? LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string? Title { get; set; }
    public string? ReviewDescription { get; set; }
    public int? Rating { get; set; }
}