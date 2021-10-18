using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;

namespace YouKpiBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CompetenceController : Controller
    {
        private readonly YoukpiContext _ctx;
        public CompetenceController(YoukpiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompetencesTypes()
        {
            try
            {
                var res = new List<string>()
                {
                    "Podstawowy",
                    "Kierownik",
                    "Menadżer",
                    "Dyrektor",
                    "Zarząd"
                };
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompetencesLevels()
        {
            try
            {
                var res = await _ctx.CompetencesLevels.ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        

    }
}