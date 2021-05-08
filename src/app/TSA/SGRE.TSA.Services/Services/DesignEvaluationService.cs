using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SGRE.TSA.Services.Services
{
    public class DesignEvaluationService : IDesignEvaluationService
    {
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly ILogger<DesignEvaluationService> _logger;
        private readonly IConfiguration _configuration;

        public DesignEvaluationService(IExternalServiceFactory externalServiceFactory, ILogger<DesignEvaluationService> logger, IConfiguration configuration)
        {
            _externalServiceFactory = externalServiceFactory;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<(bool IsSuccess, IEnumerable<RequestDesignEvaluation> requestDesignResult)> GetRequestDesignAsync(int sstId)
        {
            var externalService = _externalServiceFactory.CreateExternalService<RequestDesignEvaluation>(_logger);

            var requestDesignResult = await externalService.GetByIdAsync(sstId);

            if (requestDesignResult.IsSuccess)
            {
                var result = new
                {
                    requestDesign = requestDesignResult.ResponseData
                };
                return (true, result.requestDesign);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<Summary> DESumResults)> GetDESummaryIdAsync(int id)
        {
            var externalService = _externalServiceFactory.CreateExternalService<Summary>(_logger);

            var DESumResult = await externalService.GetByIdAsync(id);

            if (DESumResult.IsSuccess)
            {
                if (!DESumResult.ResponseData.Any())
                    return (false, null);


                var expiresOn = DateTimeOffset.UtcNow.AddHours(1);
                var key = new StorageSharedKeyCredential(_configuration.GetSection("ServiceBus:TowerSiteStorageAccountName").Value, _configuration.GetSection("ServiceBus:TowerSiteBlobAccountKey").Value);

                if (!string.IsNullOrWhiteSpace(DESumResult.ResponseData.FirstOrDefault()?.TowersiteDesignOutputFileLink))
                {
                    var blob = new BlobClient(new Uri(HttpUtility.UrlDecode(DESumResult.ResponseData.FirstOrDefault()?.TowersiteDesignOutputFileLink)), key);

                    var sasBuilder = new BlobSasBuilder()
                    {
                        BlobName = blob.Name,
                        BlobContainerName = blob.BlobContainerName,
                        StartsOn = DateTimeOffset.UtcNow,
                        ExpiresOn = expiresOn,
                    };

                    sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);


                    DESumResult.ResponseData.FirstOrDefault().TowersiteDesignOutputFileLink = blob.GenerateSasUri(sasBuilder).ToString();
                }

                return (true, DESumResult.ResponseData);

            }
            return (false, null);

        }


        public async Task<(bool IsSuccess, IEnumerable<SstDesignEvaluation> DesignEvaluation)> GetDesignEvaluationByIdAsync(int id)
        {
            var externalService = _externalServiceFactory.CreateExternalService<SstDesignEvaluation>(_logger);

            var DesignEval = await externalService.GetByIdAsync(id);

            if (DesignEval.IsSuccess)
            {
                return (true, DesignEval.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<SegmentDimensionSummary> segmentDimensionSummaryResult)> GetSegmentDimensionSummaryAsync(int sstId)
        {
            var externalService = _externalServiceFactory.CreateExternalService<SegmentDimensionSummary>(_logger);
            var segmentDimensionSummaryResult = await externalService.GetByIdAsync(sstId);

            if (segmentDimensionSummaryResult.IsSuccess)
            {
                var result = new
                {
                    segmentDimensionSummary = segmentDimensionSummaryResult.ResponseData
                };
                return (true, result.segmentDimensionSummary);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<SegmentDimensionTable> segmentDimensionTableResult)> GetSegmentDimensionTableAsync(string SstUuid)
        {
            var externalService = _externalServiceFactory.CreateExternalService<SegmentDimensionTable>(_logger);


            var segmentDimensionTableResult = await externalService.GetAsync($"?$filter=sstUuid eq {SstUuid}");

            if (segmentDimensionTableResult.IsSuccess)
            {
                var result = new
                {
                    segmentDimensionTable = segmentDimensionTableResult.ResponseData
                };
                return (true, result.segmentDimensionTable);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic DesignEvaluationResults)> PutDesignEvaluationAsync(SstDesignEvaluation sstDesignEvaluation)
        {
            var externalService = _externalServiceFactory.CreateExternalService<SstDesignEvaluation>(_logger);

            var designEvaluationResult = await externalService.PutAsync(sstDesignEvaluation);

            if (designEvaluationResult.IsSuccess)
            {
                var result = new
                {
                    designEvaluationResult = designEvaluationResult.ResponseData
                };
                return (true, result);
            }

            return (false, designEvaluationResult.ErrorMessage);
        }

        public async Task<(bool IsSuccess, dynamic DesignEvaluationResults)> PatchDesignEvaluationAsync(SstDesignEvaluation sstDesignEvaluation)
        {
            var externalService = _externalServiceFactory.CreateExternalService<SstDesignEvaluation>(_logger);

            var designEvaluationResult = await externalService.PatchAsync(sstDesignEvaluation);
            if (designEvaluationResult.IsSuccess)
            {
                var result = new
                {
                    designEvaluationResult = designEvaluationResult.ResponseData
                };
                return (true, result);
            }

            return (false, designEvaluationResult.ErrorMessage);
        }

        public async Task<RequestDesignEvaluation> updateMessageData(RequestDesignEvaluation dataToSend, IProjectConstraintsService projectConstraintsService, IGMBService gMBService, IOpportunityService opportunityService)
        {
            RequestDesignEvaluation requestDesignEvaluation;
            requestDesignEvaluation = dataToSend;

            int _projectId = Convert.ToInt32(dataToSend.DesignDescription.Split('-')[0]);

            var constraintResults = await projectConstraintsService.GetProjectConstraintsAsync(_projectId);

            if (!constraintResults.IsSuccess)
                return requestDesignEvaluation;

            if (constraintResults.ConstraintResults == null || !constraintResults.ConstraintResults.Any())
            {
                //which means project constraints data not availabe...
                requestDesignEvaluation = await updateWithGenericMarketData(requestDesignEvaluation, _projectId, opportunityService, gMBService, 1);
            }
            else
            {
                var projectConstraint = constraintResults.ConstraintResults.FirstOrDefault();

                bool usingProjectSpecificBoundary = projectConstraint?.LogisticConstraint?.UsingProjectSpecificBoundary ?? false;

                if (usingProjectSpecificBoundary)
                {
                    if (requestDesignEvaluation.BottomDiameter_max == 0)
                        requestDesignEvaluation.BottomDiameter_max = projectConstraint.LogisticConstraint.logisticProjectBoundaries.FirstOrDefault(prb => prb.TransportModeId == projectConstraint.LogisticConstraint.TransportModeId)?.MaxTowerBaseDiameterMtrs * 1000 ?? 0; // Converting to mm

                    if (requestDesignEvaluation.SectionLength_max == 0)
                        requestDesignEvaluation.SectionLength_max = projectConstraint.LogisticConstraint.logisticProjectBoundaries.FirstOrDefault(prb => prb.TransportModeId == projectConstraint.LogisticConstraint.TransportModeId)?.MaxSegmentLengthMtrs ?? 0;

                    if (requestDesignEvaluation.SectionWeight_max == 0)
                        requestDesignEvaluation.SectionWeight_max = projectConstraint.LogisticConstraint.logisticProjectBoundaries.FirstOrDefault(prb => prb.TransportModeId == projectConstraint.LogisticConstraint.TransportModeId)?.MaxSegmentWeightTon ?? 0;
                }
                else
                    requestDesignEvaluation = await updateWithGenericMarketData(requestDesignEvaluation, _projectId, opportunityService, gMBService, projectConstraint.LogisticConstraint.TransportModeId);
            }

            return requestDesignEvaluation;
        }

        public async Task<RequestDesignEvaluation> updateWithGenericMarketData(RequestDesignEvaluation _requestDesignEvaluation, int _projectId, IOpportunityService opportunityService, IGMBService gMBService, int _TransportModeId)
        {
            var opportunityResult = await opportunityService.GetOpportunityByIdAsync(_projectId);

            if (opportunityResult.IsSuccess)
            {
                var GMBData = await gMBService.GetGMBAsync(opportunityResult.OpportunityResults.FirstOrDefault().CountryId);
                if (GMBData.IsSuccess)
                {
                    IEnumerable<GenericMarketBoundary> genericMarketBoundary;
                    genericMarketBoundary = GMBData.genericMarketBoundaries; // road and rail both
                    if (genericMarketBoundary != null && genericMarketBoundary.Any())
                    {
                        if (_requestDesignEvaluation.BottomDiameter_max == 0)
                            _requestDesignEvaluation.BottomDiameter_max = genericMarketBoundary.FirstOrDefault(gmb => gmb.TransportModeId == _TransportModeId)?.MaxTowerBaseDiameterMtrs * 1000 ?? 0; // Converting to mm

                        if (_requestDesignEvaluation.SectionLength_max == 0)
                            _requestDesignEvaluation.SectionLength_max = genericMarketBoundary.FirstOrDefault(gmb => gmb.TransportModeId == _TransportModeId)?.MaxSegmentLengthMtrs ?? 0;

                        if (_requestDesignEvaluation.SectionWeight_max == 0)
                            _requestDesignEvaluation.SectionWeight_max = genericMarketBoundary.FirstOrDefault(gmb => gmb.TransportModeId == _TransportModeId)?.MaxSegmentWeightTon ?? 0;
                    }

                }
            }
            return _requestDesignEvaluation;
        }
    }
}
