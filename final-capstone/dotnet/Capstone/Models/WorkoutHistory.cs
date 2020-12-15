using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class WorkoutHistory
    {
        public int UserId { get; set; }
        public int Time { get; set; }
        public DateTime Date { get; set; }

    }
}
