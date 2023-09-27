using System.Collections;
using AutoFixture;
using FluentAssertions;

namespace FluentAssertionsAndNSubstitute;

public class RepositoryTests
{
    private readonly Repository _sut;

    public RepositoryTests()
    {
        _sut = new Repository();
    }

    [Fact]
    public void CreatePerson_Should_GenerateRandomNameAndAge()
    {
        // Setup
        Fixture fixture = new();

        var person = fixture.Build<Person>()
            .With(p => p.Parents, fixture.CreateMany<Person>().ToList())
            .Create();

        // Exercise
        _sut.Add(person);

        // Verify
        var received = _sut.Get(person.Id);

        received
            .Should()
            .BeEquivalentTo(person);

        received.Parents
            .Should()
            .HaveCountGreaterThan(1);
    }
}