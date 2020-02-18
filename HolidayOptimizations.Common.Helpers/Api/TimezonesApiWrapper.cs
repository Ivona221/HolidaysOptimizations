using HolidayOptimizations.Common.Base.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HolidayOptimizations.Common.Helpers.Api
{
    public static class TimezonesApiWrapper<T> where T : class
    {

        public static async Task<T> GetCountryTimezones(string countryCode)
        {
            var url = string.Format("https://restcountries.eu/rest/v2/alpha/{0}", countryCode);
            var response = await ApiRequestWrapper<T>.Get(url);

            return response;
        }
    }
}
