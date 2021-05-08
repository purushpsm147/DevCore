using Microsoft.Extensions.Caching.Memory;
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
    public class OpportunitiesControllerTest
    {
        //private readonly oq<IHttpClientFactory> httpClientFactory;

        //private Mock<IHttpClientFactory> _mockHttpClientFactory;

        OpportunitiesController _opportunitiesController;

        private readonly Mock<IOpportunityService> _opportunityService = new Mock<IOpportunityService>();

        private readonly Mock<IProposalService> _proposalService = new Mock<IProposalService>();
        private readonly Mock<IMemoryCache> _cache = new Mock<IMemoryCache>();

        public OpportunitiesControllerTest()
        {
           _opportunitiesController = new OpportunitiesController(_opportunityService.Object, _cache.Object);
        }


        [Fact(DisplayName="Should be able to get Opportunity By Id ")]
        public async Task GetOpportunityByIdIsSuccess()
        {
            IEnumerable<Project> project = new List<Project>() { new Project()
            {
                ProjectName = "Project Name",
                CustomerName = "Customer Name",
                ContractStatus = "Status",
            } };

            _opportunityService.Setup(x => x.GetOpportunityByIdAsync(It.IsAny<int>())).ReturnsAsync((true, project));

            var id = 1;
            var result = await _opportunitiesController.GetOpportunityById(id);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Should Fail to get the Data : Opportunity By Id ")]
        public async Task GetOpportunityByIdIsFailure()
        {
            IEnumerable<Project> project = null;

            _opportunityService.Setup(x => x.GetOpportunityByIdAsync(It.IsAny<int>())).ReturnsAsync((false, project));

            var id = 1;
            var result = await _opportunitiesController.GetOpportunityById(id);
            Assert.Null(result);
        }

        [Fact(DisplayName = "get All the Opportunity Records with user input parameter")]
        public async Task GetMyOpportunitiesIsSuccess()
        {
            IEnumerable<Project> project = new List<Project>() { new Project()
            {
                ProjectName = "Project Name",
                CustomerName = "Customer Name",
                ContractStatus = "Status",
            } };

            _opportunityService.Setup(x => x.GetMyOpportunityAsync(It.IsAny<string>())).ReturnsAsync((true, project));

            var result = await _opportunitiesController.GetMyOpportunities();
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "No Records for get All the Opportunity Records with user input parameter")]
        public async Task GetMyOpportunitiesIsFailure()
        {
            IEnumerable<Project> project = null;

            _opportunityService.Setup(x => x.GetMyOpportunityAsync(It.IsAny<string>())).ReturnsAsync((true, project));

            var result = await _opportunitiesController.GetMyOpportunities();
            Assert.Null(result);
        }

        [Fact(DisplayName = "get All the Search Opportunity Records")]
        public async Task SearchOpportunityIsSuccess()
        {
            IEnumerable<Project> project = new List<Project>() { new Project()
            {
                ProjectName = "Project Name",
                CustomerName = "Customer Name",
                ContractStatus = "Status",
            } };

            _opportunityService.Setup(x => x.SearchOpportunityAsync()).ReturnsAsync((true, project));

            var result = await _opportunitiesController.SearchOpportunityAsync();
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "No Records for get All the Search  Opportunity Records")]
        public async Task SearchOpportunityIsFailure()
        {
            IEnumerable<Project> project = null;

            _opportunityService.Setup(x => x.SearchOpportunityAsync()).ReturnsAsync((true, project));

            var result = await _opportunitiesController.SearchOpportunityAsync();
            Assert.Null(result);
        }

        [Fact(DisplayName = "Put My Opportunity")]
        public async Task PutMyOpportunityIsSuccess()
        {
            Project project = new Project()
            {
                ProjectName = "Project Name",
                CustomerName = "Customer Name",
                ContractStatus = "Status",
            };

            _opportunityService.Setup(x => x.PutProjectsAsync(It.IsAny<Project>())).ReturnsAsync((true, project));

            var result = await _opportunitiesController.PutMyOpportunities(project);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Patch My Opportunity")]
        public async Task PatchMyOpportunityIsSuccess()
        {
            Project project = new Project()
            {
                ProjectName = "Project Name",
                CustomerName = "Customer Name",
                ContractStatus = "Status",
            };

            _opportunityService.Setup(x => x.PatchProjectsAsync(It.IsAny<int>(), It.IsAny<Project>())).ReturnsAsync((true, project));

            var id = 1;
            var result = await _opportunitiesController.PatchMyOpportunities(id, project);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "get All the Proposals Based on Opportunity ID")]
        public async Task GetProposalByOpportunityIsSuccess()
        {
            var proposalService = new Mock<IProposalService>();

            IEnumerable<Proposal> proposals = new List<Proposal>() { new Proposal()
            {
                 Id =1,
                 ProjectId = 1,
                 RecordEndDateTime = DateTime.UtcNow,
                 LastModifiedDateTime = DateTime.UtcNow
            },
            new Proposal() {
                 Id =2,
                 ProjectId = 2,
                 RecordEndDateTime = DateTime.UtcNow,
                 LastModifiedDateTime = DateTime.UtcNow
            }};

            proposalService.Setup(x => x.GetProposalByOpportunityAsync(It.IsAny<int>())).ReturnsAsync((true, proposals));

            var opportunityId = 1;
            var result = await _opportunitiesController.GetProposalByOpportunity(proposalService.Object, opportunityId);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "No Records Proposals Based on Opportunity ID")]
        public async Task GetProposalByOpportunityIsFailure()
        {
            var proposalService = new Mock<IProposalService>();

            IEnumerable<Proposal> proposals = null;

            proposalService.Setup(x => x.GetProposalByOpportunityAsync(It.IsAny<int>())).ReturnsAsync((true, proposals));

            var opportunityId = 1;
            var result = await _opportunitiesController.GetProposalByOpportunity(proposalService.Object, opportunityId);
            Assert.Null(result);
        }


        [Fact(DisplayName = "get all the Proposals Based on Proposal ID and Opportunity ID")]
        public async Task GetProposalByIdIsSuccess()
        {
            var proposalService = new Mock<IProposalService>();

            IEnumerable<Proposal> proposals = new List<Proposal>() { new Proposal()
            {
                 Id =1,
                 ProjectId = 1,
                 RecordEndDateTime = DateTime.UtcNow,
                 LastModifiedDateTime = DateTime.UtcNow
            },
            new Proposal() {
                 Id =2,
                 ProjectId = 2,
                 RecordEndDateTime = DateTime.UtcNow,
                 LastModifiedDateTime = DateTime.UtcNow
            }};

            proposalService.Setup(x => x.GetProposalByOpportunityProposalIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((true, proposals));

            var opportunityId = 1;
            var proposal = 1;
            var result = await _opportunitiesController.GetProposalById(proposalService.Object, opportunityId, proposal);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "No Records For Proposals Based on Proposal ID and Opportunity ID")]
        public async Task GetProposalByIdIsFailure()
        {
            IEnumerable<Proposal> proposals = null;

            _proposalService.Setup(x => x.GetProposalByOpportunityProposalIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((true, proposals));

            var opportunityId = 1;
            var proposal = 1;
            var result = await _opportunitiesController.GetProposalById(_proposalService.Object, opportunityId, proposal);
            Assert.Null(result);
        }

        /*
                [Fact(DisplayName = "Put MyOpportunity Failed - HasDuplicateMilestones")]
                public async Task PutMyOpportunityIsFailure_HasDuplicateMilestones()
                {
                    Project project = new Project()
                    {
                        ProjectName = "Project Name",
                        CustomerName = "Customer Name",
                        ContractStatus = "Status",

                    };
                    //var a = new Mock<Project>(HasDuplicateMilestones) { CallBase = true };
                    var example = new Mock<Project>();
                    example.SetupGet(x => x.HasDuplicateMilestones).Returns(true);

                    _opportunityService.Setup(x => x.PutProjectsAsync(It.IsAny<Project>())).ReturnsAsync((true, project));

                    var OpportunityId = 1;

                    var result = await _opportunitiesController.PutMyOpportunities(project);
                    Assert.NotNull(result);
                }
        */


    }

}