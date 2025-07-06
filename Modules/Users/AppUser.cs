using Microsoft.AspNetCore.Identity;

namespace SchoolManagementSystem.Modules.Users.Entities;

public class AppUser : IdentityUser<int>
{
    public string Role { get; set; } = "Student"; // Default role
}
