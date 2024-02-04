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
    public class ApplicationsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;

        //public ApplicationsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}


        private readonly IUnitOfWork _unitOfWork;

        public ApplicationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }



        //// GET: api/Applications
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        //{
        //  if (_context.Applications == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Applications.ToListAsync();
        //}

        [HttpGet]
        public async Task<IActionResult> GetApplications()
        {

            var applications = await _unitOfWork.Applications.GetAll(includes: q => q.Include(x => x.Job));
            return Ok(applications);
        }


        //// GET: api/Applications/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Application>> GetApplication(int id)
        //{
        //  if (_context.Applications == null)
        //  {
        //      return NotFound();
        //  }
        //    var application = await _context.Applications.FindAsync(id);

        //    if (application == null)
        //    {
        //        return NotFound();
        //    }

        //    return application;
        //}

        [HttpGet("{id}")]

        public async Task<IActionResult> GetApplication(int id)
        {
            var application = await _unitOfWork.Applications.Get(q => q.Id == id);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);

        }




        //// PUT: api/Applications/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutApplication(int id, Application application)
        //{
        //    if (id != application.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(application).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ApplicationExists(id))
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
        public async Task<IActionResult> PutApplication(int id, Application application)
        {
            if (id != application.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _unitOfWork.Applications.Update(application);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ApplicationExists(id))
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



        //// POST: api/Applications
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Application>> PostApplication(Application application)
        //{
        //  if (_context.Applications == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.Applications'  is null.");
        //  }
        //    _context.Applications.Add(application);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetApplication", new { id = application.Id }, application);
        //}


        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication(Application application)
        {
            await _unitOfWork.Applications.Insert(application);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetApplication", new { id = application.Id }, application);
        }







        //// DELETE: api/Applications/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteApplication(int id)
        //{
        //    if (_context.Applications == null)
        //    {
        //        return NotFound();
        //    }
        //    var application = await _context.Applications.FindAsync(id);
        //    if (application == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Applications.Remove(application);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var application = await _unitOfWork.Applications.Get(q => q.Id == id);
            if (application == null)
            {
                return NotFound();

            }

            await _unitOfWork.Applications.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }



        //private bool ApplicationExists(int id)
        //{
        //    return (_context.Applications?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        private async Task<bool> ApplicationExists(int id)
        {
            var application = await _unitOfWork.Applications.Get(q => q.Id == id);
            return application != null;
        }
    }
}

