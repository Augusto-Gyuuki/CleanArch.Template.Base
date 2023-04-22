using Bogus;

namespace CleanArch.Base.Template.Tests.Unit.Common.Fixtures;

public abstract class BaseFixture
{
    public Faker Faker { get; set; }

    protected BaseFixture()
        => Faker = new Faker("pt_BR");
}
