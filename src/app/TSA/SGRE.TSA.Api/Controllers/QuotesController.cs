using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteService quoteService;

        public QuotesController(IQuoteService quoteService)
        {
            this.quoteService = quoteService;
        }

        [HttpPut]
        public async Task<IActionResult> PutMyQuote(Quote quote)
        {
            bool ValidateIfQuoteExists = await DoesQuoteAlreadyExists(quote.QuotationId, quote.ProposalId);
            if (ValidateIfQuoteExists)
            {
                ModelState.AddModelError("QuoteExists", $"Under ProposalId {quote.ProposalId}, same quote already exists");
                return Conflict(ModelState);
            }
            if (await CheckMainQuote(quote) && quote.QuotationType)
            {
                ModelState.AddModelError("IsMain", $"InValid Form, Main Quote already exists for {quote.ProposalId}");
                return Conflict(ModelState);
            }
            var result = await quoteService.PutQuoteAsync(quote);
            if (result.IsSuccess)
            {
                return Ok(result.quoteResults);
            }

            return NotFound(result.quoteResults);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PatchMyQuote(int id, [FromBody] Quote quote)
        {
            if(quote.QuotationType && await CheckMainQuote(quote, id))
            {
                ModelState.AddModelError("IsMain", "InValid Form, Main Quote already exists for the given proposal");
                return BadRequest(ModelState);
            }
            var result = await quoteService.PatchQuoteAsync(id, quote);
            if (result.IsSuccess)
            {
                return Ok(result.quoteResults);
            }

            return NotFound(result.quoteResults);
        }

        private async Task<bool> CheckMainQuote(Quote quote, int id)
        {
            var result = await quoteService.GetQuotesAsync(quote.ProposalId);
            if (!result.IsSuccess)
                return false;
            var mainQuote = result.quoteResults.FirstOrDefault(p => p.QuotationType);
            if (mainQuote != null && mainQuote.Id == id)
            {
                return false;
            }

            if (mainQuote != null && mainQuote.Id != id)
            {
                return true;
            }
            return false;
        }

        private async Task<bool> CheckMainQuote(Quote quote)
        {
            var result = await quoteService.GetQuotesAsync(quote.ProposalId);
            if (!result.IsSuccess)
                return false;
            return result.quoteResults.Any(p => p.QuotationType);
        }

        private async Task<bool> DoesQuoteAlreadyExists(string sfQuotationId, int proposalId)
        {
            var result = await quoteService.GetQuotesAsync(proposalId);
            if (!result.IsSuccess)
                return false;
            return result.quoteResults.Any(quo => quo.QuotationId == sfQuotationId);
        }

        [HttpGet, Route("{proposalId}"), EnableQuery()]
        public async Task<IEnumerable<Quote>> GetQuotesByProposal(int proposalId)
        {
            var result = await quoteService.GetQuotesAsync(proposalId);
            if (result.IsSuccess)
            {
                return result.quoteResults;
            }

            return new List<Quote>();
        }

        [HttpGet, Route("TriggerCriteria/CatalogueId={CatalogueId}&Quantity={Quantity}"), EnableQuery()]
        public async Task<QuoteLine> GetTriggerCriteria([FromServices] IWtgCatalogueService wtgCatalogueService, int CatalogueId, int Quantity)
        {
            var result = await wtgCatalogueService.GetWtgCatalogueAsync(CatalogueId);
            var resultThreshold = await wtgCatalogueService.GetWtgThresholdAsync();

            if (result.IsSuccess && resultThreshold.IsSuccess)
            {
                WtgCatalogue catalogueResult = result.wtgCatalogueResult;

                int TurbineQuantity = resultThreshold.wtgThresholdResult.TurbineQuantity;
                decimal WindFarmSize = resultThreshold.wtgThresholdResult.WindFarmSize;

                decimal wfSize = result.wtgCatalogueResult.WtgSizeMW * Quantity;

                var triggerResult = new QuoteLine()
                {
                    Id = catalogueResult.Id,
                    WindfarmSizeTrigger = wfSize >= WindFarmSize,
                    QuantityTrigger = Quantity >= TurbineQuantity,
                    WindfarmSize = Math.Round(wfSize,2),
                    Quantity = Quantity,
                    WtgCatalogue = new WtgCatalogue { Id = catalogueResult.Id, WtgType = catalogueResult.WtgType }
                };

                return triggerResult;
            }
            return new QuoteLine();
        }

        [HttpGet, Route("QuoteId={QuoteId}"), EnableQuery()]
        public async Task<Quote> GetQuotesById(int QuoteId)
        {
            var result = await quoteService.GetQuotesByIdAsync(QuoteId);
            if (result.IsSuccess)
            {
                return result.quoteResults.FirstOrDefault();
            }

            return null;
        }

    }
}
