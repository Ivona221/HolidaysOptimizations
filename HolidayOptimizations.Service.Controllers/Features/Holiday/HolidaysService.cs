﻿using HolidayOptimizations.Common.Helpers.Api;
using HolidayOptimizations.Service.Controllers.Enums;
using HolidayOptimizations.Service.Entities;
using HolidayOptimizations.Service.Entities.Features.Holidays;
using HolidayOptimizations.Service.Entities.Reponse;
using HolidayOptimizations.Service.Processes;
using HolidayOptimizations.StorageRepository.DataRepositoryInterface.Features.Holidays;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using HolidayOptimizations.Service.Processes.Helpers;

namespace HolidayOptimizations.Service.Controllers
{
    public class HolidaysService : IHolidaysProcess
    {
        public IHolidaysHelper _helper;
        public ITimezonesRepository _timezonesRepository;


        public HolidaysService(IHolidaysHelper helper, ITimezonesRepository timezonesRepository)
        {
            _helper = helper;
            _timezonesRepository = timezonesRepository;
        }

        public BaseReponse<string> GetCountryWithMostHolidays(long year)
        {
            try
            {
                var publicHolidaysByCountry = new Dictionary<string, int>();

                var holidays = _helper.GetAllHolidays(year);

                foreach (var holiday in holidays)
                {
                    if (!publicHolidaysByCountry.ContainsKey(holiday.CountryCode))
                    {
                        publicHolidaysByCountry[holiday.CountryCode] = 0;
                    }

                    publicHolidaysByCountry[holiday.CountryCode] += 1;
                }

                publicHolidaysByCountry = publicHolidaysByCountry.OrderByDescending(x => x.Value).ToDictionary(z => z.Key, y => y.Value);

                var countryWithMostHolidaysCode = publicHolidaysByCountry.FirstOrDefault().Key;

                var countryInfo = HolidaysApiWrapper<Country>.GetCountryInfo(countryWithMostHolidaysCode).Result;

                BaseReponse<string> response = new BaseReponse<string>
                {
                    Response = countryInfo.CommonName,
                    StatusCode = HttpStatusCode.OK,
                    Exception = null
                };

                return response;
            }
            catch (Exception ex)
            {
                BaseReponse<string> response = new BaseReponse<string>
                {
                    Response = null,
                    StatusCode = HttpStatusCode.BadRequest,
                    Exception = ex
                };

                return response;
            }
        }

        public BaseReponse<string> GetMostHolidaysByMonth(long year)
        {
            try
            {
                var publicHolidaysByMonth = new Dictionary<int, List<PublicHoliday>>();

                //populate the dictionary for every month with empty list of holidays for that month
                for (var i = 1; i <= 12; i++)
                {
                    publicHolidaysByMonth[i] = new List<PublicHoliday>();
                }

                var holidays = _helper.GetAllHolidays(year);
                holidays.ForEach(x => publicHolidaysByMonth[x.Date.Month].Add(x));

                publicHolidaysByMonth = publicHolidaysByMonth.OrderByDescending(x => x.Value.Count).ToDictionary(z => z.Key, y => y.Value);

                var response = new BaseReponse<string>
                {
                    Response = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(publicHolidaysByMonth.FirstOrDefault().Key),
                    StatusCode = HttpStatusCode.OK,
                    Exception = null
                };

                return response;
            }
            catch(Exception ex)
            {
                BaseReponse<string> response = new BaseReponse<string>
                {
                    Response = null,
                    StatusCode = HttpStatusCode.BadRequest,
                    Exception = ex
                };

                return response;
            }
            
        }
        
        public BaseReponse<string> GetCountryWithMostUniqueHolidays(long year)
        {
            try
            {
                var publicHolidaysByCountry = new Dictionary<string, List<PublicHoliday>>();

                var holidays = _helper.GetAllHolidays(year);

                foreach (var holiday in holidays)
                {
                    if (!publicHolidaysByCountry.ContainsKey(holiday.CountryCode))
                    {
                        publicHolidaysByCountry[holiday.CountryCode] = new List<PublicHoliday>();
                    }

                    publicHolidaysByCountry[holiday.CountryCode].Add(holiday);
                }

                var uniqueHolidaysByCountry = new Dictionary<string, List<PublicHoliday>>();

                foreach (KeyValuePair<string, List<PublicHoliday>> element in publicHolidaysByCountry)
                {
                    uniqueHolidaysByCountry[element.Key] = new List<PublicHoliday>();
                    uniqueHolidaysByCountry[element.Key].AddRange(element.Value.Where(x => !holidays.Any(y => y.Date == x.Date
                                                                                                              && y.CountryCode != element.Key)).ToList());
                }

                uniqueHolidaysByCountry = uniqueHolidaysByCountry.OrderByDescending(x => x.Value.Count).ToDictionary(z => z.Key, y => y.Value);

                var countryWithMostUniqueHolidays = uniqueHolidaysByCountry.FirstOrDefault();

                var countryInfo = HolidaysApiWrapper<Country>.GetCountryInfo(countryWithMostUniqueHolidays.Key).Result;

                countryWithMostUniqueHolidays.Value.ForEach(x => x.Id = 0);

                var response = new BaseReponse<string>
                {
                    Response = countryInfo.OfficialName,
                    StatusCode = HttpStatusCode.OK,
                    Exception = null
                };

                return response;
            }
            catch (Exception ex)
            {
                BaseReponse<string> response = new BaseReponse<string>
                {
                    Response = null,
                    StatusCode = HttpStatusCode.BadRequest,
                    Exception = ex
                };

                return response;
            }
        }

        public BaseReponse<LightspeedTravelResponse> LightSpeedTravel(long year)
        {
            try
            {
                var publicHolidaysByDate = new List<PublicHoliday>();

                //var holidaysToInsert = new List<PublicHoliday>();

                var allHolidays = new List<PublicHoliday>();

                var timezonesToSave = new List<Timezone>();

                //var holidaysFromDb = _repository.GetPulbicHolidaysByYear(year);
                var countryTimezonesDb = _timezonesRepository.GetAllTimezones();

                var holidays = _helper.GetAllHolidays(year);

                foreach (var holiday in holidays)
                {
                    TimeSpan timespan = new TimeSpan(12, 00, 00);
                    var modifiedHoliday = holiday;
                    modifiedHoliday.Date = modifiedHoliday.Date.Add(timespan);
                    modifiedHoliday.EndDate = modifiedHoliday.Date.AddHours(24);
                    var countryTimezones = new Timezone();
                    var countryTimezonesByCountry = countryTimezonesDb.Where(x => x.CountryCode.Trim() == holiday.CountryCode);
                    if (countryTimezonesByCountry.Any())
                    {
                        var timezoneCodes = new List<string>();
                        countryTimezonesDb.ToList().ForEach(x => timezoneCodes.Add(x.TimezoneUTC));
                        countryTimezones = new Timezone
                        {
                            CountryCode = countryTimezonesDb.FirstOrDefault().CountryCode,
                            Timezones = timezoneCodes
                        };
                    }
                    else
                    {
                        countryTimezones = TimezonesApiWrapper<Timezone>.GetCountryTimezones(holiday.CountryCode).Result;
                        foreach (var timezone in countryTimezones.Timezones)
                        {
                            timezonesToSave.Add(new Timezone
                            {
                                CountryCode = holiday.CountryCode,
                                TimezoneUTC = timezone
                            });
                        }
                    }

                    var timeZones = new List<double>();
                    foreach (var timezone in countryTimezones.Timezones)
                    {
                        if (timezone == "UTC")
                        {
                            timeZones.Add(0);
                        }
                        else
                        {
                            var timezoneValue = double.Parse(timezone.Replace("UTC", "").Replace("+", "").Split(":")[0]);
                            timeZones.Add(timezoneValue);
                        }
                    }
                    timeZones = timeZones.OrderBy(x => x).ToList();
                    modifiedHoliday.Date = modifiedHoliday.Date.AddHours(timeZones[0]);
                    modifiedHoliday.EndDate = modifiedHoliday.EndDate.AddHours(timeZones[timeZones.Count - 1]);
                    publicHolidaysByDate.Add(modifiedHoliday);
                }

                _timezonesRepository.InsertTimezonesAsync(timezonesToSave);

                publicHolidaysByDate = publicHolidaysByDate.OrderBy(x => x.Date).ToList();

                int count = 0;
                int result = 0;

                var listOfHolidays = new List<PublicHoliday>();
                var listOfHolidaysFinal = new List<PublicHoliday>();

                for (int i = 0; i < publicHolidaysByDate.Count - 1; i++)
                {
                    var holiday = publicHolidaysByDate[i];
                    var nextHoliday = publicHolidaysByDate[i + 1];
                    var lengthOfHoliday = (holiday.Date - holiday.EndDate).TotalHours;
                    if (holiday.EndDate < nextHoliday.Date)
                    {
                        count = 0;
                        listOfHolidays = new List<PublicHoliday>();
                    }
                    else
                    {
                        count++; //increase count 
                        result = Math.Max(result, count);
                        listOfHolidays.Add(holiday);
                        if (listOfHolidays.Count > listOfHolidaysFinal.Count)
                        {
                            listOfHolidaysFinal = listOfHolidays;
                        }                        
                    }
                }

                listOfHolidaysFinal.ForEach(x => x.Id = 0);

                var response = new BaseReponse<LightspeedTravelResponse>
                {
                    Response = new LightspeedTravelResponse
                    {
                        LongestSequence = listOfHolidaysFinal.Count,
                        Holidays = listOfHolidaysFinal
                    },
                    StatusCode = HttpStatusCode.OK,
                    Exception = null
                };

                return response;
            }
            catch (Exception ex)
            {
                BaseReponse<LightspeedTravelResponse> response = new BaseReponse<LightspeedTravelResponse>
                {
                    Response = null,
                    StatusCode = HttpStatusCode.BadRequest,
                    Exception = ex
                };

                return response;
            }

        }
    }
}
