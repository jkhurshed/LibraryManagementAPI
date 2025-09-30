namespace Library.Dtos.LoanDtos;

public record LoanGetAllDto
{
    public Guid Id { get; init; }
    public Guid BookId { get; init; }
    public Guid UserId { get; init; }
    public int LoanCount { get; init; }
    public bool IsReturned { get; init; }
    public DateTime LoanDate { get; init; }
    public DateTime? ReturnDate { get; init; }
}