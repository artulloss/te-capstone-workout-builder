using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IWorkoutHistoryDAO
    {
        WorkoutHistory GetWorkoutHistory(int id, DateTime dateTime);
        List<WorkoutHistory> GetWorkoutHistory(int id);
        WorkoutHistory AddWorkoutHistory(WorkoutHistory workoutHistory);
        WorkoutHistory EditWorkoutHistory(WorkoutHistory workoutHistory);
    }
}
