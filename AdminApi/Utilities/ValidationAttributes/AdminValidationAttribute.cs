using System.ComponentModel.DataAnnotations;
using AdminApi.DTOs;
using AdminApi.Services.ValidationService;

namespace AdminApi.Utilities.ValidationAttributes
{
    public class AdminValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var request = (AdminDTO)validationContext.ObjectInstance;

            if (request.NIC <= 0 || request.Firstname == null || request.Lastname == null || request.Email == null)
            {
                return new ValidationResult("All fields are required.");
            }

            ICreateAdminValidationService validationService = (ICreateAdminValidationService)validationContext.GetService(typeof(ICreateAdminValidationService));

            if (validationService == null)
            {
                return new ValidationResult("Validation service is unavailable.");
            }

            var validationErrors = validationService.Validate(request);

            if (validationErrors.Any())
            {
                return new ValidationResult(string.Join("; ", validationErrors));
            }

            return ValidationResult.Success;
        }
    }
}
