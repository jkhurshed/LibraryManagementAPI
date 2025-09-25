using AutoMapper;
using Library.Dtos.InventoryDtos;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController(LibDbContext context, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Get all Inventories
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<InventoryGetAllDto>>> GetAllInventories()
    {
        var inventories = await context.Inventories.ToListAsync();
        return Ok(mapper.Map<List<InventoryGetAllDto>>(inventories));
    }

    /// <summary>
    /// Create Inventory
    /// </summary>
    /// <param name="inventoryDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<List<Inventory>>> CreateInventory(InventoryCreateDto inventoryDto)
    {
        var inventory = mapper.Map<Inventory>(inventoryDto);
        
        if (inventory == null) return BadRequest();
        
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
        
        mapper.Map(inventoryDto, inventory);
        
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