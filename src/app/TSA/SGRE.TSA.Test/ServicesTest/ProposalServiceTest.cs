using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SGRE.TSA.Test.ServicesTest
{
    public class ProposalServiceTest
    {
        /// <summary>
        /// Defines the IOpportunitiesExternalService
        /// </summary>
        private Mock<IProposalExternalService> _proposalExternalService = new Mock<IProposalExternalService>();

        /// <summary>
        /// The v
        /// </summary>
        /// <returns></returns>
        private ProposalService CreateProposalService()
        {
            return new ProposalService(_proposalExternalService.Object);
        }

        [Fact(DisplayName = "Get all Proposal By Opportunity")]
        public async Task GetProposalByOpportunityAsync_IsSuccess()
        {
            var proposalService = CreateProposalService();

            IEnumerable<Proposal> data = new List<Proposal>() { new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new Proposal()
            {
                Id = 2,
                ProjectId = 2,
                CertificationId = 2,
                IsMain = false,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            }};

            ExternalServiceResponse<IEnumerable<Proposal>> responseData = new ExternalServiceResponse<IEnumerable<Proposal>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _proposalExternalService.Setup(x => x.GetProposalByOpportunityAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var opportunityId = 1;
            var result = await proposalService.GetProposalByOpportunityAsync(opportunityId);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all Proposal By Opportunity No Records Found")]
        public async Task GetProposalByOpportunity_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var proposalService = CreateProposalService();

            IEnumerable<Proposal> data = new List<Proposal>() { new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new Proposal()
            {
                Id = 2,
                ProjectId = 2,
                CertificationId = 2,
                IsMain = false,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            }};

            ExternalServiceResponse<IEnumerable<Proposal>> responseData = new ExternalServiceResponse<IEnumerable<Proposal>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _proposalExternalService.Setup(x => x.GetProposalByOpportunityAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var opportunityId = 1;
            var result = await proposalService.GetProposalByOpportunityAsync(opportunityId);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all Proposal By Id")]
        public async Task GetProposalByIdAsync_IsSuccess()
        {
            var proposalService = CreateProposalService();

            IEnumerable<Proposal> data = new List<Proposal>() { new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new Proposal()
            {
                Id = 2,
                ProjectId = 2,
                CertificationId = 2,
                IsMain = false,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            }};

            ExternalServiceResponse<IEnumerable<Proposal>> responseData = new ExternalServiceResponse<IEnumerable<Proposal>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _proposalExternalService.Setup(x => x.GetProposalByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var id = 1;
            var result = await proposalService.GetProposalByIdAsync(id);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all Proposal By Id No Records Found")]
        public async Task GetProposalByIdAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var proposalService = CreateProposalService();

            IEnumerable<Proposal> data = new List<Proposal>() { new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new Proposal()
            {
                Id = 2,
                ProjectId = 2,
                CertificationId = 2,
                IsMain = false,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            }};

            ExternalServiceResponse<IEnumerable<Proposal>> responseData = new ExternalServiceResponse<IEnumerable<Proposal>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _proposalExternalService.Setup(x => x.GetProposalByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var id = 1;
            var result = await proposalService.GetProposalByIdAsync(id);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all Proposal By Opportunity Proposal Id")]
        public async Task GetProposalByOpportunityProposalIdAsync_IsSuccess()
        {
            var proposalService = CreateProposalService();

            IEnumerable<Proposal> data = new List<Proposal>() { new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new Proposal()
            {
                Id = 2,
                ProjectId = 2,
                CertificationId = 2,
                IsMain = false,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            }};

            ExternalServiceResponse<IEnumerable<Proposal>> responseData = new ExternalServiceResponse<IEnumerable<Proposal>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _proposalExternalService.Setup(x => x.GetProposalByOpportunityProposalIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((responseData));

            var opportunityId = 1;
            var proposalId = 1;
            var result = await proposalService.GetProposalByOpportunityProposalIdAsync(opportunityId, proposalId);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all Proposal By Opportunity Proposal Id No Records Found")]
        public async Task GetProposalByOpportunityProposalId_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var proposalService = CreateProposalService();

            IEnumerable<Proposal> data = new List<Proposal>() { new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new Proposal()
            {
                Id = 2,
                ProjectId = 2,
                CertificationId = 2,
                IsMain = false,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            }};

            ExternalServiceResponse<IEnumerable<Proposal>> responseData = new ExternalServiceResponse<IEnumerable<Proposal>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _proposalExternalService.Setup(x => x.GetProposalByOpportunityProposalIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((responseData));

            var opportunityId = 1;
            var proposalId = 1;
            var result = await proposalService.GetProposalByOpportunityProposalIdAsync(opportunityId, proposalId);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Update Proposal")]
        public async Task PatchProposalAsync_IsSuccess()
        {
            //Arrange
            var proposalService = CreateProposalService();

            Proposal proposal = new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<Proposal> responseData = new ExternalServiceResponse<Proposal>()
            {
                ResponseData = proposal,
                IsSuccess = true
            };

            _proposalExternalService.Setup(x => x.PatchProposalAsync(It.IsAny<int>(), It.IsAny<Proposal>())).ReturnsAsync((responseData));

            int id = 1;
            var result = await proposalService.PatchProposalAsync(id, proposal);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Update Proposal No Records Found")]
        public async Task PatchProposalAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var proposalService = CreateProposalService();

            Proposal proposal = new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<Proposal> responseData = new ExternalServiceResponse<Proposal>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _proposalExternalService.Setup(x => x.PatchProposalAsync(It.IsAny<int>(), It.IsAny<Proposal>())).ReturnsAsync((responseData));

            int id = 1;
            var result = await proposalService.PatchProposalAsync(id, proposal);

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Insert New Project")]
        public async Task PutProposalAsync_IsSuccess()
        {
            //Arrange
            var proposalService = CreateProposalService();

            Proposal proposal = new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<Proposal> responseData = new ExternalServiceResponse<Proposal>()
            {
                ResponseData = proposal,
                IsSuccess = true
            };

            _proposalExternalService.Setup(x => x.PutProposalAsync(It.IsAny<Proposal>())).ReturnsAsync((responseData));

            var result = await proposalService.PutProposalAsync(proposal);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "New Project Scenario No Records Found")]
        public async Task PutProjectsAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var proposalService = CreateProposalService();

            Proposal proposal = new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<Proposal> responseData = new ExternalServiceResponse<Proposal>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _proposalExternalService.Setup(x => x.PutProposalAsync(It.IsAny<Proposal>())).ReturnsAsync((responseData));

            var result = await proposalService.PutProposalAsync(proposal);

            Assert.False(result.IsSuccess);
        }
    }
}