using Application.IServices;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RazorPageWebApp.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentUserId
        {
            get
            {
                var Id = _contextAccessor.HttpContext?.Session?.GetString(AppConstants.USER_ID);
                return string.IsNullOrEmpty(Id) ? Guid.Empty : Guid.Parse(Id);
            }
        }

        public string GetCurrentUserName
        {
            get
            {
                var username = _contextAccessor.HttpContext?.Session?.GetString(AppConstants.USER_NAME);
                return string.IsNullOrEmpty(username) ? string.Empty : username;
            }
        }
    }
}
