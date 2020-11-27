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
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly YoukpiContext _ctx;
        public ProductsController(YoukpiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var res = await _ctx.Produkty.ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
