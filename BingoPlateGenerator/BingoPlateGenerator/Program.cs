using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BingoPlateGenerator.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BingoPlateGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Models.DisplayModels.FactoryDisplay display = new Models.DisplayModels.FactoryDisplay();
            CreateHostBuilder(args).Build().Run();


            //testing space

            

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
