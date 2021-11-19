using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlateForm.Core.Models;
using PlateForm.DataStore.EF;

namespace PlateForm.API.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketsController : Controller
    {
        private readonly BugsContext db;

        public TicketsController(BugsContext db)
        {
            this.db = db;
        }
        /// <summary>
        /// Get all Tickets
        /// </summary>
        /// <returns>List of Tickets</returns>
        [HttpGet]
        public async Task<IList<Ticket>> GetAsync()
        {
            var tickets = await db.Tickets.ToListAsync();
            return tickets; 
        }
        /// <summary>
        /// Return Ticket by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ticket</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var ticket = await db.Tickets.SingleOrDefaultAsync(t => t.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }
        
        /// <summary>
        /// Add new Ticket
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Ticket ticket)
        {
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAsync).Replace("Async",""), new { id = ticket.TicketId}, ticket);
        }
        /// <summary>
        /// Update Ticket
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();

            db.Entry(ticket).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var existingTicket = await db.Tickets.FindAsync(id);
                if (existingTicket == null || ex.GetType() == typeof(DbUpdateConcurrencyException))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }
        /// <summary>
        /// Delete Ticket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await db.Tickets.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            db.Tickets.Remove(project);
            await db.SaveChangesAsync();

            return Ok(project);
        }
    }
}
