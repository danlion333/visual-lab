using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualLabAPI.Models
{
    public class TaskInProgress
    {
        [Key]
        public int Id { get; set; } // Auto-incrementing primary key

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; } // Foreign key to User

        public User User { get; set; }

        [Required]
        [ForeignKey("Task")]
        public int TaskId { get; set; } // Foreign key to Task

        public Task Task { get; set; }

        [Required]
        public string Uml { get; set; } // JSON string representing the UML progress

        [Range(0, int.MaxValue, ErrorMessage = "Points must be a positive number.")]
        public int Points { get; set; } // Points awarded for the task
    }
}
