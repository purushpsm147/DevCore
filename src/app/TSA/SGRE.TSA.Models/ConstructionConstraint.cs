using SGRE.TSA.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class ConstructionConstraint : Audit
    {
        public int Id { get; set; }
        // Foreign key to ProjectConstraints
        public ProjectConstraint ProjectConstraints { get; set; }
        [Required]
        public int ConstraintId { get; set; }

        [Required]
        public ConstructionRestrictions Status { get; set; }

        [Required]
        public decimal CraneLiftingHeighMtrs { get; set; }

        [Required]
        public decimal CraneLiftingWeightTon { get; set; }

        public string AdditionalRequirements { get; set; }
    }
}
