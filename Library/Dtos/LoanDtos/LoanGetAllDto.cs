namespace Library.Dtos.LoanDtos;

public class LoanGetAllDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public int LoanCount { get; set; }
    public bool IsReturned { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}