using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class Exercise
    {
        public int? ExerciseId { get; set; }

        [Required]
        public string ExerciseName { get; set; }

        [Required]
        public int FocusId { get; set; }

        [Required]
        public string Description { get; set; }
        
        public int? Weight { get; set; }

        [Required]
        public int Time { get; set; }
        
        public int? Repetitions { get; set; }
        
        public int? Sets { get; set; }

        [Required]
        public int UserId { get; set; }

    }
}