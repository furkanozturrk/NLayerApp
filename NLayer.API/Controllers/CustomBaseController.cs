using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreatActionResult<T>(CustomResponseDto<T> customResponseDto)
        {
            if (customResponseDto.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = customResponseDto.StatusCode
                };
            }
            return new ObjectResult(customResponseDto)
            {
                StatusCode = customResponseDto.StatusCode
            };
        }
    }
}
