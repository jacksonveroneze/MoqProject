using System.Threading.Tasks;
using MoqProject.Api.CrossCuting;
using MoqProject.Api.Models;
using MoqProject.Api.Repositories;

namespace MoqProject.Api.Services
{
    public class CepService : ICepService
    {
        private readonly ICepRequest _cepRequest;
        private readonly ICepRepository _cepRepository;

        public CepService(ICepRequest cepRequest, ICepRepository cepRepository)
        {
            _cepRequest = cepRequest;
            _cepRepository = cepRepository;
        }

        public async Task<CepModel> FindByCepAsync(string number)
        {
            CepModel cepModelDatabase = await _cepRepository.FindByCepAsync(number);

            if (cepModelDatabase != null)
                return cepModelDatabase;

            CepModel cepModel = await _cepRequest.FindByNumberAsync(number);

            await _cepRepository.Add(cepModel);

            return cepModel;
        }
    }
}