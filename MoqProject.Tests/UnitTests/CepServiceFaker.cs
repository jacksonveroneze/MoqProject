using Bogus;
using MoqProject.Api.Models;

namespace MoqProject.Tests.UnitTests
{
    public class CepServiceFaker
    {
        public static Faker<CepModel> GenerateFakerCepModel()
        {
            return new Faker<CepModel>()
                .RuleFor(x => x.Cep, f => f.Address.ZipCode())
                .RuleFor(x => x.Logradouro, f => f.Address.StreetName())
                .RuleFor(x => x.Complemento, f => f.Address.SecondaryAddress())
                .RuleFor(x => x.Bairro, f => f.Address.Direction())
                .RuleFor(x => x.Localidade, f => f.Address.SecondaryAddress())
                .RuleFor(x => x.Uf, f => f.Address.StateAbbr())
                .RuleFor(x => x.Ibge, 020423)
                .RuleFor(x => x.Uf, f => f.Address.StateAbbr())
                .RuleFor(x => x.Gia, f => f.Address.StateAbbr())
                .RuleFor(x => x.Ddd, 49)
                .RuleFor(x => x.Siafi, 0);
        }
    }
}