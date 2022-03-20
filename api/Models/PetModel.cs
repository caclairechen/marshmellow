using api.Data;
namespace api.Models;

public class PetModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int OwnerId { get; set; }
    public float Age { get; set; }
    public string Gender { get; set; }
    public string Species { get; set; }
}
