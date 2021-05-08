using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class LogisticProjectBoundary : Audit
    {
        public int Id { get; set; }
        // Foreign key to LogisticConstraint
        public LogisticConstraint LogisticConstraint { get; set; }
        [Required]
        public int LogisticConstraintId { get; set; }
        // Foreign key to TransportMode
        public TransportMode TransportMode { get; set; }

        [Required]
        public int TransportModeId { get; set; }
        [Required]
        public decimal MaxTowerBaseDiameterMtrs { get; set; }
        [Required]
        public decimal MaxSegmentWeightTon { get; set; }
        [Required]
        public decimal MaxSegmentLengthMtrs { get; set; }

        public string Comments { get; set; }
    }
}
