using api.Data;

namespace api.Models;

public class SlotModel
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int CafeId { get; set; }
    public int ProfileId { get; set; }
    public ProfileType ProfileType { get; set; }
}