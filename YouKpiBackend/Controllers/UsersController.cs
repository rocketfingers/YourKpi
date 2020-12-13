using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;
using YouKpiBackend.ModelsEntity;
using YouKpiBackend.ViewModels;

namespace YouKpiBackend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly YoukpiContext _dbContext;
        private readonly IMapper _mapper;

        public UsersController(YoukpiContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lst = await _dbContext.Pracownik.ToListAsync();
                var mappedList = _mapper.Map<List<PracownikViewModel>>(lst);
                return Ok(mappedList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] PracownikViewModel pracViewModel)
        {
            if (pracViewModel == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var newPracownik = _mapper.Map<Pracownik>(pracViewModel);
                newPracownik.Password = "54de7f606f2523cba8efac173fab42fb7f59d56ceff974c8fdb7342cf2cfe345";
                newPracownik.Salt = new Random().Next(1000, 111111).ToString();
                _dbContext.Pracownik.Add(newPracownik);
                await _dbContext.SaveChangesAsync();

                return Created("", newPracownik);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] Pracownik entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var pracToChange = await _dbContext.Pracownik.FirstAsync(p => p.Id == entity.Id);
                pracToChange.IsAdministrator = entity.IsAdministrator;
                pracToChange.Name = entity.Name;
                pracToChange.TrybPracy = entity.TrybPracy;
                pracToChange.CzasDostepuMin = entity.CzasDostepuMin;
                pracToChange.Email = entity.Email;
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var partEntity = _dbContext.Pracownik.FirstOrDefault(c => c.Id == id);
                if (partEntity != null)
                {
                    _dbContext.Pracownik.Remove(partEntity);
                    await _dbContext.SaveChangesAsync();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUserProcess(int userId)
        {
            try
            {
                var responseLst = new List<PracownikProcessViewModel>();
                var lstProcess = await _dbContext.Process.ToListAsync();
                var lstUserProcesses = await _dbContext.PracownikProcess.Where(p => p.PracownikId == userId).ToListAsync();
                foreach (var item in lstProcess)
                {
                    responseLst.Add(new PracownikProcessViewModel
                    {
                        ProcessId = item.Id,
                        ProcessName = item.NazwaProcesu,
                        UserAssigned = lstUserProcesses.Where(p => p.ProcessId == item.Id).Any()
                    });
                }
                return Ok(responseLst);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteUserProcess(string processId, int userId)
        {
            if (string.IsNullOrWhiteSpace(processId) && userId < 1)
            {
                return BadRequest("Złe parametry do usunięcia");
            }
            try
            {
                var entity = await _dbContext.PracownikProcess.FirstAsync(c => c.PracownikId == userId && c.ProcessId == processId);

                _dbContext.PracownikProcess.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddUserProcess(PracownikProcessViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Zły model");
            }
            try
            {
                _dbContext.PracownikProcess.Add(new PracownikProcess() { PracownikId = model.UserId, ProcessId = model.ProcessId });
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}