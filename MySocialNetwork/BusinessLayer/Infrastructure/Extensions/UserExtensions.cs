using BusinessLayer.Models;
using DataLayer.Entities;

namespace BusinessLayer.Infrastructure.Extensions;

public static class UserExtensions
{
    public static User Convert(this User user, UserEditViewModel model)
    {
        user.Photo = model.Photo;
        user.LastName = model.LastName;
        user.MiddleName = model.MiddleName;
        user.FirstName = model.FirstName;
        user.Email = model.Email;
        user.BirthDate = model.BirthDate;
        user.UserName = model.Email;
        user.Status = model.Status;
        user.AboutMe = model.AboutMe;

        return user;
    }
}