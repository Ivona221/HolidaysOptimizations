using HolidayOptimizations.Service.Processes;
using HolidayOptimizations.Web.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayOptimizations.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HolidaysController : ControllerBase
    {
        private IHolidaysProcess _process; 

        public HolidaysController(IHolidaysProcess process)
        {
            _process = process;
        }

        [SwaggerOperation(
            Summary = "Returns an object with the name of the country that has most public holidays globally",
            Description = "Returns an object with the name of the country that has most public holidays globally",
            OperationId = "GetCountryWithMostHolidays",
            Tags = new[] { "PublicHolidays" }
        )]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet("{year?}")]
        [Throttle(Name = "GetCountryWithMostHolidays", Milliseconds = 200)]
        public ActionResult GetCountryWithMostHolidays(long year = 0)
        {
            year = year != 0 ? year : DateTime.Now.Year;

            var response = _process.GetCountryWithMostHolidays(year);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var countryWithMostHolidays = response.Response;

                return Ok(countryWithMostHolidays);

            }
            else
            {
                return BadRequest(400);
            }
        }

        [HttpGet("{year?}")]
        [SwaggerOperation(
            Summary = "Returns an object with the name of the month that has most public holidays globally",
            Description = "Returns an object with the name of the month that has most public holidays globally",
            OperationId = "GetMostHolidaysByMonth",
            Tags = new[] { "PublicHolidays" }
        )]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [Throttle(Name = "GetMostHolidaysByMonth", Milliseconds = 200)]
        public ActionResult GetMostHolidaysByMonth(long year = 0)
        {
            year = year != 0 ? year : DateTime.Now.Year;

            var response = _process.GetMostHolidaysByMonth(year);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var monthWithMostHolidays = response.Response;

                return Ok(monthWithMostHolidays);

            }
            else
            {
                return BadRequest(400);
            }
        }

        [HttpGet("{year?}")]
        [SwaggerOperation(
            Summary = "Returns an object with the country that has the most unique public holidays",
            Description = "Returns an object with the country that has the most unique public holidays together with the unique holidays",
            OperationId = "GetCountryWithMostUniqueHolidays",
            Tags = new[] { "PublicHolidays", "UniqueHolidays" }
        )]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [Throttle(Name = "GetCountryWithMostUniqueHolidays", Milliseconds = 200)]
        public ActionResult GetCountryWithMostUniqueHolidays(long year = 0)
        {
            year = year != 0 ? year : DateTime.Now.Year;

            var response = _process.GetCountryWithMostUniqueHolidays(year);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var monthWithMostHolidays = response.Response;

                return Ok(monthWithMostHolidays);

            }
            else
            {
                return BadRequest(400);
            }
        }

        [HttpGet("{year?}")]
        [SwaggerOperation(
            Summary = "Longest lasting sequence of holidays around the world",
            Description = "What is the longest lasting sequence of holidays around the world you can find this year if you could travel at lightspeed between countries and timezones ? ",
            OperationId = "LightSpeedTravel",
            Tags = new[] { "LightSpeedTravel", "Holidays" }
        )]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [Throttle(Name = "LightSpeedTravel", Milliseconds = 200)]
        public ActionResult LightSpeedTravel(long year = 0)
        {
            year = year != 0 ? year : DateTime.Now.Year;

            var response = _process.LightSpeedTravel(year);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var longestSequence = response.Response;

                return Ok(longestSequence);

            }
            else
            {
                return BadRequest(400);
            }
        }
    }
}
