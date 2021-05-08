using SGRE.TSA.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRE.TSA.Models
{
    public class CostsTowerConstruction : Audit
    {
        public int Id { get; set; }
        public CostFeedback CostFeedback { get; set; }

        [Required]
        public int CostFeedbackId { get; set; }

        public CostsTowerConstructionMeta CostsTowerConstructionMeta { get; set; }

        [Required]
        public int CostsTowerConstructionMetaId { get; set; }

        public decimal CostNomination { get; set; }

        public decimal CostOffer { get; set; }

        public decimal CostSignature { get; set; }

        public string Comments { get; set; }

        public int LeadTimesNominationWTG { get; set; }

        public int LeadTimesOfferWTG { get; set; }

        public int LeadTimesSignatureWTG { get; set; }

        public ToolKitEvaluation ToolKitEvaluation { get; set; }

        public string ToolKitEvaluationDocLink { get; set; }

        public string AdditionalFeeback { get; set; }

        public decimal DesignCostWindFarm { get; set; }

        public decimal ManufacturingInvestmentCostsWindFarm { get; set; }

        public int AdditionalTimelineWeeksWindfarm { get; set; }

        [Required]
        public ScenarioProgress ScenarioProgress { get; set; }

        public bool IsPhaseComplete { get; set; }

    }
}
