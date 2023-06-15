using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Infrastructure.UnitOfWorks;
using RazorPageWebApp.Models.AccountModels;
using Domain.Entities;
using Application.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RazorPageWebApp.Pages.Auth;

public class RegisterModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public string? Message { get; set; }
    public bool? IsValid { get; set; } = false;
    [BindProperty]
    public RegisterAccount AccountObj { get; set; }

    public async Task OnPost()
    {
        if (ModelState.IsValid)
        {            
            ModelState.Clear();

            // logic code to register
            try
            {                
                var account = new Account
                {
                    Username = AccountObj.Username,
                    Password = AccountObj.Password.Hash(),
                    Email = AccountObj.Email,
                    FirstName = AccountObj.FirstName,
                    LastName = AccountObj.LastName
                };
                await _unitOfWork.AccountRepository.InsertAccountAsync(account);                
                Message = "Registered in successfully! Please log in.";
                IsValid = true;
            } catch (Exception ex)
            {
                Message = ex.Message;
            }
        } else
        {
            Message = "Registered failed!";
        }
    }
}
