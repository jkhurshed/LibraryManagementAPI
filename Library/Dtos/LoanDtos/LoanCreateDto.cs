namespace Library.Models.Dtos.LoanDtos;

public record LoanCreateDto
{
    public Guid BookId { get; init; }
    public Guid UserId { get; init; }
    public int LoanCount { get; init; } = 1;
}