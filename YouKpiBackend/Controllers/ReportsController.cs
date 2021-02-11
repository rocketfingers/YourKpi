using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouKpiBackend.DbContexts;
using YouKpiBackend.Helpers;
using YouKpiBackend.Helpers.Extensions;
using YouKpiBackend.ViewModels;

namespace YouKpiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ReportsController : ControllerBase
    {
        readonly YoukpiContext _context;

        public ReportsController(YoukpiContext context)
        {
            _context = context;
        }
           
        [HttpGet("[action]")]
        public async Task<IActionResult> GetOtifReport([FromHeader]string authorization)
        {
            try
            {
                TestAuthorization(authorization);
            }
            catch 
            {
                return Unauthorized();
            }
            try
            {
                var resData = await _context.VOtifReport.ToListAsync();
                List<ReportOtifViewModel> mappedData = new List<ReportOtifViewModel>();
                DateTime? minDate = resData.Min(p => p.PlannedEndOffer);
                DateTime? maxDate = resData.Max(p => p.PlannedEndOffer);
                List<string> allProcesses = resData.Select(p => p.ProcessId).Distinct().ToList();

                for (DateTime i = minDate.Value; i <= maxDate.Value; i = i.AddDays(7))
                {
                    for (int j = 0; j <= allProcesses.Count -1; j++)
                    {
                        mappedData.Add(new ReportOtifViewModel()
                        {
                            Target = 100,
                            Process = allProcesses[j],
                            Week = i.GetIso8601WeekOfYear(),
                            Year = i.Year,
                            Month = i.Month,
                            ProcessName = resData.First(p => p.ProcessId == allProcesses[j]).NazwaProcesu,
                            ProcessGroupName = resData.First(p => p.ProcessId == allProcesses[j]).NazwaGrupyProcesu

                        });
                    }

                }
                foreach (var item in mappedData)
                {
                    item.Total = resData.Count(p => p.PlannedEndOffer.Value.GetIso8601WeekOfYear() == item.Week && p.PlannedEndOffer.Value.Year == item.Year);
                    item.OnTime = resData.Count(p => p.PlannedEndOffer.Value.GetIso8601WeekOfYear() == item.Week && p.PlannedEndOffer.Value.Year == item.Year && p.PlannedEndOffer.Value.AddDays(-(int)p.StepShouldEndBeforeDays) >= p.ClosedOn.Value);
                }
                return Ok(mappedData);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
            
        }

        private static void TestAuthorization(string authorization)
        {
            (string username, string password) = BasicAuthHelper.GetUsernameAndPasswordFromAuthorizeHeader(authorization);
            string properUsername = ConfigHelper.AppSetting("BasicAuth", "Username");
            string properPassword = ConfigHelper.AppSetting("BasicAuth", "Password");
            if (username != properUsername || password != properPassword)
            {
                throw new System.Exception("Unauthorized");
            }
        }
    }
}