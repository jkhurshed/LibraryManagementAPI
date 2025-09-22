namespace Library.Models.Dtos.LoanDtos;

public class LoanCreateDto
{
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public int LoanCount { get; set; } = 1;
}