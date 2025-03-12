using AdminApi.DTOs;
using AdminApi.DTOs.Responses;
using AdminApi.Services.AdminService;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpPost("register")]
        public BaseResponse AddAdmin(AdminDTO request)
        {
            return adminService.PutAdmin(request);
        }

        [HttpPost("login")]
        public BaseResponse LoginAdmin(AdminDTO request)
        {
            return adminService.PutAdmin(request);
        }
        
        [HttpPost("get-all")]
        public BaseResponse AdminList()
        {
            return adminService.GetAdmins();
        }

    }
}
