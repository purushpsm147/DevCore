using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface ISupportExternalService
    {
        Task<ExternalServiceResponse<string>> SendMail(MailBody mailBody);

    }
}
