using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IQuoteExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<Quote>>> GetQuotesAsync(int proposalId);
        Task<ExternalServiceResponse<IEnumerable<Quote>>> GetQuotesByIdAsync(int id);
        Task<ExternalServiceResponse<Quote>> PutQuoteAsync(Quote quote);
        Task<ExternalServiceResponse<Quote>> PatchQuoteAsync(int id, Quote quote);
    }
}
