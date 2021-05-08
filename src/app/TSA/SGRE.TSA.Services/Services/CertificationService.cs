using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class CertificationService : ICertificationService
    {
        private readonly ICertificationExternalService certificationExternalService;

        public CertificationService(ICertificationExternalService certificationExternalService)
        {
            this.certificationExternalService = certificationExternalService;
        }
        public async Task<(bool IsSuccess, IEnumerable<Certification> certifications)> GetCertificationAsync()
        {
            var certification = await certificationExternalService.GetCertificationAsync();
            if (certification.IsSuccess)
            {
                return (true, certification.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<Certification> certifications)> GetCertificationAsync(int id)
        {
            var certification = await certificationExternalService.GetCertificationAsync(id);
            if (certification.IsSuccess)
            {
                return (true, certification.ResponseData);
            }

            return (false, null);
        }
    }
}
