using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace api.Controllers;

[ApiController]
[Route("users/{userId}/pets")]
public class PetsController : ControllerBase
{
    private static readonly Mapper _mapper = new Mapper(
        new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PetModel, Pet>().ReverseMap();
                cfg.CreateMap<ICollection<PetModel>, IEnumerable<Pet>>().ReverseMap();
                cfg.CreateMap<SlotModel, Slot>().ReverseMap();
                cfg.CreateMap<ICollection<SlotModel>, IEnumerable<Slot>>().ReverseMap();
            }
        )
    );
    private readonly DataContext _dbcontext;

    public PetsController(DataContext context)
    {
        _dbcontext = context;
    }

    [HttpGet]
    public async Task<IEnumerable<PetModel>> GetPets(int userId)
    {
        var result = await _dbcontext.Users
              .Where(u => u.Id == userId)
              .Select(u => new
              {
                  pets = u.Pets
              })
              .FirstOrDefaultAsync();

        if (result == null)
        {
            throw new Exception("User does not exit");
        }

        return _mapper.Map<IEnumerable<PetModel>>(result.pets.ToList());
    }

    [HttpGet("{petId}")]
    public async Task<PetModel> GetPet(int userId, int petId)
    {
        var pet = await _dbcontext.Pets.Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.OwnerId == userId && p.Id == petId);

        if (pet == null)
        {
            throw new Exception("Pet does not exit");
        }

        if (pet.Owner == null)
        {
            throw new Exception("User does not exit");
        }

        return _mapper.Map<PetModel>(pet);
    }

    [HttpPost]
    public async Task<ActionResult> CreatePet(int userId, PetModel petModel)
    {
        var pet = _mapper.Map<Pet>(petModel);
        pet.Gender = petModel.Gender == "Male" ? GenderType.Male : GenderType.Female;
        pet.OwnerId = userId;

        _dbcontext.Pets.Add(pet);
        await _dbcontext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{petId}")]
    public async Task<IActionResult> UpdatePet(int userId, int petId, PetModel petModel)
    {
        if (petId != petModel.Id)
        {
            return BadRequest();
        }

        var pet = await _dbcontext.Pets.FirstOrDefaultAsync(p => p.Id == petId);
        if (pet == null)
        {
            return NotFound();
        }

        // pet = _mapper.Map<Pet>(petModel);
        pet.Name = petModel.Name;
        pet.OwnerId = petModel.OwnerId;
        pet.Age = petModel.Age;
        pet.Gender = petModel.Gender == "Male" ? GenderType.Male : GenderType.Female;
        pet.Species = petModel.Species;

        try
        {
            await _dbcontext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!PetExists(petId))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{petId}")]
    public async Task<IActionResult> DeletePet(int userId, int petId)
    {
        var pet = await _dbcontext.Pets.FirstOrDefaultAsync(p => p.Id == petId);

        if (pet == null)
        {
            return NotFound();
        }

        _dbcontext.Pets.Remove(pet);
        await _dbcontext.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{petId}/slots")]

    public async Task<IEnumerable<SlotModel>> GetSlotsOfPet(int userId, int petId)
    {
        var slots = await _dbcontext.Slots
            .Where(s => s.ProfileType == ProfileType.Pet && s.ProfileId == petId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<SlotModel>>(slots);
    }

    [HttpGet("{petId}/slots/{slotId}")]

    public async Task<SlotModel> GetSlotOfPet(int userId, int petId, int slotId)
    {
        var slot = await _dbcontext.Slots.FirstOrDefaultAsync(p => p.ProfileType == ProfileType.Pet && p.ProfileId == petId && p.Id == slotId);

        if (slot == null)
        {
            throw new Exception("Slot does not exit");
        }

        return _mapper.Map<SlotModel>(slot);
    }

    private bool PetExists(int petId)
    {
        return _dbcontext.Pets.Any(p => p.Id == petId);
    }
}
