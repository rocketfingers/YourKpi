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
        public async Task StopStepLongerThan480()
        {
            List<PracownikCzasStep> stepsToStop = await NewMethod();

            stepsToStop.ForEach(p => p.CzasStop = DateTime.Now);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<List<PracownikCzasStep>> NewMethod()
        {
            return await _dbContext.PracownikCzasStep.Where(
                p => p.CzasStart != null
                && p.CzasStop == null
                && (int)DateTime.Now.Subtract(p.CzasStart.Value).TotalMinutes > MaxWorkTime).ToListAsync();
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
