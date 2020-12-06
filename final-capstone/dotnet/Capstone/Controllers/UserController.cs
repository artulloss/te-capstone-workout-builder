using System.Collections.Generic;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDAO _userDao;
        
        public UserController(IUserDAO userDao) {
            _userDao = userDao;
        }
        
        [HttpGet] // TODO Remove or add auth
        public List<User> GetUsers() {
            return _userDao.GetUsers();
        }

        [HttpGet("{username}")] // TODO Remove or add auth
        public User GetUserByUsername(string username) {
            return _userDao.GetUser(username);
        }

        [HttpGet("{username}/exercise")]
        public List<Exercise> GetUserExercises(string username) {
            return _userDao.GetUserExercises(username);
        }
        
        [HttpGet("{username}/trainer/exercise")]
        public List<Exercise> GetTrainerExercises(string username) { 
            return _userDao.GetTrainerExercises(username);
        }
    }
}