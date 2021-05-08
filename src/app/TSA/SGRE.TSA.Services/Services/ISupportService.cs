using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ISupportService
    {
        Task<(bool IsSuccess, string SendMailResults)> SendMail(MailBody mailBody);
    }
}
