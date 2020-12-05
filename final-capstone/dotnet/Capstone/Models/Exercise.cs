namespace Capstone.Models
{
    public class Exercise
    {
        public int ExerciseId { get; set; }
        
        public string ExerciseName { get; set; }
        
        public int FocusId { get; set; }
        
        public string Description { get; set; }
        
        public int? Weight { get; set; }
        
        public int Time { get; set; }
        
        public int? Repetitions { get; set; }
        
        public int? Sets { get; set; }

        public int UserId { get; set; }

    }
}