using Library.Models.Enums;

namespace Library.Models.Dtos;

public class UserGetAllDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public UserType UserType { get; set; }
    public bool IsActive {get; set; }
    public DateTime RegisteredAt { get; set; }
}