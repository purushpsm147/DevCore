using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public class QuoteExternalService : IQuoteExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<QuoteExternalService> logger;

        public QuoteExternalService(IHttpClientFactory httpClientFactory, ILogger<QuoteExternalService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<ExternalServiceResponse<IEnumerable<Quote>>> GetQuotesAsync(int proposalId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                //TODO: Take care of magic strings
                var response = await client.GetAsync($"api/Quotes/?$expand=QuoteLines($expand=WtgCatalogue)");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Quote>>(content, options);
                    IEnumerable<Quote> filterQuote = result.Where(q => q.ProposalId == proposalId);

                    return new ExternalServiceResponse<IEnumerable<Quote>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = filterQuote
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Quote>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Quote>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<Quote>> PutQuoteAsync(Quote quote)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var jsonString = JsonConvert.SerializeObject(quote, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"api/Quotes", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return new ExternalServiceResponse<Quote>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = quote
                    };
                }

                return new ExternalServiceResponse<Quote>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<Quote>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }

        public async Task<ExternalServiceResponse<Quote>> PatchQuoteAsync(int id, Quote quote)
        {
            try
            {
                ICollection<QuoteLine> QuoteLines = quote.QuoteLines;
                if (QuoteLines != null)
                {
                    var deletedCon = QuoteLines.Where(c => c.IsDeleted);

                    foreach (var item in deletedCon.ToList())
                    {
                        await DeleteQuoteLineAsync(item.Id);
                        quote.QuoteLines.Remove(item);
                    }
                }

                var client = httpClientFactory.CreateClient("ToSAService");

                var jsonString = JsonConvert.SerializeObject(quote, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PatchAsync($"api/Quotes/{id}", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return new ExternalServiceResponse<Quote>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = quote
                    };
                }

                return new ExternalServiceResponse<Quote>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<Quote>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }

        private async System.Threading.Tasks.Task DeleteQuoteLineAsync(int? id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.DeleteAsync($"api/QuoteLine/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"QuoteLine {id} not deleted");
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
            }
        }

        public async Task<ExternalServiceResponse<IEnumerable<Quote>>> GetQuotesByIdAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                //TODO: Take care of magic strings
                var response = await client.GetAsync($"api/Quotes/?$filter=id eq {id}&$expand=QuoteLines($expand=WtgCatalogue)");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Quote>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<Quote>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Quote>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Quote>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }
    }
}
