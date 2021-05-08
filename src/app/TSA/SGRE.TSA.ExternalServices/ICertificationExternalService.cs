using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface ICertificationExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<Certification>>> GetCertificationAsync();
        Task<ExternalServiceResponse<IEnumerable<Certification>>> GetCertificationAsync(int id);
    }
}
