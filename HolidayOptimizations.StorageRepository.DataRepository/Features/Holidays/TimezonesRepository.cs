using Dapper;
using HolidayOptimizations.Service.Entities.Configuration;
using HolidayOptimizations.Service.Entities.Features.Holidays;
using HolidayOptimizations.StorageRepository.DataRepositoryInterface.Features.Holidays;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayOptimizations.StorageRepository.DataRepository.Features.Holidays
{
    public class TimezonesRepository : BaseDbRepository<Timezone>, ITimezonesRepository
    {
        public TimezonesRepository(IAppSettings settings) : base(settings)
        {
        }

        public List<Timezone> GetAllTimezones()
        {
            var reponse = Using(connection =>
            {
                var query = @"SELECT * FROM Timezones";
                var result = connection.Query<Timezone>(query).ToList();
                return result;
            });

            return reponse;
        }

        public List<Timezone> GetAllTimezonesByCountry(string countryCode)
        {
            var reponse = Using(connection =>
            {
                var query = string.Concat(@"SELECT * FROM Timezones WHERE CountryCode = '", countryCode, "'");
                var result = connection.Query<Timezone>(query).ToList();
                return result;
            });

            return reponse;
        }

        public void InsertTimezonesAsync(List<Timezone> timezones)
        {
            Task.Factory.StartNew(() =>
            {
                Using(connection =>
                {
                    connection.Execute(@"INSERT INTO Timezones 
                                        VALUES (@CountryCode, 
                                                @TimezoneUTC,
                                                @ModifiedAt,
                                                @CreatedAt)", timezones);
                });

            });
        }
    }
}
