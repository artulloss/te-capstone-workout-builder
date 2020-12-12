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
    public class TrainerController : ControllerBase
    {
        private readonly IUserDAO _userDao;

        public TrainerController(IUserDAO userDao)
        {
            _userDao = userDao;
        }

        [HttpGet]
        public List<string> GetTrainers()
        {
            IEnumerable<User> trainers = _userDao.GetUsers().Where(u => u.Role == "admin");
            List<string> trainerNames = new List<string>();

            foreach (User trainer in trainers)
            {
                trainerNames.Add(trainer.Username);
            }
            return trainerNames;
        }
    }
}