using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageWebApp.Models.AccountModels;

namespace RazorPageWebApp.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IHttpContextAccessor _context;
        private IUnitOfWork _unitOfWork;
        public string Message { get; set; }
        [BindProperty]
        public LoginAccount AccountObj { get; set; }

        public LoginModel(IUnitOfWork unitOfWork, IHttpContextAccessor context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var session = _context.HttpContext!.Session;
                ModelState.Clear();
                //logic code to login here
                var username = AccountObj.Username;
                var password = AccountObj.Password;
                if ( await _unitOfWork.AccountRepository.LoginAsync(username, password))
                {
                    var account = await _unitOfWork.AccountRepository.GetAccountAsync(username);
                    session.SetString("UserId", account!.Id.ToString());
                    //return new RedirectToPageResult("./Admin/Index");
                    Message = "Login successfully!";
                    return Page();
                }
                Message = "Wrong username/password";
            }
            else
            {
                Message = "Login failed!";
            }
            return Page();
        }
    }
}
