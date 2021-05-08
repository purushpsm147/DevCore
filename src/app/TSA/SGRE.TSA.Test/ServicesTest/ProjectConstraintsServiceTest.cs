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
    public class ProjectConstraintsServiceTest
    {
        /// <summary>
        /// Defines the IProjectConstraintsExternalService
        /// </summary>
        private Mock<IProjectConstraintsExternalService> _projectConstraintsExternalService = new Mock<IProjectConstraintsExternalService>();

        /// <summary>
        /// The ProjectConstraintsService
        /// </summary>
        /// <returns></returns>
        private ProjectConstraintsService CreateProjectConstraintsService()
        {
            return new ProjectConstraintsService(_projectConstraintsExternalService.Object);
        }

        [Fact(DisplayName = "Get all ProjectConstraints")]
        public async Task GetProjectConstraintsAsync_IsSuccess()
        {
            var projectConstraintsService = CreateProjectConstraintsService();

            IEnumerable<ProjectConstraint> data = new List<ProjectConstraint>() { new ProjectConstraint()
            {
                Id = 1,
                ProjectId =1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new ProjectConstraint()
            {
                Id = 2,
                ProjectId =1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            }};

            ExternalServiceResponse<IEnumerable<ProjectConstraint>> responseData = new ExternalServiceResponse<IEnumerable<ProjectConstraint>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _projectConstraintsExternalService.Setup(x => x.GetProjectConstraintsAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var projectId = 1;
            var result = await projectConstraintsService.GetProjectConstraintsAsync(projectId);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all ProjectConstraints No Records Found")]
        public async Task GetProjectConstraints_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var projectConstraintsService = CreateProjectConstraintsService();

            IEnumerable<ProjectConstraint> data = new List<ProjectConstraint>() { new ProjectConstraint()
            {
                Id = 1,
                ProjectId =1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new ProjectConstraint()
            {
                Id = 2,
                ProjectId =1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            }};

            ExternalServiceResponse<IEnumerable<ProjectConstraint>> responseData = new ExternalServiceResponse<IEnumerable<ProjectConstraint>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _projectConstraintsExternalService.Setup(x => x.GetProjectConstraintsAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var projectId = 1;
            var result = await projectConstraintsService.GetProjectConstraintsAsync(projectId);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all ProjectConstraints By Id")]
        public async Task GetProjectConstraintsByIdAsync_IsSuccess()
        {
            var projectConstraintsService = CreateProjectConstraintsService();

            IEnumerable<ProjectConstraint> data = new List<ProjectConstraint>() { new ProjectConstraint()
            {
                Id = 1,
                ProjectId =1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new ProjectConstraint()
            {
                Id = 2,
                ProjectId =1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            }};

            ExternalServiceResponse<IEnumerable<ProjectConstraint>> responseData = new ExternalServiceResponse<IEnumerable<ProjectConstraint>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _projectConstraintsExternalService.Setup(x => x.GetProjectConstraintsByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var id = 1;
            var result = await projectConstraintsService.GetProjectConstraintsByIdAsync(id);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all ProjectConstraints By Id No Records Found")]
        public async Task GetProjectConstraintsById_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var projectConstraintsService = CreateProjectConstraintsService();

            IEnumerable<ProjectConstraint> data = new List<ProjectConstraint>() { new ProjectConstraint()
            {
                Id = 1,
                ProjectId =1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new ProjectConstraint()
            {
                Id = 2,
                ProjectId =1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            }};

            ExternalServiceResponse<IEnumerable<ProjectConstraint>> responseData = new ExternalServiceResponse<IEnumerable<ProjectConstraint>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _projectConstraintsExternalService.Setup(x => x.GetProjectConstraintsByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var id = 1;
            var result = await projectConstraintsService.GetProjectConstraintsByIdAsync(id);

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Update Project Constraints")]
        public async Task PatchProjectConstraintsAsync_IsSuccess()
        {
            //Arrange
            var projectConstraintsService = CreateProjectConstraintsService();

            var data = new ProjectConstraint()
            {
                Id = 2,
                ProjectId =1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<ProjectConstraint> responseData = new ExternalServiceResponse<ProjectConstraint>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _projectConstraintsExternalService.Setup(x => x.PatchProjectConstraintsAsync(It.IsAny<int>(), It.IsAny<ProjectConstraint>())).ReturnsAsync((responseData));

            var id = 1;
            var result = await projectConstraintsService.PatchProjectConstraintsAsync(id, data);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Update Project Constraints No Records Found")]
        public async Task PatchProjectConstraintsAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var projectConstraintsService = CreateProjectConstraintsService();

            var data = new ProjectConstraint()
            {
                Id = 1,
                ProjectId = 1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<ProjectConstraint> responseData = new ExternalServiceResponse<ProjectConstraint>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _projectConstraintsExternalService.Setup(x => x.PatchProjectConstraintsAsync(It.IsAny<int>(), It.IsAny<ProjectConstraint>())).ReturnsAsync((responseData));

            var id = 1;
            var result = await projectConstraintsService.PatchProjectConstraintsAsync(id, data);

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Insert New Project Constraints")]
        public async Task PutProjectConstraintsAsync_IsSuccess()
        {
            //Arrange
            var projectConstraintsService = CreateProjectConstraintsService();

            var data = new ProjectConstraint()
            {
                Id = 1,
                ProjectId = 1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<ProjectConstraint> responseData = new ExternalServiceResponse<ProjectConstraint>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _projectConstraintsExternalService.Setup(x => x.PutProjectConstraintsAsync(It.IsAny<ProjectConstraint>())).ReturnsAsync((responseData));

            var result = await projectConstraintsService.PutProjectConstraintsAsync(data);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "New Project Constraints No Records Found")]
        public async Task PutProjectConstraintsAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var projectConstraintsService = CreateProjectConstraintsService();

            var data = new ProjectConstraint()
            {
                Id = 1,
                ProjectId = 1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<ProjectConstraint> responseData = new ExternalServiceResponse<ProjectConstraint>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _projectConstraintsExternalService.Setup(x => x.PutProjectConstraintsAsync(It.IsAny<ProjectConstraint>())).ReturnsAsync((responseData));

            var result = await projectConstraintsService.PutProjectConstraintsAsync(data);

            Assert.False(result.IsSuccess);
        }
    }
}