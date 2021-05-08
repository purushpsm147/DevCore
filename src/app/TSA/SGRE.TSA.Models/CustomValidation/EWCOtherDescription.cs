using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models.CustomValidation
{
    public class EWCOtherDescription : ValidationAttribute
    {
        private readonly string _errorMessage = "Other Wind Conditions Required (Min Lenghth 3)";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var special = (SpecialRequirementsSalesConstraint)validationContext.ObjectInstance;

            if (special.ExtremeWindConditions.Length > 2 && (string.IsNullOrWhiteSpace(special.ExtremeWindConditionsOtherDescription) || special.ExtremeWindConditionsOtherDescription.Length <3))
                return new ValidationResult(_errorMessage);

            return ValidationResult.Success;
        }
    }
}
