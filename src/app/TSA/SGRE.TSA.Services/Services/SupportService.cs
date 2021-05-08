using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class SupportService : ISupportService
    {
        private readonly ISupportExternalService _supportExternalService;

        public SupportService(ISupportExternalService supportExternalService)
        {
            _supportExternalService = supportExternalService;
        }

        public async Task<(bool IsSuccess, string SendMailResults)> SendMail(MailBody mailBody)
        {
            var result = await _supportExternalService.SendMail(mailBody);
            if (result.IsSuccess)
            {
                return (true, result.ResponseData);
            }
            return (false, null);
        }
    }
}
