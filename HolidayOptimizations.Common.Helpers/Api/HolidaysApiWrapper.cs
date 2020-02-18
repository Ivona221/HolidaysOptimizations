using HolidayOptimizations.Common.Base.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HolidayOptimizations.Common.Helpers.Api
{
    /// <summary>
    /// A wrapper class for public holidays api
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class HolidaysApiWrapper<T> where T : class
    {

        public static async Task<T> GetPublicHolidays(long year, string countryCode)
        {
            var url = string.Format("https://date.nager.at/api/v2/publicholidays/{0}/{1}", year, countryCode);
            var response = await ApiRequestWrapper<T>.Get(url);

            return response;
        }

        public static async Task<T> GetCountryInfo(string countryCode)
        {
            var url = string.Format("https://date.nager.at/Api/v2/CountryInfo?countryCode={0}", countryCode);
            var response = await ApiRequestWrapper<T>.Get(url);

            return response;
        }
    }
}
