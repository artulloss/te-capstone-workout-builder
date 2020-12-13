using System;
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
        private readonly IUserExerciseDAO _userExerciseDao;

        public UserController(IUserDAO userDao, IUserExerciseDAO userExerciseDao) {
            _userDao = userDao;
            _userExerciseDao = userExerciseDao;
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

        [HttpPost("{username}/exercise")]
        public ActionResult<Exercise> AddExercises(string username, List<Exercise> exercises)
        {
            List<Exercise> successfullyAdded = new List<Exercise>(); 

            foreach(Exercise exercise in exercises)
            {
                if (_userExerciseDao.AddUserExercise(exercise, username) != null) 
                {
                    successfullyAdded.Add(exercise);
                }         
                
            }
            Console.WriteLine(exercises);
            return successfullyAdded.Count > 0 ? (ActionResult) Created($"{username}/exercise", successfullyAdded) : BadRequest();
        }

        [HttpDelete("{username}/exercise")]
        public ActionResult DeleteAllUserExercises(string username)
        {
            _userExerciseDao.DeleteAllUserExercises(username);
            return NoContent();

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