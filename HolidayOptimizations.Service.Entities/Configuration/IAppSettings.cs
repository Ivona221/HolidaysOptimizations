using System;
using System.Collections.Generic;
using System.Text;

namespace HolidayOptimizations.Service.Entities.Configuration
{
    public interface IAppSettings
    {
        string ConnectionString { get; }

        bool IsDebug { get; }

        bool InMaintenanceMode { get; }
    }
}
