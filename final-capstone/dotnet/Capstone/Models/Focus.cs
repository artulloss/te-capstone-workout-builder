using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class Focus
    {

        public int FocusId { get; set; }
        [Required]
        public string FocusName { get; set; }
    }
}