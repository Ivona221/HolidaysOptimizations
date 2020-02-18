using HolidayOptimizations.Service.Entities;
using HolidayOptimizations.Service.Entities.Reponse;
using System;

namespace HolidayOptimizations.Service.Processes
{
    public interface IHolidaysProcess
    {
        BaseReponse<string> GetCountryWithMostHolidays(long year);

        BaseReponse<string> GetMostHolidaysByMonth(long year);

        BaseReponse<string> GetCountryWithMostUniqueHolidays(long year);

        BaseReponse<LightspeedTravelResponse> LightSpeedTravel(long year);
    }
}
