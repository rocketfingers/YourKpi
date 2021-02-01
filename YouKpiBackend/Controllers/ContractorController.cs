using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;
using YouKpiBackend.ViewModels;

namespace YouKpiBackend.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class ContractorController : Controller
    {
        private readonly YoukpiContext _ctx;
        public ContractorController(YoukpiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _ctx.Kontrahent.ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllSimpleView()
        {
            try
            {
                var res = await _ctx.Kontrahent.Select(x => new SimpleViewModel(x.Id.ToString(), x.Name)).ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}