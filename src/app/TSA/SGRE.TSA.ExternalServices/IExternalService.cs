using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IExternalService<T>
    {
        Task<ExternalServiceResponse<IEnumerable<T>>> GetAsync(string queryPram);
        Task<ExternalServiceResponse<IEnumerable<T>>> GetByIdAsync(int id);
        Task<ExternalServiceResponse<T>> PutAsync(T model);
        Task<ExternalServiceResponse<T>> PatchAsync(int id, T model);
        Task<ExternalServiceResponse<T>> PatchAsync(T model);
        Task<ExternalServiceResponse<IEnumerable<T>>> DeleteAsync(int id);


    }
}
