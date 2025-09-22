using Library.Models.Enums;

namespace Library.Models.Dtos;

public class UserUpdateDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public UserType UserType { get; set; }
    public bool IsActive { get; set; } = true;
}