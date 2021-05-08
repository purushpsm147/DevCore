using System;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class SstTower : Audit
    {
        public int Id { get; set; }

        public Quote Quote { get; set; }
        [Required]
        public int QuoteId { get; set; }

        [Required]
        public string WindfarmConfigurationId { get; set; }

        public LoadsCluster LoadsCluster { get; set; }
        [Required]
        public int LoadsClusterId { get; set; }

        public WtgCatalogue WtgCatalogue { get; set; }
        [Required]
        public int WtgCatalogueId { get; set; }

        public WtgCatalogueModel WtgCatalogueModel { get; set; }
        [Required]
        public int WtgCatalogueModelId { get; set; }

        [Required]
        public int TurbineQuantity { get; set; }

        [Required]
        public decimal ClusterSize { get; set; }

        public bool TurbineQuantityTrigger { get; set; }

        public bool WindfarmSizeTrigger { get; set; }

        public TowerType TowerType { get; set; }
        [Required]
        public int TowerTypeId { get; set; }

        public decimal? ExistingHubHeight { get; set; }

        [Required]
        public decimal LoadsetHubHeight { get; set; }

        public decimal? ProposedHubHeight { get; set; }

        public bool IsProposeHubHeight { get; set; }

        public InitialTower InitialTower { get; set; }

        public int? InitialTowerId { get; set; }

        public bool IsInitialTowerFound { get; set; }
        [Required]
        public decimal MaxShear { get; set; }

        [Required]
        public decimal AverageShear { get; set; }

        [Required]
        public decimal TargetLifeTime { get; set; }

        [StringLength(256, ErrorMessage = "HigherHubHeightPossible exceeded maximum length")]
        public string HigherHubHeightPossible { get; set; }

        [Required]
        public decimal TowerWeight { get; set; }

        [Required]
        public int[] ApplicationModes { get; set; }

        [Required]
        public int[] NoiseModes { get; set; }

        public string NoiseModeDocLink { get; set; }

        [StringLength(64, ErrorMessage = "Features is Required")]
        public string Features { get; set; }

        public string ElsaLoadEnvelopLink { get; set; }

        public string SafalLoadEnvelopLink { get; set; }

        [Required]
        public int[] ApplicationReasons { get; set; }

        [StringLength(64, ErrorMessage = "ApplicationReasonsOtherDescription exceeded maximum length")]
        public string ApplicationReasonsOtherDescription { get; set; }

        public bool IsSarInputRequest { get; set; }

        public bool IsDesignEvaluationRequest { get; set; }

        public bool IsDuplicateScenario { get; set; }

        [Required]
        public Guid AepLookupUuid { get; set; }

        public bool IsDesignEvaluationComplete { get; set; }

        public DateTimeOffset? RequestDesignEvaluationStartDatetime { get; set; }

        public DateTimeOffset? RequestDesignEvaluationEndDatetime { get; set; }

        public Guid SstUuid { get; set; }

        public string ErrorCode { get; set; }

        public string TowersiteDesignOutputFileLink { get; set; }

        public int TowersiteDesignOutputFileSizeBytes { get; set; }

        public SstPredesignDimesions SstPredesignDimesions { get; set; }

        public SstPredesignProposedHubHeight SstPredesignProposedHubHeights { get; set; }
        public SstPredesignExistingHubHeight SstPredesignExistingHubHeights { get; set; }
    }
}
