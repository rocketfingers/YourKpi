﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using YouKpiBackend.DbContexts;
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
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMainPage()
        {
            try
            {
                var res = await _dbContext.ProdExe.ToListAsync();
                //var res = await _ctx.Offer.Include(p => p.Clients).Include(p => p.OfferLines).ThenInclude(p => p.Product).ToListAsync();
                return Ok(res.OrderByDescending(p => p.Id));
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
                var steps = await _dbContext.Steps.Where(p => p.ProcessesId == processId).ToListAsync();

                var wykonanieList = await _dbContext.StepOfferWykonanie.Where(p => p.OfferLineId == offerLineId && p.ProcessId == processId).ToListAsync();

                var response = steps.Select(p => new SpepsWykonanieViewModel()
                {
                    StepName = p.StepName,
                    StepNum = p.StepNum,
                    Sekwencja = p.Sekwencja,
                    LiczbaPomiarow = 0,
                    LiczbaPomiarowNok = 0,
                    Zakonczone = false
                });
                foreach (var res in response)
                {
                    var wykonanie = wykonanieList.FirstOrDefault(p => p.Step == res.StepNum);
                    if (wykonanie != null)
                    {
                        res.LiczbaPomiarow = wykonanie.LiczbaPomiarow;
                        res.LiczbaPomiarowNok = wykonanie.LiczbaPomiarowNok;
                        res.Zakonczone = wykonanie.Zakonczonie;
                        
                    }
                }
                //var res = await _ctx.Offer.Include(p => p.Clients).Include(p => p.OfferLines).ThenInclude(p => p.Product).ToListAsync();
                return Ok(response.OrderBy(p => p.StepNum));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}