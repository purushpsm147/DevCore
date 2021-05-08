using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteExternalService quoteExternalService;

        public QuoteService(IQuoteExternalService quoteExternalService)
        {
            this.quoteExternalService = quoteExternalService;
        }

        public async Task<(bool IsSuccess, IEnumerable<Quote> quoteResults)> GetQuotesAsync(int proposalId)
        {

            var quoteResult = await quoteExternalService.GetQuotesAsync(proposalId);
            if (quoteResult.IsSuccess)
            {
                return (true, quoteResult.ResponseData);
            }
            return (false, null);

        }
         public async Task<(bool IsSuccess, IEnumerable<Quote> quoteResults)> GetQuotesByIdAsync(int id)
        {

            var quoteResult = await quoteExternalService.GetQuotesByIdAsync(id);
            if (quoteResult.IsSuccess)
            {
                return (true, quoteResult.ResponseData);
            }
            return (false, null);

        }

        public async Task<(bool IsSuccess, dynamic quoteResults)> PutQuoteAsync(Quote quote)
        {
            var quoteResult = await quoteExternalService.PutQuoteAsync(quote);
            if (quoteResult.IsSuccess)
            {
                var result = new
                {
                    project = quoteResult.ResponseData
                };
                return (true, result);
            }

            return (false, quoteResult.ErrorMessage);
        }

        public async Task<(bool IsSuccess, dynamic quoteResults)> PatchQuoteAsync(int id, Quote quote)
        {
            var quoteResult = await quoteExternalService.PatchQuoteAsync(id, quote);
            if (quoteResult.IsSuccess)
            {
                var result = new
                {
                    project = quoteResult.ResponseData
                };
                return (true, result);
            }

            return (false, quoteResult.ErrorMessage);
        }
    }
}
