using ActivitySignUp.Domain;
using ActivitySignUp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySignUp.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SubscriptionsController : Controller
    {
        private readonly IActivitySignUpRepository _repository;
        private readonly ILogger<SubscriptionsController> _logger;

        public SubscriptionsController(IActivitySignUpRepository repository, ILogger<SubscriptionsController> logger)
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
                return Ok(_repository.GetSubscriptions());
            }
            catch (Exception ex)
            {

                _logger.LogError($"Unable to get the activities: {ex}");
                return BadRequest("Unable to get the activities");
            }
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var subscriptions = _repository.GetSubscriptionsByActivityId(id);

                if (subscriptions != null)
                {
                    return Ok(subscriptions);
                } 
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to get the activities: {ex}");
                return BadRequest("Unable to get the activities");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Subscription model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.AddEntity(model);

                    if (_repository.SaveAll())
                    {
                        return Created($"/api/subscriptions/{model.SubscriptionId}", model);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Unable to subscribe! {ex}");
                }
            }
            return BadRequest("Unable to subscribe!");
        }
    }
}
