using EvrenDev.Application.DTOS.Auth;
using EvrenDev.Application.DTOS.Settings;
using EvrenDev.Application.Exceptions;
using EvrenDev.Application.Interfaces;
using EvrenDev.Application.Interfaces.Shared;
using EvrenDev.Application.SharedPreferences;
using EvrenDev.Application.Interfaces.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Localization;
using EvrenDev.Infrastructure.Identity.Model;

namespace EvrenDev.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IStringLocalizer<ApplicationUser> _loc;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        private readonly IEmailSender _emailSender;

        public IdentityService(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IStringLocalizer<ApplicationUser> loc,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            SignInManager<ApplicationUser> signInManager, 
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _loc = loc;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<Result<TokenResponse>> GetTokenAsync(TokenRequest model, 
            string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(email: model.Email);
            if(user == null) 
                return Result<TokenResponse>.Fail(string.Format(_loc["login_user_not_fount"], model.Email));

            var result = await _signInManager.PasswordSignInAsync( user: user, 
                password: model.Password, 
                isPersistent: false, 
                lockoutOnFailure: true
            );

            var response = new TokenResponse();

            if(result.Succeeded) {
                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user: user, 
                    ipAddress: ipAddress
                );
                
                response.Id = user.Id;
                response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                response.IssuedOn = jwtSecurityToken.ValidFrom.ToLocalTime();
                response.Expired = jwtSecurityToken.ValidTo.ToLocalTime();
                response.Email = user.Email;

                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                response.Roles = rolesList.ToList();
                var message = string.Empty;

                if(user.RefreshTokens != null && user.RefreshTokens.Any(token => token.IsActive)) {
                    var activeRefreshToken = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                    response.RefreshToken = activeRefreshToken.Token;
                    
                    message = string.Format(_loc["login_success"], model.Email);
                } else {
                    var refreshToken = GenerateRefreshToken(ipAddress: ipAddress);
                    response.RefreshToken = refreshToken.Token;

                    user.RefreshTokens.Add(refreshToken);
                    var updateResult = await _userManager.UpdateAsync(user);

                    if(updateResult.Succeeded) {
                        message = string.Format(_loc["login_success"], model.Email);
                    } else {
                        message = string.Format(_loc["login_update_info_failed"], model.Email);
                    }
                }
                
                return Result<TokenResponse>.Success(response, message);
            } else {
                return Result<TokenResponse>.Fail(_loc["login_failed"]);
            }
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user, 
            string ipAddress)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString()),
                new Claim("first_name", user.FirstName),
                new Claim("last_name", user.LastName),
                new Claim("full_name", user.FullName),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);
            
            return JWTGeneration(claims);
        }

        public async Task<Result<TokenResponse>> ForgotPassword(ForgotPasswordRequest model, 
            string origin,
            string url)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null) {
                #region Mailing
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var route = "account/reset-password/";
                var endPointUri = new Uri(string.Concat($"{url}/", route));
                var link = QueryHelpers.AddQueryString(endPointUri.ToString(), "code", code);

                var subject = _loc["forgot_password_subject"];
                var title = _loc["forgot_password_title"];
                var message = _loc["forgot_password_message"];;

                using var reader = new StreamReader("./MailTemplate/forgot-password.html");
                var htmlContent = reader.ReadToEnd();
                htmlContent = htmlContent.Replace("{{title}}", title);
                htmlContent = htmlContent.Replace("{{message}}", message);
                htmlContent = htmlContent.Replace("{{link}}", link);
                htmlContent = htmlContent.Replace("{{linkText}}", subject);
                htmlContent = htmlContent.Replace("{{year}}", DateTime.Now.Year.ToString());

                var emailRequest = new MailParameter()
                {
                    ToEmail = model.Email,
                    ToName = model.Email,
                    HtmlContent = htmlContent,
                    PlainTextContent = string.Format(_loc["forgot_password_plaintext"], code),
                    Subject = subject,
                };
                
                try
                {
                    await _emailSender.SendEmailAsync(emailRequest);

                    var responseMessage = string.Format(_loc["forgot_password_success_message"], model.Email);
                    return Result<TokenResponse>.Success(message: responseMessage);
                }
                catch (Exception ex)
                {
                    var responseMessage = string.Format(_loc["forgot_password_error_message"], ex.Message);

                    return Result<TokenResponse>.Fail(message: responseMessage);
                }

                #endregion
            }


            var userNotFoundMessage = string.Format(_loc["forgot_password_user_not_found"], model.Email);
            return Result<TokenResponse>.Fail(message: userNotFoundMessage);
        }

        public async Task<Result<string>> ResetPassword(ResetPasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) 
            {
                var message = string.Format(_loc["reset_password_user_not_found"], model.Email);
                    throw new ApiException(message: message);
            }
            
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            var responseMessage = string.Empty;

            if (result.Succeeded)
            {
                responseMessage = string.Format(_loc["reset_password_success", model.Email]);
            }
            else
            {
                responseMessage = string.Format(_loc["reset_password_fail", model.Email]);
            }

            return Result<string>.Fail(responseMessage);
        }

        public async Task<Result<string>> LogoutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, "EvrenDevPublicApi", "RefreshToken");
                await _signInManager.SignOutAsync();
                
                var message = string.Format(_loc["logout_success"], user.FullName);
                
                return Result<string>.Success(message);
            } else {
                return Result<string>.Fail(_loc["logout_faield"]);
            }
        }

        public async Task<Result<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request,
            string ipAddress)
        {
            if(request == null)
                return await Result<RefreshTokenResponse>.FailAsync(_loc["refresh_request_failed"]);

            var principal = GetPrincipalFromExpiredToken(request.Token);
            if(principal == null)
                return await Result<RefreshTokenResponse>.FailAsync(_loc["invalid_token_or_refresh_token"]);

            string email = principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(email: email);

            if (user == null || !user.HasValidRefreshToken(request.RefreshToken))
                return await Result<RefreshTokenResponse>.FailAsync(_loc["invalid_token_or_refresh_token"]);

            var refreshToken = user.GetRefreshToken(request.RefreshToken);

            if (!refreshToken.IsActive)
                return await Result<RefreshTokenResponse>.FailAsync(_loc["refresh_token_is_not_active"]); 


            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user: user, 
                ipAddress: ipAddress
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var newRefreshToken = GenerateRefreshToken(ipAddress: ipAddress);

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;

            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var response = new RefreshTokenResponse() {
                Token = token,
                RefreshToken = newRefreshToken.Token
            };

            var message = _loc["refresh_token_success"];
            return await Result<RefreshTokenResponse>.SuccessAsync(response, message);
        }

        public async Task<Result<string>> RevokeTokenAsync(RevokeTokenRequest request, 
            string ipAddress)
        {
            if(request == null)
                return await Result<string>.FailAsync(_loc["revoke_request_failed"]);

            var principal = GetPrincipalFromExpiredToken(request.Token);
            if(principal == null)
                return await Result<string>.FailAsync(_loc["invalid_token"]);

            string email = principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(email: email);

            if (user == null || !user.HasValidRefreshToken(request.RefreshToken))
                return await Result<string>.FailAsync(_loc["invalid_token_or_refresh_token"]);

            var refreshToken = user.GetRefreshToken(request.RefreshToken);

            if (!refreshToken.IsActive)
                return await Result<string>.FailAsync(_loc["refresh_token_is_not_active"]); 

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = null;
            await _userManager.UpdateAsync(user);

            return await Result<string>.SuccessAsync(_loc["revoke_token_success"]);
        }

        private JwtSecurityToken JWTGeneration(IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private IdentityRefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new IdentityRefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress,
            };
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

    }
}