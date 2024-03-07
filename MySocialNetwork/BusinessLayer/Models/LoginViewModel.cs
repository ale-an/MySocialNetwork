using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models;

public class LoginViewModel
{
    [Required]
    public string Email { get; set; }
    public string Password { get; set; }
}