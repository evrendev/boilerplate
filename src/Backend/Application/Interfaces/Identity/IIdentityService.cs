using System.Threading.Tasks;
using EvrenDev.Application.DTOS.Auth;
using EvrenDev.Application.Interfaces.Result;

namespace EvrenDev.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);

        Task<Result<TokenResponse>> ForgotPassword(ForgotPasswordRequest model, string origin, string url);

        Task<Result<string>> ResetPassword(ResetPasswordRequest model);
    }
}