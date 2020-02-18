using System;
using System.Collections.Generic;
using System.Text;

namespace HolidayOptimizations.Service.Entities.Features.Holidays
{
    public class Country : BaseEntity
    {
        public string CommonName { get; set; }

        public string OfficialName { get; set; }

        public string CountryCode { get; set; }

        public List<string> AlternativeSpellings { get; set; }

        public string Region { get; set; }

        public List<Country> Borders { get; set; }

        public Country()
        {
            AlternativeSpellings = new List<string>();
            Borders = new List<Country>();
        }
    }
}
