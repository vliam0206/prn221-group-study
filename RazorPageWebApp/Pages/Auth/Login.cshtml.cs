using Application.Utils;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageWebApp.Models.AccountModels;

namespace RazorPageWebApp.Pages.Auth;

public class LoginModel : PageModel
{
    private readonly IHttpContextAccessor _httpContext;
    private IUnitOfWork _unitOfWork;
    public string Message { get; set; }
    [BindProperty]
    public LoginAccount AccountObj { get; set; }

    public LoginModel(IUnitOfWork unitOfWork, IHttpContextAccessor context)
    {
        _unitOfWork = unitOfWork;
        _httpContext = context;
    }
    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPost()
    {
        try
        {
            if (ModelState.IsValid)
            {
                var session = _httpContext.HttpContext!.Session;
                ModelState.Clear();

                //logic code to login here
                var account = await _unitOfWork.AccountRepository.GetAccountAsync(AccountObj.Username);

                if (account == null || !AccountObj.Password.Verify(account.Password))
                {
                    Message = "Wrong username/password";
                    return Page();
                }

                // Add current user to session
                session.SetString("UserId", account!.Id.ToString());
                session.SetString("UserName", account!.Username);

                // logged in successful, redicrect to Homepage
                Message = "Login successfully!";
                return new RedirectToPageResult("/UserScreen/HomePage");
            }
        } catch (Exception ex)
        {
            Message = ex.Message;                
        }
        return Page();
    }
}
