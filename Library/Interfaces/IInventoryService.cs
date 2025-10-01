using Library.Dtos.InventoryDtos;

namespace Library.Interfaces;

public interface IInventoryService
    : ICrudService<InventoryGetAllDto, InventoryCreateDto, InventoryCreateDto>;
