using AdminApi.DTOs.Responses;
using MimeKit;

namespace AdminApi.Utilities.EmailServiceUtility
{
    public interface IEmailService
    {
        public Task<BaseResponse> SendEmail(MimeMessage msg);
    }
}
