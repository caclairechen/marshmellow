namespace api.Data;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public ICollection<Pet> Pets { get; set; }
    public ICollection<Cafe> Cafes { get; set; }
}