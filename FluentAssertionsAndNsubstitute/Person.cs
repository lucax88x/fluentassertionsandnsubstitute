namespace FluentAssertionsAndNSubstitute;

public record Person
{
    public IList<Address> Addresses { get; init; } = new List<Address>();
    public required string Id { get; init; }
    public required string Name { get; init; }
}

public record Address
{
    public required string City { get; init; }
}

public class Repository
{
    private readonly Dictionary<string, Person> _database = new();

    public int Add(params Person[] persons)
    {
        foreach (var person in persons) _database.Add(person.Id, person);

        return persons.Length;
    }

    public Person Get(string id) => _database[id];
}