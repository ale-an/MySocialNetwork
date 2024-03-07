using AutoMapper;
using BusinessLayer.Models;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class LoginController : Controller
{
    private readonly IMapper mapper;
    private readonly SignInManager<User> signInManager;

    public LoginController(IMapper mapper, SignInManager<User> signInManager)
    {
        this.mapper = mapper;
        this.signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("MainPage", "Profile");
            }

            ModelState.AddModelError("Email", "Неправильный логин и (или) пароль");
        }

        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}