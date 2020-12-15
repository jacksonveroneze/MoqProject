using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus.DataSets;
using FluentAssertions;
using Moq;
using MoqProject.Api.CrossCuting;
using MoqProject.Api.Models;
using MoqProject.Api.Repositories;
using MoqProject.Api.Services;
using Xunit;

namespace MoqProject.Tests.UnitTests
{
    [Collection(nameof(CepCollection))]
    public class CepService
    {
        private readonly CepServiceFixture _cepServiceFixture;
        private ICepService _cepService;

        public CepService(CepServiceFixture cepServiceFixture)
            => _cepServiceFixture = cepServiceFixture;

        [Fact(DisplayName = "Deve buscar os dados corretammente.")]
        [Trait("CepService2", "CepService")]
        public async Task CepService_FindByCepAsync_DeveBuscarOsDadosNoWSQuandoNaoAcharLocalmente()
        {
            // Arange
            Mock<ICepRequest> mockCepRequest = new Mock<ICepRequest>();
            Mock<ICepRepository> mockCepRepository = new Mock<ICepRepository>();

            CepModel cepModel = _cepServiceFixture.GenerateCepModel();

            mockCepRequest.Setup(x => x.FindByNumberAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(cepModel));

            mockCepRepository.Setup(x => x.Add(It.IsAny<CepModel>()))
                .Returns(Task.CompletedTask);

            _cepService = new Api.Services.CepService(mockCepRequest.Object, mockCepRepository.Object);

            // Act
            CepModel result = await _cepService.FindByCepAsync("89665000");

            // Assert
            result.Should().NotBeNull();
            mockCepRepository.Verify(x => x.FindByCepAsync("89665000"), Times.Once);
            mockCepRequest.Verify(x => x.FindByNumberAsync("89665000"), Times.Once);
            mockCepRepository.Verify(x => x.Add(cepModel), Times.Once);
        }

        [Fact(DisplayName = "Deve buscar os dados corretammente.")]
        [Trait("CepService2", "CepService")]
        public async Task CepService_FindByCepAsync_NaoDeveBuscarOsDadosNoWSQuandoAcharLocalmente()
        {
            // Arange
            Mock<ICepRequest> mockCepRequest = new Mock<ICepRequest>();
            Mock<ICepRepository> mockCepRepository = new Mock<ICepRepository>();

            CepModel cepModel = _cepServiceFixture.GenerateCepModel();

            mockCepRepository.Setup(x => x.FindByCepAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(cepModel));

            _cepService = new Api.Services.CepService(mockCepRequest.Object, mockCepRepository.Object);

            // Act
            CepModel result = await _cepService.FindByCepAsync("89665000");

            // Assert
            result.Should().NotBeNull();
            mockCepRepository.Verify(x => x.FindByCepAsync("89665000"), Times.Once);
            mockCepRequest.Verify(x => x.FindByNumberAsync("89665000"), Times.Never);
            mockCepRepository.Verify(x => x.Add(cepModel), Times.Never);
        }
    }
}