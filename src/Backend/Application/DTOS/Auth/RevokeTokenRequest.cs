namespace EvrenDev.Application.DTOS.Auth
{
    public class RevokeTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}