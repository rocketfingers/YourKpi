using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YouKpiBackend.BusinessLibrary;
using YouKpiBackend.BusinessLibrary.Company;
using YouKpiBackend.BusinessLibrary.User;
using YouKpiBackend.ViewModels;

namespace YouKpiBackend.Controllers
{
    [Route("api/[controller]")]

    public class LoginController : ControllerBase
    {
        UserLibrary _userLibrary;
        CompanyLibrary _companyLibrary;

        public LoginController(UserLibrary userLibrary, CompanyLibrary companyLibrary)
        {
            _userLibrary = userLibrary;
            _companyLibrary = companyLibrary;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetBasicInfo()
        {
            try
            {
                var res = await _companyLibrary.GetUserCompanyInfo();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequestViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Bad login or password");
            }
            try
            {
               var user =  await _userLibrary.Authenticate(model);
               var token = _userLibrary.BuildToken(user);             

                return Created("", token);
            }
            catch(BadLoginOrPasswordException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}