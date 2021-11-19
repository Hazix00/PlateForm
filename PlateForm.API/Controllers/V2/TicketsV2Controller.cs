using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlateForm.Core.Models;
using PlateForm.DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlateForm.API.Filters.V2;

namespace PlatformDemo.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/tickets")]    
    public class TicketsV2Controller : ControllerBase
    {
        private readonly BugsContext db;

        public TicketsV2Controller(BugsContext db)
        {
            this.db = db;
        }


        [HttpGet]        
        public async Task<IActionResult> Get()
        {           
            return Ok(await db.Tickets.ToListAsync());
        }

        [HttpGet("{id}")]        
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        [Ticket_EnsureDescriptionPresentActionFilter]
        public async Task<IActionResult> Post([FromBody]Ticket ticket)
        {
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                    new { id = ticket.TicketId },
                    ticket
                );
        }        

        [HttpPut("{id}")]
        [Ticket_EnsureDescriptionPresentActionFilter]
        public async Task<IActionResult> Put(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();

            db.Entry(ticket).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                if (await db.Tickets.FindAsync(id) == null)
                    return NotFound();
                throw;
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]        
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            db.Tickets.Remove(ticket);
            await db.SaveChangesAsync();

            return Ok(ticket);
        }

    }
}
