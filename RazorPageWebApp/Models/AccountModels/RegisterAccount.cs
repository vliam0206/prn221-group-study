using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RazorPageWebApp.Models.AccountModels;

public class RegisterAccount
{
    [Required(ErrorMessage = "Username is required!")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required!")]
    [PasswordPropertyText]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm password is required!")]
    [Compare("Password", ErrorMessage = "Confirm password does not match the password.")]
    [PasswordPropertyText]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Email is required!")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "First name is required!")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required!")]
    public string LastName { get; set; }
}
