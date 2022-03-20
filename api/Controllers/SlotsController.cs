using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace api.Controllers;

[ApiController]
[Route("slots")]
public class SlotsController : ControllerBase
{
    private static readonly Mapper _mapper = new Mapper(
        new MapperConfiguration(cfg =>
            cfg.CreateMap<SlotModel, Slot>().ReverseMap()
        )
    );
    private readonly DataContext _dbcontext;

    public SlotsController(DataContext context)
    {
        _dbcontext = context;
    }

    [HttpPost]
    public async Task<ActionResult> CreateSlot(SlotModel slotModel)
    {
        var slot = _mapper.Map<Slot>(slotModel);

        _dbcontext.Slots.Add(slot);
        await _dbcontext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{slotId}")]
    public async Task<IActionResult> UpdateSlot(int slotId, SlotModel slotModel)
    {
        if (slotId != slotModel.Id)
        {
            return BadRequest();
        }

        var slot = await _dbcontext.Slots.FirstOrDefaultAsync(s => s.Id == slotId);
        if (slot == null)
        {
            return NotFound();
        }

        // slot = _mapper.Map<Slot>(slotModel);
        slot.StartTime = slotModel.StartTime;
        slot.EndTime = slotModel.EndTime;
        slot.CafeId = slotModel.CafeId;
        slot.ProfileId = slotModel.ProfileId;
        slot.ProfileType = slotModel.ProfileType;

        try
        {
            await _dbcontext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!SlotExists(slotId))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{slotId}")]
    public async Task<IActionResult> DeleteSlot(int slotId)
    {
        var slot = await _dbcontext.Slots.FirstOrDefaultAsync(s => s.Id == slotId);

        if (slot == null)
        {
            return NotFound();
        }

        _dbcontext.Slots.Remove(slot);
        await _dbcontext.SaveChangesAsync();

        return NoContent();
    }

    private bool SlotExists(int slotId)
    {
        return _dbcontext.Slots.Any(s => s.Id == slotId);
    }
}
