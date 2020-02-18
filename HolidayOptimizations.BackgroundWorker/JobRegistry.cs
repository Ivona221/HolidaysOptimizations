using FluentScheduler;
using HolidayOptimizations.BackgroundWorker.Jobs;
using System;

namespace HolidayOptimizations.BackgroundWorker
{
    public class JobRegistry : Registry
    {
        public JobRegistry()
        {
            Schedule<PublicHolidaysJob>().ToRunEvery(10).Days();
        }       
    }
}
