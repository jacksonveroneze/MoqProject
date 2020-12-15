using System.Threading.Tasks;
using MoqProject.Api.Models;
using Refit;

namespace MoqProject.Api.CrossCuting
{
    [Headers("Accept: application/json;charset=UTF-8")]
    public interface ICepRequest
    {
        [Get("/ws/{value}/json/")]
        Task<CepModel> FindByNumberAsync(string value);
    }
}