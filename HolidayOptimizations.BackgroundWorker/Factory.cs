using FluentScheduler;
using HolidayOptimizations.Service.Entities.Configuration;
using HolidayOptimizations.StorageRepository.DataRepository.Features.Holidays;
using HolidayOptimizations.StorageRepository.DataRepositoryInterface.Features.Holidays;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace HolidayOptimizations.BackgroundWorker
{
    public class Factory
    {
        public class MyNinjectModule : NinjectModule
        {
            public override void Load()
            {
                Bind<IHolidaysRepository>().To<HolidaysRepository>();
                Bind<IAppSettings>().To<AppSettings>();
            }
        }

        public class JobFactory : IJobFactory
        {

            private IKernel Kernel { get; }

            public JobFactory(IKernel kernel)
            {
                Kernel = kernel;
            }

            public IJob GetJobInstance<T>() where T : IJob
            {
                return Kernel.Get<T>();
            }
        }
    }
}
