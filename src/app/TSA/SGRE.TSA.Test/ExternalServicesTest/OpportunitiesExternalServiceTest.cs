using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SGRE.TSA.Test.ExternalServicesTest
{
    public class OpportunitiesExternalServiceTest
    {
        /// <summary>
        /// Defines the mockHttpClientFactory
        /// </summary>
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        /// <summary>
        /// Defines the mocklogger
        /// </summary>
        private Mock<ILogger<OpportunitiesExternalService>> _mocklogger = new Mock<ILogger<OpportunitiesExternalService>>();

        /// <summary>
        /// Defines the supportExternalService
        /// </summary>
        private Mock<ISupportExternalService> _supportExternalService = new Mock<ISupportExternalService>();

        /// <summary>
        /// Defines the RoleExternalService
        /// </summary>
        private Mock<IRoleExternalService> _roleExternalService = new Mock<IRoleExternalService>();

        /// <summary>
        /// Defines the Configuration
        /// </summary>
        private Mock<IConfiguration> _configuration = new Mock<IConfiguration>();



        /// <summary>
        /// The RegionExternalService
        /// </summary>
        /// <returns></returns>
        private OpportunitiesExternalService CreateOpportunitiesExternalService()
        {
            return new OpportunitiesExternalService(_mockHttpClientFactory.Object, _mocklogger.Object, _supportExternalService.Object, _roleExternalService.Object, _configuration.Object);
        }

        [Fact(DisplayName = "Get All Projects")]
        public async Task GetProjectsAsync_IsSuccess()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

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

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(payload, Encoding.UTF8, "application/json"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var queryParam = "query";
            var result = await opportunitiesExternalService.GetProjectsAsync(queryParam);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get All Projects No Records Found")]
        public async Task GetProjectsAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

            IEnumerable<Project> data = new List<Project>() { };

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var queryParam = "query";
            var result = await opportunitiesExternalService.GetProjectsAsync(queryParam);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get All Projects Bad Request/Ecxeption")]
        public async Task GetProjectsAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var queryParam = "query";
            var result = await opportunitiesExternalService.GetProjectsAsync(queryParam);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get All Projects By User")]
        public async Task GetProjectsByUserAsync_IsSuccess()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

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

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(payload, Encoding.UTF8, "application/json"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var user = "User - 1";
            var result = await opportunitiesExternalService.GetProjectsByUserAsync(user);

            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "Get All Projects By User No Records Found")]
        public async Task GetProjectsByUserAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

            IEnumerable<Project> data = new List<Project>() { };

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var user = "User - 1";
            var result = await opportunitiesExternalService.GetProjectsByUserAsync(user);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get All Projects By User Bad Request/Ecxeption")]
        public async Task GetProjectsByUserAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var user = "User - 1";
            var result = await opportunitiesExternalService.GetProjectsByUserAsync(user);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get All Projects By Id")]
        public async Task GetProjectsByIdAsync_IsSuccess()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

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

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(payload, Encoding.UTF8, "application/json"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var id = 1;
            var result = await opportunitiesExternalService.GetProjectsAsync(id);

            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "Get All Projects By Id No Records Found")]
        public async Task GetProjectsByIdAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

            IEnumerable<Project> data = new List<Project>() { };

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var id = 1;
            var result = await opportunitiesExternalService.GetProjectsAsync(id);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get All Projects By Id Bad Request/Ecxeption")]
        public async Task GetProjectsByIdAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var id = 1;
            var result = await opportunitiesExternalService.GetProjectsAsync(id);

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Update Project")]
        public async Task PatchProjectsAsync_IsSuccess()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();
            //var _configSSTInputService = _sSTInputExternalService.Object;

            Project data = new Project()
            {
                Id = 513,
                OpportunityId = "000000000212",
                ProjectName = "Project 1",
                CustomerName = "Test",
                ContractStatus = "Open",
                Country = new Country() { CountryName = "Brazil", Id = 2, Region = new Region() { Id = 1, RegionName = "APAC" }, RegionId = 1 },
                CountryId = 2,
                CreatedBy = "Purushottam",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow,
                UpdatedBy = "User - 1",
                ProjectRoles = new List<ProjectRoles>() { new ProjectRoles()
                {
                    Id = 1,
                    ProjectId = 513,
                    Role = new Role(){ Id = 1, ActiveRecordIndicator = "Test", CreatedBy = "Puru",RoleDescription = "Test", RoleName = "Test"},
                    RoleId = 2,
                    userId = "Test",
                },
                new ProjectRoles()
                {
                    Id = 0,
                    ProjectId = 513,
                    Role = new Role(){ Id = 1, ActiveRecordIndicator = "Test", CreatedBy = "Puru",RoleDescription = "Test", RoleName = "Test0"},
                    RoleId = 2,
                    userId = "Test",
                }

                }

            };
            IEnumerable<Project> resultData = new List<Project>() { new Project()
            {
                Id = 513,
                OpportunityId = "000000000212",
                ProjectName = "Project 1",
                CustomerName = "Test",
                ContractStatus = "Open",
                Country = new Country() { CountryName = "Brazil", Id = 2, Region = new Region() { Id = 1, RegionName = "APAC" }, RegionId = 1 },
                CountryId = 2,
                CreatedBy = "Purushottam",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow,
                UpdatedBy = "User - 1",
                ProjectRoles = new List<ProjectRoles>() { new ProjectRoles()
                {
                    Id = 1,
                    ProjectId = 513,
                    Role = new Role(){ Id = 1, ActiveRecordIndicator = "Test", CreatedBy = "Puru",RoleDescription = "Test", RoleName = "Test"},
                    RoleId = 2,
                    userId = "Test",
                }, new ProjectRoles()
                {
                    Id = 2,
                    ProjectId = 513,
                    Role = new Role(){ Id = 1, ActiveRecordIndicator = "Test1", CreatedBy = "Puru",RoleDescription = "Test", RoleName = "Test"},
                    RoleId = 2,
                    userId = "Test",
                }}

            } };


            IEnumerable<Role> RolesData = new List<Role>() { new Role()
            {
                Id = 1,
                RoleName = "Engineer"
            },
            new Role()
            {
                Id = 2,
                RoleName = "Service Engineer"
            }};

            ExternalServiceResponse<IEnumerable<Role>> responseData = new ExternalServiceResponse<IEnumerable<Role>>()
            {
                ResponseData = RolesData,
                IsSuccess = false
            };

            _roleExternalService.Setup(x => x.GetRoleAsync()).ReturnsAsync((responseData));
            var SectionMock = new Mock<IConfigurationSection>();
            SectionMock.Setup(s => s.Value).Returns("https://20.73.195.228/");
            _configuration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(SectionMock.Object);
            string payload = JsonConvert.SerializeObject(resultData);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(payload, Encoding.UTF8, "application/json"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();


            var id = 1;
            var result = await opportunitiesExternalService.PatchProjectsAsync(id, data);

            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "Udpate Projects No Records Found")]
        public async Task PatchProjectsAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

            Project data = new Project()
            {
                Id = 1,
                OpportunityId = "Test",
                ProjectName = "Project 1",
                CustomerName = "Test",
                ContractStatus = "Open",
                CountryId = 2,
                CreatedBy = "Purushottam",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow,
                UpdatedBy = "User - 1"

            };

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var id = 1;
            var result = await opportunitiesExternalService.PatchProjectsAsync(id, data);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Update Bad Request/Ecxeption")]
        public async Task PatchProjectsAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

            Project data = new Project()
            {
                Id = 1,
                OpportunityId = "Test",
                ProjectName = "Project 1",
                CustomerName = "Test",
                ContractStatus = "Open",
                CountryId = 2,
                CreatedBy = "Purushottam",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow,
                UpdatedBy = "User - 1"

            };

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var id = 1;
            var result = await opportunitiesExternalService.PatchProjectsAsync(id, data);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Insert New Project Successfully")]
        public async Task PutProjectsAsync_IsSuccess()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

            Project data = new Project()
            {
                Id = 513,
                OpportunityId = "000000000212",
                ProjectName = "Project 1",
                CustomerName = "Test",
                ContractStatus = "Open",
                Country = new Country() { CountryName = "Brazil", Id = 2, Region = new Region() { Id = 1, RegionName = "APAC" }, RegionId = 1 },
                CountryId = 2,
                CreatedBy = "Purushottam",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow,
                UpdatedBy = "User - 1",
                ProjectRoles = new List<ProjectRoles>() { new ProjectRoles()
                {
                    Id = 1,
                    ProjectId = 513,
                    Role = new Role(){ Id = 1, ActiveRecordIndicator = "Test", CreatedBy = "Puru",RoleDescription = "Test", RoleName = "Test"},
                    RoleId = 2,
                    userId = "Test",
                },
                new ProjectRoles()
                {
                    Id = 0,
                    ProjectId = 513,
                    Role = new Role(){ Id = 1, ActiveRecordIndicator = "Test", CreatedBy = "Puru",RoleDescription = "Test", RoleName = "Test0"},
                    RoleId = 2,
                    userId = "Test",
                }

                }

            };
            IEnumerable<Role> RolesData = new List<Role>() { new Role()
            {
                Id = 1,
                RoleName = "Engineer"
            },
            new Role()
            {
                Id = 2,
                RoleName = "Service Engineer"
            }};

            ExternalServiceResponse<IEnumerable<Role>> responseData = new ExternalServiceResponse<IEnumerable<Role>>()
            {
                ResponseData = RolesData,
                IsSuccess = false
            };

            _roleExternalService.Setup(x => x.GetRoleAsync()).ReturnsAsync((responseData));
            var SectionMock = new Mock<IConfigurationSection>();
            SectionMock.Setup(s => s.Value).Returns("https://20.73.195.228/");
            _configuration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(SectionMock.Object);

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(payload, Encoding.UTF8, "application/json"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await opportunitiesExternalService.PutProjectsAsync(data);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Insert New Project Failed")]
        public async Task PutProjectsAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

            Project data = new Project()
            {
                Id = 1,
                OpportunityId = "Test",
                ProjectName = "Project 1",
                CustomerName = "Test",
                ContractStatus = "Open",
                CountryId = 2,
                CreatedBy = "Purushottam",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow,
                UpdatedBy = "User - 1"

            };

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await opportunitiesExternalService.PutProjectsAsync(data);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Insert New Scenario Failed Bad Request/Exception")]
        public async Task PutScenarioAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var opportunitiesExternalService = CreateOpportunitiesExternalService();

            Project data = new Project()
            {
                Id = 1,
                OpportunityId = "Test",
                ProjectName = "Project 1",
                CustomerName = "Test",
                ContractStatus = "Open",
                CountryId = 2,
                CreatedBy = "Purushottam",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow,
                UpdatedBy = "User - 1"

            };

            string ex = "some error while processing";

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await opportunitiesExternalService.PutProjectsAsync(data);

            Assert.False(result.IsSuccess);
        }
    }
}