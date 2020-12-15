using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    // [Authorize]
    [ApiController]
    public class WorkoutHistoryController : ControllerBase
    {
        private readonly IWorkoutHistoryDAO _workoutHistoryDao;
       

        public WorkoutHistoryController(IWorkoutHistoryDAO workoutHistoryDao)
        {
            _workoutHistoryDao = workoutHistoryDao;
        }

        [HttpGet("{id}/{date}")]
        public ActionResult<WorkoutHistory> GetWorkoutHistory(int id, string date)
        {
            DateTime dateTime = DateTime.Parse(date);
            WorkoutHistory workoutHistory = _workoutHistoryDao.GetWorkoutHistory(id, dateTime);
            return workoutHistory != null ? (ActionResult)Ok(workoutHistory) : NotFound();
        }

        [HttpPost]
        public ActionResult<WorkoutHistory> AddWorkoutHistory(WorkoutHistory workoutHistory)
        {
            var previousWorkoutHistory = _workoutHistoryDao.GetWorkoutHistory(workoutHistory.UserId, workoutHistory.Date);
            if (previousWorkoutHistory != null)
            {
                workoutHistory.Time += previousWorkoutHistory.Time;
                return Created($"workoutHistory/{workoutHistory.UserId}", _workoutHistoryDao.EditWorkoutHistory(workoutHistory));
            }

            WorkoutHistory addedWorkoutHistory = _workoutHistoryDao.AddWorkoutHistory(workoutHistory);
            return addedWorkoutHistory != null ? (ActionResult)Created($"workoutHistory/{workoutHistory.UserId}", addedWorkoutHistory) : BadRequest();
            
        }
                
    }
}
