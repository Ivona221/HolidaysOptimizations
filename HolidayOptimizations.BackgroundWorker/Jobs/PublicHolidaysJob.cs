using FluentScheduler;
using HolidayOptimizations.Common.Helpers.Api;
using HolidayOptimizations.Service.Controllers.Enums;
using HolidayOptimizations.Service.Entities.Features.Holidays;
using HolidayOptimizations.StorageRepository.DataRepositoryInterface.Features.Holidays;
using System;
using System.Collections.Generic;
using System.Text;

namespace HolidayOptimizations.BackgroundWorker.Jobs
{
    public class PublicHolidaysJob : IJob
    {

        private IHolidaysRepository _repository;

        public PublicHolidaysJob(IHolidaysRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            var year = DateTime.Now.Year;

            _repository.DeleteHolidaysByYear(year);
            foreach (var enumValue in Enum.GetValues(typeof(CountryCodesEnum)))
            {
                var holidays = HolidaysApiWrapper<List<PublicHoliday>>.GetPublicHolidays(year, enumValue.ToString()).Result;
                holidays.ForEach(x => x.EndDate = x.Date.AddHours(24));
                _repository.InsertHolidays(holidays);
            }
                
        }
    }
}
