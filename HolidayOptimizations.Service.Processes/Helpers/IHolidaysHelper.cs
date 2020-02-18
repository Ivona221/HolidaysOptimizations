using System;
using System.Collections.Generic;
using System.Text;
using HolidayOptimizations.Service.Entities.Features.Holidays;

namespace HolidayOptimizations.Service.Processes.Helpers
{
    public interface IHolidaysHelper
    {
        List<PublicHoliday> GetAllHolidays(long year);
    }
}
