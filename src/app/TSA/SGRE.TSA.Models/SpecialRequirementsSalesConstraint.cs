using SGRE.TSA.Models.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class SpecialRequirementsSalesConstraint : Audit
    {
        public int Id { get; set; }
        // Foreign key to ProjectConstraints
        public ProjectConstraint ProjectConstraint { get; set; }
        [Required]
        public int ProjectConstraintId { get; set; }
        // Requirements - Sesimic
        [Required]
        public bool SesimicRequirements { get; set; }

        // Requirements - Sesimic country code
        [SesimicRequirements]
        public string SesimicRequirementsCountryCode { get; set; }
        // Requirements - Nearshore
        [Required]
        public bool Nearshore { get; set; }
       
        [Required]
        public string[] ExtremeWindConditions { get; set; }

        [EWCOtherDescription]
        public string ExtremeWindConditionsOtherDescription { get; set; }
        // Requirements - Limit Supplier selection
        // TODO : Store it as an array in DB or comma separated string
        [Required]
        public string[] ProjReqLimitSupplierSelection { get; set; }
        // Requirements - Supplier        
        public string SupplierRequirements { get; set; }
        // Requirements - Additional
        public string AdditionalRequirements { get; set; }
    }
}
