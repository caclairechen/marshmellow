using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace api.Controllers;

[ApiController]
[Route("users/{userId}/cafes")]
public class CafesController : ControllerBase
{
    private static readonly Mapper _mapper = new Mapper(
        new MapperConfiguration(cfg =>
         {
             cfg.CreateMap<CafeModel, Cafe>().ReverseMap();
             cfg.CreateMap<ICollection<CafeModel>, IEnumerable<Cafe>>().ReverseMap();
             cfg.CreateMap<SlotModel, Slot>().ReverseMap();
             cfg.CreateMap<ICollection<SlotModel>, IEnumerable<Slot>>().ReverseMap();
         }
        )
    );
    private readonly DataContext _dbcontext;

    public CafesController(DataContext context)
    {
        _dbcontext = context;
    }

    [HttpGet]
    public async Task<IEnumerable<CafeModel>> GetCafes(int userId)
    {
        var result = await _dbcontext.Users
               .Where(u => u.Id == userId)
               .Select(u => new
               {
                   cafes = u.Cafes
               })
               .FirstOrDefaultAsync();

        if (result == null)
        {
            throw new Exception("User does not exit");
        }

        return _mapper.Map<IEnumerable<CafeModel>>(result.cafes.ToList());
    }

    [HttpGet("{cafeId}")]
    public async Task<CafeModel> GetCafe(int userId, int cafeId)
    {
        var cafe = await _dbcontext.Cafes.Include(p => p.Owner)
           .FirstOrDefaultAsync(p => p.OwnerId == userId && p.Id == cafeId);

        if (cafe == null)
        {
            throw new Exception("Cafe does not exit");
        }

        if (cafe.Owner == null)
        {
            throw new Exception("User does not exit");
        }

        return _mapper.Map<CafeModel>(cafe);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCafe(int userId, CafeModel cafeModel)
    {
        var cafe = _mapper.Map<Cafe>(cafeModel);
        cafe.OwnerId = userId;

        _dbcontext.Cafes.Add(cafe);
        await _dbcontext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{cafeId}")]
    public async Task<IActionResult> UpdateCafe(int userId, int cafeId, CafeModel cafeModel)
    {
        if (cafeId != cafeModel.Id)
        {
            return BadRequest();
        }

        var cafe = await _dbcontext.Cafes.FirstOrDefaultAsync(c => c.Id == cafeId);
        if (cafe == null)
        {
            return NotFound();
        }

        // cafe = _mapper.Map<Cafe>(cafeModel);
        cafe.OwnerId = cafeModel.OwnerId;
        cafe.Name = cafeModel.Name;
        cafe.Address = cafeModel.Address;
        cafe.PhoneNumber = cafeModel.PhoneNumber;
        cafe.Email = cafeModel.Email;
        cafe.StartTime = cafeModel.StartTime;
        cafe.EndTime = cafeModel.EndTime;
        cafe.CustomerCapacity = cafeModel.CustomerCapacity;
        cafe.PetCapacity = cafeModel.PetCapacity;

        try
        {
            await _dbcontext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!CafeExists(cafeId))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{cafeId}")]
    public async Task<IActionResult> DeleteCafe(int userId, int cafeId)
    {
        var cafe = await _dbcontext.Cafes.FirstOrDefaultAsync(c => c.Id == cafeId);

        if (cafe == null)
        {
            return NotFound();
        }

        _dbcontext.Cafes.Remove(cafe);
        await _dbcontext.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{cafeId}/slots")]

    public async Task<IEnumerable<SlotModel>> GetSlotsOfCafe(int userId, int cafeId)
    {
        var result = await _dbcontext.Cafes
            .Where(u => u.Id == cafeId)
            .Select(u => new
            {
                slots = u.Slots
            })
            .FirstOrDefaultAsync();

        if (result == null)
        {
            throw new Exception("Cafe does not exit");
        }

        return _mapper.Map<IEnumerable<SlotModel>>(result.slots.ToList());
    }

    [HttpGet("{cafeId}/slots/{slotId}")]

    public async Task<SlotModel> GetSlotOfCafe(int userId, int cafeId, int slotId)
    {
        var slot = await _dbcontext.Slots.FirstOrDefaultAsync(p => p.CafeId == cafeId && p.Id == slotId);

        if (slot == null)
        {
            throw new Exception("Slot does not exit");
        }

        return _mapper.Map<SlotModel>(slot);
    }

    private bool CafeExists(int cafeId)
    {
        return _dbcontext.Cafes.Any(c => c.Id == cafeId);
    }
}
