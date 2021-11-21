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
                new List<string>()
                {
                    //Seed widoków
                    @"\SQL\Views\VActivityHistory.sql",
                    @"\SQL\Views\VProdExe.sql",
                    @"\SQL\Views\VOtifReport.sql"
                    //Seed danych
                    //@"\SQL\Seed\InsertMediumsDictionary.sql",
                    //@"\SQL\Seed\InsertCompetencesLevels.sql"
                    //@"\SQL\Seed\InsertProcessSubjects.sql"
                    //@"\SQL\Seed\InsertProcessAreas.sql"

                }.ForEach(scriptPath => ExecuteScriptFile(scriptPath, context));
            }
        }

        private static void ExecuteScriptFile(string relPath, YoukpiContext context)
        {
            string script = File.ReadAllText(Directory.GetCurrentDirectory() + relPath);
            context.Database.ExecuteSqlRaw(script);
        }
    }
}
