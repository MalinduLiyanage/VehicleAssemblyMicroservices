using AdminApi.DTOs;

namespace AdminApi.Services.ValidationService
{
    public interface ICreateAdminValidationService
    {
        List<string> Validate(AdminDTO request);
    }
}
