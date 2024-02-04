using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBoard.Server.Data;
using JobBoard.Shared.Domain;
using Microsoft.Identity.Client;
using JobBoard.Server.IRepository;
using JobBoard.Server.IRepository;

namespace JobBoard.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;

        //public JobsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}


        private readonly IUnitOfWork _unitOfWork;

        public JobsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }



        //// GET: api/Jobs
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        //{
        //  if (_context.Jobs == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Jobs.ToListAsync();
        //}

        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {

            var jobs = await _unitOfWork.Jobs.GetAll(includes: q => q.Include(x =>x.Employer));
            return Ok(jobs);
        }


        //// GET: api/Jobs/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Job>> GetJob(int id)
        //{
        //  if (_context.Jobs == null)
        //  {
        //      return NotFound();
        //  }
        //    var job = await _context.Jobs.FindAsync(id);

        //    if (job == null)
        //    {
        //        return NotFound();
        //    }

        //    return job;
        //}

        [HttpGet("{id}")]

        public async Task<IActionResult> GetJob(int id)
        {
            var job = await _unitOfWork.Jobs.Get(q => q.Id == id);

            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);

        }




        //// PUT: api/Jobs/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutJob(int id, Job job)
        //{
        //    if (id != job.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(job).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!JobExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, Job job)
        {
            if (id != job.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _unitOfWork.Jobs.Update(job);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await JobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        //// POST: api/Jobs
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Job>> PostJob(Job job)
        //{
        //  if (_context.Jobs == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.Jobs'  is null.");
        //  }
        //    _context.Jobs.Add(job);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetJob", new { id = job.Id }, job);
        //}


        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            await _unitOfWork.Jobs.Insert(job);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetJob", new { id = job.Id }, job);
        }







        //// DELETE: api/Jobs/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteJob(int id)
        //{
        //    if (_context.Jobs == null)
        //    {
        //        return NotFound();
        //    }
        //    var job = await _context.Jobs.FindAsync(id);
        //    if (job == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Jobs.Remove(job);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _unitOfWork.Jobs.Get(q => q.Id == id);
            if (job == null)
            {
                return NotFound();

            }

            await _unitOfWork.Jobs.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }



        //private bool JobExists(int id)
        //{
        //    return (_context.Jobs?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        private async Task<bool> JobExists(int id)
        {
            var job = await _unitOfWork.Jobs.Get(q => q.Id == id);
            return job != null;
        }
    }
}

