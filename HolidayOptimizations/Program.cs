using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentScheduler;
using HolidayOptimizations.BackgroundWorker;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ninject;
using static HolidayOptimizations.BackgroundWorker.Factory;

namespace HolidayOptimizations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            JobManager.JobFactory = new JobFactory(new StandardKernel(new MyNinjectModule()));
            JobManager.Initialize(new JobRegistry());
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
