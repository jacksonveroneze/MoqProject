using System.Threading.Tasks;
using MoqProject.Api.Models;

namespace MoqProject.Api.Services
{
    public interface ICepService
    {
        public Task<CepModel> FindByCepAsync(string number);
    }
}