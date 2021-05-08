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
    public class PermissionServiceTest
    {
        /// <summary>
        /// Defines the PermissionExternalService
        /// </summary>
        private Mock<IExternalServiceFactory> _permissionExternalService = new Mock<IExternalServiceFactory>();

        /// <summary>
        /// Defines the IRoleExternalService
        /// </summary>
        private Mock<IRoleExternalService> _roleExternalService = new Mock<IRoleExternalService>();
        private Mock<ILogger<Permission>> _logger = new Mock<ILogger<Permission>>();

        /// <summary>
        /// The PermissionService
        /// </summary>
        /// <returns></returns>
        private PermissionService CreatePermissionService()
        {
            return new PermissionService( _roleExternalService.Object, _permissionExternalService.Object, _logger.Object);
        }

        [Fact(DisplayName = "Get all Permissions")]
        public async Task GetPermissionAsync_IsSuccess()
        {
            // Arrange
            var permissionService = CreatePermissionService();

            IEnumerable<Permission> data = new List<Permission>() { new Permission()
            {
                Id = 1,
                RoleId = 1,
                CreatedBy = "user - 1",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,

            },
            new Permission()
            {
                Id = 2,
                CreatedBy = "user - 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
            }};

            ExternalServiceResponse<IEnumerable<Permission>> responseData = new ExternalServiceResponse<IEnumerable<Permission>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _permissionExternalService.Setup(x => x.CreateExternalService<Permission>(_logger.Object).GetByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var roleId = 1;
            var result = await permissionService.GetPermissionAsync(roleId);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all Permission No Records Found")]
        public async Task GetPermissionAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var permissionService = CreatePermissionService();

            IEnumerable<Permission> data = new List<Permission>() { new Permission()
            {
                Id = 1,
                RoleId = 1,
                CreatedBy = "user - 1",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,

            },
            new Permission()
            {
                Id = 2,
                CreatedBy = "user - 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
            }};

            ExternalServiceResponse<IEnumerable<Permission>> responseData = new ExternalServiceResponse<IEnumerable<Permission>>()
            {
                ResponseData = null,
                IsSuccess = false
            };


            _permissionExternalService.Setup(x => x.CreateExternalService<Permission>(_logger.Object).GetByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var roleId = 1;
            var result = await permissionService.GetPermissionAsync(roleId);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all Permission By Role Name ")]
        public async Task GetPermissionByRoleNameAsync_IsSuccess()
        {
            // Arrange
            var permissionService = CreatePermissionService();

            IEnumerable<Permission> data = new List<Permission>() { new Permission()
            {
                Id = 1,
                RoleId = 1,
                CreatedBy = "user - 1",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,

            },
            new Permission()
            {
                Id = 2,
                CreatedBy = "user - 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
            }};

            ExternalServiceResponse<IEnumerable<Permission>> responseData = new ExternalServiceResponse<IEnumerable<Permission>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            IEnumerable<Role> roleData = new List<Role>() { new Role()
            {
                Id = 1,
                RoleName = "Engineer"
            },
            new Role()
            {
                Id = 2,
                RoleName = "Service Engineer"
            }};

            ExternalServiceResponse<IEnumerable<Role>> roleResponseData = new ExternalServiceResponse<IEnumerable<Role>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _roleExternalService.Setup(x => x.GetRoleAsync()).ReturnsAsync((roleResponseData));

            _permissionExternalService.Setup(x => x.CreateExternalService<Permission>(_logger.Object).GetByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var roleId = 1;
            var result = await permissionService.GetPermissionAsync(roleId);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all Permission By Role Name No Records Found")]
        public async Task GetPermissionByRoleNameAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var permissionService = CreatePermissionService();

            IEnumerable<Permission> data = new List<Permission>() { new Permission()
            {
                Id = 1,
                RoleId = 1,
                CreatedBy = "user - 1",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,

            },
            new Permission()
            {
                Id = 2,
                CreatedBy = "user - 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
            }};

            ExternalServiceResponse<IEnumerable<Permission>> responseData = new ExternalServiceResponse<IEnumerable<Permission>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            IEnumerable<Role> roleData = new List<Role>() { new Role()
            {
                Id = 1,
                RoleName = "Engineer"
            },
            new Role()
            {
                Id = 2,
                RoleName = "Service Engineer"
            }};

            ExternalServiceResponse<IEnumerable<Role>> roleResponseData = new ExternalServiceResponse<IEnumerable<Role>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _roleExternalService.Setup(x => x.GetRoleAsync()).ReturnsAsync((roleResponseData));

            _permissionExternalService.Setup(x => x.CreateExternalService<Permission>(_logger.Object).GetByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));


            var roleId = 1;
            var result = await permissionService.GetPermissionAsync(roleId);

            Assert.False(result.IsSuccess);
        }

    }
}