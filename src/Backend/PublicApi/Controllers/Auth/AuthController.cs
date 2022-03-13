using EvrenDev.Application.DTOS.Auth;
using EvrenDev.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EvrenDev.PublicApi.Controllers.Auth
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            this._identityService = identityService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenAsync(TokenRequest request)
        {
            var ipAddress = GenerateIPAddress();
            var token = await _identityService.GetTokenAsync(request, ipAddress);

            return Ok(token);
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var result = await _identityService.ForgotPassword(request, Request.Headers["origin"], url);
            
            return Ok(result);
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _identityService.ResetPassword(request);
            return Ok(result);
        }

        // [HttpPost("refreshtoken")]
        // [AllowAnonymous]
        // public async Task<IActionResult> RefreshToken(RefreshToken request)
        // {
        //     var result = await _identityService.ResetPassword(request);
        //     return Ok(result);
        // }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}