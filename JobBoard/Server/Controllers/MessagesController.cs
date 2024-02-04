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
    public class MessagesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;

        //public MessagesController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}


        private readonly IUnitOfWork _unitOfWork;

        public MessagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }



        //// GET: api/Messages
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        //{
        //  if (_context.Messages == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Messages.ToListAsync();
        //}

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {

            var messages = await _unitOfWork.Messages.GetAll(includes: q => q.Include(x => x.Employer).Include(x => x.JobSeeker));
            return Ok(messages);
        }


        //// GET: api/Messages/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Message>> GetMessage(int id)
        //{
        //  if (_context.Messages == null)
        //  {
        //      return NotFound();
        //  }
        //    var message = await _context.Messages.FindAsync(id);

        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    return message;
        //}

        [HttpGet("{id}")]

        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await _unitOfWork.Messages.Get(q => q.Id == id);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);

        }




        //// PUT: api/Messages/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMessage(int id, Message message)
        //{
        //    if (id != message.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(message).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MessageExists(id))
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
        public async Task<IActionResult> PutMessage(int id, Message message)
        {
            if (id != message.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _unitOfWork.Messages.Update(message);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MessageExists(id))
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



        //// POST: api/Messages
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Message>> PostMessage(Message message)
        //{
        //  if (_context.Messages == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.Messages'  is null.");
        //  }
        //    _context.Messages.Add(message);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMessage", new { id = message.Id }, message);
        //}


        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            await _unitOfWork.Messages.Insert(message);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
        }







        //// DELETE: api/Messages/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMessage(int id)
        //{
        //    if (_context.Messages == null)
        //    {
        //        return NotFound();
        //    }
        //    var message = await _context.Messages.FindAsync(id);
        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Messages.Remove(message);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _unitOfWork.Messages.Get(q => q.Id == id);
            if (message == null)
            {
                return NotFound();

            }

            await _unitOfWork.Messages.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }



        //private bool MessageExists(int id)
        //{
        //    return (_context.Messages?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        private async Task<bool> MessageExists(int id)
        {
            var message = await _unitOfWork.Messages.Get(q => q.Id == id);
            return message != null;
        }
    }
}

