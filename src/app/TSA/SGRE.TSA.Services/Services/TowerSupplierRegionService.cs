using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using SGRE.TSA.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class TowerSupplierRegionService : ITowerSupplierRegionService
    {
        private readonly IExternalServiceFactory externalServiceFactory;
        private readonly ILogger<TowerSupplierRegion> logger;

        private readonly IConfigScenarioService configScenarioService;

        public TowerSupplierRegionService(IExternalServiceFactory externalServiceFactory, ILogger<TowerSupplierRegion> logger, IConfigScenarioService configScenarioService)
        {
            this.externalServiceFactory = externalServiceFactory;
            this.logger = logger;
            this.configScenarioService = configScenarioService;
        }
        public async Task<(bool IsSuccess, IEnumerable<TowerSupplierRegion> towerSupplierRegionResult)> GetTowerSupplierRegionAsync(int scenarioId)
        {
            var externalService = externalServiceFactory.CreateExternalService<TowerSupplierRegion>(logger);

            var scenarioResult = await configScenarioService.GetScenarioByIdAsync(scenarioId);

            if (scenarioResult.IsSuccess)
            {
                var scenarioData = scenarioResult.scenarioResults.FirstOrDefault();

                string oDataQuery = (scenarioData.ScenarioType == ScenarioTypes.SST) ? "?$expand=towerSupplierSource,interfaceTools&$filter=InterfaceTools/ToolName eq 'SST'" : "?$expand=towerSupplierSource,interfaceTools&$filter=InterfaceTools/ToolName eq 'TowerCubo'";

                var TowerSupplierRegionResult = await externalService.GetAsync(oDataQuery);

                if (TowerSupplierRegionResult.IsSuccess)
                    return (true, TowerSupplierRegionResult.ResponseData);

                return (false, null);
            }
            return (false, null);

        }
    }
}
