using System;
using System.Collections.Generic;
using System.Text;

namespace HolidayOptimizations.Service.Entities.Configuration
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString { get; set; }

        public bool IsDebug { get; set; }

        public bool InMaintenanceMode { get; set; }
    }
}
