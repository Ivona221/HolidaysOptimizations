using HolidayOptimizations.Service.Entities.Features.Holidays;
using System;
using System.Collections.Generic;
using System.Text;

namespace HolidayOptimizations.StorageRepository.DataRepositoryInterface.Features.Holidays
{
    public interface ITimezonesRepository
    {
        List<Timezone> GetAllTimezones();

        List<Timezone> GetAllTimezonesByCountry(string countryCode);

        void InsertTimezonesAsync(List<Timezone> timezones);
    }
}
