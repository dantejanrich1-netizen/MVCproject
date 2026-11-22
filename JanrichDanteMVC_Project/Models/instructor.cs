using System.ComponentModel.DataAnnotations;

namespace JanrichDanteMVC_Project.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Department { get; set; }
    }
}