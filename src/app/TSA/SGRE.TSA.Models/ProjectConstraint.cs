using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class ProjectConstraint : Audit
    {
        public int Id { get; set; }
        // Foreign key to Project
        public Project Project { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public PermitsSalesConstraint PermitsSalesConstraint { get; set; }
        public LogisticConstraint LogisticConstraint { get; set; }
        public ConstructionConstraint ConstructionConstraint { get; set; }
        public SpecialRequirementsSalesConstraint SpecialRequirementsSalesConstraint { get; set; }
    }
}
