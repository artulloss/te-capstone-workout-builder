using System.Collections.Generic;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseDAO _exerciseDao;
        
        public ExerciseController(IExerciseDAO exerciseDao) {
            _exerciseDao = exerciseDao;
        }
        
        [HttpGet]
        public List<Exercise> GetAllExercises() {
            return _exerciseDao.GetAllExercises();
        }
    }
}