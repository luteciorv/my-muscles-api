using Bogus;
using MyMuscles.Domain.ValueObjects.Informacoes;

namespace MyMuscles.Common.UnitTesting.ValueObjects;

public static class NomeBuilder
{
    public static Nome Build()
    {
        var faker = new Faker("pt_BR");
        return new Nome(primeiro: faker.Name.FirstName(), sobrenome: faker.Name.LastName());
    } 
}
