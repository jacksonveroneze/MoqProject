using System.Collections.Generic;
using System.Threading.Tasks;
using MoqProject.Api.Models;

namespace MoqProject.Api.Repositories
{
    public class CepRepository : ICepRepository
    {
        public Task Add(CepModel cepModel)
        {
            return Task.CompletedTask;
        }

        public Task<CepModel> FindByCepAsync(string number)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<CepModel>> FindAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}