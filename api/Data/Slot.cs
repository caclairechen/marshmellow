using api.Models;
namespace api.Data;

public class Slot
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int CafeId { get; set; }
    public int ProfileId { get; set; }
    public Cafe Cafe { get; set; }
    public ProfileType ProfileType { get; set; }
}