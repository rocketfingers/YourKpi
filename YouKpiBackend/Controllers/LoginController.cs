using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YouKpiBackend.BusinessLibrary;
using YouKpiBackend.BusinessLibrary.User;
using YouKpiBackend.ViewModels;

namespace YouKpiBackend.Controllers
{
    [Route("api/[controller]")]

    public class LoginController : ControllerBase
    {

        UserLibrary _userLibrary;

        public LoginController(UserLibrary userLibrary)
        {
            _userLibrary = userLibrary;
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