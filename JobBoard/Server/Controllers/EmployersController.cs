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
    public class EmployersController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;

        //public EmployersController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}


        private readonly IUnitOfWork _unitOfWork;

        public EmployersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }



        //// GET: api/Employers
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Employer>>> GetEmployers()
        //{
        //  if (_context.Employers == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Employers.ToListAsync();
        //}

        [HttpGet]
        public async Task<IActionResult> GetEmployers()
        {

            var employers = await _unitOfWork.Employers.GetAll(includes: q => q.Include(x =>x.Company));
            return Ok(employers);
        }


        //// GET: api/Employers/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employer>> GetEmployer(int id)
        //{
        //  if (_context.Employers == null)
        //  {
        //      return NotFound();
        //  }
        //    var employer = await _context.Employers.FindAsync(id);

        //    if (employer == null)
        //    {
        //        return NotFound();
        //    }

        //    return employer;
        //}

        [HttpGet("{id}")]

        public async Task<IActionResult> GetEmployer(int id)
        {
            var employer = await _unitOfWork.Employers.Get(q => q.Id == id);

            if (employer == null)
            {
                return NotFound();
            }

            return Ok(employer);

        }




        //// PUT: api/Employers/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEmployer(int id, Employer employer)
        //{
        //    if (id != employer.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(employer).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployerExists(id))
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
        public async Task<IActionResult> PutEmployer(int id, Employer employer)
        {
            if (id != employer.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _unitOfWork.Employers.Update(employer);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EmployerExists(id))
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



        //// POST: api/Employers
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Employer>> PostEmployer(Employer employer)
        //{
        //  if (_context.Employers == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.Employers'  is null.");
        //  }
        //    _context.Employers.Add(employer);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEmployer", new { id = employer.Id }, employer);
        //}


        [HttpPost]
        public async Task<ActionResult<Employer>> PostEmployer(Employer employer)
        {
            await _unitOfWork.Employers.Insert(employer);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetEmployer", new { id = employer.Id }, employer);
        }







        //// DELETE: api/Employers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEmployer(int id)
        //{
        //    if (_context.Employers == null)
        //    {
        //        return NotFound();
        //    }
        //    var employer = await _context.Employers.FindAsync(id);
        //    if (employer == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Employers.Remove(employer);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployer(int id)
        {
            var employer = await _unitOfWork.Employers.Get(q => q.Id == id);
            if (employer == null)
            {
                return NotFound();

            }

            await _unitOfWork.Employers.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }



        //private bool EmployerExists(int id)
        //{
        //    return (_context.Employers?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        private async Task<bool> EmployerExists(int id)
        {
            var employer = await _unitOfWork.Employers.Get(q => q.Id == id);
            return employer != null;
        }
    }
}

