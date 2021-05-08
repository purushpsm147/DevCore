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
    public class RolesControllerTest
    {
        RolesController _rolesController;

        private readonly Mock<IAssignRolesService> _assignRolesService = new Mock<IAssignRolesService>();


        [Fact(DisplayName = "Get Quotes By Id")]
        public async Task PostMyRolesIsSuccess()
        {
            IEnumerable<ProjectRoles> projectroles = new List<ProjectRoles>() { new ProjectRoles()
            {
                 Id = 1,
                 RoleId = 1,
                 userId = "user -1"
            } };

            _assignRolesService.Setup(x => x.PostRolesAsync(It.IsAny<IEnumerable<ProjectRoles>>())).ReturnsAsync((true, projectroles));

            _rolesController = new RolesController();

             var result = await _rolesController.PostMyRoles(projectroles, _assignRolesService.Object);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Post My Roles No Data")]
        public async Task PostMyRolesIsFailure()
        {
            IEnumerable<ProjectRoles> projectroles = new List<ProjectRoles>() { new ProjectRoles()
            {
                 Id = 1,
                 RoleId = 1,
                 userId = "user -1"
            } };

            _assignRolesService.Setup(x => x.PostRolesAsync(It.IsAny<IEnumerable<ProjectRoles>>())).ReturnsAsync((false, null));

            _rolesController = new RolesController();

            var result = await _rolesController.PostMyRoles(projectroles, _assignRolesService.Object);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }
    }

}