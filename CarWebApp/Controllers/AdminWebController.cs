using CarWebApp.Interface;
using CarWebApp.Models;
using CarWebApp.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApp.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("upload-car")]
        public async Task<ActionResult<ResponseDTO>> UploadCar([FromForm] CarUploadDTO dto)
        {
            var response = new ResponseDTO();

            try
            {
                var (success, message) = await _adminService.UploadCarAsync(dto.Car, dto.Images);

                response.Success = success;
                response.Message = message;
                response.StatusCode = success ? 200 : 400;
            }
            catch (ArgumentException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = 400;
                response.Error = ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = 409;
                response.Error = ex.Message;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An unexpected error occurred.";
                response.StatusCode = 500;
                response.Error = ex.Message;
            }

            return StatusCode(response.StatusCode, response);
        }
    }
}
