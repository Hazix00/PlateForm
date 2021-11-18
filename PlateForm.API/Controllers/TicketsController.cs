using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Ticket> Get()
        {
            return db.Tickets;
        }
        /// <summary>
        /// Return Ticket by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ticket</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ticket = db.Tickets.SingleOrDefault(t => t.TicketId == id);
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
        public IActionResult Post([FromBody] Ticket ticket)
        {
            db.Tickets.Add(ticket);
            db.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = ticket.TicketId}, ticket);
        }
        /// <summary>
        /// Update Ticket
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();

            db.Entry(ticket).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (System.Exception)
            {
                if (db.Tickets.Find(id) == null)
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
        public IActionResult Delete(int id)
        {
            var project = db.Tickets.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            db.Tickets.Remove(project);
            db.SaveChanges();

            return Ok(project);
        }
    }
}
