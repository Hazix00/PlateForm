using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            var result = new { Response = ticket };
            return Ok(result);
        }
        /// <summary>
        /// Update Ticket
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] Ticket ticket)
        {
            var result = new { Response = ticket };
            return Ok(result);
        }
        /// <summary>
        /// Delete Ticket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = new { Response = $"Delete Ticket Id = {id}" };
            return Ok(result);
        }
    }
}
