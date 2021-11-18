using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PlateForm.API.Filters;
using PlateForm.Core.Models;

namespace PlateForm.API.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketsController : Controller
    {
        /// <summary>
        /// Get all Tickets
        /// </summary>
        /// <returns>List of Tickets</returns>
        [HttpGet]
        public object Get()
        {
            return new { Response = new List<Ticket>() { 
                new Ticket() { TicketId = 1, ProjectId = 1, Title = "Hello" , Description = "this is a ticket"}
            } };
        }
        /// <summary>
        /// Return Ticket by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ticket</returns>
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return new { Response = $"Hello from api Id = {id}" };
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
