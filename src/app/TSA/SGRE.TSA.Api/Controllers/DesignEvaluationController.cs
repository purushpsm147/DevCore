using Azure.Messaging.ServiceBus;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRE.TSA.Api.Factory;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class DesignEvaluationController : ControllerBase
    {
        private readonly IDesignEvaluationService designEvaluationService;

        public DesignEvaluationController(IDesignEvaluationService designEvaluationService)
        {
            this.designEvaluationService = designEvaluationService;
        }
        [HttpGet]
        [Route("SupportedFeatures/{SstId:int}"), EnableQuery()]
        public async Task<SupportedFeatures> GetSupportedFeatures([FromServices] ISstTowerService sSTTower, [FromServices] IWtgCatalogueService wtgCatalogueService, int SstId)
        {
            var result = await sSTTower.GetSstTowerIdAsync(SstId);

            if (!result.IsSuccess)
                return new SupportedFeatures();

            (int[] appModeIds, int catalogueId, string features) = result.sstResults.Select(s => (s.ApplicationModes, s.WtgCatalogueId, s.Features)).FirstOrDefault();

            var catRes = await wtgCatalogueService.GetWtgCatalogueAsync(catalogueId);
            if (!catRes.IsSuccess)
                return new SupportedFeatures();

            var appModes = catRes.wtgCatalogueResult.ApplicationModes;

            SupportedFeatures supportedFeatures = new SupportedFeatures();

            var res = (from w in appModes
                       orderby w.Id
                       select new ApplicationModesFeatures
                       {
                           Id = w.Id,
                           Mode = w.ApplicationModeNo,
                           RatingMW = w.PowerRating,
                           Availability = Array.Exists(appModeIds, a => a == w.ApplicationModeNo),
                       }).ToList();

            bool _activated = true;
            if (string.IsNullOrWhiteSpace(features))
            {
                _activated = false;
                features = "ACS 2.0";
            }

            supportedFeatures.applicationModesFeatures = res;
            supportedFeatures.otherFeatures = new OtherFeatures()
            {
                Id = 1,
                Options = features,
                Activated = _activated,
            };

            return supportedFeatures;
        }

        [HttpGet]
        [Route("Summary/{SstId:int}"), EnableQuery()]
        public async Task<IEnumerable<Summary>> GetDesignSummary(int SstId)
        {
            var result = await designEvaluationService.GetDESummaryIdAsync(SstId);

            if (!result.IsSuccess)
                return new List<Summary>();

            return result.DESumResults;

        }

        [HttpGet]
        [Route("{sstId:int}"), EnableQuery()]
        public async Task<IEnumerable<SstDesignEvaluation>> GetDesignEvaluationAsync(int sstId, [FromServices] ISstDesignRequestServices sstDesignRequestService)
        {
            var result = await designEvaluationService.GetDesignEvaluationByIdAsync(sstId);
            var sstDesignResult = await sstDesignRequestService.GetSstDesignRequestByIdAsync(sstId);
           
            if (!result.IsSuccess)
                return new List<SstDesignEvaluation>();

            if (sstDesignResult.IsSuccess)
                result.DesignEvaluation.FirstOrDefault().SstDesignRequest = sstDesignResult.SstDesignRequest.FirstOrDefault();

            return result.DesignEvaluation;
        }

        [HttpPut]
        [Route("RequestDesignEvaluation")]
        public async Task<IActionResult> PutRequestDesignEvaluation([FromServices] ISstTowerService sstTowerService, [FromServices] IServiceBusFactory serviceBusFactory, [FromServices] IProjectConstraintsService projectConstraintsService, [FromServices] IOpportunityService opportunityService, [FromServices] IGMBService gMBService, [FromServices] ISstDesignRequestServices sstDesignRequestService , FrontendRequest frontendRequest)
        {
            IServiceBus serviceBus = serviceBusFactory.GetServiceBus("DesignEvaluation");

            if (frontendRequest.sstTowerId <= 0)
                return BadRequest("Sst/item id is missing!");

            var res = await sstTowerService.GetSstTowerIdAsync(frontendRequest.sstTowerId);


            if (!res.IsSuccess)
                return BadRequest(res.sstResults);

            if (!res.sstResults.Any())
                return StatusCode(400, "SST data are empty!");

            bool alreadyRequested = res.sstResults.FirstOrDefault().IsDesignEvaluationRequest;

            if (alreadyRequested)
                return BadRequest("It seems like you are trying to request twice!");

            if (string.IsNullOrWhiteSpace(res.sstResults.FirstOrDefault().ElsaLoadEnvelopLink) || string.IsNullOrWhiteSpace(res.sstResults.FirstOrDefault().SafalLoadEnvelopLink))
                return StatusCode(400, "Insufficient data to request design evaluation, ELSA or SAFAL file are missing!");

            var result = await designEvaluationService.GetRequestDesignAsync(frontendRequest.sstTowerId);

            if (!result.IsSuccess)
                return BadRequest("Insufficient data to request design evaluation!!");

            var dataToSend = getMessageData(result.requestDesignResult.FirstOrDefault(), frontendRequest);

            var modifiedDataToSend = dataToSend;

            if (dataToSend.BottomDiameter_max == 0 || dataToSend.SectionLength_max == 0 || dataToSend.SectionWeight_max == 0)
                modifiedDataToSend = await designEvaluationService.updateMessageData(dataToSend, projectConstraintsService, gMBService, opportunityService);

            if (modifiedDataToSend.BottomDiameter_max != 0)
                modifiedDataToSend.BottomDiameter_max = modifiedDataToSend.BottomDiameter_max / 1000; // value converted to meters

            modifiedDataToSend.SectionWeight_max = modifiedDataToSend.SectionWeight_max * 1000;  // value converted to kg
            modifiedDataToSend.Model = res.sstResults.FirstOrDefault().WtgCatalogueModel.Model;

            modifiedDataToSend.LoadsetHubHeight = res.sstResults.FirstOrDefault().LoadsetHubHeight;
            modifiedDataToSend.ElevatedFoundationHeight = res.sstResults.FirstOrDefault().SstPredesignDimesions.TotalFoundationHeight;


            if (modifiedDataToSend.BottomDiameter_max == 0 || modifiedDataToSend.SectionLength_max == 0 || modifiedDataToSend.SectionWeight_max == 0)
                return StatusCode(400, "Segment dimensions not available, so Request Design Evaluation cannot be done!");

            serviceBus.SetServiceBusClient();
            ServiceBusSender sender = serviceBus.SetServiceBusSender();
            var jsonString = JsonConvert.SerializeObject(modifiedDataToSend);
            ServiceBusMessage message = new ServiceBusMessage(jsonString);

            await sender.SendMessageAsync(message);

            var sstDesignRequest = new SstDesignRequest()
            {
                JsonServiceBusPayload = modifiedDataToSend,
                SstTowerId = modifiedDataToSend.sstTowerId
            };

            var sstDesignResult = await sstDesignRequestService.PutSstDesignRequestAsync(sstDesignRequest);
            if (!sstDesignResult.IsSuccess)
            {
                ModelState.AddModelError("sstDesignRequestServiceFailure", "Failed to add Request Design Evaluation Service Bus posted message to Database");
            }
            var _sstData = res.sstResults.FirstOrDefault();

            _sstData.IsDesignEvaluationRequest = true;
            _sstData.IsDesignEvaluationComplete = false;
            _sstData.LastModifiedDateTime = DateTime.UtcNow;
            _sstData.RequestDesignEvaluationStartDatetime = DateTime.UtcNow;

            var res_update_sst = await sstTowerService.PatchSstTowerWithoutCostKpiAsync(frontendRequest.sstTowerId, _sstData);

            var onSuccessMessage = ModelState.ErrorCount == 0 ? "Submitted for SST tower calculation. General wait time is 30-60min (Max - 180 min)!" : "Failed to add Request Design Evaluation Service Bus posted message to Database";
            
            if (res_update_sst.IsSuccess)
                return StatusCode(202, onSuccessMessage);
            else
            {
                ModelState.AddModelError("RequestDesignEvaluationError", "There is an problem to complete Request Design Evaluation!\n\n" + res_update_sst.sstResults);
                return StatusCode(400, ModelState);
            }
                

        }

        private RequestDesignEvaluation getMessageData(RequestDesignEvaluation _requestDesignEvaluation, FrontendRequest frontendRequest)
        {
            RequestDesignEvaluation requestDesignEvaluation;

            requestDesignEvaluation = _requestDesignEvaluation;
            requestDesignEvaluation.DesignAuthorEmail = "";
            requestDesignEvaluation.SectionNumber_max = 200;
            requestDesignEvaluation.Posc2Folder = "";
            requestDesignEvaluation.XmlFilePath = "";
            requestDesignEvaluation.DesignVersion = 1;
            requestDesignEvaluation.sstTowerId = frontendRequest.sstTowerId;
            requestDesignEvaluation.scenarioId = frontendRequest.scenarioId;

            requestDesignEvaluation.DesignDescription += "-" + frontendRequest.scenarioId + "-" + frontendRequest.sstTowerId;
            return requestDesignEvaluation;
        }

        [HttpPut]
        public async Task<IActionResult> PutDesignEvaluation([FromServices] IDesignEvaluationService designEvaluationService, SstDesignEvaluation sstDesignEvaluation)
        {
            if (sstDesignEvaluation.sstTowerId <= 0)
                return BadRequest("SST Id is missing!");

            var result = await designEvaluationService.PutDesignEvaluationAsync(sstDesignEvaluation);

            if (!result.IsSuccess)
            {
                return BadRequest(result.DesignEvaluationResults);
            }
            return Ok(result.DesignEvaluationResults);
        }

        [HttpPatch]
        public async Task<IActionResult> PatchDesignEvaluation([FromServices] IDesignEvaluationService designEvaluationService, SstDesignEvaluation sstDesignEvaluation)
        {
            if (sstDesignEvaluation.sstTowerId <= 0)
                return BadRequest("SST Id is missing!");

            var result = await designEvaluationService.PatchDesignEvaluationAsync(sstDesignEvaluation);
            if (result.IsSuccess)
            {
                return Ok(result.DesignEvaluationResults);
            }

            return NotFound(result.DesignEvaluationResults);
        }

        [HttpGet]
        [Route("SegmentDimension/Summary/{SstId:int}"), EnableQuery()]
        public async Task<IEnumerable<SegmentDimensionSummary>> GetSegmentDimensionSummary([FromServices] IDesignEvaluationService designEvaluationService, int SstId)
        {
            if (SstId <= 0)
                return new List<SegmentDimensionSummary>();

            var result = await designEvaluationService.GetSegmentDimensionSummaryAsync(SstId);

            if (!result.IsSuccess)
                return new List<SegmentDimensionSummary>();

            return result.segmentDimensionSummaryResult;
        }

        [HttpGet]
        [Route("SegmentDimension/Table/{SstUuid}"), EnableQuery()]
        public async Task<IEnumerable<SegmentDimensionTable>> GetSegmentDimensionTable([FromServices] IDesignEvaluationService designEvaluationService, string SstUuid)
        {
            if (string.IsNullOrWhiteSpace(SstUuid))
                return new List<SegmentDimensionTable>();

            var result = await designEvaluationService.GetSegmentDimensionTableAsync(SstUuid);

            if (!result.IsSuccess)
                return new List<SegmentDimensionTable>();

            return result.segmentDimensionTableResult;
        }      
    }
}
