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
    public class JobSeekersController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;

        //public JobSeekersController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}


        private readonly IUnitOfWork _unitOfWork;

        public JobSeekersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }



        //// GET: api/JobSeekers
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<JobSeeker>>> GetJobSeekers()
        //{
        //  if (_context.JobSeekers == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.JobSeekers.ToListAsync();
        //}

        [HttpGet]
        public async Task<IActionResult> GetJobSeekers()
        {

            var jobSeekers = await _unitOfWork.JobSeekers.GetAll();
            return Ok(jobSeekers);
        }


        //// GET: api/JobSeekers/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<JobSeeker>> GetJobSeeker(int id)
        //{
        //  if (_context.JobSeekers == null)
        //  {
        //      return NotFound();
        //  }
        //    var jobSeeker = await _context.JobSeekers.FindAsync(id);

        //    if (jobSeeker == null)
        //    {
        //        return NotFound();
        //    }

        //    return jobSeeker;
        //}

        [HttpGet("{id}")]

        public async Task<IActionResult> GetJobSeeker(int id)
        {
            var jobSeeker = await _unitOfWork.JobSeekers.Get(q => q.Id == id);

            if (jobSeeker == null)
            {
                return NotFound();
            }

            return Ok(jobSeeker);

        }




        //// PUT: api/JobSeekers/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutJobSeeker(int id, JobSeeker jobSeeker)
        //{
        //    if (id != jobSeeker.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(jobSeeker).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!JobSeekerExists(id))
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
        public async Task<IActionResult> PutJobSeeker(int id, JobSeeker jobSeeker)
        {
            if (id != jobSeeker.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _unitOfWork.JobSeekers.Update(jobSeeker);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await JobSeekerExists(id))
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



        //// POST: api/JobSeekers
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<JobSeeker>> PostJobSeeker(JobSeeker jobSeeker)
        //{
        //  if (_context.JobSeekers == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.JobSeekers'  is null.");
        //  }
        //    _context.JobSeekers.Add(jobSeeker);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetJobSeeker", new { id = jobSeeker.Id }, jobSeeker);
        //}


        [HttpPost]
        public async Task<ActionResult<JobSeeker>> PostJobSeeker(JobSeeker jobSeeker)
        {
            await _unitOfWork.JobSeekers.Insert(jobSeeker);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetJobSeeker", new { id = jobSeeker.Id }, jobSeeker);
        }







        //// DELETE: api/JobSeekers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteJobSeeker(int id)
        //{
        //    if (_context.JobSeekers == null)
        //    {
        //        return NotFound();
        //    }
        //    var jobSeeker = await _context.JobSeekers.FindAsync(id);
        //    if (jobSeeker == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.JobSeekers.Remove(jobSeeker);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobSeeker(int id)
        {
            var jobSeeker = await _unitOfWork.JobSeekers.Get(q => q.Id == id);
            if (jobSeeker == null)
            {
                return NotFound();

            }

            await _unitOfWork.JobSeekers.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }



        //private bool JobSeekerExists(int id)
        //{
        //    return (_context.JobSeekers?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        private async Task<bool> JobSeekerExists(int id)
        {
            var jobSeeker = await _unitOfWork.JobSeekers.Get(q => q.Id == id);
            return jobSeeker != null;
        }
    }
}

