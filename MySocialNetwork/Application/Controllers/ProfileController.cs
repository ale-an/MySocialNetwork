using AutoMapper;
using BusinessLayer.Infrastructure.Extensions;
using BusinessLayer.Models;
using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class ProfileController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;
    private readonly FriendsRepository friendsRepository;

    public ProfileController(UserManager<User> userManager, IMapper mapper, FriendsRepository friendsRepository)
    {
        this.userManager = userManager;
        this.mapper = mapper;
        this.friendsRepository = friendsRepository;
    }

    public async Task<IActionResult> MainPage()
    {
        var result = await userManager.GetUserAsync(User);

        var model = mapper.Map<ProfileViewModel>(result);

        var friends = friendsRepository.GetFriendsByUser(result);

        model.Friends = friends.Where(x => x != null).Select(x => new FriendItem
        {
            Id = x.Id,
            Name = $"{x.FirstName} {x.MiddleName} {x.LastName}",
            Photo = x.Photo
        }).ToList();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var result = await userManager.GetUserAsync(User);

        var model = mapper.Map<UserEditViewModel>(result);

        return RedirectToAction("EditPage", model);
    }

    public IActionResult EditPage(UserEditViewModel model)
    {
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UserEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                ModelState.AddModelError("", "Ошибка, не нашли пользователя");
                return View("EditPage", model);
            }

            user.Convert(model);

            var result = await userManager.UpdateAsync(user);

            return result.Succeeded ? RedirectToAction("MainPage") : RedirectToAction("EditPage", model);
        }

        ModelState.AddModelError("", "Некорректные данные");
        return View("EditPage", model);
    }
}