using AutoMapper;
using Library.Dtos.InventoryDtos;
using Library.Interfaces;
using Library.Models;
using Library.Models.Entities;

namespace Library.Services;

public class InventoryService(LibDbContext context, IMapper mapper)
    : CrudService<Inventory, InventoryGetAllDto, InventoryCreateDto, InventoryCreateDto>
    (context, mapper), IInventoryService;
