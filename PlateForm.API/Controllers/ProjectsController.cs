using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlateForm.Core.Models;
using PlateForm.DataStore.EF;
using System.Collections.Generic;
using System.Linq;

namespace PlateForm.API.Controllers
{
    [ApiController]
    [Route("api/Projects")]
    public class ProjectsController : Controller
    {
        private readonly BugsContext db;

        public ProjectsController(BugsContext db)
        {
            this.db = db;
        }
        /// <summary>
        /// Get all Projects
        /// </summary>
        /// <returns>List of Projects</returns>
        [HttpGet]
        public IEnumerable<Project> Get()
        {
           return db.Projects;
        }
        /// <summary>
        /// Return Project by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Project</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var project = db.Projects.SingleOrDefault(p => p.ProjectId == id);
            if (project == null) return NotFound();
            return Ok(project);
        }
        /// <summary>
        /// get all tickets that belong to a project
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        public IActionResult GetProjectTickets(int pid)
        {
            var tickets = db.Projects.SingleOrDefault(t => t.ProjectId == pid)?.Tickets;
            if (tickets == null || tickets.Count() <= 0)
            {
                return NotFound();
            }
            return Ok(tickets);
        }
        /// <summary>
        /// Add new Project
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Project project)
        {
            db.Projects.Add(project);
            db.SaveChanges();
            return CreatedAtAction(nameof(Get), project.ProjectId);
        }
        /// <summary>
        /// Update Project
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put( int id, [FromBody] Project project)
        {
            if (id != project.ProjectId) return BadRequest();

            db.Entry(project).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (System.Exception)
            {
                if (db.Projects.Find(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }
        /// <summary>
        /// Delete Project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            db.Projects.Remove(project);
            db.SaveChanges();

            return Ok(project); 
        }
    }
}
