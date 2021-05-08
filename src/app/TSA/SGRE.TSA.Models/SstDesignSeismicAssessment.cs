using SGRE.TSA.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class SstDesignSeismicAssessment : Audit
    {
        public int Id { get; set; }

        public SstDesignEvaluation SstDesignEvaluation { get; set; }

        [Required]
        public int SstDesignEvaluationId { get; set; }

        public SeismicAssessmentStatusTypes SeismicAssessmentStatusTypes { get; set; }

        public SeismicAssessmentResultTypes SeismicAssessmentResultTypes { get; set; }

        public bool IsFirstLevelAssessmentRequest { get; set; }

        public string FirstLevelAssessmentDocLink { get; set; }

        public bool IsSecondLevelAssessmentRequest { get; set; }

        public string SecondLevelAssessmentDocLink { get; set; }
    }
}
