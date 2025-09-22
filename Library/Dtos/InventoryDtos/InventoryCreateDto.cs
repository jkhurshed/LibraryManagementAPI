namespace Library.Dtos.InventoryDtos;

public class InventoryCreateDto
{
    public Guid BookId { get; set; }
    public int BookCount { get; set; }
    public int ReservedCount { get; set; } = 0;
}