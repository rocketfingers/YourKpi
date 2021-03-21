using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;
using YouKpiBackend.ModelsEntity;

namespace YouKpiBackend.BusinessLibrary.Production
{
    public class ProductionLibrary
    {
        const int MaxWorkTime = 480;
        readonly YoukpiContext _dbContext;

        public ProductionLibrary(YoukpiContext dbContext)
        {
            _dbContext = dbContext;
        }
        [Hangfire.AutomaticRetry(Attempts =1)]
        public async Task StopStepLongerThan4MaxWorkTime()
        {
            List<PracownikCzasStep> stepsToStop = await FindStepsLongerThanMaxWorkTime();

            stepsToStop.ForEach(p => p.CzasStop = DateTime.Now);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<List<PracownikCzasStep>> FindStepsLongerThanMaxWorkTime()
        {
            
            return await _dbContext.PracownikCzasStep.Where(
                p => p.CzasStart != null
                && p.CzasStop == null
                && EF.Functions.DateDiffMinute(p.CzasStart.Value, DateTime.Now) > MaxWorkTime).ToListAsync();
        }
        //public Task GetActivitiesHistory(bool isAdministrator, int? userId = null)
        //{
        //    if (userId == null && !isAdministrator)
        //    {
        //        throw new OnlyAdministratorCanSeeAllActvitiesException();
        //    }
        //    //IQueryable query = _dbContext.PracownikCzasStep.Select(p => new ActivitiesHistoryModel();
        //    //if (userId != null)
        //    //{
        //    //    return query.
        //    //}
        //}
    }
}
