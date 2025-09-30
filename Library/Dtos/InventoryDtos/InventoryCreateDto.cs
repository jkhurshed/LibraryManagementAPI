namespace Library.Dtos.InventoryDtos;

public record InventoryCreateDto
{
    public Guid BookId { get; init; }
    public int BookCount { get; init; }
    public int ReservedCount { get; init; } = 0;
}