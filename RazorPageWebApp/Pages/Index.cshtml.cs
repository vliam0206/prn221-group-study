using Application.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorPageWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IClaimService _claimService;
        private readonly IHttpContextAccessor _httpContext;

        public string? Message { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IClaimService claimService,
                            IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _claimService = claimService;
            _httpContext = httpContext;
        }

        public void OnGet()
        {

        }

        public IActionResult OnGetCurrentId()
        {
            Message = $"Current UserId: {_claimService.GetCurrentUserId}";
            return Page();
        }
        public IActionResult OnGetCurrentUsername()
        {
            Message = $"Current UserName: {_claimService.GetCurrentUserName}";
            return Page();
        }
        public IActionResult OnGetLogOut()
        {
            _httpContext.HttpContext!.Session.Clear();
            Message = "Logged out successfully!";
            return Page();
        }
    }
}