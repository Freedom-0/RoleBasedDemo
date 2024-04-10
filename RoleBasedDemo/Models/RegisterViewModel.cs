using System.ComponentModel.DataAnnotations;

namespace RoleBasedDemo.Models { 
    public class RegisterViewModel
    {

          [Required(ErrorMessage = "Username is required")]
          public string? Username { get; set; }
          
          [Required(ErrorMessage = "Email address is required")]
          [EmailAddress(ErrorMessage = "Invalid email address")]
          public string? Email { get; set; }
          
          [Required(ErrorMessage = "Password is required")]
          [MinLength(7, ErrorMessage = "Password must be at least 7 characters long")]
          public string? Password { get; set; }
          
          [Compare("Password", ErrorMessage = "Passwords do not match")]
          public string? ConfirmPassword { get; set; }

          [Required(ErrorMessage = "FullName is Required")]
          public string ? FullName { get; set; }
    }
}
