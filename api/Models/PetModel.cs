using api.Data;
namespace api.Models;

public class PetModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int OwnerId { get; set; }
    public int Age { get; set; }
    public GenderType Gender { get; set; }
    public string Species { get; set; }
}
