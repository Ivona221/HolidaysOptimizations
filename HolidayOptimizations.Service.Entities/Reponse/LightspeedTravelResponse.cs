using HolidayOptimizations.Service.Entities.Features.Holidays;
using System;
using System.Collections.Generic;
using System.Text;

namespace HolidayOptimizations.Service.Entities.Reponse
{
    public class LightspeedTravelResponse
    {
        public int LongestSequence { get; set; }

        public List<PublicHoliday> Holidays { get;set; }
    }
}
