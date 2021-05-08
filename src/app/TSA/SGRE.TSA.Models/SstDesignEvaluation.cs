using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class SstDesignEvaluation : Audit
    {
        public int Id { get; set; }

        public SstTower sstTower { get; set; }

        [Required]
        public int sstTowerId { get; set; }

        public decimal HubHeightM { get; set; }

        public decimal TowerHeightM { get; set; }

        public decimal TowerWeightT { get; set; }

        public int DesignLifeTime { get; set; }

        public bool IsDesignSuccess { get; set; }

        [StringLength(128, ErrorMessage = "Risks exceeded max length")]
        public string Risks { get; set; }

        [StringLength(128, ErrorMessage = "DesignLifetimeStatus exceeded max length")]
        public string DesignLifetimeStatus { get; set; }

        [StringLength(128, ErrorMessage = "LoadsAssessmentStatus exceeded max length")]
        public string LoadsAssessmentStatus { get; set; }

        [StringLength(128, ErrorMessage = "TowerDesignStatus exceeded max length")]
        public string TowerDesignStatus { get; set; }

        public bool IsSeismicConstraint { get; set; }

        public bool IsPreliminaryLogisticAssessmentRequest { get; set; }

        public bool IsCostsTimelineFeedbackRequest { get; set; }

        public SstDesignSeismicAssessment SstDesignSeismicAssessment { get; set; }

        public int SstDesignSeismicAssessmentId { get; set; }

        public SstDesignRequest SstDesignRequest { get; set; }

    }
}
