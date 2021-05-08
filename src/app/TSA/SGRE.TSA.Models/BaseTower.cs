using SGRE.TSA.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class BaseTower : Audit
    {
        public int Id { get; set; }

        public Quote Quote { get; set; }
        [Required]
        public int QuoteId { get; set; }

        [Required]
        public string SarCode { get; set; }

        [Required]
        public string WindfarmConfigurationId { get; set; }

        public LoadsCluster LoadsCluster { get; set; }
        [Required]
        public int LoadsClusterId { get; set; }

        public WtgCatalogue WtgCatalogue { get; set; }
        [Required]
        public int WtgCatalogueId { get; set; }

        [Required]
        public int TurbineQuantity { get; set; }

        [Required]
        public decimal ClusterSize { get; set; }

        public TowerType TowerType { get; set; }
        [Required]
        public int TowerTypeId { get; set; }

        [Required]
        public decimal RnaLifeTime { get; set; }

        [Required]
        public decimal MaxShear { get; set; }

        [Required]
        public decimal AverageShear { get; set; }

        [Required]
        public bool IsRnaComponentCheck { get; set; }

        [Required]
        public bool HigherHubheightPossibleTrigger { get; set; }

        [Required]
        public decimal TowerLoadsMargin { get; set; }

        [Required]
        public bool TowerLoadsMarginTrigger { get; set; }

        [Required]
        public decimal AepP50HubHeight { get; set; }

        [Required]
        public decimal HubHeight { get; set; }

        [Required]
        public int[] ApplicationModes { get; set; }

        [Required]
        public int[] NoiseModes { get; set; }

        [StringLength(64, ErrorMessage = "Features is Required")]
        public string Features { get; set; }

        public string NoiseModeDocLink { get; set; }

        public bool IsSarInputRequest { get; set; }

        public SarInputRequestStages SarInputRequestStages { get; set; }

        public bool IsPredesignLogisticAssessmentRequest { get; set; }

        public bool IsCostTimelinesRequest { get; set; }

        [Required]
        public ScenarioTypes ScenarioType { get; set; }

        public BaseTowerDesignDimension BaseTowerDesignDimension { get; set; }
        public BaseTowerAepHubheight BaseTowerAepHubheight { get; set; }
    }
}
