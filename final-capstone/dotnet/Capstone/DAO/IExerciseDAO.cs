using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IExerciseDAO
    {
        List<Exercise> GetAllExercises();
        Exercise GetExercise();
    }
}