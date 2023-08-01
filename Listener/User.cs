namespace Listener;

public class User
{
    public static int gId = 1;
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }

    public User()
    {
        Id = gId++;
    }
    public override string ToString() => $"{Id} {Name} {Surname}";
}
