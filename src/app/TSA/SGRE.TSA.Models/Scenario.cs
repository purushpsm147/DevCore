using SGRE.TSA.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class Scenario : Audit
    {
        public int Id { get; set; }

        public Quote Quote { get; set; }
        [Required]
        public int QuoteId { get; set; }

        [Required]
        public string WindfarmConfigurationId { get; set; }

        [Required]
        public int ScenarioNo { get; set; }

        [Required]
        public ScenarioTypes ScenarioType { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public ScenarioStatus Status { get; set; }

        [Required]
        public ScenarioProgress Progress { get; set; }

        [Required]
        public ScenarioOptions Options { get; set; }

        [Required]
        public bool StepProgress { get; set; }

        public ICollection<ScenarioDesign> ScenarioDesigns { get; set; }
        public ICollection<ScenarioCostsKpi> ScenarioCostsKpis { get; set; }

        public WtgCatalogue wtgCatalogue { get; set; }
        public int WtgCatalogueId { get; set; }

        public string loadCluster { get; set; }
        public string DesignEvaluationRisk { get; set; }
    }

    public class ScenarioDTO
    {
        public ScenarioTypes ScenarioType { get; set; }

        public int ItemId { get; set; }

        public int QuoteId { get; set; }

        public string WindfarmConfigurationId { get; set; }

        public decimal AepP50BindingOfferNet { get; set; }
        public decimal AepP50NominationGross { get; set; }
        public decimal AepP50SignatureNet { get; set; }

    }
}
