using System.Collections.Generic;
using System.Threading.Tasks;
using MoqProject.Api.Models;

namespace MoqProject.Api.Repositories
{
    public interface ICepRepository
    {
        public Task Add(CepModel cepModel);
        public Task<CepModel> FindByCepAsync(string number);
        public Task<IList<CepModel>> FindAllAsync();
    }
}