using BusinessLayer.Models;
using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Application.Controllers;

public class FriendsController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly FriendsRepository friendsRepository;

    public FriendsController(UserManager<User> userManager, FriendsRepository friendsRepository)
    {
        this.userManager = userManager;
        this.friendsRepository = friendsRepository;
    }

    [HttpGet]
    public IActionResult UsersList()
    {
        return View("Search", new FriendListModelView());
    }


    [HttpPost]
    public IActionResult Search(string search)
    {
        List<FriendItem> users;
        if (search.IsNullOrEmpty())
        {
            users = userManager.Users.AsEnumerable().Select(x => new FriendItem
            {
                Id = x.Id,
                Name = $"{x.FirstName} {x.MiddleName} {x.LastName}"
            }).ToList();
        }
        else
        {
            users = userManager.Users.AsEnumerable().Select(x => new FriendItem
                {
                    Id = x.Id,
                    Name = $"{x.FirstName} {x.MiddleName} {x.LastName}",

                })
                .Where(x => x.Name.Contains(search, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        return View(new FriendListModelView
        {
            Users = users
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddFriend(string friendId)
    {
        var currentUser = await userManager.GetUserAsync(User);

        var friendUser = await userManager.FindByIdAsync(friendId);

        if (friendUser == null || currentUser == null)
            return BadRequest();
        
        friendsRepository.AddFriend(currentUser, friendUser);

        return RedirectToAction("Search");
    }
    
    [Route("DeleteFriend")]
    [HttpPost]
    public async Task < IActionResult > DeleteFriend(string id) 
    {
        var currentuser = User;

        var result = await userManager.GetUserAsync(currentuser);

        var friend = await userManager.FindByIdAsync(id);

        friendsRepository.DeleteFriend(result, friend);

        return RedirectToAction("MainPage", "Profile");

    }
}