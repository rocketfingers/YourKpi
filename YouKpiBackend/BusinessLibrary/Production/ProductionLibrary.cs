using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;

namespace YouKpiBackend.BusinessLibrary.Production
{
    public class ProductionLibrary
    {
        readonly YoukpiContext _dbContext;

        public ProductionLibrary(YoukpiContext dbContext)
        {
            _dbContext = dbContext;
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
