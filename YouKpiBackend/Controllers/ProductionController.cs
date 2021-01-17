using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.BusinessLibrary.Production;
using YouKpiBackend.DbContexts;
using YouKpiBackend.Helpers.Extensions;
using YouKpiBackend.ModelsEntity;
using YouKpiBackend.ViewModels;

namespace YouKpiBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductionController : ControllerBase
    {
        private readonly YoukpiContext _dbContext;

        public ProductionController(YoukpiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpGet("[action]")]

        public async Task<IActionResult> GetProductionReport()
        {
            try
            {
                var res = await _dbContext.ProdExe.ToListAsync();
                return Ok(res.OrderByDescending(p => p.Id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveStep([FromBody] SaveCompleteStepViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var find = await _dbContext.StepOfferWykonanie.FirstOrDefaultAsync(p =>
                    p.OfferLineId == model.OfferLineId
                    && p.Step == model.StepNum
                    && p.ProcessId == model.ProcessId);

                if (find != null)
                {
                    find.LiczbaPomiarowNok = model.LiczbaPomiarowNok;
                    find.LiczbaPomiarow = model.LiczbaPomiarow;
                    find.Zakonczonie = model.Zakonczenie;
                    find.ClosedOn = DateTime.Now;
                    find.ClosesBy = this.GetUserId();
                }
                else
                {
                    AddStepOfferWykonanie(model);
                }
                await _dbContext.SaveChangesAsync();
                var lstStepsStartedTimeRegisterToClose = await _dbContext.PracownikCzasStep.Where(p =>
                p.ProcessId == model.ProcessId
                && p.OfferLinesId == model.OfferLineId
                && p.CzasStart != null
                && p.CzasStop == null).ToListAsync();
                foreach (var item in lstStepsStartedTimeRegisterToClose)
                {
                    item.CzasStop = DateTime.Now;
                }
                //var res = await _ctx.Offer.Include(p => p.Clients).Include(p => p.OfferLines).ThenInclude(p => p.Product).ToListAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private void AddStepOfferWykonanie(SaveCompleteStepViewModel model)
        {
            var itemToAdd = new StepOfferWykonanie()
            {
                LiczbaPomiarow = model.LiczbaPomiarow,
                LiczbaPomiarowNok = model.LiczbaPomiarowNok,
                Zakonczonie = model.Zakonczenie,
                ProcessId = model.ProcessId,
                OfferLineId = model.OfferLineId,
                Step = model.StepNum,
                StartedOn = DateTime.Now
            };
            _dbContext.StepOfferWykonanie.Add(itemToAdd);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMainPage()
        {
            try
            {
                var res = await _dbContext.ProdExe.ToListAsync();
                var possibleProcesses = await _dbContext.PracownikProcess.Where(p => p.PracownikId == this.GetUserId()).Select(p => p.ProcessId).ToListAsync();
                res = res.Where(p => possibleProcesses.Contains(p.ProcessId)).ToList();
                //var res = await _ctx.Offer.Include(p => p.Clients).Include(p => p.OfferLines).ThenInclude(p => p.Product).ToListAsync();
                return Ok(res.OrderByDescending(p => p.Id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> TimeStartStop([FromBody] CzasStartStopViewModel model)
        { 
            try
            {
                int timeToAdd = 0;
                var userId = this.GetUserId();
                model.PracownikId = userId;
                var isStepFinished = await _dbContext.StepOfferWykonanie.AnyAsync(p =>
                p.ProcessId == model.ProcessId
                && p.OfferLineId == model.OfferLineId
                && p.Step == model.StepNum && p.Zakonczonie == true);

                if (isStepFinished)
                {
                    throw new StepAlreadyClosedException("Step został już zakończony i pomiary czasu zostaly zastopowane");
                }

                PracownikCzasStep find = null;
                find = await _dbContext.PracownikCzasStep.FirstOrDefaultAsync(x =>
                 x.PracownikId == model.PracownikId
                 && x.OfferLinesId == model.OfferLineId
                 && x.ProcessId == model.ProcessId
                 && x.Step == model.StepNum
                 && x.CzasStop == null);
                if (find != null)
                {
                    timeToAdd = DateTime.Now.Subtract(find.CzasStart.Value).Minutes;
                    find.LiczbaMinut = timeToAdd;
                    find.CzasStop = DateTime.Now;
                }
                else
                {
                    bool wasStepEarlierStarted;
                    int wasStepEarlierStartedCountItems = await _dbContext.PracownikCzasStep.CountAsync(x =>
                   x.PracownikId == model.PracownikId
                   && x.OfferLinesId == model.OfferLineId
                   && x.ProcessId == model.ProcessId
                   && x.Step == model.StepNum
                   && x.CzasStop != null);

                    wasStepEarlierStarted = wasStepEarlierStartedCountItems > 0;

                    if (!wasStepEarlierStarted)
                    {
                        var itemToUpdateStartedTime = await _dbContext.StepOfferWykonanie.FirstOrDefaultAsync(x =>
                         x.OfferLineId == model.OfferLineId
                         && x.ProcessId == model.ProcessId
                         && x.Step == model.StepNum); if (itemToUpdateStartedTime == null)
                        {
                            AddStepOfferWykonanie(new SaveCompleteStepViewModel() {
                                OfferLineId = model.OfferLineId,
                                ProcessId = model.ProcessId,
                                StepNum = model.StepNum                                
                            });
                        }
                        else
                        {
                            itemToUpdateStartedTime.StartedOn = DateTime.Now;
                        }
                    }

                    _dbContext.PracownikCzasStep.Add(new PracownikCzasStep()
                    {
                        Id = 0,
                        CzasStart = DateTime.Now,
                        OfferLinesId = model.OfferLineId,
                        PracownikId = userId,
                        ProcessId = model.ProcessId,
                        Step = model.StepNum
                    });
                }
                await _dbContext.SaveChangesAsync();
                return Ok(timeToAdd);
            }
            catch (StepAlreadyClosedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSteps(int offerLineId, string processId)
        {
            if (offerLineId < 1 || string.IsNullOrWhiteSpace(processId))
            {
                return BadRequest("Bad model");
            }

            try
            {
                var userId = this.GetUserId();
                var steps = await _dbContext.Steps.Where(p => p.ProcessesId == processId).Select(p => new SpepsWykonanieViewModel()
                {
                    StepName = p.StepName,
                    StepNum = p.StepNum,
                    Sekwencja = p.Sekwencja,
                    LiczbaPomiarow = 0,
                    LiczbaPomiarowNok = 0,
                    Zakonczone = false,
                    StepStartedByMe = _dbContext.PracownikCzasStep.Any(x =>
                    x.PracownikId == userId
                    && x.OfferLinesId == offerLineId
                    && x.ProcessId == processId
                    && x.Step == p.StepNum
                    && x.CzasStop == null),
                    StepUsedBySomeoneElse = _dbContext.PracownikCzasStep.Any(x =>
                    x.PracownikId != userId
                    && x.OfferLinesId == offerLineId
                    && x.ProcessId == processId
                    && x.Step == p.StepNum
                    && x.CzasStop == null)
                }).ToListAsync();

                var wykonanieList = await _dbContext.StepOfferWykonanie.Where(p =>
                p.OfferLineId == offerLineId 
                && p.ProcessId == processId)
                    .ToListAsync();

                var procesEndTime = await _dbContext.OfferLines.Include(p => p.Offer).Where(p => p.Id == offerLineId)
                    .Select(p => p.Offer.PlannedEnd).FirstOrDefaultAsync();

                foreach (var res in steps)
                {
                    var wykonanie = wykonanieList.FirstOrDefault(p => p.Step == res.StepNum);
                    if (wykonanie != null)
                    {
                        res.StepStarted = wykonanie.StartedOn.HasValue;
                        res.LiczbaPomiarow = wykonanie.LiczbaPomiarow;
                        res.LiczbaPomiarowNok = wykonanie.LiczbaPomiarowNok;
                        res.Zakonczone = wykonanie.Zakonczonie;
                        res.StartedDate = wykonanie.StartedOn;
                    }
                }
                steps = steps.OrderBy(p => p.StepNum).ToList();
                var shouldStartBeBefore = procesEndTime;
                for (int i = steps.Count - 1; i >= 0; i--)
                {
                    var curStep = steps[i];
                    if (shouldStartBeBefore.HasValue)
                    {
                        shouldStartBeBefore = shouldStartBeBefore.Value.AddDays(-curStep.Sekwencja);
                        curStep.ShouldStartBefore = shouldStartBeBefore;
                        if (curStep.ShouldStartBefore > DateTime.Now.AddDays(1))
                        {
                            curStep.PlannedStartStatus = TimeToEnd.OnTime;

                        }
                        else if (curStep.ShouldStartBefore >= DateTime.Now && curStep.ShouldStartBefore < DateTime.Now.AddDays(1))
                        {
                            curStep.PlannedStartStatus = TimeToEnd.AlmostRed;
                        }
                        else
                        {
                            if (curStep.StartedDate.HasValue)
                            {
                                if (curStep.StartedDate.Value > shouldStartBeBefore)
                                {
                                    curStep.PlannedStartStatus = TimeToEnd.Red;
                                }
                                else
                                {
                                    curStep.PlannedStartStatus = TimeToEnd.OnTime;
                                }
                            }
                            else
                            {
                                curStep.PlannedStartStatus = TimeToEnd.Red;
                            }
                        }
                    }
                    else
                    {
                        curStep.PlannedStartStatus = TimeToEnd.Unknown;
                    }
                    
                }
                return Ok(steps);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}