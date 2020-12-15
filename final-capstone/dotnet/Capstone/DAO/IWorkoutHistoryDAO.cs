using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IWorkoutHistoryDAO
    {
        WorkoutHistory GetWorkoutHistory(int id, DateTime dateTime);
        WorkoutHistory AddWorkoutHistory(WorkoutHistory workoutHistory);
        WorkoutHistory EditWorkoutHistory(WorkoutHistory workoutHistory);
    }
}
