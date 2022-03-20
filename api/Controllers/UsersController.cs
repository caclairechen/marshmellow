using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace api.Controllers;

[ApiController]
[Route("users/")]
public class UsersController : ControllerBase
{
    private static readonly Mapper _mapper = new Mapper(
        new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserModel, User>().ReverseMap();
                cfg.CreateMap<ICollection<UserModel>, IEnumerable<User>>().ReverseMap();
                cfg.CreateMap<SlotModel, Slot>().ReverseMap();
                cfg.CreateMap<ICollection<SlotModel>, IEnumerable<Slot>>().ReverseMap();
            }
        )
    );
    private readonly DataContext _dbcontext;

    public UsersController(DataContext context)
    {
        _dbcontext = context;
    }

    [HttpGet]
    public async Task<IEnumerable<UserModel>> GetUsers()
    {
        var users = await _dbcontext.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserModel>>(users);
    }

    [HttpGet("{userId}")]
    public async Task<UserModel> GetUser(int userId)
    {
        var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new Exception("User does not exit");
        }

        return _mapper.Map<UserModel>(user);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(UserModel userModel)
    {
        var user = _mapper.Map<User>(userModel);

        _dbcontext.Users.Add(user);
        await _dbcontext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser(int userId, UserModel userModel)
    {
        if (userId != userModel.Id)
        {
            return BadRequest();
        }

        var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return NotFound();
        }

        // user = _mapper.Map<User>(userModel);
        user.Name = userModel.Name;
        user.PhoneNumber = userModel.PhoneNumber;
        user.Email = userModel.Email;

        try
        {
            await _dbcontext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!UserExists(userId))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return NotFound();
        }

        _dbcontext.Users.Remove(user);
        await _dbcontext.SaveChangesAsync();

        return NoContent();
    }


    [HttpGet("{userId}/slots")]

    public async Task<IEnumerable<SlotModel>> GetSlotsOfUser(int userId)
    {
        var slots = await _dbcontext.Slots
              .Where(s => s.ProfileType == ProfileType.User && s.ProfileId == userId)
              .ToListAsync();

        return _mapper.Map<IEnumerable<SlotModel>>(slots);
    }

    [HttpGet("{userId}/slots/{slotId}")]

    public async Task<SlotModel> GetSlotOfUser(int userId, int slotId)
    {
        var slot = await _dbcontext.Slots.FirstOrDefaultAsync(p => p.ProfileType == ProfileType.User && p.ProfileId == userId && p.Id == slotId);

        if (slot == null)
        {
            throw new Exception("Slot does not exit");
        }

        return _mapper.Map<SlotModel>(slot);
    }

    private bool UserExists(int userId)
    {
        return _dbcontext.Users.Any(u => u.Id == userId);
    }
}
