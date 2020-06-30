using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoggingExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _logger.LogInformation("Im here!");
           
            using (_logger.BeginScope(("correlation_id", Guid.NewGuid().ToString())))
            {
                _logger.LogInformation("Entering values/Get");

                _logger.LogInformation("Log #2");
                _logger.LogInformation("Large json is comming ahead");

                _logger.LogInformation(
              "{'host': 'example.org','short_message': 'A short message that helps you identify what is going on',  'full_message': 'Backtrace here more stuff', '_user_id': 9001,  '_some_info': 'foo',  '_some_env_var': 'bar'}"
              );

                _logger.LogInformation("Large json has been and gone");

                _logger.LogInformation("Exit values/Get");
            }
            _logger.LogDebug("Lonely log here");

            return Ok();
        }
    }
}
