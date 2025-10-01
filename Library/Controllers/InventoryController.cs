using AutoMapper;
using Library.Dtos.InventoryDtos;
using Library.Interfaces;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController(IInventoryService inventoryService) : ControllerBase
{
    /// <summary>
    /// Get all Inventories
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<InventoryGetAllDto>>> GetAllInventories()
    {
        return Ok(await inventoryService.GetAllAsync());
    }

    /// <summary>
    /// Create Inventory
    /// </summary>
    /// <param name="inventoryDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<List<Inventory>>> CreateInventory(InventoryCreateDto inventoryDto)
    {
        var inventory = await inventoryService.CreateAsync(inventoryDto);
        return StatusCode(201, inventory);
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
        var inventory = await inventoryService.UpdateAsync(id, inventoryDto);
        return inventory == null ? NotFound() : Ok(inventory);
    }

    /// <summary>
    /// Delete Inventory by providing its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Inventory>> DeleteInventory(Guid id)
    {
        var inventory = await inventoryService.DeleteAsync(id);
        return inventory ? NoContent() : NotFound();
    }
}