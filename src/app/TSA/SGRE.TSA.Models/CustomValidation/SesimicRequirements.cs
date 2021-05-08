using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models.CustomValidation
{
    public class SesimicRequirements : ValidationAttribute
    {
        private readonly string _errorMessage = "Country Code is Required (Min Length 2)";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var special = (SpecialRequirementsSalesConstraint)validationContext.ObjectInstance;

            if (special.SesimicRequirements && (special.SesimicRequirementsCountryCode.Length <= 1))
                return new ValidationResult(_errorMessage);

            return ValidationResult.Success;
        }
    }
}
