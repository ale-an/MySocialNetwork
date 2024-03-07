namespace BusinessLayer.Models;

public class MainPageViewModel
{
    public LoginViewModel LoginViewModel { get; set; } = new();
    public RegisterViewModel RegisterViewModel { get; set; } = new();
}