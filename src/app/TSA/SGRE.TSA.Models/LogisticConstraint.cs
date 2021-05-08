using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class LogisticConstraint : Audit
    {
        public int Id { get; set; }
        // Foreign key to ProjectConstraints
        public ProjectConstraint ProjectConstraint { get; set; }
        [Required]
        public int ProjectConstraintId { get; set; }
        // Foreign key to Logistic Status
        public LogisticStatus LogisticStatus { get; set; }

        [Required]
        public int LogisticStatusId { get; set; }
        public string PreliminaryLogisticsDocumentLink { get; set; }
        public string RoadSurveyDocumentLink { get; set; }

        public bool UsingProjectSpecificBoundary { get; set; }

        public TransportMode TransportMode { get; set; }

        [Required]
        public int TransportModeId { get; set; }

        public ICollection<LogisticProjectBoundary> logisticProjectBoundaries { get; set; }
    }
}
