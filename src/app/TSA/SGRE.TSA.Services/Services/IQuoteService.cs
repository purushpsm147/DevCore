using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IQuoteService
    {
        Task<(bool IsSuccess, IEnumerable<Quote> quoteResults)> GetQuotesAsync(int proposalId);
        Task<(bool IsSuccess, IEnumerable<Quote> quoteResults)> GetQuotesByIdAsync(int id);
        Task<(bool IsSuccess, dynamic quoteResults)> PutQuoteAsync(Quote quote);
        Task<(bool IsSuccess, dynamic quoteResults)> PatchQuoteAsync(int id, Quote quote);
    }
}
