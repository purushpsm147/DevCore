using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class ApplicationReasonService : IApplicationReasonService
    {
        private readonly ExternalServices.IApplicationReasonExternalService _applicationReasonExternalService;
        public ApplicationReasonService(ExternalServices.IApplicationReasonExternalService applicationReasonExternalService)
        {
            _applicationReasonExternalService = applicationReasonExternalService;
        }

        public async Task<(bool IsSuccess, IEnumerable<ApplicationReason> ApplicationReasonResult)> GetApplicationReasonAsync()
        {
            var applicationReasonResult = await _applicationReasonExternalService.GetApplicationReasonAsync();
            if (applicationReasonResult.IsSuccess)
            {
                
                return (true, applicationReasonResult.ResponseData);
            }

            return (false, null);
        }             
    }
}
