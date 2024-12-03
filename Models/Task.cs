using System.ComponentModel.DataAnnotations;

namespace VirtualLabAPI.Models
{
    public class Task
    {
        public int Id { get; set; } // Auto-incrementing primary key

        [Required]
        public string Title { get; set; } // The title of the task

        public string Description { get; set; } // Optional description of the task

        [Range(0, int.MaxValue, ErrorMessage = "MaxPoints must be a positive number.")]
        public int MaxPoints { get; set; } // Maximum points for the task

        [Required]
        public string Uml { get; set; } // JSON representation of UML diagrams
    }
}
