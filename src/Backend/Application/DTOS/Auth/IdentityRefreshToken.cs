using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvrenDev.Application.DTOS.Auth
{
    public class IdentityRefreshToken
    {
        public IdentityRefreshToken()
        {
            
        }

        public IdentityRefreshToken(Guid id,
            string token,
            DateTime expires,
            DateTime created,
            string createdByIp,
            DateTime? revoked,
            string revokedByIp,
            string replacedByToken)
        {
            Id = id;
            Token = token;
            Expires = expires;
            Created = created;
            CreatedByIp = createdByIp;
            Revoked = revoked;
            RevokedByIp = revokedByIp;
            ReplacedByToken = replacedByToken;
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; } = null;
        public string RevokedByIp { get; set; } = string.Empty;
        public string ReplacedByToken { get; set; } = string.Empty;
        public bool IsActive => Revoked == null && !IsExpired;

    }
}