using Library.Models.Enums;

namespace Library.Models.Dtos;

public record UserGetAllDto
{
    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
    public UserType UserType { get; init; }
    public bool IsActive {get; init; }
    public DateTime RegisteredAt { get; init; }
}