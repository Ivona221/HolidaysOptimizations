using System;
using System.Collections.Generic;
using System.Text;

namespace HolidayOptimizations.Service.Entities.Features.Holidays
{
    public class PublicHoliday : BaseEntity
    {
        //The date
        public DateTime Date { get; set; }

        //Local name
        public string LocalName { get; set; }

        //English name
        public string Name { get; set; }

        //ISO 3166-1 alpha-2
        public string CountryCode { get; set; }

        //Is this public holiday every year on the same date
        public bool Fixed { get; set; }

        //Is this public holiday in every county (federal state)
        public bool Global { get; set; }

        public string Countries { get; set; }

        //The launch year of the public holiday
        public long? LaunchYear { get; set; }

        //when is the holiday ending
        public DateTime EndDate { get; set; }

        public PublicHoliday()
        {
            //Countries = new List<string>();
        }
    }
}
