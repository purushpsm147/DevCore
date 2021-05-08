using Microsoft.Extensions.DependencyInjection;
using SGRE.TSA.Api.Factory;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Services.Services;

namespace SGRE.TSA.Api.Extensions
{
    public static class ScopedServiceExtensions
    {
        public static void AddScopedServices(this IServiceCollection services)
        {
            //Telling the dependency injection container that OppSer is concrete impl. of IOppSer
            #region Service
            services.AddScoped<IOpportunityService, OpportunityService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMileStoneService, MileStoneService>();
            services.AddScoped<IProposalService, ProposalService>();
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ICertificationService, CertificationService>();
            services.AddScoped<IWtgCatalogueService, WtgCatalogueService>();
            services.AddScoped<IProjectConstraintsService, ProjectConstraintsService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IConfigScenarioService, ConfigScenarioService>();
            services.AddScoped<ISupportService, SupportService>();
            services.AddScoped<IGMBService, GMBService>();
            services.AddScoped<IAssignRolesService, AssignRolesService>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<ILoadsClusterService, LoadsClusterService>();
            services.AddScoped<IApplicationReasonService, ApplicationReasonService>();
            services.AddScoped<ISstTowerService, SstTowerService>();
            services.AddScoped<IBaseTowerService, BaseTowerService>();
            services.AddScoped<IAepLookupService, AepLookupService>();
            services.AddScoped<IDesignEvaluationService, DesignEvaluationService>();
            services.AddScoped<IServiceBusFactory, ServiceBusFactory>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IWtgCatalogueModelService, WtgCatalogueModelService>();

            // cost and feedback service ...

            services.AddScoped<ICostOverViewService, CostOverViewService>();
            services.AddScoped<ICostsTowerConstructionLeadTimeService, CostsTowerConstructionLeadTimeService>();
            services.AddScoped<ICostsTowerConstructionService, CostsTowerConstructionService>();
            services.AddScoped<ICostsTowerCustomsService, CostsTowerCustomsService>();
            services.AddScoped<ICostsTowerExWorksMetaService, CostsTowerExWorksMetaService>();
            services.AddScoped<ICostsTowerLogisticsLeadTimeService, CostsTowerLogisticsLeadTimeService>();
            services.AddScoped<ICostsTowerLogisticsService, CostsTowerLogisticsService>();
            services.AddScoped<ICostsTowerSupplierServiceMeta, CostsTowerSupplierServiceMeta>();

            services.AddScoped<ICostsFeedbackService, CostsFeedbackService>();

            services.AddScoped<ITowerSupplierRegionService, TowerSupplierRegionService>();

            services.AddScoped<ISstDesignRequestServices, SstDesignRequestServices>();


            #endregion


            #region External Service
            services.AddScoped<IRegionExternalService, RegionExternalService>();
            services.AddScoped<IOpportunitiesExternalService, OpportunitiesExternalService>();
            services.AddScoped<ICountryExternalService, CountryExternalService>();
            services.AddScoped<IRoleExternalService, RoleExternalService>();
            services.AddScoped<IMileStonesExternalService, MileStonesExternalService>();
            services.AddScoped<IProposalExternalService, ProposalExternalService>();
            services.AddScoped<IQuoteExternalService, QuoteExternalService>();
            services.AddScoped<ITaskExternalService, TaskExternalService>();
            services.AddScoped<ICertificationExternalService, CertificationExternalService>();
            services.AddScoped<IWtgCatalogueExternalService, WtgCatalogueExternalService>();
            services.AddScoped<IProjectConstraintsExternalService, ProjectConstraintsExternalService>();
            services.AddScoped<IPermissionExternalService, PermissionExternalService>();
            services.AddScoped<IConfigScenarioExternalService, ConfigScenarioExternalService>();
            services.AddScoped<ISupportExternalService, SupportExternalService>();
            services.AddScoped<IGMBExternalService, GMBExternalService>();
            services.AddScoped<IAssignRolesExternalService, AssignRolesExternalService>();
            services.AddScoped<IFileUploadExternalService, FileUploadExternalService>();
            services.AddScoped<ILoadsClusterExternalService, LoadsClusterExternalService>();
            services.AddScoped<IApplicationReasonExternalService, ApplicationReasonExternalService>();
            services.AddScoped<IAepLookupExternalService, AepLookupExternalService>();

            services.AddScoped<IExternalServiceFactory, ExternalServiceFactory>();

            #endregion
        }
    }
}
