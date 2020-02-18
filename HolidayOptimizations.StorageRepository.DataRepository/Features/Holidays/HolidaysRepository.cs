using Dapper;
using HolidayOptimizations.Service.Entities.Configuration;
using HolidayOptimizations.Service.Entities.Features.Holidays;
using HolidayOptimizations.StorageRepository.DataRepositoryInterface.Features.Holidays;
using SQLinq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayOptimizations.StorageRepository.DataRepository.Features.Holidays
{
    public class HolidaysRepository : BaseDbRepository<PublicHoliday>, IHolidaysRepository
    {
        public HolidaysRepository(IAppSettings settings) : base(settings)
        {
        }

        public List<PublicHoliday> GetPulbicHolidaysByYear(long? year)
        {
            var reponse = Using(connection =>
            {
                var query = string.Concat(@"SELECT * FROM PublicHolidays WHERE YEAR(Date) = ", year);
                var result = connection.Query<PublicHoliday>(query).ToList();
                return result;
            });

            return reponse;
        }

        public void InsertHolidaysAsync(List<PublicHoliday> holidays)
        {
            Task.Factory.StartNew(() =>
            {
                Using(connection =>
                {
                    connection.Execute(@"INSERT INTO PublicHolidays 
                                        VALUES (@Date, 
                                                @LocalName,
                                                @Name,
                                                @CountryCode,
                                                @Fixed,
                                                @Global,
                                                @Countries,
                                                @LaunchYear,
                                                @ModifiedAt,
                                                @CreatedAt,
                                                @EndDate)", holidays);
                });

            });
        }

        public void InsertHolidays(List<PublicHoliday> holidays)
        {
            Using(connection =>
            {
                connection.Execute(@"INSERT INTO PublicHolidays 
                                        VALUES (@Date, 
                                                @LocalName,
                                                @Name,
                                                @CountryCode,
                                                @Fixed,
                                                @Global,
                                                @Countries,
                                                @LaunchYear,
                                                @ModifiedAt,
                                                @CreatedAt,
                                                @EndDate)", holidays);
            });
        }

        public void DeleteHolidaysByYear(long year)
        {
            Using(connection =>
            {
                connection.Execute(@"DELETE FROM PublicHolidays WHERE YEAR(Date) = @Year", new { Year = year });
            });
        }
    }
}
