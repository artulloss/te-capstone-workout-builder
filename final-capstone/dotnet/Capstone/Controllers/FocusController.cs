using System.Collections.Generic;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    // [Authorize]
    [ApiController]
    public class FocusController : ControllerBase
    {
        private readonly IFocusDAO _focusDAO;

        public FocusController(IFocusDAO focusSqlDAO)
        {
            _focusDAO = focusSqlDAO;
        }

        [HttpGet]
        public List<Focus> GetFocuses()
        {
            return _focusDAO.GetFocuses();
        }

        [HttpGet("{id}")]
        public Focus GetFocusById(int id)
        {
            return _focusDAO.GetFocus(id);
        }
    }
}