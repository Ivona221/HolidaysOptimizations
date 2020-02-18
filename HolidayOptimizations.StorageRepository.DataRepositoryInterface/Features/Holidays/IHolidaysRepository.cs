using HolidayOptimizations.Service.Entities.Features.Holidays;
using System;
using System.Collections.Generic;
using System.Text;

namespace HolidayOptimizations.StorageRepository.DataRepositoryInterface.Features.Holidays
{
    public interface IHolidaysRepository
    {
        List<PublicHoliday> GetPulbicHolidaysByYear(long? year);

        void InsertHolidaysAsync(List<PublicHoliday> holidays);

        void InsertHolidays(List<PublicHoliday> holidays);

        void DeleteHolidaysByYear(long year);
    }
}
