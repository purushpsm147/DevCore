using Microsoft.AspNetCore.Mvc;
using Moq;
using SGRE.TSA.Api.Controllers;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SGRE.TSA.Test.ControllerTest
{
    public class QuotesControllerTest
    {
        QuotesController _quotesController;

        private readonly Mock<IQuoteService> _quoteService = new Mock<IQuoteService>();

        public QuotesControllerTest()
        {
            _quotesController = new QuotesController(_quoteService.Object);
        }


        [Fact(DisplayName= "Get Quotes By Id")]
        public async Task GetQuotesByIdIsSuccess()
        {
            IEnumerable<Quote> quote = new List<Quote>() { new Quote()
            {
                 Id = 1,
                 QuotationName = "Quote - 1",
                 LastModifiedDateTime = DateTime.UtcNow,
                 RecordInsertDateTime = DateTime.UtcNow
            } };

            _quoteService.Setup(x => x.GetQuotesByIdAsync(It.IsAny<int>())).ReturnsAsync((true, quote));

            var id = 1;
            var result = await _quotesController.GetQuotesById(id);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "No Data Get Quotes By Id")]
        public async Task GetQuotesByIdIsFailure()
        {
            _quoteService.Setup(x => x.GetQuotesByIdAsync(It.IsAny<int>())).ReturnsAsync((false, null));

            var id = 1;
            var result = await _quotesController.GetQuotesById(id);

            Assert.Null(result);
        }

        [Fact(DisplayName = "Insert New Quote Record")]
        public async Task PutMyQuoteIsSuccess()
        {
            Quote quote = new Quote()
            {
                 Id = 1,
                 QuotationName = "Quote - 1",
                 LastModifiedDateTime = DateTime.UtcNow,
                 RecordInsertDateTime = DateTime.UtcNow
            };

            _quoteService.Setup(x => x.PutQuoteAsync(It.IsAny<Quote>())).ReturnsAsync((true, quote));

            var result = await _quotesController.PutMyQuote(quote);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Failed to Insert New Quote Record")]
        public async Task PutMyQuoteIsFailure()
        {
            Quote quote = new Quote()
            {
                Id = 1,
                QuotationName = "Quote - 1",
                LastModifiedDateTime = DateTime.UtcNow,
                RecordInsertDateTime = DateTime.UtcNow
            };

            _quoteService.Setup(x => x.PutQuoteAsync(It.IsAny<Quote>())).ReturnsAsync((false, quote));

            var result = await _quotesController.PutMyQuote(quote);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }

        [Fact(DisplayName = "Update Quote Record")]
        public async Task PatchMyQuoteIsSuccess()
        {
            Quote quote = new Quote()
            {
                Id = 1,
                QuotationName = "Quote - 1",
                LastModifiedDateTime = DateTime.UtcNow,
                RecordInsertDateTime = DateTime.UtcNow
            };

            _quoteService.Setup(x => x.PatchQuoteAsync(It.IsAny<int>(), It.IsAny<Quote>())).ReturnsAsync((true, quote));

            var id = 1;
            var result = await _quotesController.PatchMyQuote(id, quote);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Failed to Insert New Scenario Record")]
        public async Task PatchMyScenarioIsFailure()
        {
            Quote quote = new Quote()
            {
                Id = 1,
                QuotationName = "Quote - 1",
                LastModifiedDateTime = DateTime.UtcNow,
                RecordInsertDateTime = DateTime.UtcNow
            };

            _quoteService.Setup(x => x.PatchQuoteAsync(It.IsAny<int>(), It.IsAny<Quote>())).ReturnsAsync((false, quote));

            var id = 1;
            var result = await _quotesController.PatchMyQuote(id, quote);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }
    }

}