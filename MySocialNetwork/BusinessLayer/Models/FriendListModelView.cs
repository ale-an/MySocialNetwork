namespace BusinessLayer.Models;

public class FriendListModelView
{
    public List<FriendItem> Users { get; set; } = new();
}

public class FriendItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Photo { get; set; }
}