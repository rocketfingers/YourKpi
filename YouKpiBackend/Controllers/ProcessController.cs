using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;
using YouKpiBackend.ModelsEntity;
using YouKpiBackend.ViewModels;

namespace YouKpiBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProcessController : Controller
    {
        private readonly YoukpiContext _ctx;
        private readonly IMapper _mapper;

        public ProcessController(YoukpiContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lst = await _ctx.Process
                    .Include(p => p.Steps)
                    //TODO przerobić jeśli będzie potwierdzenie, że powiązany moze być tylko jeden proces
                    .Include(p => p.ProcessesProcessProcess)
                    .Include(p => p.ProcessCompetences)
                    .Include(p => p.ProcessCompetences)
                    .ThenInclude(p => p.CompetenceLevel)
                    .Include(p => p.ProcessAreas)
                    .ThenInclude(p => p.ProcessArea)
                    .Include(p => p.ProcessSubjects)
                    .ThenInclude(p => p.ProcessSubject)
                    .Include(p => p.ProcessManager)
                    .ToListAsync();

                //todo var res = _mapper.Map<List<ProcessViewModel>>(lst);
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Process entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var res = _ctx.Process.Add(entity);
                await _ctx.SaveChangesAsync();

                return Created("", entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] Process entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var processEntity = _ctx.Process
                    .Include(p => p.Steps)
                    .Include(p => p.ProcessesProcessProcess)
                    .Include(p => p.ProcessCompetences)
                    .Include(p => p.ProcessAreas)
                    .ThenInclude(p => p.ProcessArea)
                    .Include(p => p.ProcessSubjects)
                    .ThenInclude(p => p.ProcessSubject)
                    .Include(p => p.ProcessManager)
                    .FirstOrDefault(c => c.Id == entity.Id);
                processEntity.Steps.ToList().ForEach(p =>
                {
                    _ctx.Entry(p).State = EntityState.Deleted;
                });

                processEntity.TypZlecenia = entity.TypZlecenia;
                processEntity.BusinessArea = entity.BusinessArea;
                processEntity.NazwaGrupyProcesu = entity.NazwaGrupyProcesu;
                processEntity.NazwaProcesu = entity.NazwaProcesu;
                processEntity.Steps = entity.Steps;
                processEntity.ProcessManagerId = entity.ProcessManagerId;
                processEntity.ProcessesProcessProcess.ToList().ForEach(p =>
                {
                    _ctx.Entry(p).State = EntityState.Deleted;
                });

                entity.ProcessesProcessProcess.ToList().ForEach(p =>
                {
                    _ctx.ProcessesProcess.Add(p);
                });

                processEntity.ProcessSubjects.ToList().ForEach(p =>
                {
                    _ctx.Entry(p).State = EntityState.Deleted;
                });

                entity.ProcessSubjects.ToList().ForEach(p =>
                {
                    p.ProcessSubjectId = p.Id;
                    p.Id = 0;
                    p.ProcessId = entity.Id;
                    _ctx.ProcessesProcessSubjects.Add(p);
                });

                processEntity.ProcessAreas.ToList().ForEach(p =>
                {
                    _ctx.Entry(p).State = EntityState.Deleted;
                });

                entity.ProcessAreas.ToList().ForEach(p =>
                {
                    p.ProcessAreaId = p.Id;
                    p.Id = 0;
                    p.ProcessId = entity.Id;
                    _ctx.ProcessesProcessAreas.Add(p);
                });

                processEntity.ProcessCompetences.ToList().ForEach(p =>
                {
                    _ctx.Entry(p).State = EntityState.Deleted;
                });

                entity.ProcessCompetences.ToList().ForEach(p =>
                {
                    p.ProcessId = processEntity.Id;
                    p.CompetenceLevel = null;
                    _ctx.ProcessCompetences.Add(p);
                });

                await _ctx.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {

                var processEntity = _ctx.Process.Include(p => p.Steps).FirstOrDefault(c => c.Id == id);
                processEntity.Steps.ToList().ForEach(p =>
                {
                    _ctx.Entry(p).State = EntityState.Deleted;
                });
                if (processEntity != null)
                {
                    _ctx.Process.Remove(processEntity);
                    await _ctx.SaveChangesAsync();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
