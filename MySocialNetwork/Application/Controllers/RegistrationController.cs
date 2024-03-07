using AutoMapper;
using BusinessLayer.Models;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class RegistrationController : Controller
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public RegistrationController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public IActionResult RegistrationPage(RegisterViewModel model)
    {
        return View(model);
    }

    [HttpPost]
    public IActionResult FirstRegistration(RegisterViewModel model)
    {
        return RedirectToAction("RegistrationPage", model);
    }

    [HttpPost]
    public async Task<IActionResult> Registration(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("RegistrationPage", model);
        
        var user = mapper.Map<User>(model);

        var result = await userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, false);
            return RedirectToAction("MainPage", "Profile");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return RedirectToAction("RegistrationPage", model);
    }
}