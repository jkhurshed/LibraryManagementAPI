namespace Library.Dtos.InventoryDtos;

public record InventoryGetAllDto
{
    public Guid Id { get; init; }
    public Guid BookId { get; init; }
    public int BookCount { get; init; }
    public int ReservedCount { get; init; }
    public DateTime? ReservedDate { get; init; }
    public bool IsActive { get; init; }
}