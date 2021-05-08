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
    public class ProposalsControllerTest
    {
        ProposalsController _proposalsController;

        private readonly Mock<IProposalService> _proposalService = new Mock<IProposalService>();

        public ProposalsControllerTest()
        {
            _proposalsController = new ProposalsController(_proposalService.Object);
        }

        [Fact(DisplayName= "Get GetProposal By Id Success")]
        public async Task GetProposalByIdIsSuccess()
        {
            IEnumerable<Proposal> projectConstraint = new List<Proposal>() { new Proposal()
            {
                 Id = 1,
                 ProjectId = 1,
                 LastModifiedDateTime = DateTime.UtcNow,
                 RecordInsertDateTime = DateTime.UtcNow
            } };

            _proposalService.Setup(x => x.GetProposalByIdAsync(It.IsAny<int>())).ReturnsAsync((true, projectConstraint));

            int id = 1;
            var result = await _proposalsController.GetProposalById(id);
            Assert.NotNull(result);
        }


        [Fact(DisplayName = "Get Project Proposal By Id No Data Found")]
        public async Task GetProposalByIdIsFailure()
        {
            _proposalService.Setup(x => x.GetProposalByIdAsync(It.IsAny<int>())).ReturnsAsync((false, null));

            int id = 1;
            var result = await _proposalsController.GetProposalById(id);

            Assert.Null(result);
        }

        [Fact(DisplayName = "Insert New Proposal Record")]
        public async Task PutMyProposalIsSuccess()
        {
            Proposal proposal = new Proposal()
            {
                ProjectId = 1,
                CertificationId = 1, 
                ActiveRecordIndicator = "yes",
                LastModifiedDateTime = DateTime.UtcNow,
                RecordInsertDateTime = DateTime.UtcNow

            };

            _proposalService.Setup(x => x.PutProposalAsync(It.IsAny<Proposal>())).ReturnsAsync((true, proposal));

            var result = await _proposalsController.PutMyProposal(proposal);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Failed to Insert New Proposal Constraint Record")]
        public async Task PutProposalFailure()
        {
            Proposal proposal = new Proposal()
            {
                ProjectId = 1,
                CertificationId = 1,
                ActiveRecordIndicator = "yes",
                LastModifiedDateTime = DateTime.UtcNow,
                RecordInsertDateTime = DateTime.UtcNow

            };

            _proposalService.Setup(x => x.PutProposalAsync(It.IsAny<Proposal>())).ReturnsAsync((false, proposal));

            var result = await _proposalsController.PutMyProposal(proposal);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }

        [Fact(DisplayName = "Update Proposal Record")]
        public async Task PatchProposalAsyncIsSuccess()
        {

            Proposal proposal = new Proposal()
            {
                ProjectId = 1,
                CertificationId = 1,
                ActiveRecordIndicator = "yes",
                LastModifiedDateTime = DateTime.UtcNow,
                RecordInsertDateTime = DateTime.UtcNow

            };

            _proposalService.Setup(x => x.PatchProposalAsync(It.IsAny<int>(), It.IsAny<Proposal>())).ReturnsAsync((true, proposal));

            var id = 1;
            var result = await _proposalsController.PatchMyProposal(id, proposal);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Failed to Update Proposal Record")]
        public async Task PatchProposalIsFailure()
        {
            Proposal proposal = new Proposal()
            {
                ProjectId = 1,
                CertificationId = 1,
                ActiveRecordIndicator = "yes",
                LastModifiedDateTime = DateTime.UtcNow,
                RecordInsertDateTime = DateTime.UtcNow

            };

            _proposalService.Setup(x => x.PatchProposalAsync(It.IsAny<int>(), It.IsAny<Proposal>())).ReturnsAsync((false, proposal));

            var id = 1;
            var result = await _proposalsController.PatchMyProposal(id, proposal);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }
    }

}