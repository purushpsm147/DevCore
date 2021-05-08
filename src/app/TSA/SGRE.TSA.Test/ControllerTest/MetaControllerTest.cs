using Microsoft.AspNetCore.Mvc;
using Moq;
using SGRE.TSA.Api.Controllers;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace SGRE.TSA.Test.ControllerTest
{
    public class MetaControllerTest
    {

        MetaController _metaController;

        private readonly Mock<IRegionService> _regionService = new Mock<IRegionService>();
        private readonly Mock<ICountryService> _countryService = new Mock<ICountryService>();
        private readonly Mock<IRoleService> _roleService = new Mock<IRoleService>();
        private readonly Mock<IMileStoneService> _mileStoneService = new Mock<IMileStoneService>();
        private readonly Mock<ICertificationService> _certificationService = new Mock<ICertificationService>();
        private readonly Mock<ITaskService> _taskService = new Mock<ITaskService>();
        private readonly Mock<IWtgCatalogueService> _wtgCatalogueService = new Mock<IWtgCatalogueService>();
        private readonly Mock<ILoadsClusterService> _loadsClusterService = new Mock<ILoadsClusterService>();
        private readonly Mock<IApplicationReasonService> _applicationReasonService = new Mock<IApplicationReasonService>();


        [Fact(DisplayName = "should return all the regions retrived by external service")]
        public void GetRegionIsSuccess()
        {
            IEnumerable<Region> regions = new List<Region>() { new Region()
            {
                 Id =1,
                 RegionName = "Test Region 1"
            },
            new Region() {
                 Id =2,
                 RegionName = "Test Region 2"
            }};

            _regionService.Setup(x => x.GetRegionAsync()).ReturnsAsync((true, regions));

            _metaController = new MetaController();
            var result = _metaController.GetRegion(_regionService.Object);
            Assert.NotNull(result.Result);
        }

        [Fact(DisplayName = "No Data for Get All Regions")]
        public void GetRegionIsFailure()
        {

            _regionService.Setup(x => x.GetRegionAsync()).ReturnsAsync((false, null));

            _metaController = new MetaController();
            var result = _metaController.GetRegion(_regionService.Object);
            var objResult = new OkObjectResult(result);
            Assert.Equal(200, objResult.StatusCode);
        }

        [Fact(DisplayName = "get all the Regions By ID")]
        public void GetRegionByIdIsSuccess()
        {
            IEnumerable<Region> regions = new List<Region>() { new Region()
            {
                 Id =1,
                 RegionName = "Test Region 1"
            },
            new Region() {
                 Id =2,
                 RegionName = "Test Region 2"
            }};

            _regionService.Setup(x => x.GetRegionAsync(It.IsAny<int>())).ReturnsAsync((true, regions));
            _metaController = new MetaController();

            var id = 1;
            var result = _metaController.GetRegion(_regionService.Object, id);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
        }


        [Fact(DisplayName = "No Data for Get All Regions By ID")]
        public void GetRegionByIdIsFailure()
        {
            IEnumerable<Region> regions = new List<Region>() { };

            _regionService.Setup(x => x.GetRegionAsync()).ReturnsAsync((true, regions));
            _metaController = new MetaController();

            var id = 1;
            var result = _metaController.GetRegion(_regionService.Object, id);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }

        [Fact(DisplayName = "get all the Country")]
        public void GetCountryIsSuccess()
        {
            IEnumerable<Country> countries = new List<Country>() { new Country()
            {
                 Id =1,
                 CountryName = "India"
            },
            new Country() {
                 Id =2,
                 CountryName = "China"
            }};

            _countryService.Setup(x => x.GetCountryAsync()).ReturnsAsync((true, countries));
            _metaController = new MetaController();

            var result = _metaController.GetCountry(_countryService.Object);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
        }



        [Fact(DisplayName = "Not Data get all the Country")]
        public void GetCountryIsFailure()
        {
            IEnumerable<Country> countries = new List<Country>() { };

            _countryService.Setup(x => x.GetCountryAsync()).ReturnsAsync((true, countries));
            _metaController = new MetaController();

            var result = _metaController.GetCountry(_countryService.Object);

            Assert.Equal(result.Result, countries);
        }

        [Fact(DisplayName = "get all the Country By Id")]
        public void GetCountryByIdIsSuccess()
        {
            IEnumerable<Country> countries = new List<Country>() { new Country()
            {
                 Id =1,
                 CountryName = "India"
            },
            new Country() {
                 Id =2,
                 CountryName = "China"
            }};

            _countryService.Setup(x => x.GetCountryAsync(It.IsAny<int>())).ReturnsAsync((true, countries));
            _metaController = new MetaController();

            var id = 1;
            var result = _metaController.GetCountry(_countryService.Object, id);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
        }

        [Fact(DisplayName = "Not Data get all the Country By Id")]
        public void GetCountryIdIsFailure()
        {
            IEnumerable<Country> countries = new List<Country>() { };

            _countryService.Setup(x => x.GetCountryAsync(It.IsAny<int>())).ReturnsAsync((true, countries));
            _metaController = new MetaController();

            var id = 1;
            var result = _metaController.GetCountry(_countryService.Object, id);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }

        [Fact(DisplayName = "get all the Country By RegionId")]
        public void GetCountryByRegionIdIsSuccess()
        {
            IEnumerable<Country> countries = new List<Country>() { new Country()
            {
                 Id =1,
                 CountryName = "India"
            },
            new Country() {
                 Id =2,
                 CountryName = "China"
            }};

            _countryService.Setup(x => x.GetCountryByRegionAsync(It.IsAny<int>())).ReturnsAsync((true, countries));
            _metaController = new MetaController();

            var regionId = 1;
            var result = _metaController.GetCountryByRegion(_countryService.Object, regionId);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
        }

        [Fact(DisplayName = "Not Data get all the Country By RegionId")]
        public void GetCountryIdByRegionIdIsFailure()
        {
            IEnumerable<Country> countries = null;

            _countryService.Setup(x => x.GetCountryAsync(It.IsAny<int>())).ReturnsAsync((true, countries));
            _metaController = new MetaController();

            var regionId = 1;
            var result = _metaController.GetCountryByRegion(_countryService.Object, regionId);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }

        [Fact(DisplayName = "get all Preset Roles")]
        public void GetPresetRolesIsSuccess()
        {
            IEnumerable<PresetRoles> roles = new List<PresetRoles>() { new PresetRoles()
            {
                 Id =1,
                 RegionId = 1,
                 UserName = "Manager"

            },
            new PresetRoles() {
                 Id =2,
                 RegionId = 2,
                 UserName = "Employee"
            }};
            _roleService.Setup(x => x.GetPresetRolesAsync()).ReturnsAsync((true, roles));
            _metaController = new MetaController();

            var result = _metaController.GetPresetRoles(_roleService.Object);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
            Assert.IsType<List<PresetRoles>>(result.Result);
            Assert.Equal(result.Result, roles);

        }

        [Fact(DisplayName = "No Data for get all preset Roles")]
        public void GetPresetRolesIsFailure()
        {
            IEnumerable<PresetRoles> roles = new List<PresetRoles>() { };
            _roleService.Setup(x => x.GetPresetRolesAsync()).ReturnsAsync((true, roles));
            _metaController = new MetaController();

            var result = _metaController.GetPresetRoles(_roleService.Object);

            Assert.Equal(result.Result, roles);
        }

        [Fact(DisplayName = "get all the Roles")]
        public void GetRolesIsSuccess()
        {
            IEnumerable<Role> roles = new List<Role>() { new Role()
            {
                 Id =1,
                 RoleName = "Role -1",
                 RoleDescription = "Manager"
                 
            },
            new Role() {
                 Id =2,
                 RoleName = "Role - 2",
                 RoleDescription = "Employee"
            }};

            _roleService.Setup(x => x.GetRoleAsync()).ReturnsAsync((true, roles));
            _metaController = new MetaController();

            var result = _metaController.GetRole(_roleService.Object);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
            Assert.IsType<List<Role>>(result.Result);
            Assert.Equal(result.Result, roles);
        }

        [Fact(DisplayName = "No Data for get all the Roles")]
        public void GetRolesIsFailure()
        {
            IEnumerable<Role> roles = new List<Role>() { };

            _roleService.Setup(x => x.GetRoleAsync()).ReturnsAsync((true, roles));
            _metaController = new MetaController();

            var result = _metaController.GetRole(_roleService.Object);

            Assert.Equal(result.Result, roles);
        }

        [Fact(DisplayName = "get all the MileStones")]
        public void GetMileStonesIsSuccess()
        {
            IEnumerable<MileStone> mileStones = new List<MileStone>() { new MileStone()
            {
                 Id =1,
                 MileStoneName = "MileStone - 1",
                 RecordInsertDateTime = DateTime.UtcNow,
                 LastModifiedDateTime = DateTime.UtcNow
            },
            new MileStone() {
                 Id =2,
                 MileStoneName = "MileStone - 2",
                 RecordInsertDateTime = DateTime.UtcNow,
                 LastModifiedDateTime = DateTime.UtcNow
            }};

            _mileStoneService.Setup(x => x.GetMileStoneAsync()).ReturnsAsync((true, mileStones));
            _metaController = new MetaController();

            var result = _metaController.GetMileStones(_mileStoneService.Object);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
            Assert.IsType<List<MileStone>>(result.Result);
            Assert.Equal(result.Result, mileStones);
        }

        [Fact(DisplayName = "No Data for get all the MileStones")]
        public void GetMileStonesIsFailure()
        {
            IEnumerable<MileStone> mileStones = new List<MileStone>() { };

            _mileStoneService.Setup(x => x.GetMileStoneAsync()).ReturnsAsync((true, mileStones));
            _metaController = new MetaController();

            var result = _metaController.GetMileStones(_mileStoneService.Object);

            Assert.Equal(result.Result, mileStones);
        }

        [Fact(DisplayName = "get all the Certifications")]
        public void GetCertificationsIsSuccess()
        {
            IEnumerable<Certification> certifications = new List<Certification>() { new Certification()
            {
                 Id =1,
                 CertificationName = "Certificate - 1"
            },
            new Certification() {
                 Id =2,
                 CertificationName = "Certificate - 2"
            }};

            _certificationService.Setup(x => x.GetCertificationAsync()).ReturnsAsync((true, certifications));
            _metaController = new MetaController();

            var result = _metaController.GetCertifications(_certificationService.Object);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
            Assert.IsType<List<Certification>>(result.Result);
            Assert.Equal(result.Result, certifications);
        }

        [Fact(DisplayName = "No Data for get all the Certifications")]
        public void GetCertificationsIsFailure()
        {
            IEnumerable<Certification> certifications = new List<Certification>() { };

            _certificationService.Setup(x => x.GetCertificationAsync()).ReturnsAsync((true, certifications));
            _metaController = new MetaController();

            var result = _metaController.GetCertifications(_certificationService.Object);

            Assert.Equal(result.Result, certifications);
        }

        [Fact(DisplayName = "get all the Tasks")]
        public void GetTasksIsSuccess()
        {
            IEnumerable<Task> tasks = new List<Task>() { new Task()
            {
                 Id =1,
                 TaskName = "Task -1"
            },
            new Task() {
                 Id =2,
                 TaskName = "Task - 2"
            }};

            _taskService.Setup(x => x.GetTaskAsync()).ReturnsAsync((true, tasks));
            _metaController = new MetaController();

            var result = _metaController.GetTasks(_taskService.Object);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
            Assert.IsType<List<Task>>(result.Result);
            Assert.Equal(result.Result, tasks);
        }

        [Fact(DisplayName = "No Data for get all the Tasks")]
        public void GetTasksIsFailure()
        {
            IEnumerable<Task> tasks= new List<Task>() { };

            _taskService.Setup(x => x.GetTaskAsync()).ReturnsAsync((true, tasks));
            _metaController = new MetaController();

            var result = _metaController.GetTasks(_taskService.Object);

            Assert.Equal(result.Result, tasks);
        }

        [Fact(DisplayName = "get all the WtgCatalogue")]
        public void GetWtgCatalogueIsSuccess()
        {
            IEnumerable<WtgCatalogue> wtgCatalogues = new List<WtgCatalogue>() { new WtgCatalogue()
            {
                 Id =1,
                 WtgType = "Type - 1",
                 WtgSizeMW = 1
            },
            new WtgCatalogue() {
                 Id =2,
                 WtgType = "Type - 2",
                 WtgSizeMW = 2
            }};

            _wtgCatalogueService.Setup(x => x.GetWtgCatalogueAsync()).ReturnsAsync((true, wtgCatalogues));
            _metaController = new MetaController();

            var result = _metaController.GetWtgCatalogue(_wtgCatalogueService.Object);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
            Assert.IsType<List<WtgCatalogue>>(result.Result);
            Assert.Equal(result.Result, wtgCatalogues);
        }

        [Fact(DisplayName = "No Data for get all the WtgCatalogue")]
        public void GetWtgCatalogueIsFailure()
        {
            IEnumerable<WtgCatalogue> wtgCatalogues = new List<WtgCatalogue>() { };

            _wtgCatalogueService.Setup(x => x.GetWtgCatalogueAsync()).ReturnsAsync((true, wtgCatalogues));
            _metaController = new MetaController();

            var result = _metaController.GetWtgCatalogue(_wtgCatalogueService.Object);

            Assert.Equal(result.Result, wtgCatalogues);
        }


        [Fact(DisplayName = "get all the Load Clusters")]
        public void GetLoadClustersIsSuccess()
        {
            IEnumerable<LoadsCluster> loadsClusters = new List<LoadsCluster>() { new LoadsCluster()
            {
                 Id =1,
                 ClusterName = "A"
            },
            new LoadsCluster() {
                 Id =1,
                 ClusterName = "B"
            }};

            _loadsClusterService.Setup(x => x.GetLoadsClusterAsync()).ReturnsAsync((true, loadsClusters));
            _metaController = new MetaController();

            var result = _metaController.GetLoadClusters(_loadsClusterService.Object);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
            Assert.IsType<List<LoadsCluster>>(result.Result);
            Assert.Equal(result.Result, loadsClusters);
        }

        [Fact(DisplayName = "No Data for get all the Load Clusters")]
        public void GetLoadClustersIsFailure()
        {
            IEnumerable<LoadsCluster> loadsClusters = new List<LoadsCluster>() { };

            _loadsClusterService.Setup(x => x.GetLoadsClusterAsync()).ReturnsAsync((false, loadsClusters));
            _metaController = new MetaController();

            var result = _metaController.GetLoadClusters(_loadsClusterService.Object);

            Assert.Equal(result.Result, loadsClusters);
            Assert.IsType<List<LoadsCluster>>(result.Result);
        }

        [Fact(DisplayName = "get all the Application Reason")]
        public void GetApplicationReasonIsSuccess()
        {
            IEnumerable<ApplicationReason> applicationReasons = new List<ApplicationReason>() { new ApplicationReason()
            {
                 Id =1,
                 Reason = "reason - 1"
            },
            new ApplicationReason() {
                 Id =1,
                 Reason = "reason - 2"
            }};

            _applicationReasonService.Setup(x => x.GetApplicationReasonAsync()).ReturnsAsync((true, applicationReasons));
            _metaController = new MetaController();

            var result = _metaController.GetApplicationReason(_applicationReasonService.Object);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
            Assert.IsType<List<ApplicationReason>>(result.Result);
            Assert.Equal(result.Result, applicationReasons);
        }

        [Fact(DisplayName = "No Data for get all the Application Reason")]
        public void GetApplicationReasonIsFailure()
        {
            IEnumerable<ApplicationReason> applicationReasons = new List<ApplicationReason>() { };

            _applicationReasonService.Setup(x => x.GetApplicationReasonAsync()).ReturnsAsync((false, applicationReasons));
            _metaController = new MetaController();

            var result = _metaController.GetApplicationReason(_applicationReasonService.Object);

            Assert.Equal(result.Result, applicationReasons);
            Assert.IsType<List<ApplicationReason>>(result.Result);
        }
    }

}