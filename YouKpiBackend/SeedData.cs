using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace YouKpiBackend
{
    public static class Services
    {
        public static void SeedData(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var context = serviceProvider.GetService<YoukpiContext>();
                string script = File.ReadAllText(Directory.GetCurrentDirectory() + @"\SQL\Views\VActivityHistory.sql");
                context.Database.ExecuteSqlRaw(script);

                script = File.ReadAllText(Directory.GetCurrentDirectory() + @"\SQL\Views\VProdExe.sql");
                context.Database.ExecuteSqlRaw(script);

                script = File.ReadAllText(Directory.GetCurrentDirectory() + @"\SQL\Views\VOtifReport.sql");
                context.Database.ExecuteSqlRaw(script);

                //Seed danych
                //script = File.ReadAllText(Directory.GetCurrentDirectory() + @"\SQL\Seed\InsertMediumsDictionary.sql");
                //context.Database.ExecuteSqlRaw(script);
            }
        }
    }
    
}
