using EvrenDev.Application.DTOS.Auth;
using EvrenDev.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
        public async Task<IActionResult> GetToken(TokenRequest request)
        {
            var ipAddress = GenerateIPAddress();
            var response = await _identityService.GetTokenAsync(request, ipAddress);

            if(response.Error)
                return Unauthorized(response);

            return Ok(response);
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
            var response = await _identityService.ResetPassword(request);

            if(response.Error)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var ipAddress = GenerateIPAddress();
            var response = await _identityService.RefreshTokenAsync(request, ipAddress);

            if(response.Error)
                return Unauthorized(response);

            return Ok(response);
        }

        [HttpPost("revoke")]
        [AllowAnonymous]
        public async Task<IActionResult> RevokeTokenAsync(RevokeTokenRequest request)
        {
            var ipAddress = GenerateIPAddress();
            var response = await _identityService.RevokeTokenAsync(request, ipAddress);

            if(response.Error)
                return Unauthorized(response);

            return Ok(response);
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}