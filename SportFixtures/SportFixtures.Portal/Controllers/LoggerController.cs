using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    [Route("api/logger")]
    public class LoggerController : ControllerBase
    {
        private ILoggerBusinessLogic loggerBusinessLogic;

        public LoggerController(ILoggerBusinessLogic loggerBL)
        {
            this.loggerBusinessLogic = loggerBL;
        }

        [HttpGet]
        public ActionResult GetLogsBetweenDates([FromQuery]string from, [FromQuery]string to)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var files = loggerBusinessLogic.GetLogsBetweenDates(
                    DateTime.ParseExact(from, "ddMMyyyy", CultureInfo.InvariantCulture),
                    DateTime.ParseExact(to, "ddMMyyyy", CultureInfo.InvariantCulture));
                return Ok(files);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
