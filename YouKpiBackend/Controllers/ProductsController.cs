using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        public ProductsController()
        {

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
               
                return Ok("Its working");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
