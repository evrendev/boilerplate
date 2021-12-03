using System.ComponentModel.DataAnnotations;

namespace EvrenDev.Application.DTOS.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}