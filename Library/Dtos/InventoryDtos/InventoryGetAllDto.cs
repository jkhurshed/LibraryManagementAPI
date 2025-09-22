namespace Library.Dtos.InventoryDtos;

public class InventoryGetAllDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public int BookCount { get; set; }
    public int ReservedCount { get; set; }
    public DateTime? ReservedDate { get; set; }
    public bool IsActive { get; set; }
}