using Library.Models.Enums;

namespace Library.Models.Dtos;

public record UserCreateDto
{
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public UserType UserType { get; init; }
    public bool IsActive { get; init; } = true;
}