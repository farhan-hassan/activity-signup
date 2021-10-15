using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivitySignUp.Infrastructure;
using ActivitySignUp.Domain;
using ActivitySignUp.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ActivitySignUp.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitySignUpRepository _repository;
        private readonly ILogger<ActivitiesController> _logger;

        public ActivitiesController(IActivitySignUpRepository repository, ILogger<ActivitiesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllActivities());
            }
            catch (Exception ex)
            {

                _logger.LogError($"Unable to get the activities: {ex}");
                return BadRequest("Unable to get the activities");
            }
        }
    }
}
