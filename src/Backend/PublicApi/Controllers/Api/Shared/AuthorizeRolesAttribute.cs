using Microsoft.AspNetCore.Authorization;

namespace EvrenDev.PublicApi.Controllers.Api.Shared {
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}