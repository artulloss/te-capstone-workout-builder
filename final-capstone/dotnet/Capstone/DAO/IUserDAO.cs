using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUserDAO
    {
        List<User> GetUsers();
        User GetUser(string username);
        User AddUser(string username, string password, string role);
        List<Exercise> GetUserExercises(string username);
        List<Exercise> GetTrainerExercises(string username);
    }
}
