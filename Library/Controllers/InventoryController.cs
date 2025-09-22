using Library.Dtos.InventoryDtos;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController(LibDbContext context) : ControllerBase
{
    /// <summary>
    /// Get all Inventories
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<InventoryGetAllDto>>> GetAllInventories()
    {
        return Ok(
            await context.Inventories
            .Select(i => new InventoryGetAllDto
            {
                Id = i.Id,
                BookId = i.BookId,
                BookCount = i.BookCount,
                ReservedCount = i.ReservedCount,
                ReservedDate = i.CreatedAt,
                IsActive = i.IsActive
            })
            .ToListAsync());
    }

    /// <summary>
    /// Create Inventory
    /// </summary>
    /// <param name="inventoryDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<List<Inventory>>> CreateInventory(InventoryCreateDto inventoryDto)
    {
        var inventory = new Inventory
        {
            BookId = inventoryDto.BookId,
            BookCount = inventoryDto.BookCount,
            ReservedCount = inventoryDto.ReservedCount
        };
        
        await context.Inventories.AddAsync(inventory);
        await context.SaveChangesAsync();
        return StatusCode(statusCode: 201, value: inventory);
    }

    /// <summary>
    /// Updating Inventory by providing its id
    /// </summary>
    /// <param name="inventoryDto"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Inventory>> UpdateInventory(InventoryCreateDto inventoryDto, Guid id)
    {
        var inventory = await context.Inventories.FirstOrDefaultAsync(i => i.Id == id);
        
        if (inventory == null) return NotFound();
        inventory.BookId = inventoryDto.BookId;
        inventory.BookCount = inventoryDto.BookCount;
        inventory.ReservedCount = inventoryDto.ReservedCount;
        inventory.UpdatedAt = DateTime.Now;
        
        await context.SaveChangesAsync();
        return Ok(inventory);
    }

    /// <summary>
    /// Delete Inventory by providing its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Inventory>> DeleteInventory(Guid id)
    {
        var inventory = await context.Inventories.FindAsync(id);
        if (inventory == null) return NotFound();
        context.Inventories.Remove(inventory);
        await context.SaveChangesAsync();
        return StatusCode(statusCode: 204, value: inventory);
    }
}