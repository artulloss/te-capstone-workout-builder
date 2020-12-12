using System.Collections.Generic;
using System.Linq;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    // [Authorize]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseDAO _exerciseDao;
        
        public ExerciseController(IExerciseDAO exerciseDao)
        {
            _exerciseDao = exerciseDao;
        }
        
        [HttpGet]
        public List<Exercise> GetExercises(int? focusId = null, int? time = null)
        {
            return _exerciseDao.GetExercises().Where(e => // Arrow function using Linq
            {
                // If default (focusId == null) then it will be true to match, but if something is provided it will
                // compare the values
                bool focusIdMatches = focusId == null || focusId == e.FocusId;
                bool timeMatches = time == null || time == e.Time;
                return focusIdMatches && timeMatches;
            }).ToList();
        }

        [HttpGet("{id}")]
        public Exercise GetExerciseById(int id) {
            return _exerciseDao.GetExercise(id);
        }

        [HttpPost]
        public ActionResult<Exercise> AddExercise(Exercise newExercise)
        {
            newExercise = _exerciseDao.AddExercise(newExercise);
            return newExercise != null ? (ActionResult) Created($"/exercise/{newExercise.ExerciseId}", newExercise) : BadRequest();
        }

        [HttpPut]
        public ActionResult<Exercise> EditExercise(Exercise exercise)
        {
            Exercise editedExercise = _exerciseDao.EditExercise(exercise);
            return editedExercise != null ? (ActionResult) Ok(editedExercise) : BadRequest() ;
        }

        [HttpDelete ("{id}")]
        public ActionResult DeleteExercise(int exerciseId)
        {
            if (_exerciseDao.GetExercise(exerciseId) == null)
            {
                return BadRequest("Exercise not found.");
            }
            if (_exerciseDao.DeleteExercise(exerciseId))
            {
                return NoContent();
            }
            return StatusCode(500);
        }

    }
}