using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EvrenDev.Application.DTOS.Auth
{
    public class TokenResponse
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; }

        public string Token { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime Expired { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}