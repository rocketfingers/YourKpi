using Hangfire;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.BusinessLibrary.Production;

namespace YouKpiBackend.BusinessLibrary
{

    public class HangFireJobs
    {

        public HangFireJobs()
        {
            if (Debugger.IsAttached)
            {
                //wywala joby ktore sie nieodpalily w prze3szlosci a sa w bazie

                RecurringJob.AddOrUpdate<ProductionLibrary>("DebugStopStepLongerThan480", x => x.StopStepLongerThan480(), "0 * * * *");
            }
            else
            {
                RecurringJob.AddOrUpdate<ProductionLibrary>("StopStepLongerThan480",  x =>  x.StopStepLongerThan480(), "0 * * * *");
            }
        }
    }
}
