using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MoqProject.Api.CrossCuting;
using MoqProject.Api.Models;
using MoqProject.Api.Repositories;
using Xunit;

namespace MoqProject.Tests.UnitTests
{
    [CollectionDefinition(nameof(CepCollection))]
    public class CepCollection : ICollectionFixture<CepServiceFixture>
    {
    }

    public class CepServiceFixture : IDisposable
    {
        public readonly Mock<ICepRequest> MockCepRequest;
        public readonly Mock<ICepRepository> MockCepRepository;

        public CepServiceFixture()
        {
            MockCepRequest = new Mock<ICepRequest>();
            MockCepRepository = new Mock<ICepRepository>();

            CepModel cepModel = GenerateCepModel();

            MockCepRequest.Setup(x => x.FindByNumberAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(cepModel));

            MockCepRepository.Setup(x => x.Add(It.IsAny<CepModel>()))
                .Returns(Task.CompletedTask);

            MockCepRepository.Setup(x => x.FindByCepAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(cepModel));
        }

        public CepModel GenerateCepModel()
            => CepServiceFaker.GenerateFakerCepModel().Generate();


        public IList<CepModel> GenerateCepModelCollection()
            => CepServiceFaker.GenerateFakerCepModel().Generate(5);

        public void Dispose()
        {
        }
    }
}