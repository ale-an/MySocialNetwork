namespace BusinessLayer.Models;

public class UserEditViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string? Photo { get; set; }
    public string? Status { get; set; }
    public string? AboutMe { get; set; }
}