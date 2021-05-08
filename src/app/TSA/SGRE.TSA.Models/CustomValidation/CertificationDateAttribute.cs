using System;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models.CustomValidation
{
    public class CertificationDateAttribute : ValidationAttribute
    {
        private readonly string _errorMessage = "Certification date is required";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var proposal = (Proposal)validationContext.ObjectInstance;

            if (proposal.CertificationId != 4 && !proposal.CertificationDate.HasValue)
                return new ValidationResult(_errorMessage);

            return ValidationResult.Success;
        }
    }
}
