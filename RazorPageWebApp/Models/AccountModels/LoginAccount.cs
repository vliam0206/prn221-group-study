using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RazorPageWebApp.Models.AccountModels;

public class LoginAccount
{
    [Required(ErrorMessage = "Username is required!")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required!")]
    [PasswordPropertyText]
    public string Password { get; set; }
}
