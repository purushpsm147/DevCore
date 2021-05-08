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
    public class OpportunityTest
    {
        /// <summary>
        /// Defines the IOpportunitiesExternalService
        /// </summary>
        private Mock<IOpportunitiesExternalService> _opportunitiesExternalService = new Mock<IOpportunitiesExternalService>();

        /// <summary>
        /// The OpportunityService
        /// </summary>
        /// <returns></returns>
        private OpportunityService CreateOpportunityService()
        {
            return new OpportunityService(_opportunitiesExternalService.Object);
        }

        [Fact(DisplayName = "Get all MyOpportunity")]
        public async Task GetMyOpportunityAsync_IsSuccess()
        {
            var opportunityService = CreateOpportunityService();

            IEnumerable<Project> data = new List<Project>() { new Project()
            {
                Id = 1,
                ProjectName = "Project 1",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
                UpdatedBy = "User - 1"

            },
            new Project()
            {
                Id = 2,
                ProjectName = "Project 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
                UpdatedBy = "User - 1"
            }};

            ExternalServiceResponse<IEnumerable<Project>> responseData = new ExternalServiceResponse<IEnumerable<Project>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _opportunitiesExternalService.Setup(x => x.GetProjectsByUserAsync(It.IsAny<string>())).ReturnsAsync((responseData));

            var user = "user - 1";
            var result = await opportunityService.GetMyOpportunityAsync(user);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all Country No Records Found")]
        public async Task GetCountryAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var opportunityService = CreateOpportunityService();

            IEnumerable<Project> data = new List<Project>() { new Project()
            {
                Id = 1,
                ProjectName = "Project 1",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
                UpdatedBy = "User - 1"

            },
            new Project()
            {
                Id = 2,
                ProjectName = "Project 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
                UpdatedBy = "User - 1"
            }};

            ExternalServiceResponse<IEnumerable<Project>> responseData = new ExternalServiceResponse<IEnumerable<Project>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _opportunitiesExternalService.Setup(x => x.GetProjectsByUserAsync(It.IsAny<string>())).ReturnsAsync((responseData));

            var user = "user - 1";
            var result = await opportunityService.GetMyOpportunityAsync(user);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all MyOpportunity By Id")]
        public async Task GetMyOpportunityByIdAsync_IsSuccess()
        {
            var opportunityService = CreateOpportunityService();

            IEnumerable<Project> data = new List<Project>() { new Project()
            {
                Id = 1,
                ProjectName = "Project 1",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
                UpdatedBy = "User - 1"

            },
            new Project()
            {
                Id = 2,
                ProjectName = "Project 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
                UpdatedBy = "User - 1"
            }};

            ExternalServiceResponse<IEnumerable<Project>> responseData = new ExternalServiceResponse<IEnumerable<Project>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _opportunitiesExternalService.Setup(x => x.GetProjectsAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var id = 1;
            var result = await opportunityService.GetOpportunityByIdAsync(id);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all Country By Id No Records Found")]
        public async Task GetCountryByIdAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var opportunityService = CreateOpportunityService();

            IEnumerable<Project> data = new List<Project>() { new Project()
            {
                Id = 1,
                ProjectName = "Project 1",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
                UpdatedBy = "User - 1"

            },
            new Project()
            {
                Id = 2,
                ProjectName = "Project 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
                UpdatedBy = "User - 1"
            }};

            ExternalServiceResponse<IEnumerable<Project>> responseData = new ExternalServiceResponse<IEnumerable<Project>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _opportunitiesExternalService.Setup(x => x.GetProjectsAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var id = 1;
            var result = await opportunityService.GetOpportunityByIdAsync(id);

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Search Opportunity")]
        public async Task SearchOpportunityAsync_IsSuccess()
        {
            var opportunityService = CreateOpportunityService();

            IEnumerable<Project> data = new List<Project>() { new Project()
            {
                Id = 1,
                ProjectName = "Project 1",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
                UpdatedBy = "User - 1"

            },
            new Project()
            {
                Id = 2,
                ProjectName = "Project 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
                UpdatedBy = "User - 1"
            }};

            ExternalServiceResponse<IEnumerable<Project>> responseData = new ExternalServiceResponse<IEnumerable<Project>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _opportunitiesExternalService.Setup(x => x.GetProjectsAsync(It.IsAny<string>())).ReturnsAsync((responseData));

            var result = await opportunityService.SearchOpportunityAsync();

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Search Opportunity No Records Found")]
        public async Task SearchOpportunityAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var opportunityService = CreateOpportunityService();

            IEnumerable<Project> data = new List<Project>() { new Project()
            {
                Id = 1,
                ProjectName = "Project 1",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
                UpdatedBy = "User - 1"

            },
            new Project()
            {
                Id = 2,
                ProjectName = "Project 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
                UpdatedBy = "User - 1"
            }};

            ExternalServiceResponse<IEnumerable<Project>> responseData = new ExternalServiceResponse<IEnumerable<Project>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _opportunitiesExternalService.Setup(x => x.GetProjectsAsync(It.IsAny<string>())).ReturnsAsync((responseData));

            var result = await opportunityService.SearchOpportunityAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Update Project")]
        public async Task PatchProjectsAsync_IsSuccess()
        {
            //Arrange
            var opportunityService = CreateOpportunityService();

            Project project = new Project()
            {
                Id = 2,
                ProjectName = "Project 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow,
                UpdatedBy = "User - 1"
            };

            ExternalServiceResponse<Project> responseData = new ExternalServiceResponse<Project>()
            {
                ResponseData = project,
                IsSuccess = true
            };

            _opportunitiesExternalService.Setup(x => x.PatchProjectsAsync(It.IsAny<int>(), It.IsAny<Project>())).ReturnsAsync((responseData));

            int id = 1;
            var result = await opportunityService.PatchProjectsAsync(id, project);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Update Scenario No Records Found")]
        public async Task PatchProjectsAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var opportunityService = CreateOpportunityService();

            Project project = new Project()
            {
                Id = 2,
                ProjectName = "Project 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow,
                UpdatedBy = "User - 1"
            };

            ExternalServiceResponse<Project> responseData = new ExternalServiceResponse<Project>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _opportunitiesExternalService.Setup(x => x.PatchProjectsAsync(It.IsAny<int>(), It.IsAny<Project>())).ReturnsAsync((responseData));

            int id = 1;
            var result = await opportunityService.PatchProjectsAsync(id, project);

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Insert New Project")]
        public async Task PutProjectsAsync_IsSuccess()
        {
            //Arrange
            var opportunityService = CreateOpportunityService();

            Project project = new Project()
            {
                Id = 2,
                ProjectName = "Project 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow,
                UpdatedBy = "User - 1"
            };

            ExternalServiceResponse<Project> responseData = new ExternalServiceResponse<Project>()
            {
                ResponseData = project,
                IsSuccess = true
            };

            _opportunitiesExternalService.Setup(x => x.PutProjectsAsync(It.IsAny<Project>())).ReturnsAsync((responseData));

            var result = await opportunityService.PutProjectsAsync(project);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "New Project Scenario No Records Found")]
        public async Task PutProjectsAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var opportunityService = CreateOpportunityService();

            Project project = new Project()
            {
                Id = 2,
                ProjectName = "Project 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow,
                UpdatedBy = "User - 1"
            };

            ExternalServiceResponse<Project> responseData = new ExternalServiceResponse<Project>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _opportunitiesExternalService.Setup(x => x.PutProjectsAsync(It.IsAny<Project>())).ReturnsAsync((responseData));

            var result = await opportunityService.PutProjectsAsync(project);

            Assert.False(result.IsSuccess);
        }
    }
}