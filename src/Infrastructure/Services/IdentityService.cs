using EvrenDev.Application.DTOS.Auth;
using EvrenDev.Application.DTOS.Settings;
using EvrenDev.Application.Exceptions;
using EvrenDev.Application.Interfaces;
using EvrenDev.Application.Interfaces.Shared;
using EvrenDev.Application.SharedPreferences;
using EvrenDev.Domain.Entities;
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

namespace EvrenDev.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        private readonly IEmailSender _emailSender;

        public IdentityService(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<Result<TokenResponse>> GetTokenAsync(TokenRequest model, 
            string ipAddress)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
            var response = new TokenResponse();

            if(result.Succeeded) {
                var user = await _userManager.FindByEmailAsync(model.Email);

                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user, ipAddress);
                
                response.Id = user.Id;
                response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                response.IssuedOn = jwtSecurityToken.ValidFrom.ToLocalTime();
                response.Expired = jwtSecurityToken.ValidTo.ToLocalTime();
                response.Email = user.Email;

                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                response.Roles = rolesList.ToList();
                
                var refreshToken = GenerateRefreshToken(ipAddress);
                response.RefreshToken = refreshToken.Token;
                return Result<TokenResponse>.Success(response, "Başarılı bir şekilde giriş yapıldı.");
            } else {
                return Result<TokenResponse>.Fail("Eposta adresi veya parola hatalı!");
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
                new Claim("full_name", $"{user.FirstName} {user.LastName}"),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);
            
            return JWTGeneration(claims);
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

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
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

                const string subject = "Parolamı Sıfırla";
                const string title = "Parolamı Unutttum :(";
                const string message = "Bağlantıyı kullanarak parolanınızı sıfırlayabilirsiniz.";

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
                    PlainTextContent = $"Parola sıfırlama kodunuz - {code}",
                    Subject = subject,
                };
                
                try
                {
                    await _emailSender.SendEmailAsync(emailRequest);

                    return Result<TokenResponse>.Success(message: $"{model.Email} eposta adresine parolanızı sıfırlamanız için gerekli bilgiler gönderildi.");
                }
                catch (Exception ex)
                {
                    return Result<TokenResponse>.Fail($"Hata oluştu: ${ex.Message}");
                }

                #endregion
            }

            return Result<TokenResponse>.Fail($"{model.Email} eposta adresi ile kayıtlı bir kullanıcı sistemde bulunamadı.");
        }

        public async Task<Result<string>> ResetPassword(ResetPasswordRequest model)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);
            if (account == null) throw new ApiException($"{model.Email} eposta adresi ile kayıtlı bir hesap bulunamadı.");
            var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
            if (result.Succeeded)
            {
                return Result<string>.Success(model.Email, message: "Parola başarılı bir şekilde sıfırlandı.");
            }
            else
            {
                return Result<string>.Fail("Parola sıfırlanırken bir hata oluştu.");
            }
        }
    }
}