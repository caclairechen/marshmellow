namespace api.Data;

public class Cafe
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public User Owner { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int CustomerCapacity { get; set; }
    public int PetCapacity { get; set; }
    public ICollection<Slot> Slots { get; set; }
}