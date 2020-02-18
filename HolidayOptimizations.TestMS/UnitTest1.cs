using HolidayOptimizations.Service.Entities.Features.Holidays;
using HolidayOptimizations.Service.Entities.Reponse;
using HolidayOptimizations.Service.Processes;
using HolidayOptimizations.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;

namespace HolidayOptimizations.TestMS
{
    [TestClass]
    public class HolidayControllerTest
    {
        private Mock<IHolidaysProcess> holidaysProcess;
        private HolidaysController holidaysController;

        [TestInitialize]
        public void TestInitialize()
        {
            holidaysProcess = new Mock<IHolidaysProcess>();
            holidaysController = new HolidaysController(holidaysProcess.Object);
            Setup();
        }

        [TestMethod]
        public void HolidayController_GetCountryWithMostHolidays_Should_Return_Ok_Result()
        {
            var actionResult = holidaysController.GetCountryWithMostHolidays(2020);
            var okResult = actionResult as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public void HolidayController_GetMostHolidaysByMonth_Should_Return_Ok_Result()
        {
            var actionResult = holidaysController.GetMostHolidaysByMonth(2020);
            var okResult = actionResult as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public void HolidayController_GetCountryWithMostUniqueHolidays_Should_Return_Ok_Result()
        {
            var actionResult = holidaysController.GetCountryWithMostUniqueHolidays(2020);
            var okResult = actionResult as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public void HolidayController_LightSpeedTravel_Should_Return_Ok_Result()
        {
            var actionResult = holidaysController.LightSpeedTravel(2020);
            var okResult = actionResult as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        private void Setup()
        {
            holidaysProcess.Setup(x => x.GetCountryWithMostHolidays(2020)).Returns(new BaseReponse<string> { Response = "Venezuela", StatusCode = HttpStatusCode.OK, Exception = null });
            holidaysProcess.Setup(x => x.GetMostHolidaysByMonth(2020)).Returns(new BaseReponse<string> { Response = "May", StatusCode = HttpStatusCode.OK, Exception = null });
            holidaysProcess.Setup(x => x.GetCountryWithMostUniqueHolidays(2020)).Returns(new BaseReponse<string> { Response = "Venezuela", StatusCode = HttpStatusCode.OK, Exception = null });
            holidaysProcess.Setup(x => x.LightSpeedTravel(2020)).Returns(new BaseReponse<LightspeedTravelResponse>
            {
                Response = new LightspeedTravelResponse
                {
                    LongestSequence = 200,
                    Holidays = new List<PublicHoliday>
                {
                    new PublicHoliday
                    {
                        Name = "New Year's Day",
                        LocalName = "Any nou",
                        CountryCode = "AD",
                        Fixed = true,
                        Global = true,
                        Countries = null,
                        LaunchYear = null,
                        ModifiedAt = DateTime.Now,
                        CreatedAt = DateTime.Now,
                        EndDate = DateTime.Now
                    }
                }

                },
                StatusCode = HttpStatusCode.OK,
                Exception = null
            });
        }
    }
}
