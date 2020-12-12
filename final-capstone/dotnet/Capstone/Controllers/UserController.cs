using System.Collections.Generic;
using System.Linq;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public User GetUserByToken() {
            return _userDao.GetUser(User.Identity.Name);
        }

        [HttpGet("{username}/exercise")]
        public List<Exercise> GetUserExercises(string username) {
            return _userDao.GetUserExercises(username);
        }

        [HttpGet("{username}/trainer/exercise")]
        public List<Exercise> GetTrainerExercises(string username, int? focusId = null, int? time = null)
        {
            return _userDao.GetTrainerExercises(username).Where(e => // Arrow function using Linq
            {
                // If default (focusId == null) then it will be true to match, but if something is provided it will
                // compare the values
                bool focusIdMatches = focusId == null || focusId == e.FocusId;
                bool timeMatches = time == null || time == e.Time;
                return focusIdMatches && timeMatches;
            }).ToList();
        }
    }
}