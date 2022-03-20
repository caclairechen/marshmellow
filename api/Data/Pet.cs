namespace api.Data;

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int OwnerId { get; set; }
    public User Owner { get; set; }
    public float Age { get; set; }
    public GenderType Gender { get; set; }
    public string Species { get; set; }
}