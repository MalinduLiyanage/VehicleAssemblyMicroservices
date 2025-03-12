using AdminApi.DTOs;

namespace AdminApi.Services.ValidationService
{
    public class CreateAdminValidationService : ICreateAdminValidationService
    {
        private readonly ApplicationDbContext context;

        public CreateAdminValidationService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<string> Validate(AdminDTO request)
        {
            List<string> errors = new List<string>();

            if (context.admins.Any(v => v.NIC == request.NIC))
            {
                errors.Add("The NIC is already registered!");
            }

            if (context.admins.Any(v => v.Email == request.Email))
            {
                errors.Add("The Email is already registered!");
            }

            return errors;
        }
    }
}
