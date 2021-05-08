using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, EnableQuery()]
    public class MetaController : ControllerBase
    {
        #region Region Meta
        [HttpGet]
        [Route("Region")]
        public async Task<IEnumerable<Region>> GetRegion([FromServices] IRegionService regionService)
        {
            var result = await regionService.GetRegionAsync();
            if (result.IsSuccess)
            {
                return result.regionResult;
            }
            return new List<Region>();
        }

        [HttpGet]
        [Route("Region/{id}")]
        public async Task<IActionResult> GetRegion([FromServices] IRegionService regionService, int id)
        {

            var result = await regionService.GetRegionAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.regionResult);
            }
            return NotFound();
        }
        #endregion

        #region Country Meta
        [HttpGet]
        [Route("Country")]
        public async Task<IEnumerable<Country>> GetCountry([FromServices] ICountryService countryService)
        {
            var result = await countryService.GetCountryAsync();
            if (result.IsSuccess)
            {
                return result.countryResult;
            }

            return new List<Country>();
        }

        [HttpGet]
        [Route("Country/{id}")]
        public async Task<IActionResult> GetCountry([FromServices] ICountryService countryService, int id)
        {
            var result = await countryService.GetCountryAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.countryResult);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("Country/Region/{regionId}")]
        public async Task<IActionResult> GetCountryByRegion([FromServices] ICountryService countryService, int regionId)
        {
            var result = await countryService.GetCountryByRegionAsync(regionId);
            if (result.IsSuccess)
            {
                return Ok(result.countryResult);
            }
            return NotFound();
        }
        #endregion

        [HttpGet]
        [Route("Roles")]
        public async Task<IEnumerable<Role>> GetRole([FromServices] IRoleService roleService)
        {
            var result = await roleService.GetRoleAsync();
            if (result.IsSuccess)
            {
                return result.roleResult;
            }
            return new List<Role>();
        }

        [HttpGet]
        [Route("MileStones")]
        public async Task<IEnumerable<MileStone>> GetMileStones([FromServices] IMileStoneService mileStoneService)
        {
            var result = await mileStoneService.GetMileStoneAsync();
            if (result.IsSuccess)
            {
                return result.mileStoneResult;
            }
            return new List<MileStone>();
        }

        [HttpGet]
        [Route("Certification")]
        public async Task<IEnumerable<Certification>> GetCertifications([FromServices] ICertificationService certificationService)
        {
            var result = await certificationService.GetCertificationAsync();
            if (result.IsSuccess)
            {
                return result.certifications;
            }

            return new List<Certification>();
        }

        [HttpGet]
        [Route("Certification/{id}")]
        public async Task<IEnumerable<Certification>> GetCertifications([FromServices] ICertificationService certificationService, int id)
        {
            var result = await certificationService.GetCertificationAsync(id);
            if (result.IsSuccess)
            {
                return result.certifications;
            }

            return new List<Certification>();
        }

        [HttpGet]
        [Route("Tasks")]
        public async Task<IEnumerable<Models.Task>> GetTasks([FromServices] ITaskService taskService)
        {
            var result = await taskService.GetTaskAsync();
            if (result.IsSuccess)
            {
                return result.taskResult;
            }
            return new List<Models.Task>();
        }

        [HttpGet]
        [Route("WtgCatalogues")]
        public async Task<IEnumerable<WtgCatalogue>> GetWtgCatalogue([FromServices] IWtgCatalogueService catalogueService)
        {
            var result = await catalogueService.GetWtgCatalogueAsync();
            if (result.IsSuccess)
            {
                return result.wtgCatalogueResult;
            }
            return new List<WtgCatalogue>();
        }

        [HttpGet]
        [Route("GenericMarketBoundaries/{CountryId:int}")]
        public async Task<IEnumerable<GenericMarketBoundary>> GetGenericMarketBoundaries([FromServices] IGMBService gMBService, int CountryId)
        {
            var result = await gMBService.GetGMBAsync(CountryId);
            if (result.IsSuccess)
            {
                return result.genericMarketBoundaries;
            }
            return new List<GenericMarketBoundary>();
        }

        [HttpGet]
        [Route("PresetRoles")]
        public async Task<IEnumerable<PresetRoles>> GetPresetRoles([FromServices] IRoleService roleService)
        {
            var result = await roleService.GetPresetRolesAsync();
            if (result.IsSuccess)
            {
                return result.presetRoleResult;
            }
            return new List<PresetRoles>();
        }

        [HttpGet]
        [Route("LoadClusters")]
        public async Task<IEnumerable<LoadsCluster>> GetLoadClusters([FromServices] ILoadsClusterService loadsClusterService)
        {
            var result = await loadsClusterService.GetLoadsClusterAsync();
            if (result.IsSuccess)
            {
                return result.loadsClusterResult;
            }
            return new List<LoadsCluster>();
        }

        [HttpGet]
        [Route("ApplicationReason")]
        public async Task<IEnumerable<ApplicationReason>> GetApplicationReason([FromServices] IApplicationReasonService applicationReasonService)
        {
            var result = await applicationReasonService.GetApplicationReasonAsync();
            if (result.IsSuccess)
            {
                return result.ApplicationReasonResult;
            }
            return new List<ApplicationReason>();
        }

        #region Cost and feedback meta api

        [HttpGet]
        [Route("CostsTowerSupplierMeta")]
        public async Task<IEnumerable<CostsTowerSupplierMeta>> GetCostsTowerSupplierMeta([FromServices] ICostsTowerSupplierServiceMeta costsTowerSupplierService)
        {
            var result = await costsTowerSupplierService.GetCostsTowerSupplierAsync();
            if (result.IsSuccess)
            {
                return result.costsTowerSupplierResult;
            }
            return new List<CostsTowerSupplierMeta>();
        }

        [HttpGet]
        [Route("CostsTowerSupplierMeta/{id:int}")]
        public async Task<IActionResult> GetCostsTowerSupplierMeta([FromServices] ICostsTowerSupplierServiceMeta costsTowerSupplierService, int id)
        {
            var result = await costsTowerSupplierService.GetCostsTowerSupplierAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.costsTowerSupplierResult.FirstOrDefault());
            }
            return NotFound();
        }

        [HttpGet]
        [Route("CostsTowerExWorksMeta")]
        public async Task<IEnumerable<CostsTowerExWorksMeta>> GetCostsTowerExWorksMeta([FromServices] ICostsTowerExWorksMetaService costsTowerExWorksMetaService)
        {
            var result = await costsTowerExWorksMetaService.GetCostsTowerExWorksAsync();
            if (result.IsSuccess)
            {
                return result.costsTowerExWorksResult;
            }
            return new List<CostsTowerExWorksMeta>();
        }

        [HttpGet]
        [Route("CostsTowerExWorksMeta/{id:int}")]
        public async Task<IActionResult> GetCostsTowerExWorksMeta([FromServices] ICostsTowerExWorksMetaService costsTowerExWorksMetaService, int id)
        {
            var result = await costsTowerExWorksMetaService.GetCostsTowerExWorksAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.costsTowerExWorksResult.FirstOrDefault());
            }
            return NotFound();
        }

        [HttpGet]
        [Route("CostOverViewMeta")]
        public async Task<IEnumerable<CostOverViewMeta>> GetCostOverViewMeta([FromServices] ICostOverViewService costOverViewService)
        {
            var result = await costOverViewService.GetCostOverViewMetaAsync();
            if (result.IsSuccess)
            {
                return result.costOverViewMetaResult;
            }
            return new List<CostOverViewMeta>();
        }

        [HttpGet]
        [Route("CostOverViewMeta/{id:int}")]
        public async Task<IActionResult> GetCostOverViewMeta([FromServices] ICostOverViewService costOverViewService, int id)
        {
            var result = await costOverViewService.GetCostOverViewMetaAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.costOverViewMetaResult.FirstOrDefault());
            }
            return NotFound();
        }

        [HttpGet]
        [Route("CostsTowerConstructionLeadMeta")]
        public async Task<IEnumerable<CostsTowerConstructionLeadTimeMeta>> GetCostOverViewMeta([FromServices] ICostsTowerConstructionLeadTimeService costsTowerConstructionLeadTimeService)
        {
            var result = await costsTowerConstructionLeadTimeService.GetCostsTowerConstructionLeadTimeMetaAsync();
            if (result.IsSuccess)
            {
                return result.costsTowerConstructionLeadTimeMetaResult;
            }
            return new List<CostsTowerConstructionLeadTimeMeta>();
        }

        [HttpGet]
        [Route("CostsTowerConstructionLeadMeta/{id:int}")]
        public async Task<IActionResult> GetCostOverViewMeta([FromServices] ICostsTowerConstructionLeadTimeService costsTowerConstructionLeadTimeService, int id)
        {
            var result = await costsTowerConstructionLeadTimeService.GetCostsTowerConstructionLeadTimeMetaAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.costsTowerConstructionLeadTimeMetaResult.FirstOrDefault());
            }
            return NotFound();
        }

        [HttpGet]
        [Route("CostsTowerConstructionMeta")]
        public async Task<IEnumerable<CostsTowerConstructionMeta>> GetCostsTowerConstructionMeta([FromServices] ICostsTowerConstructionService costsTowerConstructionService)
        {
            var result = await costsTowerConstructionService.GetCostsTowerConstructionMetaAsync();
            if (result.IsSuccess)
            {
                return result.costsTowerConstructionMetaResult;
            }
            return new List<CostsTowerConstructionMeta>();
        }

        [HttpGet]
        [Route("CostsTowerConstructionMeta/{id:int}")]
        public async Task<IActionResult> GetCostsTowerConstructionMeta([FromServices] ICostsTowerConstructionService costsTowerConstructionService, int id)
        {
            var result = await costsTowerConstructionService.GetCostsTowerConstructionMetaAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.costsTowerConstructionMetaResult.FirstOrDefault());
            }
            return NotFound();
        }

        [HttpGet]
        [Route("CostsTowerCustomsMeta")]
        public async Task<IEnumerable<CostsTowerCustomsMeta>> GetCostsTowerConstructionMeta([FromServices] ICostsTowerCustomsService costsTowerCustomsService)
        {
            var result = await costsTowerCustomsService.GetCostsTowerCustomsMetaAsync();
            if (result.IsSuccess)
            {
                return result.costsTowerCustomsMetaResult;
            }
            return new List<CostsTowerCustomsMeta>();
        }

        [HttpGet]
        [Route("CostsTowerCustomsMeta/{id:int}")]
        public async Task<IActionResult> GetCostsTowerConstructionMeta([FromServices] ICostsTowerCustomsService costsTowerCustomsService, int id)
        {
            var result = await costsTowerCustomsService.GetCostsTowerCustomsMetaAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.costsTowerCustomsMetaResult.FirstOrDefault());
            }
            return NotFound();
        }

        [HttpGet]
        [Route("CostsTowerLogisticsMeta")]
        public async Task<IEnumerable<CostsTowerLogisticsMeta>> GetCostsTowerConstructionMeta([FromServices] ICostsTowerLogisticsService costsTowerLogisticsService)
        {
            var result = await costsTowerLogisticsService.GetCostsTowerLogisticsMetaAsync();
            if (result.IsSuccess)
            {
                return result.costsTowerLogisticsMetaResult;
            }
            return new List<CostsTowerLogisticsMeta>();
        }

        [HttpGet]
        [Route("CostsTowerLogisticsMeta/{id:int}")]
        public async Task<IActionResult> GetCostsTowerConstructionMeta([FromServices] ICostsTowerLogisticsService costsTowerLogisticsService, int id)
        {
            var result = await costsTowerLogisticsService.GetCostsTowerLogisticsMetaAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.costsTowerLogisticsMetaResult.FirstOrDefault());
            }
            return NotFound();
        }

        [HttpGet]
        [Route("CostsTowerLogisticsLeadTimeMeta")]
        public async Task<IEnumerable<CostsTowerLogisticsLeadTimeMeta>> GetCostsTowerLogisticsLeadTimeMeta([FromServices] ICostsTowerLogisticsLeadTimeService costsTowerLogisticsLeadTimeService)
        {
            var result = await costsTowerLogisticsLeadTimeService.GetCostsTowerLogisticsLeadTimeMetaAsync();
            if (result.IsSuccess)
            {
                return result.costsTowerLogisticsLeadTimeMetaResult;
            }
            return new List<CostsTowerLogisticsLeadTimeMeta>();
        }

        [HttpGet]
        [Route("CostsTowerLogisticsLeadTimeMeta/{id:int}")]
        public async Task<IActionResult> GetCostsTowerLogisticsLeadTimeMeta([FromServices] ICostsTowerLogisticsLeadTimeService costsTowerLogisticsLeadTimeService, int id)
        {
            var result = await costsTowerLogisticsLeadTimeService.GetCostsTowerLogisticsLeadTimeMetaAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.costsTowerLogisticsLeadTimeMetaResult.FirstOrDefault());
            }
            return NotFound();
        }

        #endregion

        [HttpGet]
        [Route("Currency")]
        public async Task<IEnumerable<Currency>> GetCurrencyMeta([FromServices] ICurrencyService currencyService)
        {
            var result = await currencyService.GetCurrencyAsync();
            if (result.IsSuccess)
            {
                return result.currencyResult;
            }
            return new List<Currency>();
        }

        [HttpGet]
        [Route("Currency/{id:int}")]
        public async Task<IActionResult> GetCurrencyMeta([FromServices] ICurrencyService currencyService, int id)
        {
            var result = await currencyService.GetCurrencyAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.currencyResult.FirstOrDefault());
            }
            return NotFound();
        }


        [HttpGet]
        [Route("TowerSupplierRegion/{scenarioId:int}")]
        public async Task<IEnumerable<TowerSupplierRegion>> GetTowerSupplierRegionMeta([FromServices] ITowerSupplierRegionService towerSupplierRegionService, int scenarioId)
        {
            var result = await towerSupplierRegionService.GetTowerSupplierRegionAsync(scenarioId);
            if (result.IsSuccess)
            {
                return result.towerSupplierRegionResult;
            }
            return new List<TowerSupplierRegion>();
        }

        [HttpGet]
        [Route("TowerCatalogueModel/{wtgCatalougeId:int}")]
        public async Task<IEnumerable<WtgCatalogueModel>> GetWtgCatalogueModelMeta([FromServices] IWtgCatalogueModelService wtgCatalogueModelService, int wtgCatalougeId)
        {
            var result = await wtgCatalogueModelService.GetWtgCatalogueModelBywtgCatalougeIdAsync(wtgCatalougeId);
            if (result.IsSuccess)
            {
                return result.wtgCatalogueModelResult;
            }
            return new List<WtgCatalogueModel>();
        }
    }

}

