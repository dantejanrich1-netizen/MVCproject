using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JanrichDanteMVC_Project.Models
{
    public class Admin
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Full Name is required")] public string FullName { get; set; }
        [Required(ErrorMessage = "Username is required")] public string Username { get; set; }
        [Required(ErrorMessage = "Email is required"), EmailAddress] public string Email { get; set; }
        [Required, MinLength(6, ErrorMessage = "Min 6 chars")] public string Password { get; set; }
        [NotMapped, Required, Compare("Password", ErrorMessage = "Passwords don't match")] public string ConfirmPassword { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        [Required] public string StudentNumber { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required] public string Course { get; set; }
        [Required, Range(1, 5)] public int YearLevel { get; set; }
    }
}