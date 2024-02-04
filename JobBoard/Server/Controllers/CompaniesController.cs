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
    public class CompaniesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;

        //public CompaniesController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}


        private readonly IUnitOfWork _unitOfWork;

        public CompaniesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }



        //// GET: api/Companies
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Make>>> GetCompanies()
        //{
        //  if (_context.Companies == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Companies.ToListAsync();
        //}

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
          
            var companies = await _unitOfWork.Companies.GetAll();
            return Ok(companies);
        }


        //// GET: api/Companies/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Make>> GetMake(int id)
        //{
        //  if (_context.Companies == null)
        //  {
        //      return NotFound();
        //  }
        //    var company = await _context.Companies.FindAsync(id);

        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    return company;
        //}

        [HttpGet("{id}")]

        public async Task<IActionResult> GetMake(int id)
        {
            var company = await _unitOfWork.Companies.Get(q => q.Id == id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);

        }




        //// PUT: api/Companies/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMake(int id, Make company)
        //{
        //    if (id != company.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(company).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MakeExists(id))
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
        public async Task<IActionResult> PutMake(int id, Company company)
        {
            if (id != company.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _unitOfWork.Companies.Update(company);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MakeExists(id))
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



        //// POST: api/Companies
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Make>> PostMake(Make company)
        //{
        //  if (_context.Companies == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.Companies'  is null.");
        //  }
        //    _context.Companies.Add(company);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMake", new { id = company.Id }, company);
        //}


        [HttpPost]
        public async Task<ActionResult<Company>> PostMake(Company company)
        {
            await _unitOfWork.Companies.Insert(company);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetMake", new { id = company.Id }, company);
        }







        //// DELETE: api/Companies/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMake(int id)
        //{
        //    if (_context.Companies == null)
        //    {
        //        return NotFound();
        //    }
        //    var company = await _context.Companies.FindAsync(id);
        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Companies.Remove(company);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMake(int id)
        {
            var company = await _unitOfWork.Companies.Get(q => q.Id == id);
            if (company == null)
            {
                return NotFound();

            }

            await _unitOfWork.Companies.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }



        //private bool MakeExists(int id)
        //{
        //    return (_context.Companies?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        private async Task<bool> MakeExists(int id)
        {
            var company = await _unitOfWork.Companies.Get(q => q.Id == id);
            return company != null;
        }
    }
}

