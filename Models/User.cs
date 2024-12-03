using System.ComponentModel.DataAnnotations;

namespace VirtualLabAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [RegularExpression("Student|Teacher", ErrorMessage = "Role must be either 'Student' or 'Teacher'.")]
        public string Role { get; set; }
    }
}

