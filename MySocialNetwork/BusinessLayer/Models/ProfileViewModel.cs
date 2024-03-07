namespace BusinessLayer.Models;

public class ProfileViewModel
{
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Status { get; set; }
    public string AboutMe { get; set; }
    public string Email { get; set; }
    public string Photo { get; set; }
    public List<FriendItem> Friends { get; set; } = new();
}