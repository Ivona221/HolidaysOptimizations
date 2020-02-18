using System;
using System.Collections.Generic;
using System.Text;

namespace HolidayOptimizations.Service.Entities.Features.Holidays
{
    public class Timezone : BaseEntity
    {
        public string CountryCode { get; set; }

        public string TimezoneUTC { get; set; }

        public List<string> Timezones { get; set; }

        public Timezone()
        {
            Timezones = new List<string>();
        }
    }
}
