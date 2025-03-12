using AdminApi.DTOs;
using AdminApi.DTOs.Responses;

namespace AdminApi.Services.AdminService
{
    public interface IAdminService
    {
        BaseResponse GetAdmins();
        BaseResponse PutAdmin(AdminDTO request);
        BaseResponse LoginAdmin(AdminDTO request);
    }
}
