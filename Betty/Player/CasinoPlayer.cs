// See https://aka.ms/new-console-template for more information
public class CasinoPlayer
{
    public CasinoPlayer(Guid id, string name, int startingBalance)
    {
        Id = id;
        Name = name;
        Balance = startingBalance;
    }

    public Guid Id { get; }
    public string Name { get; }
    public int Balance { get; set; }
}