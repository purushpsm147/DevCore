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
    public class ProjectConstraintsControllerTest
    {
        ProjectConstraintsController _projectConstraintsController;

        private readonly Mock<IProjectConstraintsService> _projectConstraintsService = new Mock<IProjectConstraintsService>();

        public ProjectConstraintsControllerTest()
        {
            _projectConstraintsController = new ProjectConstraintsController(_projectConstraintsService.Object);
        }

        [Fact(DisplayName= "Get Project Constraints Success")]
        public async Task GetProjectConstraintsIsSuccess()
        {
            IEnumerable<ProjectConstraint> projectConstraint = new List<ProjectConstraint>() { new ProjectConstraint()
            {
                 Id = 1,
                 ProjectId = 1,
                 LastModifiedDateTime = DateTime.UtcNow,
                 RecordInsertDateTime = DateTime.UtcNow
            } };

            _projectConstraintsService.Setup(x => x.GetProjectConstraintsAsync(It.IsAny<int>())).ReturnsAsync((true, projectConstraint));

            int projectId = 1;
            var result = await _projectConstraintsController.GetProjectConstraints(projectId);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Get Project Constraints No Data Found")]
        public async Task GetProjectConstraintsIsFailure()
        {
            IEnumerable<ProjectConstraint> projectConstraint = new List<ProjectConstraint>() { };

            _projectConstraintsService.Setup(x => x.GetProjectConstraintsAsync(It.IsAny<int>())).ReturnsAsync((true, projectConstraint));

            int projectId = 1;
            var result = await _projectConstraintsController.GetProjectConstraints(projectId);

            Assert.Equal(result, projectConstraint);
        }

        [Fact(DisplayName = "Get Project Constraints By Id Success")]
        public async Task GetProjectConstraintsByIdIsSuccess()
        {
            IEnumerable<ProjectConstraint> projectConstraint = new List<ProjectConstraint>() { new ProjectConstraint()
            {
                 Id = 1,
                 ProjectId = 1,
                 LastModifiedDateTime = DateTime.UtcNow,
                 RecordInsertDateTime = DateTime.UtcNow
            } };

            _projectConstraintsService.Setup(x => x.GetProjectConstraintsAsync(It.IsAny<int>())).ReturnsAsync((true, projectConstraint));

            int id = 1;
            var result = await _projectConstraintsController.GetProjectConstraints(id);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Get Project Constraints By Id No Data Found")]
        public async Task GetProjectConstraintsByIdIsFailure()
        {
            IEnumerable<ProjectConstraint> projectConstraint = new List<ProjectConstraint>() { };

            _projectConstraintsService.Setup(x => x.GetProjectConstraintsAsync(It.IsAny<int>())).ReturnsAsync((true, projectConstraint));

            int id = 1;
            var result = await _projectConstraintsController.GetProjectConstraints(id);

            Assert.Equal(result, projectConstraint);
        }

        [Fact(DisplayName = "Insert New Project Constraints Record")]
        public async Task PutProjectConstraintsIsSuccess()
        {
            ProjectConstraint projectConstraint = new ProjectConstraint()
            {
                ProjectId = 1,
                LastModifiedDateTime = DateTime.UtcNow,
                RecordInsertDateTime = DateTime.UtcNow,
                LogisticConstraint = new LogisticConstraint()
                {
                    LogisticStatusId = 1,
                    logisticProjectBoundaries =
                    new List<LogisticProjectBoundary>()
                    { new LogisticProjectBoundary()
                    {
                        TransportModeId = 1
                    }
                 }
                }
            };

            _projectConstraintsService.Setup(x => x.PutProjectConstraintsAsync(It.IsAny<ProjectConstraint>())).ReturnsAsync((true, projectConstraint));

            var result = await _projectConstraintsController.PutProjectConstraints(projectConstraint);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Failed to Insert New Project Constraint Record")]
        public async Task PutProjectConstraintsFailure()
        {
            ProjectConstraint projectConstraint = new ProjectConstraint()
            {
                Id = 1,
                ProjectId = 1,
                LastModifiedDateTime = DateTime.UtcNow,
                RecordInsertDateTime = DateTime.UtcNow,
                LogisticConstraint = new LogisticConstraint()
                {
                    LogisticStatusId = 1,
                    logisticProjectBoundaries = 
                    new List<LogisticProjectBoundary>() 
                    { new LogisticProjectBoundary()
                    { 
                        TransportModeId = 1 
                    } 
                 }
                }
            };

            _projectConstraintsService.Setup(x => x.PutProjectConstraintsAsync(It.IsAny<ProjectConstraint>())).ReturnsAsync((true, projectConstraint));

            var result = await _projectConstraintsController.PutProjectConstraints(projectConstraint);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }

        [Fact(DisplayName = "Update Project Constraints Record")]
        public async Task PatchProjectConstraintsAsyncIsSuccess()
        {

            ProjectConstraint projectConstraint = new ProjectConstraint()
            {
                Id = 1,
                ProjectId = 1,
                LastModifiedDateTime = DateTime.UtcNow,
                RecordInsertDateTime = DateTime.UtcNow,
                LogisticConstraint = new LogisticConstraint()
                {
                    LogisticStatusId = 1,
                    logisticProjectBoundaries =
                    new List<LogisticProjectBoundary>()
                    { new LogisticProjectBoundary()
                    {
                        TransportModeId = 1
                    }
                 }
                }
            };

            _projectConstraintsService.Setup(x => x.PatchProjectConstraintsAsync(It.IsAny<int>(), It.IsAny<ProjectConstraint>())).ReturnsAsync((true, projectConstraint));

            var id = 1;
            var result = await _projectConstraintsController.PatchProjectConstraintsAsync(id, projectConstraint);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Failed to Insert New Scenario Record")]
        public async Task PatchMyScenarioIsFailure()
        {
            ProjectConstraint projectConstraint = new ProjectConstraint()
            {
                Id = 1,
                ProjectId = 1,
                LastModifiedDateTime = DateTime.UtcNow,
                RecordInsertDateTime = DateTime.UtcNow,
                LogisticConstraint = new LogisticConstraint()
                {
                    LogisticStatusId = 1,
                    logisticProjectBoundaries =
                    new List<LogisticProjectBoundary>()
                    { new LogisticProjectBoundary()
                    {
                        TransportModeId = 1
                    }
                 }
                }
            };

            _projectConstraintsService.Setup(x => x.PatchProjectConstraintsAsync(It.IsAny<int>(), It.IsAny<ProjectConstraint>())).ReturnsAsync((true, projectConstraint));

            var id = 1;
            var result = await _projectConstraintsController.PatchProjectConstraintsAsync(id, projectConstraint);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }
    }

}